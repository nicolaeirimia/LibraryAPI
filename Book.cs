using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI
{
    [Table("tbl_Books")]
    public class Book
    {
        [Key]
        [Column ("book_id")]
        public int Book_Id { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("author_id")]
        public int Author_Id { get; set; }
        [Column("genre")]
        public string? Genre { get; set; }
        [Column("publish_date")]
        public DateTime Publish_Date { get; set; }

    }
}
