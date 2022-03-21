using CSM.Bynder.Fields;
using CSM.Bynder.Settings;
using CSM.Bynder.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace CSM.Bynder.Drivers;

public class BynderFieldDisplayDriver : ContentFieldDisplayDriver<BynderField>
{
    private readonly IOptions<BynderOptions> _bynderOptionsOptions;
    private readonly IStringLocalizer T;

    public BynderFieldDisplayDriver(
        IOptions<BynderOptions> bynderOptionsOptions,
        IStringLocalizer<BynderFieldDisplayDriver> stringLocalizer)
    {
        _bynderOptionsOptions = bynderOptionsOptions;
        T = stringLocalizer;
    }

    public override IDisplayResult Display(BynderField field, BuildFieldDisplayContext fieldDisplayContext) =>
        Initialize<BynderFieldDisplayViewModel>(
            GetDisplayShapeType(fieldDisplayContext),
            viewModel => viewModel.Resources.AddRange(field.Resources))
        .Location("Detail", "Content:5")
        .Location("Summary", "Content:5");

    public override IDisplayResult Edit(BynderField field, BuildFieldEditorContext context) =>
        Initialize<BynderFieldEditViewModel>(
            GetEditorShapeType(context),
            viewModel =>
            {
                viewModel.PortalUrl = _bynderOptionsOptions.Value.PortalUrl;
                viewModel.Resources.AddRange(field.Resources);
                viewModel.ResourcesJson = JsonConvert.SerializeObject(field.Resources.ToArray());
                viewModel.PartFieldDefinition = context.PartFieldDefinition;
            });

    public override async Task<IDisplayResult> UpdateAsync(BynderField field, IUpdateModel updater, UpdateFieldEditorContext context)
    {
        var viewModel = new BynderFieldEditViewModel();

        await updater.TryUpdateModelAsync(viewModel, Prefix);

        field.Resources.Clear();

        if (!string.IsNullOrWhiteSpace(viewModel.ResourcesJson))
        {
            field.Resources.AddRange(JsonConvert.DeserializeObject<BynderResource[]>(viewModel.ResourcesJson));

            foreach (var resource in field.Resources.Where(resource => string.IsNullOrEmpty(resource.Description)))
            {
                resource.Description = resource.Name;
            }
        }

        var settings = context.PartFieldDefinition.GetSettings<BynderFieldSettings>();

        if (settings.Required && field.Resources.Count < 1)
        {
            updater.ModelState.AddModelError(
                Prefix,
                nameof(viewModel.ResourcesJson),
                T["{0}: A Bynder resource is required.", context.PartFieldDefinition.DisplayName()]);
        }

        if (field.Resources.Count > 1 && !settings.Multiple)
        {
            updater.ModelState.AddModelError(
                Prefix,
                nameof(viewModel.ResourcesJson),
                T["{0}: Selecting multiple Bynder resources is forbidden.", context.PartFieldDefinition.DisplayName()]);
        }

        return await EditAsync(field, context);
    }
}
