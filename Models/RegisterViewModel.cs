using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UpDown.Models
{
    public class RegisterViewModel
    {

        //[Required(ErrorMessage = "*")]
        //[Display(Name = "First Name")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "מייל לא תקין")]
        public string Email { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "R Password")]
        [Compare("Password", ErrorMessage = "*")]
        public string ConfirmPassword { get; set; }


        //[Required(ErrorMessage = "*")]
        //[Display(Name = "Captcha")]
        //public string Captcha { get; set; }
    }
}