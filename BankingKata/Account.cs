using System;

namespace BankingKata
{
    public class Account
    {
        private readonly ILedger _ledger;
        private readonly IOverdraftLimit _overdraftLimit;

        public Account(ILedger ledger, IOverdraftLimit overdraftLimit)
        {
            _ledger = ledger;
            _overdraftLimit = overdraftLimit;
        }

        public Account(ILedger ledger)
            :this(ledger, new UnlimitedOverdraft())
        {
        }

        public Account()
            : this(new Ledger())
        {
        }

        public void Deposit(DateTime transactionDate, Money money)
        {
            var depositTransaction = new CreditEntry(transactionDate, money);
            _ledger.Record(depositTransaction);
        }

        public Money CalculateBalance()
        {
            return _ledger.Accept(new BalanceCalculatingVisitor(), new Money(0m));
        }

        public void Withdraw(DebitEntry debitEntry)
        {
            _overdraftLimit.CheckTransactionIsAllowed(CalculateBalance(), debitEntry);
            _ledger.Record(debitEntry);
        }

        public void PrintBalance(IPrinter printer)
        {
            var balance = CalculateBalance();
            printer.PrintBalance(balance);
        }

        public void PrintLastTransaction(IPrinter printer)
        {
            printer.PrintLastTransaction(_ledger);
        }
    }
}