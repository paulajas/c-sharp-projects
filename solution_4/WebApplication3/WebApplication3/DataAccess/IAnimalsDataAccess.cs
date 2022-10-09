using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.DataAccess
{
    public interface IAnimalsDataAccess
    {
        IEnumerable<Animal> GetAnimal(string OrderBy);
        void CreateAnimal(Animal animal);
        void ChangeAnimal(int idAnimal, Animal animal);
        void DeleteAnimal(int idAnimal);
    }
}
