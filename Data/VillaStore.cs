using HouseApp_APIs.Models.Dto;

namespace HouseApp_APIs.Data
{
    public class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto{Id = 1, Name = "Pool View",Sqft=100, Occupancy=4, CReatedDate = DateTime.Now},
            new VillaDto{Id = 2, Name = "Beach View", Sqft=300, Occupancy=3, CReatedDate = DateTime.Now},

        };

    }
}
