using System.Runtime.Serialization;

namespace WhenRepair.Application
{
    [DataContract]
    public class AddressAutocomplete
    {
        [DataMember(Name = "total")]
        public int TotalCount { get; set; }

        [DataMember(Name = "addresses")]
        public string[] Addresses { get; set; }
    }
}