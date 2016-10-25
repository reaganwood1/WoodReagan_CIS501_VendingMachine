//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors

    public class CoinInserter
    {
        /// <summary>
        /// Handles the coinInserter press by controlling the coin insertion process
        /// </summary>
        private AmountController vmControl;

        /// <summary>
        /// The type of coin this inserter will insert
        /// </summary>
        private CurrencyTypes currencyType;

        /// <summary>
        /// Controller sets the amountController that will handle coin insertion
        /// </summary>
        /// <param name="vm">ammount controller</param>
        /// <param name="type">type of currency this inserter will insert</param>
        public CoinInserter(AmountController vm, CurrencyTypes type)
        {
            vmControl = vm;
            currencyType = type;
        }

        /// <summary>
        /// Inserts a coin of the given type into the vending machine controller
        /// </summary>
        public void CoinInserted()
        {
            vmControl.CoinInserted(currencyType);
        }

    }

    public class PurchaseButton
    {
        /// <summary>
        /// Handles the purchase button press by controlling the purchase process
        /// </summary>
        private AmountController vmControl;

        /// <summary>
        /// index specifying which item this button corresponds to on the display
        /// </summary>
        private int buttonIndex;

        /// <summary>
        /// Constructor places reference to AmountController to handle purchase button presses. Additionally, assigns the specific item index to this button
        /// </summary>
        /// <param name="vm">AmountController</param>
        /// <param name="index">index specifying which item it corresponds to on the display</param>
        public PurchaseButton(AmountController vm, int index)
        {
            vmControl = vm;
            buttonIndex = index;
        }

        /// <summary>
        /// Calls the amountController to handle the potential purchasing of the can at this index
        /// </summary>
        public void ButtonPressed()
        {
            vmControl.PurchaseButtonPressed(buttonIndex);
        }
    }

    public class CoinReturnButton
    {
        /// <summary>
        /// Handles the return button press by controlling the return of coins if necessary
        /// </summary>
        private AmountController vmControl;
       
        /// <summary>
        /// Constructor sets the AmountController to handle return button presses
        /// </summary>
        /// <param name="vm">AmountController</param>
        public CoinReturnButton(AmountController vm)
        {
            vmControl = vm;
        }

        /// <summary>
        /// checks to see if change can be returned, and if so, vmControl orchestrates the return
        /// </summary>
        public void ButtonPressed()
        {
            vmControl.ReturnChange(); 
        }
    }
}
