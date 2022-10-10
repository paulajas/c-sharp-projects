using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brak index")]
        [RegularExpression("^[1-9][0-9]{0,10}$", ErrorMessage = "Not valid index")]
        public int IndexNumber { get; set; }
/* numer indeksu jest jest int bo w csv z którego mogliśmy korzystać był jako int*/

        [Required(ErrorMessage = "Brak imienia")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Brak nazwiska")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Brak email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage="Brak daty urodzenia")]
        
        [RegularExpression("[1-2][0-9]{3}.{0,3}[0-9]{2}.{0,3}[0-9]{2}", ErrorMessage = "Problem with date")]
        public string Birthdate { get; set; }
        [Required(ErrorMessage ="Brak imienia matki")]
        public string MothersName { get; set; }
        [Required(ErrorMessage ="Brak imienia ojca")]
        public string FathersName { get; set; }
        [Required(ErrorMessage ="Brak kierunku studiów")]
        public string StudiesName { get; set; }
        [Required(ErrorMessage ="Brak studies mode")]
        public string StudiesMode { get; set; }

        public string toCSV()
        {
            return this.FirstName + ","
                + this.LastName + ","
                + this.IndexNumber + ","
                + this.Birthdate + ","
                + this.StudiesName + ","
                + this.StudiesMode + ","
                + this.Email + ","
                + this.FathersName + ","
                + this.MothersName;
        }

    }
}
