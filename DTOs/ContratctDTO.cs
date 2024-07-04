namespace VehicleTrafficManagement.Dto
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string VehicleIds { get; set; }
        public bool IsOpen { get; set; }
    }
}
