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
        OverdraftState OverdraftStateForBalance(Money balance);
    }

    public class OverdraftLimit : IOverdraftLimit
    {
        private readonly Money m_HardOverdraftLimit;
        private readonly Money m_SoftOverdraftLimit;

        public OverdraftLimit(Money hardOverdraftLimit, Money softOverdraftLimit)
        {
            m_HardOverdraftLimit = hardOverdraftLimit;
            m_SoftOverdraftLimit = softOverdraftLimit;
        }

        public OverdraftState OverdraftStateForBalance(Money balance)
        {
            if (balance < m_HardOverdraftLimit)
            {
                return OverdraftState.OverHardLimit;
            }
            if (balance < m_SoftOverdraftLimit)
            {
                return OverdraftState.OverSoftLimit;
            }
            return OverdraftState.WithinLimit;
        }
    }
}
