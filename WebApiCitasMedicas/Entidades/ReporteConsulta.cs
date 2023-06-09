﻿using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.Entidades
{
    public class ReporteConsulta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Motivo { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Tratamiento { get; set; }
        [Required(ErrorMessage = "Es necesario el campo {0}. ")]
        public string Receta { get; set; }
        public int CitasAgendadasId { get; set; }
        public CitasAgendadas citasAgendadas { get; set; }
        
    }
}
