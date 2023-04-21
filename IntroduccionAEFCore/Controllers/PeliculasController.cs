using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PeliculasController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Include(p => p.Comentarios)
                .Include(p => p.Generos)
                .Include(p => p.PeliculaActores.OrderBy(pa => pa.Orden))
                        .ThenInclude(pa => pa.Actor) //Sirve para incluir ciertos campos qu eno se tiene como tal sino se tiene la instancia del objeto
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult> GetSelect(int id)
        {
            var pelicula = await context.Peliculas
                .Select(pel => new
                {
                    pel.Id,
                    pel.Titulo,
                    Generos = pel.Generos.Select(g => g.Nombre).ToList(),
                    Actores = pel.PeliculaActores.OrderBy(pa => pa.Orden).Select(pa =>
                    new {
                        Id = pa.ActorId,
                        pa.Actor.Nombre,
                        pa.Personaje
                    }),
                    CantidadComentarios = pel.Comentarios.Count()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO) 
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            //ESTO SE HACE EN UNA RELACION MUCHOIS A MUCHOS SALTNAOD L ATABLA INTERMEDIA
            if(pelicula.Generos is not null) 
            {
                foreach (var genero in pelicula.Generos)
                {
                    // Es para darle sweguimiento a los dintintos objetos con registros a la base de datos, 
                    // Uncganges = Sin cambiar
                    // Se le decia "Compa no hagas un numero genero checate los existentes"
                    context.Entry(genero).State = EntityState.Unchanged;
                }
            }

            if(pelicula.PeliculaActores is not null) 
            {
                for(int i = 0; i< pelicula.PeliculaActores.Count; i++) 
                {
                    pelicula.PeliculaActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await context.Peliculas
                .Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
