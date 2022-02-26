using CSM.Bynder.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;

namespace CSM.Bynder.ViewModels
{
    public class BynderFieldEditViewModel
    {
        public BynderFieldEditViewModel() =>
            Resources = new List<BynderResource>();

        public string PortalUrl { get; set; }
        public ICollection<BynderResource> Resources { get; }
        public string ResourcesJson { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }
}
