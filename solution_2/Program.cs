using ConsoleApp2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Serilog;
using Serilog.Events;

namespace ConsoleApp2
{
    class Program
    {
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        private static string csvfile { get; set; } = Path.Combine(CurrentDirectory, "data.csv");
        

        static void Main(string[] args)
        {
/*            args[0] = "Data/dane.csv";
            args[1] = "out.json";
            args[2] = "json";*/
            string logfile = Path.Combine(CurrentDirectory, "log.txt");
            string csvfile = null;
            string outputfile = null;
            string inputPath = null;
            string outputPath = null;
            string outformat = "json";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            if (!String.IsNullOrEmpty(args[0]))
            {
                csvfile = args[0];
                inputPath = Path.Combine(CurrentDirectory, args[0]);
            }
            var isPathValid = inputPath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            if (!isPathValid)
            {
                Log.Logger.Error("Podana ścieżka jest niepoprawna");
                string[] tmp = { "Podana ścieżka jest niepoprawna"};
                File.WriteAllLines(logfile, tmp);
                throw new ArgumentException("Podana ścieżka jest niepoprawna");
            }

            if (!File.Exists(csvfile))
            {
                string[] tmp = { "Plik " + args[0] + " nie istnieje" };
                File.WriteAllLines(logfile, tmp);
                Log.Logger.Error("Plik " + args[0] + " nie istnieje");
                throw new FileNotFoundException("Plik " + args[0] + " nie istnieje");
            }
            if (!String.IsNullOrEmpty(args[1]))
            {
                outputfile = args[1];
                outputPath = Path.Combine(CurrentDirectory, args[1]);
            }
            if (!String.IsNullOrEmpty(args[2]))
            {
                outformat = args[2];
                outformat = Path.Combine(CurrentDirectory, args[2]);
            }


            List<Student> students = new List<Student>();
            List<Studies> studiesList = new List<Studies>();
            using (StreamReader sr = new StreamReader(inputPath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(",");
                    if (fields.Length != 9)
                    {
                        string[] tmp = { "Brak 9 kolumn: " + fields };
                        File.WriteAllLines(logfile, tmp);
                        Log.Logger.Error("Brak 9 kolumn: " + fields);
                    }
                    if (!studiesList.Exists(Name => Name.Equals(fields[2])))
                    {
                        Studies study = new Studies()
                        {
                            Name = fields[2],
                            NumberOfStudents = 1
                        };
                        studiesList.Add(study);
                    }
                   
                    foreach (string c in fields)
                    {
                        if (string.IsNullOrWhiteSpace(c))
                        {
                            string[] tmp = { "Brak 9 kolumn: " + fields };
                            File.WriteAllLines(logfile, tmp);
                            Log.Logger.Error("Brak 9 kolumn: " + fields);
                        }
                    }
                    var s = new Student()
                    {
                        FirstName = fields[0],
                        LastName = fields[1],
                        Indexno = fields[4],
                        studies = new Studies()
                        {
                            Name = fields[2],
                            Mode = fields[3]
                        },
                        Birthdate = fields[5],
                        Email = fields[6],
                        MothersName = fields[7],
                        FathersName = fields[8]

                    };
                    students.Add(s);
                }
            }
            University un = new University()
            {
                classCreatedAt = DateTime.Now,
                authorOfClass = "Paulina Jasinska",
                students = students,
                ActiveStudies = studiesList
            };
            if (outformat.Equals("json")){
                string strJson = JsonConvert.SerializeObject(un);
                File.WriteAllText(outputPath, strJson);
            }
            else if (outformat.Equals("xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(University));
                serializer.Serialize(File.OpenWrite(outputPath), un);
            }
        }
    }
}
