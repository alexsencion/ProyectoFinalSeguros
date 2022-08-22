using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InsuranceCompany.Entities
{
    public class Policy
    {
        [Key]
        public string Id { get; set; }
        public double AmountInsured { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string InceptionDate { get; set; }
        public bool InstallmentPayment { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public string ClientId { get; set; }
    }
}