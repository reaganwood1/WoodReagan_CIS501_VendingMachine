//////////////////////////////////////////////////////////////////////
//      Vending Machine (Form1.cs)                                  //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010       //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VendingMachine
{
    public partial class VendingMachine : Form
    {
        // Static Constants
        public const int NUMCANTYPES = 4;
        public const int NUMCOINTYPES = 4;
        public static readonly int[] NUMCANS = {4,4,4,4};
        public static readonly int[] CANPRICES = { 120, 170, 130, 110 };
        public static readonly string[] CANNAMES = { "Coca-Cola", "Pepsi", "Dr. Pepper", "Sprite" };

        public static readonly int[] COINVALUES = { 10, 50, 100, 500 };
        public static readonly int[] NUMCOINS = { 15, 10, 5, 2 };
        // 10Yen, 50Yen, 100Yen, 500Yen

        // Boundary Objects
        private AmountDisplay amountDisplay;
        private DebugDisplay displayPrice0, displayPrice1, displayPrice2, displayPrice3;
        private DebugDisplay displayNum10Yen, displayNum50Yen, displayNum100Yen, displayNum500Yen;
        private DebugDisplay displayName0, displayName1, displayName2, displayName3;
        private DebugDisplay displayNumCans0, displayNumCans1, displayNumCans2, displayNumCans3;
        private Light soldOutLight0, soldOutLight1, soldOutLight2, soldOutLight3;
        private TimerLight noChangeLight;
        private Light purchasableLight0, purchasableLight1, purchasableLight2, purchasableLight3;
        private CoinDispenser coinDispenser10Yen, coinDispenser50Yen, coinDispenser100Yen, coinDispenser500Yen;
        private CanDispenser canDispenser0, canDispenser1, canDispenser2, canDispenser3;
        private CoinInserter coinInserter10Yen, coinInserter50Yen, coinInserter100Yen, coinInserter500Yen;
        private PurchaseButton purchaseButton0, purchaseButton1, purchaseButton2, purchaseButton3;
        private CoinReturnButton coinReturnButton;

        // instantiate Can array
        private Can[] allCans = new Can[NUMCANS.Length];
        private Coin[] allCoins = new Coin[NUMCOINS.Length];

        // instantiate controller
        AmountController amountControl;

        public VendingMachine()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            amountDisplay = new AmountDisplay(txtAmount);

            displayNum10Yen = new DebugDisplay(txtNum10Yen);
            displayNum50Yen = new DebugDisplay(txtNum50Yen);
            displayNum100Yen = new DebugDisplay(txtNum100Yen);
            displayNum500Yen = new DebugDisplay(txtNum500Yen);
            displayPrice0 = new DebugDisplay(txtPrice0);
            displayPrice1 = new DebugDisplay(txtPrice1);
            displayPrice2 = new DebugDisplay(txtPrice2);
            displayPrice3 = new DebugDisplay(txtPrice3);
            displayName0 = new DebugDisplay(txtName0);
            displayName1 = new DebugDisplay(txtName1);
            displayName2 = new DebugDisplay(txtName2);
            displayName3 = new DebugDisplay(txtName3);
            displayNumCans0 = new DebugDisplay(txtNumCan0);
            displayNumCans1 = new DebugDisplay(txtNumCan1);
            displayNumCans2 = new DebugDisplay(txtNumCan2);
            displayNumCans3 = new DebugDisplay(txtNumCan3);

            soldOutLight0 = new Light(pbxSOLight0, Color.Orange);
            soldOutLight1 = new Light(pbxSOLight1, Color.Orange);
            soldOutLight2 = new Light(pbxSOLight2, Color.Orange);
            soldOutLight3 = new Light(pbxSOLight3, Color.Orange);

            noChangeLight = new TimerLight(pbxNoChange, Color.Red, timer1);

            purchasableLight0 = new Light(pbxPurLight0, Color.Aqua);
            purchasableLight1 = new Light(pbxPurLight1, Color.Aqua);
            purchasableLight2 = new Light(pbxPurLight2, Color.Aqua);
            purchasableLight3 = new Light(pbxPurLight3, Color.Aqua);

            coinDispenser10Yen = new CoinDispenser(txtChange10Yen);
            coinDispenser50Yen = new CoinDispenser(txtChange50Yen);
            coinDispenser100Yen = new CoinDispenser(txtChange100Yen);
            coinDispenser500Yen = new CoinDispenser(txtChange500Yen);

            // All candispensers share the same output textbox for simulation
            canDispenser0 = new CanDispenser(txtCanDispenser, CANNAMES[0]);
            canDispenser1 = new CanDispenser(txtCanDispenser, CANNAMES[1]);
            canDispenser2 = new CanDispenser(txtCanDispenser, CANNAMES[2]);
            canDispenser3 = new CanDispenser(txtCanDispenser, CANNAMES[3]);

            // Instantiate Coins
            allCoins[0] = new Coin(COINVALUES[0], coinDispenser10Yen, NUMCOINS[0]);
            allCoins[1] = new Coin(COINVALUES[1], coinDispenser50Yen, NUMCOINS[1]);
            allCoins[2] = new Coin(COINVALUES[2], coinDispenser100Yen, NUMCOINS[2]);
            allCoins[3] = new Coin(COINVALUES[3], coinDispenser500Yen, NUMCOINS[3]);

            // Instantiate Cans
            allCans[0] = new Can(canDispenser0, soldOutLight0, purchasableLight0, CANPRICES[0], NUMCANS[0]);
            allCans[1] = new Can(canDispenser1, soldOutLight1, purchasableLight1, CANPRICES[1], NUMCANS[1]);
            allCans[2] = new Can(canDispenser2, soldOutLight2, purchasableLight2, CANPRICES[2], NUMCANS[2]);
            allCans[3] = new Can(canDispenser3, soldOutLight3, purchasableLight3, CANPRICES[3], NUMCANS[3]);

            // Instantiate control class
            amountControl = new AmountController(allCoins, allCans, noChangeLight, amountDisplay);

            // You must replace the following default constructors with 
            // constructors with arguments (non-default constructors)
            // to pass (set) the first object that ButtonPressed() will
            // visit
            purchaseButton0 = new PurchaseButton(amountControl, 0);
            purchaseButton1 = new PurchaseButton(amountControl, 1);
            purchaseButton2 = new PurchaseButton(amountControl, 2);
            purchaseButton3 = new PurchaseButton(amountControl, 3);
           
            // You must replace the following default constructors with
            // constructors that take armuments to pass the first object that
            // the CoinInserted() will call
            coinInserter10Yen = new CoinInserter(amountControl, CurrencyTypes.TenYen);
            coinInserter50Yen = new CoinInserter(amountControl, CurrencyTypes.FiftyYen);
            coinInserter100Yen = new CoinInserter(amountControl, CurrencyTypes.OneHundredYen);
            coinInserter500Yen = new CoinInserter(amountControl, CurrencyTypes.FiveHundredYen);

            coinReturnButton = new CoinReturnButton(amountControl);

            // Display debug information
            displayCanPricesAndNames();
            updateDebugDisplays();
        }

 
        private void btnCoinInserter10Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter10Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter50Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter50Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter100Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter100Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter500Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter500Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnPurButtn0_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton0.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton1_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton1.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton2_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton2.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton3_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton3.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnCoinReturn_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinReturnButton.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnChangePickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            coinDispenser10Yen.Clear();
            coinDispenser50Yen.Clear();
            coinDispenser100Yen.Clear();
            coinDispenser500Yen.Clear();
        }

        private void btnCanPickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            canDispenser0.Clear(); // since all canDispenser objects accesses the
                                      // same textbox object
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            purchasableLight0.TurnOff();
            purchasableLight1.TurnOff();
            purchasableLight2.TurnOff();
            purchasableLight3.TurnOff();
            noChangeLight.TurnOff();
            soldOutLight0.TurnOff();
            soldOutLight1.TurnOff();
            soldOutLight2.TurnOff();
            soldOutLight3.TurnOff();
            txtCanDispenser.Text = "";
            amountDisplay.DisplayAmount(0);
            txtChange100Yen.Text = "";
            txtChange50Yen.Text = "";
            txtChange10Yen.Text = "";
            txtChange500Yen.Text = "";
            txtNum100Yen.Text = NUMCOINS[2].ToString();
            txtNum10Yen.Text = NUMCOINS[0].ToString();
            txtNum500Yen.Text = NUMCOINS[3].ToString();
            txtNum50Yen.Text = NUMCOINS[1].ToString();
            txtNumCan0.Text = NUMCANS[0].ToString();
            txtNumCan1.Text = NUMCANS[1].ToString();
            txtNumCan2.Text = NUMCANS[2].ToString();
            txtNumCan3.Text = NUMCANS[3].ToString();

            // Instantiate Coins
            allCoins[0] = new Coin(COINVALUES[0], coinDispenser10Yen, NUMCOINS[0]);
            allCoins[1] = new Coin(COINVALUES[1], coinDispenser50Yen, NUMCOINS[1]);
            allCoins[2] = new Coin(COINVALUES[2], coinDispenser100Yen, NUMCOINS[2]);
            allCoins[3] = new Coin(COINVALUES[3], coinDispenser500Yen, NUMCOINS[3]);

            // Instantiate Cans
            allCans[0] = new Can(canDispenser0, soldOutLight0, purchasableLight0, CANPRICES[0], NUMCANS[0]);
            allCans[1] = new Can(canDispenser1, soldOutLight1, purchasableLight1, CANPRICES[1], NUMCANS[1]);
            allCans[2] = new Can(canDispenser2, soldOutLight2, purchasableLight2, CANPRICES[2], NUMCANS[2]);
            allCans[3] = new Can(canDispenser3, soldOutLight3, purchasableLight3, CANPRICES[3], NUMCANS[3]);

            // Instantiate control class
            amountControl = new AmountController(allCoins, allCans, noChangeLight, amountDisplay);

        }

        private void displayCanPricesAndNames()
        {
            displayPrice0.Display("\\" + CANPRICES[0]);
            displayPrice1.Display("\\" + CANPRICES[1]);
            displayPrice2.Display("\\" + CANPRICES[2]);
            displayPrice3.Display("\\" + CANPRICES[3]);
            displayName0.Display(CANNAMES[0]);
            displayName1.Display(CANNAMES[1]); 
            displayName2.Display(CANNAMES[2]);
            displayName3.Display(CANNAMES[3]);
        }

        private void updateDebugDisplays()
        {
#if DEBUG
            displayNum10Yen.Display(allCoins[0].NumCoins);
            displayNum50Yen.Display(allCoins[1].NumCoins);
            displayNum100Yen.Display(allCoins[2].NumCoins);
            displayNum500Yen.Display(allCoins[3].NumCoins);
            displayNumCans0.Display(allCans[0].NumCans);
            displayNumCans1.Display(allCans[1].NumCans);
            displayNumCans2.Display(allCans[2].NumCans);
            displayNumCans3.Display(allCans[3].NumCans);
#endif
        }

    

        
    }
}