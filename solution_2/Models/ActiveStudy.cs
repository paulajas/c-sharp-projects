using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2.Models
{
    public class ActiveStudy
    {
        [JsonProperty("name")]
        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [JsonProperty("numberOfStudents")]
        [XmlAttribute(AttributeName = "numberOfStudents")]
        public int NumberOfStudents = 0;

        public ActiveStudy()
        {
        }
     
        public ActiveStudy(string name)
        {
            this.Name = name;
        }
        public ActiveStudy(string Name, int numberOfStudents)
        {
            this.Name = Name;
            this.NumberOfStudents = NumberOfStudents;
        }
    }
}
