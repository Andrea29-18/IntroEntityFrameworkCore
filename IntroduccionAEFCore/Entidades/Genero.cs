using System.ComponentModel.DataAnnotations;

namespace IntroduccionAEFCore.Entidades
{
    public class Genero
    {
        /* ANOTACIONES DE DATOS
         * Para poner algo como llave primaria se usa la notacion [KEY]. sino cualquier campo que tenga el prefijo "ID" se vuelve PK
         * Para poner el maximo de caracteres (MAX), en la parte de arriba se pone [StringLength(maximumLength: NumeroMaximo)]
         *  Esta ultima anotacion igual sirve para validar desde front si cumple o no el NUMERO maxino de caracteres
        */

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>(); // peliculas tiene genero

    }
}
