using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetLoveWeb.Models
{
    [Serializable]
    public class UsuarioModel
    {
        [Required]
        public int Id_Usuario { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }

    }
}