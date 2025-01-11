using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPKBAPI.Models
{
    [Table("ms_user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public long? UserID { get; set; }
        [Column("user_name")]
        public string? UserName { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }
    }
}
