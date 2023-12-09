
using HouseApp_APIs.Data;
using HouseApp_APIs.Models;
using HouseApp_APIs.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HouseApp_APIs.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        // GET: api/VillaAPI - get all villas
        [HttpGet]
        public ActionResult <IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);

        }


        // GET: api/VillaAPI - get Villa by Id
        [HttpGet("id")]
        public ActionResult <VillaDto> GetVilla(int id)
        {
            if(id == 0) { 
                return BadRequest(); 
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound(); 
            }
            return Ok(villa);

        }
        [HttpPost]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0)
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto);
            return Ok(villaDto);
        }

    }
}
