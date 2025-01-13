using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesBackend.Models;

namespace RegistroEstudiantesBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Estudiantes> estudiantes { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<Profesores> Profesores { get; set; }
        public DbSet<Estudiantesmaterias> Estudiantesmaterias { get; set; }
        public DbSet<MateriasEstudiantes> MateriasEstudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Estudiantesmaterias>()
                .HasKey(em => em.Id); 

            modelBuilder.Entity<Estudiantesmaterias>()
                .HasOne(em => em.Estudiante)
                .WithMany() 
                .HasForeignKey(em => em.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Estudiantesmaterias>()
                .HasOne(em => em.Materia)
                .WithMany() 
                .HasForeignKey(em => em.MateriaId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Materias>()
               .HasOne(m => m.Profesor) 
               .WithMany(p => p.Materias) 
               .HasForeignKey(m => m.ProfesorId);

            
            modelBuilder.Entity<Estudiantesmaterias>()
                .HasKey(em => em.Id); 

            modelBuilder.Entity<Estudiantesmaterias>()
                .HasOne(em => em.Estudiante)
                .WithMany(e => e.EstudiantesMaterias)
                .HasForeignKey(em => em.EstudianteId);

            modelBuilder.Entity<Estudiantesmaterias>()
                .HasOne(em => em.Materia)
                .WithMany(m => m.Estudiantesmaterias)
                .HasForeignKey(em => em.MateriaId);
            modelBuilder.Entity<Estudiantes>()
              .HasMany(e => e.EstudiantesMaterias)
              .WithOne(em => em.Estudiante)
              .HasForeignKey(em => em.EstudianteId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

