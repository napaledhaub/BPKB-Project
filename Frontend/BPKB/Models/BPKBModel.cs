using System.ComponentModel.DataAnnotations.Schema;

namespace BPKB.Models
{
    public class BPKBModel
    {
        public string? AgreementNumber { get; set; }
        public string? BPKBNo { get; set; }
        public string? BranchID { get; set; }
        public DateTime? BPKBDate { get; set; }
        public string? FakturNo { get; set; }
        public DateTime? FakturDate { get; set; }
        public long? LocationID { get; set; }
        public string? PoliceNo { get; set; }
        public DateTime? BPKBDateIn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
