namespace WhenRepair.Application
{
    public class Address
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Postcode { get; set; }

        public string State { get; set; }

        public override string ToString() => $"г. {City}, ул. {Street}, {House}";
    }
}