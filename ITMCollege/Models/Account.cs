using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMCollege.Models
{
    public class Account
    {

        public int AccountId { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        //[PageRemote(ErrorMessage = "Username already exists", AdditionalFields = "__RequestVerificationToken",
        //HttpMethod = "post", PageHandler = "CheckEmail")]
        //[BindProperty]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z\d#$!@%&*?]{1,30}$")]
        public string Password { get; set; }
        [Required]
        public byte Role { get; set; }
        [Required]
        public bool IsActive { get; set; }


        //query the database and get all existing Emails or directly check whether the email is exist in the database or not.
      
    }
}
