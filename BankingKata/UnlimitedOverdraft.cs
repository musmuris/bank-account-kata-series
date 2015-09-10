namespace BankingKata
{
    public class UnlimitedOverdraft : IOverdraftAgreement
    {
        public void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction)
        {
        }

        public void ChargeIfOverSoftLimit(Money balance, ILedger ledger)
        {
        }
    }
}