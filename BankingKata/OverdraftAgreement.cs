using System;

namespace BankingKata
{
    public class OverdraftAgreement : IOverdraftAgreement
    {
        private readonly IOverdraftLimit m_OverdraftLimit;

        public OverdraftAgreement(Money hardOverdraftLimit)
        {
            if( hardOverdraftLimit == null ) throw new ArgumentNullException("hardOverdraftLimit");

            m_OverdraftLimit = new OverdraftLimit(hardOverdraftLimit);
        }

        public void CheckTransactionIsAllowed(Money existingBalance, ITransaction transaction)
        {
            var newState = m_OverdraftLimit.TransactionEffect(existingBalance, transaction);
            if (newState == OverdraftState.OverHardLimit)
            {
                throw new Exception("You can't do that! You'd go too overdrawn!");
            }
        }

        public void ChargeIfOverSoftLimit(Money balance, ILedger ledger)
        {
        }
    }
}
