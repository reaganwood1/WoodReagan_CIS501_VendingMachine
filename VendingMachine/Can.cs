using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Can
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

        /// <summary>
        /// assigns references of dispenser, soldOutLight, and purchaseLight
        /// </summary>
        /// <param name="can"></param>
        /// <param name="soldOut"></param>
        /// <param name="purchaseLight"></param>
        public Can(CanDispenser can, Light soldOut, Light purchaseLight, int price) {

            if (can == null || soldOut == null || purchaseLight == null)
                throw new InvalidOperationException("setting a reference to null in can");

            this.price = price;
            dispenser = can;
            soldOutLight = soldOut;
            this.purchaseLight = purchaseLight;
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
        /// Dispenses a can and then manages the soldOutLight and purchaseLight
        /// </summary>
        public void DispenseCanAndManageLights() {
            if (numCans <= 0)
                throw new InvalidOperationException("Tried to dispense can when a can didn't exist");

            dispenser.Actuate(); // dispense the can
            numCans--;

            purchaseLight.TurnOff(); // turn off the light

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
