using VehicleTrafficManagement.Enum;

namespace VehicleTrafficManagement.Dto
{
  public class FineDto
    {   
        public DateTime RegistrationDate { get; set; } = DateTime.Now;   
        public int VehicleId { get; set; }
        public string FineNumber { get; set; }
        public DateTime FineDateTime  { get; set; }
        public DateTime FineDueDate { get; set; }
        public EnforcingAgency EnforcingAgency { get; set; }
        public string FineLocation  { get; set; }
        public decimal FineAmount { get; set; }
        public decimal DiscountedFineAmount { get; set; }
        public decimal FinalFineAmount { get; set; } 
        public FineStatus FineStatus { get; set; }
        public string? Description { get; set; }
    }
}
