using CSM.Bynder.Fields;
using System.Collections.Generic;

namespace CSM.Bynder.ViewModels;

public class BynderFieldDisplayViewModel
{
    public IList<BynderResource> Resources { get; } = new List<BynderResource>();
}
