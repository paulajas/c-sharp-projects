using System.Collections.Generic;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IDbService
    {
        bool AddStudent(Student newStudent);
        Student UpdateStudent(Student updateStudent);
        bool DeleteStudent(int id);
        Student GetStudent(int id);

        IEnumerable<Student> GetStudents();
    }
}
