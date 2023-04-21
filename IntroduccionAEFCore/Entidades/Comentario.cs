namespace IntroduccionAEFCore.Entidades
{
    // El signo de ? en contenido significa que puede o no ser NULL
    public class Comentario
    {
        public int Id { get; set; }
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; } = null!; // Esta la propiedad de navegacion que va desde Comentario a Pelicula

    }
}
