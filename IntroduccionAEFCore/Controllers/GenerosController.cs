using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] //Significa que este metodo va obtener datos
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await context.Generos.ToListAsync();
        }

        /*
         * Este es el metodo que se va a ejecutar cuando se hace una peticion a la URL que dice ROUTE
         * ASYN == Es buena practica cuando utilizamos una operacion Y/O
         * Que es una operacion Y/O ? R= Es aquella cuando se da que el sismtema se comunica con otro sistema en este caso con la BD
         * DTO : Tranferencia de Datos
        */
        [HttpPost]
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion)
        {

            //AnyAsyn sirve para una caracteristicas determinada
            var yaExisteGeneroConEsteNombre = await context.Generos.AnyAsync(g =>
            g.Nombre == generoCreacion.Nombre);

            if (yaExisteGeneroConEsteNombre)
            {
                return BadRequest("Ya existe un género con el nombre " + generoCreacion.Nombre);
            }
            var genero = mapper.Map<Genero>(generoCreacion);

            /* Esto es un MAPEO MANUAL
                var genero = new Genero
                {
                    Nombre = generoCreacion.Nombre
                };

            Codigo de EFC
            */

            context.Add(genero);

            //Esta linea es para guardar los cambios que se genero en la linea anteior y lo empuja a la base de datos
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generoCreacionDTO) 
        {
            var genero = mapper.Map<Genero[]>(generoCreacionDTO);
            context.AddRange(genero);
            await context.SaveChangesAsync();
            return Ok();
        }


        // El PUT sirve para actualizar registros
        [HttpPut("{id:int}/nombre2")]
        public async Task<ActionResult> Put(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null)
            {
                return NotFound();
            }

            genero.Nombre = genero.Nombre + "2"; //Solo le va agregar un dos al nombre

            await context.SaveChangesAsync();
            return Ok();

        }


        //Actualizar registros como si fueras nuevos
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;
            context.Update(genero); // Marcas el objeto como modificado
            await context.SaveChangesAsync();
            return Ok();
        }

        //Para eliminar registros
        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync(); //Regresa el valor de total de filas alteradas

            if (filasAlteradas == 0)
            {
                return NotFound(); //No encontro el ID
            }

            return NoContent(); //Todo chido no retornes nada
        }

        [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult> DeleteAnterior(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null)
            {
                return NotFound();
            }

            context.Remove(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
