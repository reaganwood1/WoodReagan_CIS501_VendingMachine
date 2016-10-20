using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Coin
    {
        /// <summary>
        /// Coin dispenser that coins will be dispensed through
        /// </summary>
        CoinDispenser dispenser;
 
        /// <summary>
        /// Represents the number of each coin present
        /// </summary>
        private int numCoins;

        /// <summary>
        /// Represents the value of each coin
        /// </summary>
        private int coinValue;

        /// <summary>
        /// Represents the number of the coin that will be dispensed
        /// </summary>
        private int numCoinsToReturn;

        /// <summary>
        /// Public getter for numCoins
        /// </summary>
        public int NumCoins {
            get {
                return numCoins;
            }
        }

        /// <summary>
        /// Public getter for coinValue
        /// </summary>
        public int CoinValue {
            get {
                return coinValue;
            }
        }

        /// <summary>
        /// Constructor for Coin, sets coin value and the CoinDispenser reference
        /// </summary>
        /// <param name="value">value of the coin to be created</param>
        /// <param name="dispenser">dispenser reference necessary to dispense coins</param>
        public Coin(int value, CoinDispenser dispenser) {
            coinValue = value;

            if (dispenser != null)
                this.dispenser = dispenser;
            else
                throw new InvalidOperationException("Dispenser was null");
        }

        /// <summary>
        /// Adds one coin to the total amount
        /// </summary>
        public void AddCoin() {
            numCoins++;
        }

        /// <summary>
        /// Dispenses the coins to the CoinDispenser
        /// </summary>
        public void DispenseCoins() {

            dispenser.Actuate(numCoinsToReturn);
            numCoins = numCoins - numCoinsToReturn;
            numCoinsToReturn = 0;


        }


    }
}
