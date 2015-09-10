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
            var overdraftLimit = new OverdraftAgreement(new Money(-1000m));

            var account = new Account(new Ledger(), overdraftLimit);

            var debitEntry = new ATMDebitEntry(DateTime.Now, new Money(1001m));

            Assert.Throws<Exception>(() => account.Withdraw(debitEntry));
        }

        [Test]
        public void CanWithdrawWithinHardLimit()
        {
            var overdraftLimit = new OverdraftAgreement(new Money(-1000m));

            var account = new Account(new Ledger(), overdraftLimit);

            var debitEntry = new ATMDebitEntry(DateTime.Now, new Money(1000m));

            account.Withdraw(debitEntry);
            Assert.AreEqual(new Money(-1000m), account.CalculateBalance());
        }


    }
}