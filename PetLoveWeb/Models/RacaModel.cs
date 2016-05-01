using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetLoveWeb.Models
{
    [Serializable]
    public class RacaModel
    {
        [Required]
        public int Id_Raca { get; set; }

        public string NomeRaca { get; set; }

        public string Tipo { get; set; }
    }
}