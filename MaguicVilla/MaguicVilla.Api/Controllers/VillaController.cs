using AutoMapper;
using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;
using MaguicVilla.Api.Repository.IRepositories;
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
        private readonly ILogger<VillaController> _logger;

        private readonly IVillaRepository _villa;

        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IMapper mapper, IVillaRepository villa)
        {
            _logger = logger;
            _mapper = mapper;
            _villa = villa;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas..");

            IEnumerable<Villa> villaList = await _villa.ObtenerTodos();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult< VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation($"Error al traer villa con la Id {id}");
                return BadRequest();
            }

            var list = await _villa.Obtener(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(list));
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto villaCreateDto)  
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _villa.Obtener(v=>v.Nombre.ToUpper()==villaCreateDto.Nombre.ToUpper())!=null)
            {
                ModelState.AddModelError("VillaExiste", "La villa con este nombre ya existe..");

                return BadRequest(ModelState);
            }

            if(villaCreateDto == null)
            {
                return BadRequest(villaCreateDto);
            }

            Villa model = _mapper.Map<Villa>(villaCreateDto);  


            await _villa.Create(model);

            return CreatedAtAction("GetVilla", new { id = model.Id }, model);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Villa villa = await _villa.Obtener(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            await _villa.Remover(villa);

            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if (villaUpdateDto==null || id != villaUpdateDto.Id) 
            { 
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(villaUpdateDto);

            await _villa.Actualizar(model);

            return NoContent();
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePartialjsonVilla(int id, JsonPatchDocument<VillaUpdateDto> pathDto)
        {
            if (pathDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villa.Obtener(x => x.Id == id,tracked:false);

            if (villa == null)
            {
                return NotFound();
            }


            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            pathDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa model = _mapper.Map<Villa>(villaDto); 

            await _villa.Actualizar(model);

            return NoContent();
        }
    }
}