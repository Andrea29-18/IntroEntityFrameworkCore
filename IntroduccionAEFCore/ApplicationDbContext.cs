using IntroduccionAEFCore.Entidades;
using IntroduccionAEFCore.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IntroduccionAEFCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        /* Esto es un API INFLUENTER
         * 2° Es para indicar que una entidad tiene una llave primaria, la letra "g" es de Genero
         * 3° Para poner el maximo de caracteres (MAX) de un atributo
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //NUNCA BORRARLO

            /*
             * Antes todo lo que esta dentro de la carpeta "Configuracion" de "Entidades" estaba en esta sección
             * Pero al ser mucho iba a haber problema en mantenimiento, entonces por eso se creo clases especifica para cada entidad
             * 
             * Ahora pata llamar esas configuraciones se necesita lo siguiente
             * Cuando nos referimos a ASSEMBLY se refiere a un DLL o un proyecto
             * Lo que hace es que busca la Interfaz de  IEntityTypeConfiguration<Entidad>
             */

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingInicial.Seed(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //TODOS LOS TIUPONS STRING == NVARCHAR SE VAN A CONFIGURAR DE 150, SIN IMPORTAR LA ENTIDAD
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }


        /*Las migraciones son clases que se van a crear cuando realicemos un comando
         * El conjutno de migraciones servira como historio de los cambios que ha sufrido la base de datos
         * DBSET == Sirve para pasar de una clase a una tabla de BD
        */
        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<PeliculaActor> PeliculaActores => Set<PeliculaActor>();

        /*
         * Una propiedad de navegacion nos permite obtener las DATAS relacionas de una entidad de una manera sencilla
         * Entidad intermedia : representa la tabvla intermedia de los regiustos de la relacion de muchos a muchos
         * Confiracion con saltos: es quel que no necesitas la tabla intermedia
         */

    }
}
