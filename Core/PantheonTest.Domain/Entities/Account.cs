using System;

namespace PantheonTest.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public decimal Balance  { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Currency { get; set; }
    }
}
