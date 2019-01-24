using System;
using System.Runtime.Serialization;

namespace WhenRepair.Application
{
    [DataContract]
    public class FiasResult
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "id")]
        public Guid Guid { get; set; }
    }
}