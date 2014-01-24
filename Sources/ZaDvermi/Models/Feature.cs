using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        public string FeatureKey { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public ICollection<Right> Rights { get; set; }
    }
}