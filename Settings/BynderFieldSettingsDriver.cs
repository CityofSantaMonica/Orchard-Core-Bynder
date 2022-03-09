using CSM.Bynder.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace CSM.Bynder.Settings
{
    public class BynderFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<BynderField>
    {
        public override IDisplayResult Edit(ContentPartFieldDefinition model)
        {
            return Initialize<BynderFieldSettings>(
                $"{nameof(BynderFieldSettings)}_Edit",
                settings => model.PopulateSettings(settings))
                .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var settings = new BynderFieldSettings();

            await context.Updater.TryUpdateModelAsync(settings, Prefix);

            context.Builder.WithSettings(settings);

            return Edit(model);
        }
    }
}
