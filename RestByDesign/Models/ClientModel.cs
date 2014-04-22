using RestByDesign.Infrastructure.Attributes;
using RestByDesign.Models.Base;

namespace RestByDesign.Models
{
    [NotPatchable]
    public class ClientModel : BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}