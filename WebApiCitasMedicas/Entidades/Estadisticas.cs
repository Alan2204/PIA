﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.Entidades
{
    public class Estadisticas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public int TotalConsultas { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        [Range(0, 100, ErrorMessage = "No puedes tener mas de 100 pacientes.")]
        public int TotalPacientes { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public DateTime FechaActualizacion { get; set; }
        public int MedicosId { get; set; }
        public Medicos medicos { get; set; }
    }
}
