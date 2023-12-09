using System.ComponentModel.DataAnnotations;

namespace HouseApp_APIs.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public DateTime CReatedDate { get; set; }
        public int Sqft { get; internal set; }
        public int Occupancy { get; internal set; }
    }
}
