namespace BankingKata
{
    public interface IOverdraftAgreement
    {
        void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction);
        void ChargeIfOverSoftLimit(Money balance, ILedger ledger);
    }
}
