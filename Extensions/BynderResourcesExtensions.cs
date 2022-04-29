using CSM.Bynder.Fields;
using CSM.Bynder.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSM.Bynder;

public static class BynderResourcesExtensions
{
    public static void AddResourceToDisplayViewModel(
        this BynderFieldDisplayViewModel viewModel,
        IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }
    }

    public static void AddResourceToEditViewModel(
        this BynderFieldEditViewModel viewModel,
        IList<BynderResource> resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }
    }

    public static void AddResourceToBynderField(
        this BynderField viewModel,
        BynderResource[] resources)
    {
        foreach (var resource in resources)
        {
            viewModel.Resources.Add(resource);
        }
    }
}
