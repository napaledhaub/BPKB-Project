using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPKBAPI.Models
{
    [Table("ms_storage_location")]
    public class StorageLocation
    {
        [Key]
        [Column("location_id")]
        public long? LocationID { get; set; }
        [Column("location_name")]
        public string? LocationName { get; set; }
    }
}
