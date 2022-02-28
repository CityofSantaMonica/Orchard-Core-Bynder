using CSM.Bynder.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;

namespace CSM.Bynder.ViewModels
{
    public class BynderFieldEditViewModel
    {
        public string PortalUrl { get; set; }
        public ICollection<BynderResource> Resources { get; } = new List<BynderResource>();
        public string ResourcesJson { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }
}
