using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gavelister.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vennligst fyll inn brukernavn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vennligst fyll inn passord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
