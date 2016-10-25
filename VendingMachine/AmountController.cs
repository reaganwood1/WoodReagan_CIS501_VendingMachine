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
        /// Array holding all of the coins in the vending machine
        /// </summary>
        private Coin[] allCoins;

        /// <summary>
        /// Array holding all of the cans in the vending machine
        /// </summary>
        private Can[] allCans;

        /// <summary>
        /// Light that will flicker indicating that no change is available for the selection
        /// </summary>
        private TimerLight noChangeLight;

        /// <summary>
        /// Display indicates the amount of currrency inserted into the machine
        /// </summary>
        private AmountDisplay amountDisplay;

        private int amountInserted;

        public AmountController(Coin[] allCoins, Can[] allCans, TimerLight noChangeLight, AmountDisplay amountDisplay) {
            if (allCoins == null || allCans == null || noChangeLight == null || amountDisplay == null)
                throw new InvalidOperationException("AmountController parameter was null");

            this.allCans = allCans;
            this.allCoins = allCoins;
            this.noChangeLight = noChangeLight;
            this.amountDisplay = amountDisplay;
            this.amountInserted = 0; // initialize amount inserted to 0
        }

        /// <summary>
        /// Handles the returning of change
        /// </summary>
        public void ReturnChange() {

            if (!TryGetReturnChange(0)) // queues coins for return
                throw new InvalidOperationException("Change could not be calculate when only change was needed");

            for (int i = 0; i < allCoins.Length; i++) // call dispense coins for each coin
                allCoins[i].DispenseCoins();
           
        }

        /// <summary>
        /// Gets change allocated for return
        /// </summary>
        /// <param name="price">price of the item calculating change for</param>
        /// <returns>true if change could be dispensed, false if it could not</returns>
        private bool TryGetReturnChange(int price) {

            int amountNeeded = amountInserted - price;
            int availableValue = 0;

            for (int i = allCoins.Length - 1; i >= 0; i--) { // get the coins each coin can contribute, subract it from amountNeeded
                availableValue = allCoins[i].GetMaxCoinsAvailabe(amountNeeded);
                amountNeeded = amountNeeded - availableValue;
            }

            if (amountNeeded == 0) // change was located
                return true;
            
            for (int i = 0; i < allCoins.Length; i++) // change was not located, dealocate coins to be returned
                allCoins[i].ResetReturn();
            return false; // change could not be located
        }

        /// <summary>
        /// Handles the purchasing of cans
        /// </summary>
        public void PurchaseButtonPressed(int buttonIndex) {

            Tuple<bool, int> canStatusAndPrice = allCans[buttonIndex].CheckCanStatusAndPrice();

            if (canStatusAndPrice.Item1 == false) // can was not available, do nothing
                return;

            if (TryGetReturnChange(canStatusAndPrice.Item2)) // change could be calculated
            {
                for (int i = 0; i < allCoins.Length; i++)
                    allCoins[i].DispenseCoins();

                allCans[buttonIndex].DispenseCanAndManageSoldOutLight(); // dispense the can

                for (int i = 0; i < allCans.Length; i++) // turn off all of the purchaseable lights
                    allCans[i].TurnOffPurchaseLight();

                amountInserted = 0; // set amountInserted back to zero because change has been returned
                amountDisplay.DisplayAmount(amountInserted); // update the amount display to reflect the returned change
            }
            else // change could not be calculated, so turn on the noChangeLight for 3 seconds
                noChangeLight.TurnOn3Sec(); 
        }

        /// <summary>
        /// Handles the insertion of coins into the machine
        /// </summary>
        /// <param name="type">type of the coin to be inserted</param>
        public void CoinInserted(CurrencyTypes type) {
            allCoins[(int)type].AddCoin(); // index coin array with enum
            int amountToAdd = 0;

            switch ((int)type) { // determine the amount to be added based off of the enum
                case 0:
                    amountToAdd = 10;
                    break;
                case 1:
                    amountToAdd = 50;
                    break;
                case 2:
                    amountToAdd = 100;
                    break;
                case 3:
                    amountToAdd = 500;
                    break;
                default:
                    throw new InvalidOperationException("Coin is not recognized");
            }
            amountInserted = amountInserted + amountToAdd; // increment the total amountinserted

            for (int i = 0; i < allCans.Length; i++) { // check if lights should be turned on
                allCans[i].CheckPurchaseLight(amountInserted);
            }

            amountDisplay.DisplayAmount(amountInserted); // update the amount display
        }
    }
}
