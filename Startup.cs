using CSM.Bynder.Drivers;
using CSM.Bynder.Fields;
using CSM.Bynder.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace CSM.Bynder;

public class Startup : StartupBase
{
    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration)
    {
        _shellConfiguration = shellConfiguration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<BynderOptions>(_shellConfiguration.GetSection("CSM_Bynder"));

        services.AddContentField<BynderField>()
            .UseDisplayDriver<BynderFieldDisplayDriver>();
        services.AddScoped<IContentPartFieldDefinitionDisplayDriver, BynderFieldSettingsDriver>();
    }
}
