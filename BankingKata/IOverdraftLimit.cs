namespace BankingKata
{
    public interface IOverdraftLimit
    {
        void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction);
    }

    public class UnlimitedOverdraft : IOverdraftLimit
    {
        public void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction)
        {
        }
    }
}