using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2.Models
{
    [XmlRoot("uczelnia")]
    class University
    {
        [XmlAttribute("createdAt")]
        public DateTime classCreatedAt = DateTime.Now;

        [XmlAttribute("author")]
        public string authorOfClass { get; set; }

        [XmlArrayItem(ElementName = "student", Type = typeof(Student))]
        [XmlArray("studenci")]
        public List<Student> students= new List<Student>();

            [XmlArrayItem(ElementName = "studies", Type = typeof(ActiveStudy))]
        [XmlArray("activeStudies")]
        public List<Studies> ActiveStudies = new List<Studies>();

        var activeStudiesNumber = students.GroupBy(s => s.studies.Mode).Select(t => new
        {
            name =t.key,
            numberOfStudents = t.Count()
        }).ToList();
        activeStudiesNumber.ForEach(s => ActiveStudies.Add(new ActiveStudy(s.Name, s.NumberOfStudents)));
    }
}
