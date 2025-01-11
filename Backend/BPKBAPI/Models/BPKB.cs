using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPKBAPI.Models
{
    [Table("tr_bpkb")]
    public class BPKB
    {
        [Key]
        [Column("agreement_number")]
        public string? AgreementNumber { get; set; }
        [Column("bpkb_no")]
        public string? BPKBNo { get; set; }
        [Column("branch_id")]
        public string? BranchID { get; set; }
        [Column("bpkb_date")]
        public DateTime? BPKBDate { get; set; }
        [Column("faktur_no")]
        public string? FakturNo { get; set; }
        [Column("faktur_date")]
        public DateTime? FakturDate { get; set; }
        [Column("location_id")]
        public long? LocationID { get; set; }
        [Column("police_no")]
        public string? PoliceNo { get; set; }
        [Column("bpkb_date_in")]
        public DateTime? BPKBDateIn { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }
        [Column("last_updated_by")]
        public string? LastUpdateBy { get; set; }
        [Column("last_updated_on")]
        public DateTime? LastUpdateOn { get; set; }
    }
}
