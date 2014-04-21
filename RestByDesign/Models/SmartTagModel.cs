
using System.ComponentModel.DataAnnotations;

namespace RestByDesign.Models
{
    public class SmartTagModel
    {
        public string Id { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }
    }
}