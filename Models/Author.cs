using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models
{
    public class Author
    {
        [Key]
        [Column("author_id")]
        public int Author_Id { get; set; }
        [Column("first_name")]
        public string? First_Name { get; set; }
        [Column("last_name")]
        public string? Last_Name { get; set; }
        [Column("nationality")]
        public string? Nationality { get; set; }
        [Column("birthdate")]
        public DateTime BirthDate { get; set; }
    }
}
