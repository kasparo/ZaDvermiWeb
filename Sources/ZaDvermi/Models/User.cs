using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Oslovení")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Telefon")]
        [StringLength(10, MinimumLength = 6)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime LastActivity { get; set; }

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<MemberShip> MemberShips { get; set; }
        public ICollection<MediaItem> MediaItems { get; set; } 
    }
}