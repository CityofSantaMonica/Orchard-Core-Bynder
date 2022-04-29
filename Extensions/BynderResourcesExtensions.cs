using CSM.Bynder.Fields;
using CSM.Bynder.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSM.Bynder;

public static class BynderResourcesExtensions
{
    public static Task AddResourceToDisplayViewModelAsync(
        this BynderFieldDisplayViewModel viewModel,
        IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }

        return Task.CompletedTask;
    }

    public static Task AddResourceToEditViewModelAsync(
        this BynderFieldEditViewModel viewModel,
        IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }

        return Task.CompletedTask;
    }

    public static Task AddResourceToBynderFieldAsync(
        this BynderField viewModel,
        BynderResource[] resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }

        return Task.CompletedTask;
    }
}
