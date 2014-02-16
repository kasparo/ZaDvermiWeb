using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jméno je povinná položka")]
        [StringLength(50)]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Příjmení je povinná položka")]
        [StringLength(50)]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Oslovení je povinná položka")]
        [StringLength(50)]
        [Display(Name = "Oslovení")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Email je povinná položka")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Zadejte platnou emailovou adresu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon je povinná položka (pouze pro účely sdružení)")]
        [Display(Name = "Telefon")]
        [StringLength(10, MinimumLength = 6)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [Display(Name = "Datum narození")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Display(Name="Oblíbená barva")]
        public string Color { get; set; }

        public DateTime LastActivity { get; set; }

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public virtual ICollection<MemberShip> MemberShips { get; set; }
        
      
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "Heslo je povinné")]
        [Display(Name = "Heslo:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress(ErrorMessage = "Zadejte platnou emailovou adresu")]
        [Display(Name = "Email:")]
        public string UserName { get; set; }

        public bool RememberMe { get; set; }
    }
}