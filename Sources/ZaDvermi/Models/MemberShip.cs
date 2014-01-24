using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class MemberShip
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Uživatel")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Skupina")]
        public int UserGroupId { get; set; }

        public virtual User User { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}