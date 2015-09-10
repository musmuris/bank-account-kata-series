using System;
using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    public class OverdraftTests
    {
        [Test]
        public void CannotWithdrawPastHardLimit()
        {
            var overdraftLimit = new OverdraftLimit(new Money(1000m));

            var account = new Account(new Ledger(), overdraftLimit);

            var debitEntry = new ATMDebitEntry(DateTime.Now, new Money(1001m));

            Assert.Throws<Exception>(() => account.Withdraw(debitEntry));
        }
    }
}