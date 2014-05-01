using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptopayApi
{
    class Balance
    {
        private Dictionary<Currency, Decimal> balanceInCurrencies =
            new Dictionary<Currency, Decimal>();

        public Balance(Dictionary<Currency, Decimal> balance)
        {
            balanceInCurrencies = balance;
        }

        public Decimal Currency(Currency currency)
        {
            Decimal balance = 0.0m;
            try
            {
                balance = balanceInCurrencies[currency];
            }
            catch (KeyNotFoundException e)
            {
                // if balance not found for currency return 0
            }
            return balance;
        }
    }
}
