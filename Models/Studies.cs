using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2.Models
{
    internal class Studies
    {

        [JsonProperty("name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [JsonProperty("mode")]
        [XmlElement(ElementName = "mode")]
        public string Mode { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
