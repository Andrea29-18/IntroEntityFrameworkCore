using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroduccionAEFCore.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {

            builder.Property(p => p.FechaEstreno).HasColumnType("date");
        }
    }
}
