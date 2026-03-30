using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class CustomerAddress
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string AddressLine { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsPrimary { get; set; }
    }
}
