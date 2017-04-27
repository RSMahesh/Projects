using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace InfoWebTicketSystem.MVC.ViewModels
{

    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
       // [Email (ErrorMessage = "Invalid Email Address")]
       
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
   
    public class LoginViewModel
    {
         [Required]
        [Display(Name = "User name")]
        public string UserName{ get; set; }
         [Required]
        [Display(Name = "Password")]
        public String  Password { get; set; }
    }
}
