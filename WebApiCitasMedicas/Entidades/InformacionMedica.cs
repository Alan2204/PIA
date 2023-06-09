﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApiCitasMedicas.Entidades
{
    public class InformacionMedica
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario registrar el peso.")]
        [Range(20, 200, ErrorMessage = "Ingrese un peso valido")]
        public float Peso { get; set; }
        [Range(0.50, 2.50, ErrorMessage = "Ingrese una altura valida")]
        public float Altura { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public DateTime FechaAtualizacion { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public Paciente paciente { get; set; }
        public Medicos Medicos { get; set; }    
    }
}
