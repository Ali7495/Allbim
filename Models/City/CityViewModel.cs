using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Models.City
{
    public class CityResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TownShipId { get; set; }
        public long ProvinceId { get; set; }
    }  
    public class CityInputViewModel
    {

        public string Name { get; set; }
        public long TownShipId { get; set; }
        public long ProvinceId { get; set; }
    }
}
