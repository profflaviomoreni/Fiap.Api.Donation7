using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation7.ViewModel
{
    public class LoginRequestVM
    {

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email deve ser válido")]
        public string EmailUsuario { get; set; }


        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }

    }
}
