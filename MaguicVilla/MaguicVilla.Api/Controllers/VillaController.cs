using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaguicVilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<VillaController> _logger;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas..");

            return Ok(_context.Villas.ToList());
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult< VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation($"Error al traer villa con la Id {id}");
                return BadRequest();
            }

            var list = _context.Villas.FirstOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto) 
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_context.Villas.FirstOrDefault(v=>v.Nombre.ToUpper()==villaDto.Nombre.ToUpper())!=null)
            {
                ModelState.AddModelError("VillaExiste", "La villa con este nombre ya existe..");

                return BadRequest(ModelState);
            }

            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model=new ()
            {
                Id = villaDto.Id,
                Nombre= villaDto.Nombre,
                Detalle =villaDto.Detalle,
                ImagenUrl= villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa=villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenida= villaDto.Amenida,
            };

            _context.Villas.Add(model);

            _context.SaveChanges();

            //villaDto.Id= _context.Villas.ToList().OrderByDescending(v=>v.Id).FirstOrDefault().Id+1;

            //VillaStore.VillaList.Add(villaDto);

            ////return Ok(villaDto);

            return CreatedAtAction("GetVilla", new { id = villaDto.Id }, villaDto);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Villa list = _context.Villas.FirstOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(list);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto==null || id != villaDto.Id) 
            { 
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenida = villaDto.Amenida,

            };

            _context.Villas.Update(model);

            _context.SaveChanges();


            //var list = _context.Villas.FirstOrDefault(x => x.Id == id);

            //if (list == null)
            //{
            //    return NotFound();
            //}
            //list.Nombre = villaDto.Nombre;
            //list.Ocupantes = villaDto.Ocupantes;
            //list.MetrosCuadrados = villaDto.MetrosCuadrados;

            return NoContent();


        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePartialjsonVilla(int id, JsonPatchDocument<VillaDto> pathDto)
        {
            if (pathDto==null || id == 0) 
            { 
                return BadRequest();
            }

            var villa = _context.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenida = villa.Amenida,

            };


            pathDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenida = villaDto.Amenida,

            };

            _context.Villas.Update(model);

            _context.SaveChanges();

            return NoContent();


        }
    }
}
