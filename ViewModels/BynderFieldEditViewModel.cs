using CSM.Bynder.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using System.Collections.Generic;

namespace CSM.Bynder.ViewModels
{
    public class BynderFieldEditViewModel
    {
        public string PortalUrl { get; set; }
        public List<BynderResource> Resources { get; } = new();
        public string ResourcesJson { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }
}
