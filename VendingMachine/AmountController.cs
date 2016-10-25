using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// The types of coins that are accepted into the vending machine controller
    /// </summary>
    public enum CurrencyTypes { TenYen, FiftyYen, OneHundredYen, FiveHundredYen };

    public class AmountController
    {
        /// <summary>
        /// Handles the returning of change
        /// </summary>
        public void ReturnChange() {

        }

        /// <summary>
        /// Handles the purchasing of cans
        /// </summary>
        public void PurchaseButtonPressed(int buttonIndex) {

        }

        /// <summary>
        /// Handles the insertion of coins into the machine
        /// </summary>
        public void CoinInserted(CurrencyTypes type) {

        }
    }
}
