using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class UserGroup
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<MemberShip> MemberShips { get; set; }
       
    }
}