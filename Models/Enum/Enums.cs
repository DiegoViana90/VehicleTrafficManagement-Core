namespace VehicleTrafficManagement.Enum;

    public enum  UserType
    {
    Standard = 0,
    Master = 1
    }

    public enum VehicleStatus
    {
        Stopped = 0,
        Contract = 1,
        Maintenance = 2
    }

    public enum ContractStatus
    {
        Active = 0,
        Inactive = 1,
        Paused = 2
    }

        public enum CompanyStatus
    {
        Active = 0,
        Inactive = 1
    }

        public enum FuelType
    {
        Ethanol = 0,
        Gasoline = 1,
        Flex = 2,
        Diesel = 3,
        Hybrid = 4,
        Eletric = 5,
        Others = 6
    }

        public enum VehicleManufacturer
    {
        Fiat = 0,
        Volkswagen = 1,
        Chevrolet = 2,
        Ford = 3,
        Toyota = 4,
        Honda = 5,
        Hyundai = 6,
        Renault = 7,
        Nissan = 8,
        Jeep = 9,
        Tesla = 10,
        BMW = 11,
        MercedesBenz = 12,
        Audi = 13,
        Kia = 14,
        Peugeot = 15,
        Suzuki = 16,
        Mazda = 17,
        Subaru = 18,
        Volvo = 19,
        Mitsubishi = 20,
        LandRover = 21,
        Jaguar = 22,
        Porsche = 23,
        Ferrari = 24,
        Lamborghini = 25,
        AstonMartin = 26,
        Maserati = 27,
        AlfaRomeo = 28,
        Bentley = 29,
        Bugatti = 30,
        Citroen = 31,
        Chrysler = 32,
        Dodge = 33,
        GMC = 34,
        Infiniti = 35,
        Lexus = 36,
        Mini = 37,
        RollsRoyce = 38,
        Saab = 39,
        Seat = 40,
        Skoda = 41,
        SsangYong = 42,
        Tata = 43,
        Chery = 44,
        JAC = 45,
        Troller = 46,
        Lifan = 47,
        Geely = 48,
        BYD = 49,
        Haval = 50
    }
    public enum EnforcingAgency
    {
        ANAC = 0,             
        ANTAQ = 1,            
        ANTT = 2,             
        CONCESSIONARIA = 3,   
        DER = 4,              
        DETRAN = 5,           
        DNIT = 6,             
        IBAMA = 7,            
        Guarda_Municipal = 8,  
        PM = 9,               
        PRE = 10,             
        PRF = 11,             
        Outros = 99           
    }
    public enum FineStatus
    {
        Active = 0,           
        SentToClient = 1,     
        Paid = 2,             
        Overdue = 3           
    }


