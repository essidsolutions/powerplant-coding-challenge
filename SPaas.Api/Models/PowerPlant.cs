using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SPaas.Api.Models
{
    public class PowerPlant
    {
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PowerPlantType  Type { get; set; }
        public decimal Efficiency { get; set; }
        public decimal PMin { get; set; }
        public decimal PMax { get; set; }
    }
}