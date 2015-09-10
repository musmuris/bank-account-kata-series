using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public enum OverdraftState
    {
        WithinLimit,
        OverSoftLimit,
        OverHardLimit
    }

    public interface IOverdraftLimit
    {
        OverdraftState TransactionEffect(Money existingBalance, ITransaction transaction);
    }

    public class OverdraftLimit : IOverdraftLimit
    {
        private readonly Money m_HardOverdraftLimit;

        public OverdraftLimit(Money hardOverdraftLimit)
        {
            m_HardOverdraftLimit = hardOverdraftLimit;
        }

        public OverdraftState TransactionEffect(Money existingBalance, ITransaction transaction)
        {
            if (transaction.ApplyTo(existingBalance) < m_HardOverdraftLimit)
            {
                return OverdraftState.OverHardLimit;
            }
            return OverdraftState.WithinLimit;
        }
    }
}
