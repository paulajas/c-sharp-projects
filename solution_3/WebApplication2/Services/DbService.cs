using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class DbService : IDbService
    {
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        private static string csvfile { get; set; } = Path.Combine(CurrentDirectory, "dane.csv");

        private static readonly string logfile = Path.Combine(CurrentDirectory, "log.txt");

        private Dictionary<string, Student> studentsDict = new Dictionary<string, Student>();

        private List<Student> students = new List<Student>();

        public DbService()
        {

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("log.txt")
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
            .CreateLogger();



            if (!File.Exists(csvfile))
            {
                string[] tmp = { "Plik " + csvfile + " nie istnieje" };
                File.WriteAllLines(logfile, tmp);
                Log.Logger.Error("Plik " + csvfile + " nie istnieje");
                throw new FileNotFoundException("Plik " + csvfile + " nie istnieje");
            }

            using (StreamReader sr = new StreamReader(csvfile))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(",");
                    if (fields.Length != 9)
                    {
                        string[] tmp = { "Brak 9 kolumn: " + fields };
                        /*File.WriteAllLines(logfile, tmp);
                        Log.Logger.Error("Brak 9 kolumn: " + fields);*/
                    }

                    foreach (string c in fields)
                    {
                        if (string.IsNullOrWhiteSpace(c))
                        {
                            string[] tmp = { "Brak 9 kolumn: " + fields };
                            /*File.WriteAllLines(logfile, tmp);*/
                            /*Log.Logger.Error("Brak 9 kolumn: " + fields);*/
                        }
                    }
                    bool isStudent = false;
                    int parsed = 0;
                    var isParsed = Int32.TryParse(fields[4], out parsed);
                    var s = new Student()
                    {
                        FirstName = fields[0],
                        LastName = fields[1],
                        IndexNumber = parsed,
                        StudiesName = fields[2],
                        StudiesMode = fields[3],
                        Birthdate = fields[5],
                        Email = fields[6],
                        MothersName = fields[7],
                        FathersName = fields[8]
                    };
                    foreach (Student student in students)
                    {
                        try
                        {
                            if (student.IndexNumber == parsed)
                            {
                                isStudent = true;
                                UpdateStudent(s);
                                break;
                            }
                        }catch (Exception ex)
                        {
                            Console.WriteLine($"Error '{ex}'");
                        }
                    }
                    if (!isStudent)
                    {
                        students.Add(s);
                    }
                }
            }
        }
        public bool AddStudent(Student newStudent)
        {
            if (GetStudent(newStudent.IndexNumber) == null)
            {
                students.Add(newStudent);
                File.AppendAllText(csvfile, newStudent.toCSV() + "\n");
                return true;
            }

            return false;
        }

        public Student UpdateStudent(Student updateStudent)
        {
            foreach (Student st in students) {
                if (st.IndexNumber == updateStudent.IndexNumber)
                {
                    st.FirstName = updateStudent.FirstName;
                    st.LastName = updateStudent.LastName;
                    st.Birthdate = updateStudent.Birthdate;
                    st.FathersName=updateStudent.FathersName;
                    st.MothersName=updateStudent.MothersName;
                    st.StudiesName=updateStudent.StudiesName;
                    st.StudiesMode=updateStudent.StudiesMode;
                    st.Email=updateStudent.Email;
                    File.Delete(csvfile);
                    foreach (Student s in students)
                        File.AppendAllText(csvfile, s.toCSV() + "\n");
                    return st; ;
                }       
            }
            return null;
        }

        public bool DeleteStudent(int IndexNumber)
        {
            students.Remove(GetStudent(IndexNumber));
            File.Delete(csvfile);
            foreach (Student s in students)
                File.AppendAllText(csvfile, s.toCSV() + "\n");
            return true;
        }

        public Student GetStudent(int IndexNumber)
        {
            return students.Where(s => s.IndexNumber.Equals(IndexNumber)).First();
           /* foreach (Student st in students)
            {
                if (st.IndexNumber == id)
                {
                    return st;
                }
            }
            return null;*/
        }

        public IEnumerable<Student> GetStudents()
        {
            return students;
        }
    }
}
