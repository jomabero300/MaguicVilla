using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MaguicVilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult< VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var list = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

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


            if(VillaStore.VillaList.FirstOrDefault(v=>v.Nombre.ToUpper()==villaDto.Nombre.ToUpper())!=null)
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

            villaDto.Id=VillaStore.VillaList.OrderByDescending(v=>v.Id).FirstOrDefault().Id+1;

            VillaStore.VillaList.Add(villaDto);

            //return Ok(villaDto);

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

            var list = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }


            VillaStore.VillaList.Remove(list);

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

            var list = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            list.Nombre = villaDto.Nombre;
            list.Ocupantes = villaDto.Ocupantes;
            list.MetrosCuadrados = villaDto.MetrosCuadrados;

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

            var list = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            pathDto.ApplyTo(list, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();


        }
    }
}
