using HouseApp_APIs.Models.Dto;

namespace HouseApp_APIs.Data
{
    public class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto{Id = 1, Name = "Pool View", CReatedDate = DateTime.Now},
            new VillaDto{Id = 2, Name = "Beach View", CReatedDate = DateTime.Now},

        };

    }
}
