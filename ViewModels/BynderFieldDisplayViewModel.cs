using CSM.Bynder.Fields;
using System.Collections.Generic;

namespace CSM.Bynder.ViewModels
{
    public class BynderFieldDisplayViewModel
    {
        public BynderFieldDisplayViewModel() =>
           Resources = new List<BynderResource>();

        public ICollection<BynderResource> Resources { get; }
    }
}
