using System;

namespace BankingKata
{
    public class Account
    {
        private readonly ILedger _ledger;
        private readonly IOverdraftAgreement _overdraftAgreement;

        public Account(ILedger ledger, IOverdraftAgreement overdraftAgreement)
        {
            _ledger = ledger;
            _overdraftAgreement = overdraftAgreement;
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
            _overdraftAgreement.CheckTransactionIsAllowed(CalculateBalance(), debitEntry);

            _ledger.Record(debitEntry);

            _overdraftAgreement.ChargeIfOverSoftLimit(CalculateBalance(), _ledger);
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