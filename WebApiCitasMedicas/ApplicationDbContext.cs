using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiCitasMedicas.Entidades;

namespace WebApiCitasMedicas
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

              modelBuilder.Entity<AlumnoClase>()
                .HasKey(al => new { al.AlumnoId, al.ClaseId });
        }*/

        public DbSet<Citas> Citas { get; set; }
        public DbSet<Estadisticas> Estadisticas { get; set; }
        public DbSet<InformacionMedica> InformacionMedica { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set;}
        public DbSet<ReporteConsulta> ReporteConsulta { get; set; }
        public DbSet<CitasAgendadas> CitasAgendadas { get; set; }
    }
}
