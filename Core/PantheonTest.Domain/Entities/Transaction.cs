﻿using System;

namespace PantheonTest.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public DateTime DateOn { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdraw
    }
}
