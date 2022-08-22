using System.Runtime.Serialization;

namespace InsuranceCompany.Models.DTO
{
    [DataContract]
    public class PolicyDto
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double AmountInsured { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string InceptionDate { get; set; }
        [DataMember]
        public bool InstallmentPayment { get; set; }
        [DataMember]
        public string ClientId { get; set; }
    }
}