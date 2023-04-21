using Microsoft.Identity.Client;

namespace IntroduccionAEFCore.Entidades
{

    // El HashSet NO regresa los valores de manera ordenada
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>(); // Este indica que vas a tener una coleccion de Comentarios
        public HashSet<Genero> Generos { get; set; } = new HashSet<Genero>(); // Pelicula tiene generos

        //Se usa listado para respetar el orden, ENTIDAD INTERMEDIA
        public List<PeliculaActor> PeliculaActores { get; set;} = new List<PeliculaActor>();
    }
}
