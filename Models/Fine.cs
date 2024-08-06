using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string FineNumber { get; set; }
        public DateTime FineDateTime { get; set; }
        public DateTime FineDueDate { get; set; }
        public EnforcingAgency EnforcingAgency { get; set; }
        public string FineLocation { get; set; }
        public decimal FineAmount { get; set; }
        public decimal? DiscountedFineAmount { get; set; }
        public decimal? InterestFineAmount { get; set; }
        public decimal FinalFineAmount { get; set; }
        public FineStatus FineStatus { get; set; }
        public string? Description { get; set; }
    }
}
