
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
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.VillaList;

        }


        // GET: api/VillaAPI - get Villa by Id
        [HttpGet("id")]
        public VillaDto GetVilla(int id)
        {
            return VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        }

    }
}
