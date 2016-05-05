using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetLoveWeb.Models
{
    [Serializable]
    public class AnimalModel
    {
        [Required]
        public int Id_Animal { get; set; }

        [Required]
        [Display(Name = "Nome do Animal")]
        public string NomeAnimal { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        public DateTime Nascimento { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int Id_Usuario { get; set; }

        public string Tipo { get; set; }

        [Required]
        public int Id_Raca { get; set; }

        public int Nome_Raca { get; set; }

        public int Racas { get; set; }
    }
}