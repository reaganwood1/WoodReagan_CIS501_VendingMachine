using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Can
    {
        /// <summary>
        /// dispenser dispenses cans to the user
        /// </summary>
        private CanDispenser dispenser;

        /// <summary>
        /// soldOutLight indicates the available inventory of the item
        /// </summary>
        private Light soldOutLight;

        /// <summary>
        /// purchaseLight indicates whether the can is purchaseable
        /// </summary>
        private Light purchaseLight;

        /// <summary>
        /// Number of can of this type present
        /// </summary>
        private int numCans;

        /// <summary>
        /// Price of this can
        /// </summary>
        private int price;

#if DEBUG
        public int NumCans {
            get {
                return numCans;
            }
        }
#endif

        /// <summary>
        /// assigns references of dispenser, soldOutLight, and purchaseLight
        /// </summary>
        /// <param name="can">dispenser for the can</param>
        /// <param name="soldOut">sold out light for the can</param>
        /// <param name="purchaseLight">purchase light for the can</param>
        /// /// <param name="initialAmount">initial amount of the cans</param>
        public Can(CanDispenser can, Light soldOut, Light purchaseLight, int price, int initialAmount) {

            if (can == null || soldOut == null || purchaseLight == null)
                throw new InvalidOperationException("setting a reference to null in can");

            this.price = price;
            dispenser = can;
            soldOutLight = soldOut;
            this.purchaseLight = purchaseLight;
            numCans = initialAmount;
        }

        /// <summary>
        /// turns off the purchaseLight
        /// </summary>
        public void TurnOffPurchaseLight() {
            purchaseLight.TurnOff();
        }

        /// <summary>
        /// Checks to see if the purchase light should be turned on
        /// </summary>
        /// <param name="amountInserted"></param>
        public void CheckPurchaseLight(int amountInserted) {

            if (!soldOutLight.IsOn() && amountInserted >= price) { // turn on light if can is available and amount inserted is enough
                purchaseLight.TurnOn();
            }
        }

        /// <summary>
        /// Dispenses a can and then manages the soldOutLight
        /// </summary>
        public void DispenseCanAndManageSoldOutLight() {
            if (numCans <= 0)
                throw new InvalidOperationException("Tried to dispense can when a can didn't exist");

            dispenser.Actuate(); // dispense the can
            numCans--;

            if (numCans == 0) // check if soldOutLight should be turned on
                soldOutLight.TurnOn();
        }

        /// <summary>
        /// Checks if the can is available, if so, returns the price of it
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, int> CheckCanStatusAndPrice() {

            if (!purchaseLight.IsOn()) // can is unavailable, so return false
                return new Tuple<bool, int>(false, 0);

            return new Tuple<bool, int>(true, price); // available, return true and price
        }
    }
}
