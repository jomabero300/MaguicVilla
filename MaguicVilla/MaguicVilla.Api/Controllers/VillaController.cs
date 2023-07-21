using AutoMapper;
using MaguicVilla.Api.Data;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;
using MaguicVilla.Api.Repository.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MaguicVilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        private readonly IVillaRepository _villa;

        private readonly IMapper _mapper;

        protected APIResponse _responsed;

        public VillaController(ILogger<VillaController> logger, IMapper mapper, IVillaRepository villa)
        {
            _logger = logger;
            _mapper = mapper;
            _villa = villa;
            _responsed = new();
        }

        [HttpGet]
        public async Task< ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Obtener las villas..");

                IEnumerable<Villa> villaList = await _villa.ObtenerTodos();

                _responsed.Resultado = _mapper.Map<IEnumerable<VillaDto>>(villaList);
                _responsed.StatusCode=HttpStatusCode.OK;
                return Ok(_responsed);

            }
            catch (Exception ex)
            {
                _responsed.Existoso= false;
                _responsed.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responsed;  
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult< APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation($"Error al traer villa con la Id {id}");

                    _responsed.StatusCode=HttpStatusCode.BadRequest;
                    _responsed.Existoso= false;
                    return BadRequest(_responsed);
                }

                var list = await _villa.Obtener(x => x.Id == id);

                if (list == null)
                {
                    _responsed.StatusCode =HttpStatusCode.NotFound;
                    _responsed.Existoso= false;
                    return NotFound(_responsed);
                }
                _responsed.Resultado = _mapper.Map<VillaDto>(list);
                _responsed.StatusCode=HttpStatusCode.OK;
                return Ok(_responsed);
            }
            catch (Exception ex)
            {

                _responsed.Existoso = false;
                _responsed.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responsed;
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] VillaCreateDto villaCreateDto)  
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _villa.Obtener(v => v.Nombre.ToUpper() == villaCreateDto.Nombre.ToUpper()) != null)
                {
                    ModelState.AddModelError("VillaExiste", "La villa con este nombre ya existe..");

                    return BadRequest(ModelState);
                }

                if (villaCreateDto == null)
                {
                    return BadRequest(villaCreateDto);
                }

                Villa model = _mapper.Map<Villa>(villaCreateDto);
                model.FechaActualizacion=DateTime.Now;
                model.FechaActualizacion = DateTime.Now;

                await _villa.Create(model);

                _responsed.Resultado = model;
                _responsed.StatusCode=HttpStatusCode.Created;

                return CreatedAtAction("GetVilla", new { id = model.Id }, _responsed);

            }
            catch (Exception ex)
            {
                _responsed.Existoso = false;
                _responsed.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responsed;
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _responsed.Existoso = false;
                    _responsed.StatusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_responsed);
                }

                Villa villa = await _villa.Obtener(x => x.Id == id);

                if (villa == null)
                {
                    _responsed.Existoso = false;
                    _responsed.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_responsed);
                }

                await _villa.Remover(villa);

                return NoContent();
            }
            catch (Exception ex)
            {
                _responsed.Existoso = false;
                _responsed.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responsed;
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                _responsed.Existoso = false;
                _responsed.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responsed);
            }

            Villa model = _mapper.Map<Villa>(villaUpdateDto);

            await _villa.Actualizar(model);

            _responsed.StatusCode = HttpStatusCode.NoContent;

            return Ok(_responsed);

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

            _responsed.StatusCode=HttpStatusCode.NoContent;

            return Ok(_responsed);
        }
    }
}