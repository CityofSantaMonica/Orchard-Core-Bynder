using CSM.Bynder.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSM.Bynder.ViewModels;

public class BynderFieldEditViewModel
{
    public string PortalUrl { get; set; }
    public IList<BynderResource> Resources { get; } = new List<BynderResource>();
    public string ResourcesJson { get; set; }
    public ContentPartFieldDefinition PartFieldDefinition { get; set; }

    public Task AddResourcesAsync(IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            Resources.Add(resource);
        }

        return Task.CompletedTask;
    }
}
