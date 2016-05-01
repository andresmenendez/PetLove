using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace PetLoveWeb.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Telefone { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; }

        public string Telefone { get; set; }
    }
}
