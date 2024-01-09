
using HouseApp_APIs.Data;
using HouseApp_APIs.Logger;
using HouseApp_APIs.Models;
using HouseApp_APIs.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HouseApp_APIs.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        private readonly ILogging _logger;


        public  VillaAPIController(ILogging logger)
        {
            _logger= logger;

        }





        // GET: api/VillaAPI - get all villas
        [HttpGet]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult <IEnumerable<VillaDto>> GetVillas()
        {
            _logger.Log("Getting all villas", "error");
            return Ok(VillaStore.VillaList);

        }


        // GET: api/VillaAPI - get Villa by Id
        [HttpGet("{id:int}", Name = "GetVilla")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult <VillaDto> GetVilla(int id)
        {
            if(id == 0) {
                _logger.Log("Get Villa Error with ID"+ id, "error");
                return BadRequest(); 
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound(); 
            }
            return Ok(villa);

        }


        // POST: api/VillaAPI - create a villa
        [HttpPost]
                [ProducesResponseType(StatusCodes.Status201Created)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            //if(!ModelState.IsValid == false)
            //{
            //    return BadRequest(ModelState);
            //}

            if(VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", " already exists");
                return BadRequest(ModelState);
            }
            { }
            {
                ModelState.AddModelError("", $"Villa with name {villaDto.Name} already exists");
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto);
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }


        // DELETE: api/VillaAPI - delete a villa
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
                [ProducesResponseType(StatusCodes.Status204NoContent)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }


        // PUT: api/VillaAPI - update a villa
        [HttpPut("{id:int}", Name = "UpdateVilla")]
                [ProducesResponseType(StatusCodes.Status204NoContent)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
                [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDto> UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;
            return NoContent(); 
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDto> UpdateVillaPatch(int id, [FromBody] JsonPatchDocument<VillaDto> patchDto)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (patchDto == null)
            {
                return BadRequest(patchDto);
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            var villaToPatch = new VillaDto
            {
                Name = villa.Name,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy
            };
            patchDto.ApplyTo(villaToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(villaToPatch))
            {
                return BadRequest(ModelState);
            }
            villa.Name = villaToPatch.Name;
            villa.Sqft = villaToPatch.Sqft;
            villa.Occupancy = villaToPatch.Occupancy;
            return NoContent(); 
        }

    }
}
