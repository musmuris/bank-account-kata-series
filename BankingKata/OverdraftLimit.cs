using System;

namespace BankingKata
{
    public class OverdraftLimit : IOverdraftLimit
    {
        private readonly Money m_HardOverdraftLimit;

        public OverdraftLimit(Money hardOverdraftLimit)
        {
            m_HardOverdraftLimit = hardOverdraftLimit;
        }

        public void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction)
        {
            if (transaction.ApplyTo(existingBalance) < m_HardOverdraftLimit)
            {
                throw new Exception("You can't do that! You'd go too overdrawn!");
            }
        }
    }
}