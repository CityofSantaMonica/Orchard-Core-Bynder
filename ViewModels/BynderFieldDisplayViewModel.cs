using CSM.Bynder.Fields;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSM.Bynder.ViewModels;

public class BynderFieldDisplayViewModel
{
    public IList<BynderResource> Resources { get; } = new List<BynderResource>();

    public Task AddResourcesAsync(IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            Resources.Add(resource);
        }

        return Task.CompletedTask;
    }
}
