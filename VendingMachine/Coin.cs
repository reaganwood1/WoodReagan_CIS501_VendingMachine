using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{

    public class Coin
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

#if DEBUG
        /// <summary>
        /// Public getter for numCoins
        /// </summary>
        public int NumCoins {
            get {
                return numCoins;
            }
        }
#endif

        /// <summary>
        /// Constructor for Coin, sets coin value and the CoinDispenser reference
        /// </summary>
        /// <param name="value">value of the coin to be created</param>
        /// <param name="dispenser">dispenser reference necessary to dispense coins</param>
        /// <param name="initialAmount">Initial number of coins of this type</param>
        public Coin(int value, CoinDispenser dispenser, int initialAmount) {
            coinValue = value;

            if (dispenser == null) // guard for null dispenser
                throw new InvalidOperationException("Dispenser was null");

            this.dispenser = dispenser;
            numCoins = initialAmount;
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
            //numCoins = numCoins - numCoinsToReturn;
            numCoinsToReturn = 0;
        }

        /// <summary>
        /// Gets the amount of coins that can be returned for the amount specified
        /// </summary>
        /// <param name="amountNeeded"></param>
        /// <returns></returns>
        public int GetMaxCoinsAvailabe(int amountNeeded) {

            if (numCoins > 0 && coinValue > 0) // checks for divide by zero
            {
                int coinsNeeded = amountNeeded / (coinValue); // integer division will produce highest number of coin that can be returned without going over
                if (coinsNeeded > numCoins) { // set all of the coins for return
                    numCoinsToReturn = numCoins;
                    numCoins = 0;
                } else { // set some of the coins for return
                    numCoins = numCoins - coinsNeeded; // remove the coins from numCoins
                    numCoinsToReturn = coinsNeeded; // add the coins to coinsToBeReturned
                }
                return numCoinsToReturn * coinValue; // return the value of the coins to be returned
            }
            else
                throw new InvalidOperationException("Num Coins is less than zero or the coin value is negative");
        }

        /// <summary>
        /// Returns coins to num coins because they were not returned
        /// </summary>
        public void ResetReturn() {
            numCoins = numCoins + numCoinsToReturn;
            numCoinsToReturn = 0;
        }
    }
}
