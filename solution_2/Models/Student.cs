using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2.Models
{
    [XmlRoot(ElementName = "student")]
    internal class Student
    {
        [XmlAttribute(AttributeName = "indexNumber")]
        [JsonProperty("indexNumber")]
        public string Indexno { get; set; }

        [JsonProperty("studies")]
        [XmlElement(ElementName = "studies")]
        public Studies studies { get; set; }

        [JsonProperty("fname")]
        [XmlElement(elementName: "fname")]
        public string FirstName { get; set; }

        [JsonProperty("lname")]
        [XmlElement(elementName: "lname")]
        private string _lastName { get; set; }

        public string LastName
        {
            get => this._lastName;
            set => this._lastName = value ?? throw new ArgumentNullException();
        }

        [JsonProperty("birthdate")]
        [XmlElement(ElementName = "birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("email")]
        [XmlElement(ElementName = "email")]
        private string _email { get; set; }
        public string Email {
            get => this._email;
            set => this._email = value ?? throw new ArgumentNullException();
        }

        [JsonProperty("mothersName")]
        [XmlElement(ElementName = "mothersName")]
        private string _mothersName { get; set; }
        public string MothersName {
            get => this._mothersName;
            set => this._mothersName = value ?? throw new ArgumentNullException();
        }

        [JsonProperty("fathersName")]
        [XmlElement(ElementName = "fathersName")]
        private string _fathersName { get; set; }
        public string FathersName {
            get => this._fathersName;
            set => this._fathersName = value ?? throw new ArgumentNullException();
        }

    }
}
