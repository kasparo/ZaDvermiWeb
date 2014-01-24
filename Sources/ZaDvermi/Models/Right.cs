using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class Right
    {
        public int Id { get; set; }

        [Required]
        public int FeatureId { get; set; }

        [Required]
        public int UserGroupId { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual Feature Feature { get; set; }

    }
}