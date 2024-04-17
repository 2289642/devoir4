using SQLite;

namespace Stagix.Models
{
    [Table("people")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(7), Unique]
        public string numDA { get; set; }

        [MaxLength(250)]
        public string pwd { get; set; }
    }
}
