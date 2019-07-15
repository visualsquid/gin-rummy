using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gin_rummy.Cards;
using gin_rummy.ControlsHelpers;

namespace gin_rummy.Controls
{
    /// <summary>
    /// GUI control for displaying the discard and stock piles. This class is concerned with the visuals only i.e. it does not store a full
    /// stack of cards, rather it uses the number of cards in each stack plus the top visible card to draw.
    /// </summary>
    public partial class CardStacks : UserControl
    {

        private ButtonCardDisplayer _cardDisplayer;

        private int _stockCount;
        private int _discardCount;
        private Card _visibleDiscard;

        private Button _stockDisplay;
        private Button _discardDisplay;


        public delegate void OnTakeDiscard();
        public delegate void OnDrawStock();

        public SuitColourScheme SuitColourScheme { get; set; }
        public bool AllowTakeDiscard { get; set; }
        public OnTakeDiscard DiscardTaken { get; set; }
        public bool AllowDrawStock { get; set; }
        public OnDrawStock StockDrawn { get; set; }
        public int StockCount
        {
            get
            {
                return _stockCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception($"Stock count must be greater than or equal to zero: value of {value} is invalid.");
                }
                _stockCount = value;
                DisplayStock(value);
            }
        }
        public int DiscardCount
        {
            get
            {
                return _discardCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception($"Discard count must be greater than or equal to zero: value of {value} is invalid.");
                }
                _discardCount = value;
                DisplayVisibleDiscard(_visibleDiscard, value);
            }
        }
        public Card VisibleDiscard
        {
            get
            {
                return _visibleDiscard;
            }
            set
            {
                _visibleDiscard = value;
                DisplayVisibleDiscard(value, _discardCount);
            }
        }

        public CardStacks()
        {
            InitializeComponent();
            InitialiseDefaultPropertyValues();
            InitialiseStacksDisplay();
        }

        private void InitialiseStacksDisplay()
        {
            _stockDisplay = new Button();
            _stockDisplay.Click += StockClick;
            
            _discardDisplay = new Button();
            _discardDisplay.Click += DiscardClick;

            pStacks.Controls.Add(_discardDisplay);
            pStacks.Controls.Add(_stockDisplay);

            _visibleDiscard = null;
            _stockCount = 0;
        }

        private void StockClick(object sender, EventArgs e)
        {
            if (AllowDrawStock)
            {
                StockDrawn?.Invoke();
            }
        }

        private void DiscardClick(object sender, EventArgs e)
        {
            if (AllowTakeDiscard)
            {
                DiscardTaken?.Invoke();
            }
        }

        private void InitialiseDefaultPropertyValues()
        {
            SuitColourScheme = new TwoColourScheme();
            _cardDisplayer = new ButtonCardDisplayer(SuitColourScheme);
        }

        private void DisplayStock(int stockCount)
        {
            if (stockCount == 0)
            {
                _cardDisplayer.DisplayNothing(_stockDisplay);
            }
            else
            {
                _cardDisplayer.DisplayCardFaceDown(_stockDisplay, stockCount);
            }
        }

        private void DisplayVisibleDiscard(Card card, int discardCount)
        {
            if (discardCount == 0 || card == null)
            {
                _cardDisplayer.DisplayNothing(_discardDisplay);
            }
            else
            {
                _cardDisplayer.DisplayCardFaceUp(_discardDisplay, card, discardCount);
            }
        }

        
    }
}
