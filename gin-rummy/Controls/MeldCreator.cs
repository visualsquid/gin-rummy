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

namespace gin_rummy.Controls
{
    /// <summary>
    /// User control for helping a player create melds from a hand.
    /// </summary>
    public partial class MeldCreator : UserControl
    {
        private ButtonCardDisplayer _displayer;
        private Hand _hand;
        private Dictionary<Button, CardPanel> _clearButtonMappings;

        public MeldCreator(Hand hand, SuitColourScheme suitColourScheme)
        {
            InitializeComponent();
            InitialiseClearButtonMappings(_clearButtonMappings);
            _displayer = new ButtonCardDisplayer(suitColourScheme);
            _hand = hand;
            InitialiseHandPanel(pHand);
            InitialiseMeldPanel(pMeldOne);
            InitialiseMeldPanel(pMeldTwo);
            InitialiseMeldPanel(pMeldThree);
            InitialiseMeldPanel(pMeldFour);
            DisplayHand(_hand);
        }

        private void InitialiseClearButtonMappings(Dictionary<Button, CardPanel> mappings)
        {
            mappings = new Dictionary<Button, CardPanel>();
            mappings.Add(bClearMeldOne, pMeldOne);
            mappings.Add(bClearMeldTwo, pMeldTwo);
            mappings.Add(bClearMeldThree, pMeldThree);
            mappings.Add(bClearMeldFour, pMeldFour);
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
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = false;
            p.AllowDrop = true;
        }

        private void InitialiseMeldPanel(CardPanel p)
        {
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = true;
            p.CardSelected += MeldPanelCardSelected;
            p.AllowDrop = true;
            // TODO: figure out drag and drop between panels
        }

        private void MeldPanelCardSelected(Card card, out bool removeCard)
        {
            removeCard = true;
            AddCardToHandPanel(card, pHand);
        }

        private void bClearMeldAny_Click(object sender, EventArgs e)
        {
            _clearButtonMappings[(sender as Button)].Clear();
        }
    }
}
