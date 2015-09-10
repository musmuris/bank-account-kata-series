using System;

namespace BankingKata
{
    public class OverdraftAgreement : IOverdraftAgreement
    {
        private readonly Money m_Charge;
        private readonly IOverdraftLimit m_OverdraftLimit;

        public OverdraftAgreement(Money hardOverdraftLimit)
        {
            if( hardOverdraftLimit == null ) throw new ArgumentNullException("hardOverdraftLimit");

            m_OverdraftLimit = new OverdraftLimit(hardOverdraftLimit, new Money(0m));
        }

        public OverdraftAgreement(Money hardOverdraftLimit, Money softOverdraftLimit, Money charge)
        {
            m_Charge = charge;
            if (hardOverdraftLimit == null)
                throw new ArgumentNullException("hardOverdraftLimit");
            if (softOverdraftLimit == null)
                throw new ArgumentNullException("softOverdraftLimit");
            if (charge == null)
                throw new ArgumentNullException("charge");

            m_OverdraftLimit = new OverdraftLimit(hardOverdraftLimit, softOverdraftLimit);
        }

        public void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction)
        {
            var newState = m_OverdraftLimit.OverdraftStateForBalance(transaction.ApplyTo(existingBalance));
            if (newState == OverdraftState.OverHardLimit)
            {
                throw new Exception("You can't do that! You'd go too overdrawn!");
            }
        }

        public void ChargeIfOverSoftLimit(Money balance, ILedger ledger)
        {
            var state = m_OverdraftLimit.OverdraftStateForBalance(balance);
            if (state == OverdraftState.OverSoftLimit && m_Charge != null)
            {
                ledger.Record(new OverdraftChargeEntry(m_Charge));
            }
        }
    }

    public class OverdraftChargeEntry : DebitEntry
    {
        public OverdraftChargeEntry(Money charge) : base(DateTime.Now, charge)
        {
        }
    }
}
