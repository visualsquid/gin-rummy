using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gin_rummy.ControlsHelpers;
using gin_rummy.Cards;
using gin_rummy.Actors;

namespace gin_rummy.Controls
{
    /// <summary>
    /// User control for helping a player create melds from a hand.
    /// </summary>
    public partial class MeldCreator : UserControl
    {

        private class MeldValidityDisplay
        {
            public Color CheckboxColour { get; set; }
            public CheckState CheckboxState { get; set; }
        }

        public delegate void UserAcceptsSelectionHandler();

        private Hand _hand;
        private Dictionary<Button, CardPanel> _clearButtonMappings;
        private Dictionary<CardPanel, CheckBox> _meldValidCheckBoxMappings;
        private MeldChecker _meldChecker;
        private MeldValidityDisplay _meldDisplayValid;
        private MeldValidityDisplay _meldDisplayInvalid;
        private MeldValidityDisplay _meldDisplayEmpty;

        public UserAcceptsSelectionHandler OnUserAcceptsSelection { get; set; }
        public SuitColourScheme SuitColourScheme { get; set; }

        public MeldCreator(Hand hand, SuitColourScheme suitColourScheme)
        {
            InitializeComponent();
            InitialiseClearButtonMappings(out _clearButtonMappings);
            InitialiseMeldValidCheckboxMappings(out _meldValidCheckBoxMappings);
            _hand = hand;
            _meldChecker = new MeldChecker();
            SuitColourScheme = suitColourScheme;
            InitialiseMeldValidityDisplays();
            InitialiseHandPanel(pHand);
            InitialiseMeldPanel(pMeldOne);
            InitialiseMeldPanel(pMeldTwo);
            InitialiseMeldPanel(pMeldThree);
            InitialiseMeldPanel(pMeldFour);
            DisplayHand(_hand);
        }

        private void InitialiseMeldValidityDisplays()
        {
            _meldDisplayValid= new MeldValidityDisplay() { CheckboxColour = Color.Green, CheckboxState = CheckState.Checked };
            _meldDisplayInvalid= new MeldValidityDisplay() { CheckboxColour = Color.Red, CheckboxState = CheckState.Unchecked };
            _meldDisplayEmpty= new MeldValidityDisplay() { CheckboxColour = Color.SlateGray, CheckboxState = CheckState.Indeterminate };
        }

        public List<Meld> GetMelds()
        {
            List<Meld> melds = new List<Meld>();

            foreach (CardPanel cardPanel in new CardPanel[] {pMeldOne, pMeldTwo, pMeldThree, pMeldFour}) 
            {
                Meld meld = new Meld();
                foreach(Card card in cardPanel.GetCards())
                {
                    meld.AddCard(card);
                }
            }

            return melds;
        }

        private void InitialiseClearButtonMappings(out Dictionary<Button, CardPanel> mappings)
        {
            mappings = new Dictionary<Button, CardPanel>();
            mappings.Add(bClearMeldOne, pMeldOne);
            mappings.Add(bClearMeldTwo, pMeldTwo);
            mappings.Add(bClearMeldThree, pMeldThree);
            mappings.Add(bClearMeldFour, pMeldFour);
        }

        private void InitialiseMeldValidCheckboxMappings(out Dictionary<CardPanel, CheckBox> mappings)
        {
            mappings = new Dictionary<CardPanel, CheckBox>();
            mappings.Add(pMeldOne, cbMeldOneValid);
            mappings.Add(pMeldTwo, cbMeldTwoValid);
            mappings.Add(pMeldThree, cbMeldThreeValid);
            mappings.Add(pMeldFour, cbMeldFourValid);
        }

        private void DisplayHand(Hand hand)
        {
            foreach(Card c in hand.ViewHand())
            {
                pHand.AddCard(c);
            }
        }

        private void AddCardToHandPanel(Card c, CardPanel p)
        {
            p.AddCard(c);
        }

        private void RemoveCardFromHandPanel(Card c, CardPanel p)
        {
            p.RemoveCard(c);
        }

        private void AddCardToMeldPanel(Card c, CardPanel p)
        {
            p.AddCard(c);
        }

        private void RemoveCardFromMeldPanel(Card c, CardPanel p)
        {
            p.RemoveCard(c);
        }

        private void ClearMeldPanel(CardPanel p)
        {
            p.Clear();
        }

        private void InitialiseHandPanel(CardPanel p)
        {
            p.ColourScheme = SuitColourScheme;
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = false;
            p.AllowDragFrom = true;
            p.AllowDragTo = true;
        }

        private void InitialiseMeldPanel(CardPanel p)
        {
            p.ColourScheme = SuitColourScheme;
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = true;
            p.CardSelected += MeldPanelCardSelected;
            p.CardAdded += MeldPanelCardAdded;
            p.CardRemoved += MeldPanelCardRemoved;
            p.AllowDragFrom = true;
            p.AllowDragTo = true;
            DisplayMeldValidity(p, false, true);
        }

        private void MeldPanelCardAdded(CardPanel sender, Card card)
        {
            ValidateMeld(sender);
        }

        private void MeldPanelCardRemoved(CardPanel sender, Card card)
        {
            ValidateMeld(sender);
        }

        private void MeldPanelCardSelected(Card card, out bool removeCard)
        {
            removeCard = true;
            AddCardToHandPanel(card, pHand);
        }

        private void ValidateMeld(CardPanel cardPanel)
        {
            Meld meld = new Meld();
            meld.AddCards(cardPanel.GetCards());

            bool isMeldValid = _meldChecker.IsValid(meld);
            bool isMeldEmpty = meld.GetListOfCardsInMeld().Count == 0;

            DisplayMeldValidity(cardPanel, isMeldValid, isMeldEmpty);
        }

        private void DisplayMeldValidity(CardPanel cardPanel, bool isMeldValid, bool isMeldEmpty)
        {
            MeldValidityDisplay display;

            if (isMeldEmpty)
            {
                display = _meldDisplayEmpty;
            }
            else 
            {
                display = isMeldValid ? _meldDisplayValid : _meldDisplayInvalid;
            }

            var relevantCheckBox = _meldValidCheckBoxMappings[cardPanel];
            relevantCheckBox.BackColor = display.CheckboxColour;
            relevantCheckBox.CheckState = display.CheckboxState;
        }

        private void bClearMeldAny_Click(object sender, EventArgs e)
        {
            CardPanel panel = _clearButtonMappings[(sender as Button)];
            foreach(Card card in panel.GetCards())
            {
                pHand.AddCard(card);
            }
            panel.Clear();
        }

        private void bAcceptMelds_Click(object sender, EventArgs e)
        {
            if (OnUserAcceptsSelection != null)
            {
                OnUserAcceptsSelection();
            }
        }
    }
}
