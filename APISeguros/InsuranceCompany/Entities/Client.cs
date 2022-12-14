using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InsuranceCompany.Entities
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}