using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmitUp.Api.ViewModels.Account
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
