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
using static gin_rummy.Cards.SuitColourScheme;
using gin_rummy.ControlsHelpers;
using static gin_rummy.Cards.Card;

namespace gin_rummy.Controls
{
    /// <summary>
    ///   GUI class for displaying a hand to the user that can be manipulated.
    /// </summary>
    public partial class CardPanel : UserControl
    {

        public class CardPanelCard
        {
            public Control DisplayedControl { get; set; }
            public Card Card { get; set; }
            public CardPanel OwnerPanel { get; set; }

            public CardPanelCard(Control displayedControl, Card card, CardPanel cardPanel)
            {
                DisplayedControl = displayedControl;
                Card = card;
                OwnerPanel = cardPanel;
            }
        }

        private List<CardPanelCard> _cards;
        private ButtonCardDisplayer _cardDisplayer;
        private SuitColourScheme _suitColourScheme;

        private bool _allowDragTo;

        public delegate void OnCardSelected(Card card, out bool removeCard);
        public delegate void OnCardAdded(CardPanel sender, Card card);
        public delegate void OnCardRemoved(CardPanel sender, Card card);

        public OnCardSelected CardSelected { get; set; }
        public OnCardAdded CardAdded { get; set; }
        public OnCardRemoved CardRemoved { get; set; }
        public Card LastHighlightedCard { get; set; }
        public SuitColourScheme ColourScheme
        {
            get
            {
                return _suitColourScheme;
            }
            set
            {
                _suitColourScheme = value;
                _cardDisplayer = new ButtonCardDisplayer(value);
            }
        }
        public bool ShowCards { get; set; } // TODO: when SET, event handlers, etc. need changing
        public bool AllowSelection { get; set; } // TODO: when SET, event handlers, etc. need changing
        public bool AllowReordering { get; set; } // TODO: when SET, event handlers, etc. need changing
        public bool AllowDragFrom { get; set; } // TODO: when SET, event handlers, etc. need changing
        public bool AllowDragTo
        {
            get
            {
                return _allowDragTo;
            }
            set
            {
                _allowDragTo = value;
                pCards.AllowDrop = value;

            }
        } // TODO: when SET, event handlers, etc. need changing

        public CardPanel()
        {
            InitializeComponent();
            _cards = new List<CardPanelCard>();
            InitialiseDefaultProperties();
        }

        public void Clear()
        {
            List<CardPanelCard> copyOfCardPanelCards = new List<CardPanelCard>();
            copyOfCardPanelCards.AddRange(_cards);

            foreach(CardPanelCard cardPanelCard in copyOfCardPanelCards)
            {
                RemoveCard(cardPanelCard.Card);
            }
        }

        public List<Card> GetCards()
        {
            List<Card> cards = new List<Card>();

            foreach(CardPanelCard cardPanelCard in _cards)
            {
                cards.Add(new Card(cardPanelCard.Card.ToString()));
            }

            return cards;
        }

        private void InitialiseDefaultProperties()
        {
            ColourScheme = new TwoColourScheme();
            ShowCards = false;
            AllowSelection = false;
            AllowReordering = false;
            AllowDragFrom = false;
            AllowDragTo = false;
        }

        private Card GetCardByDisplayedControl(Control displayedControl)
        {
            return _cards.First(i => i.DisplayedControl == displayedControl).Card; // We're checking that the control is actually the same instance, so there shouldn't be any duplicates
        }

        private Control GetDisplayedControlByCard(Card card)
        {
            return _cards.First(i => i.Card == card).DisplayedControl; // The Card equality operator is overridden, so in theory there could be duplicates - we're assuming there aren't, as the game logic doesn't support it
        }

        private CardPanelCard GetCardPanelCardByCard(Card card)
        {
            return _cards.First(i => i.Card == card);
        }

        private CardPanelCard GetCardPanelCardByDisplayedControl(Control displayedControl)
        {
            return _cards.First(i => i.DisplayedControl == displayedControl);
        }

        private void RemoveCardPanelCardByCard(Card card)
        {
            _cards.RemoveAll(i => i.Card == card); // Again, we're assuming there aren't any duplicates, which there shouldn't be by the game logic
        }

        public void AddCard(Card c)
        {
            InsertCard(c, _cards.Count);
        }

        public void InsertCard(Card c, int i)
        {
            Button button;

            InsertCardInDisplay(c, i, ShowCards, out button);

            _cards.Insert(i, new CardPanelCard(button, c, this));

            if (CardAdded != null)
            {
                CardAdded(this, c);
            }
        }

        public void RemoveCard(Card c)
        {
            _cards.RemoveAll(i => i.Card == c);

            string cardIdentifier = c.ToString();
            for (int i = 0; i < pCards.Controls.Count; i++)
            {
                if (pCards.Controls[i] is Button && (pCards.Controls[i] as Button).Text == cardIdentifier)
                {
                    pCards.Controls.RemoveAt(i);
                    break;
                }
            }

            if (CardRemoved != null)
            {
                CardRemoved(this, c);
            }
        }

        public Card GetSelectedCard()
        {
            return LastHighlightedCard; // TODO: what if a button has never been focused yet? what about resetting in between?
        }

        private void AddCardToDisplay(Card card, bool showCards, out Button newButton)
        {
            InsertCardInDisplay(card, 0, showCards, out newButton);
        }

        private void InsertCardInDisplay(Card card, int insertPosition, bool showCards, out Button newButton)
        {

            newButton = new Button();
            pCards.Controls.Add(newButton);
            pCards.Controls.SetChildIndex(newButton, insertPosition);
            if (showCards)
            {
                _cardDisplayer.DisplayCardFaceUp(newButton, card, 0);
            }
            else
            {
                _cardDisplayer.DisplayCardFaceDown(newButton, 0);
            }

            if (AllowReordering || AllowDragTo)
            {
                pCards.AllowDrop = true;
                newButton.AllowDrop = true;
                newButton.DragDrop += CardButtonDragDrop;
                newButton.DragOver += CardButtonDragOver;
            }

            if (AllowReordering || AllowDragFrom)
            {
                newButton.MouseDown += CardButtonMouseDown;
            }

            if (AllowSelection)
            {
                newButton.MouseUp += CardPanelMouseUp;
                newButton.GotFocus += CardPanelButtonGotFocus;
            }

            if (AllowSelection || AllowReordering)
            {
                newButton.PreviewKeyDown += CardPanelPreviewKeyDown;
                newButton.KeyDown += CardPanelKeyDown;
            }
        }

        private void CardPanelPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                    e.IsInputKey = true;
                    break;
                default:
                    e.IsInputKey = false;
                    break;
            }
        }

        private void CardPanelKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                HandleCardLeftRightKeyDown(sender as Button, e);
            }

            if (e.KeyCode == Keys.Space)
            {
                HandleCardSpaceKeyDown(sender as Button, e);
            }
        }

        private void HandleCardLeftRightKeyDown(Button sender, KeyEventArgs e)
        {
            int buttonIndex = pCards.Controls.IndexOf(sender);
            Button nextButton = null;
            if (e.KeyCode == Keys.Left && buttonIndex > 0)
            {
                nextButton = (pCards.Controls[buttonIndex - 1] as Button);
            }
            else if (e.KeyCode == Keys.Right && buttonIndex < pCards.Controls.Count - 1)
            {
                nextButton = (pCards.Controls[buttonIndex + 1] as Button);
            }

            if (nextButton != null)
            {
                if (e.Control)
                {
                    SwapCards(pCards, sender, nextButton);
                }
                else
                {
                    nextButton.Focus();
                }
                e.Handled = true;
            }
        }

        private void HandleCardSpaceKeyDown(Button sender, KeyEventArgs e)
        {
            ProcessCardSelection(sender);
            e.Handled = true;
        }

        private void CardPanelButtonGotFocus(object sender, EventArgs e)
        {
            LastHighlightedCard = new Card((sender as Button).Text);
        }

        private void CardPanelMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            ProcessCardSelection(sender as Button);
        }

        private void ProcessCardSelection(Button button)
        {
            if (!AllowSelection || CardSelected == null)
            {
                return;
            }
            
            string cardIdentifier = button.Text;
            Card card = new Card(cardIdentifier);
            bool removeCard;
            CardSelected(card, out removeCard);
            if (removeCard)
            {
                // TODO: we're removing in RemoveCard(Card) as well...
                _cards.RemoveAt(pCards.Controls.IndexOf(button));
                pCards.Controls.Remove(button);
            }
        }

        private void CardButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (!(AllowReordering || AllowDragFrom) || e.Button != MouseButtons.Left)
            {
                return;
            }

            Button b = (sender as Button);
            b.DoDragDrop(GetCardPanelCardByDisplayedControl(b), DragDropEffects.Move);
        }

        private void CardButtonDragDrop(object sender, DragEventArgs e)
        {
            object droppedData = e.Data.GetData(typeof(CardPanelCard));
            if (!((droppedData is CardPanelCard) && (sender is Button)))
            {
                return;
            }

            CardPanelCard droppedCardPanelCard = (droppedData as CardPanelCard);
            Button droppedButton = (droppedCardPanelCard.DisplayedControl as Button);
            Card droppedCard = droppedCardPanelCard.Card;
            Button recipientButton = (sender as Button);
            Card recipientCard = new Card(recipientButton.Text);

            if (droppedButton == recipientButton)
            {
                return;
            }

            if (droppedCardPanelCard.OwnerPanel == this)
            {
                SwapCards(pCards, droppedButton, recipientButton);
            }
            else
            {
                int dropIndex = pCards.Controls.IndexOf(recipientButton);
                InsertCard(droppedCard, dropIndex);
                droppedCardPanelCard.OwnerPanel.RemoveCard(droppedCard);
            }
        }

        private void CardButtonDragOver(object sender, DragEventArgs e)
        {
            if (AllowReordering || AllowDragTo)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            
        }

        private void SwapCards(Panel p, Button x, Button y)
        {
            if (x == y)
            {
                return;
            }

            int indexOfX = p.Controls.IndexOf(x);
            int indexOfY = p.Controls.IndexOf(y);

            Card cardX = GetCardByDisplayedControl(x);
            Card cardY = GetCardByDisplayedControl(y);
            CardPanelCard cardPanelCardY = GetCardPanelCardByCard(cardY);
            CardPanelCard cardPanelCardX = GetCardPanelCardByCard(cardX);

            if (indexOfX < indexOfY)
            {
                p.Controls.SetChildIndex(x, indexOfY);
                p.Controls.SetChildIndex(y, indexOfX);
                _cards.Remove(cardPanelCardY);
                _cards.Insert(indexOfY, cardPanelCardX);
                _cards.Remove(cardPanelCardX);
                _cards.Insert(indexOfX, cardPanelCardY);
            }
            else
            {
                p.Controls.SetChildIndex(y, indexOfX);
                p.Controls.SetChildIndex(x, indexOfY);
                _cards.Remove(cardPanelCardX);
                _cards.Insert(indexOfX, cardPanelCardY);
                _cards.Remove(cardPanelCardY);
                _cards.Insert(indexOfY, cardPanelCardX);
            }

        }

        private void pCards_DragOver(object sender, DragEventArgs e)
        {
            if (AllowReordering || AllowDragTo)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pCards_DragDrop(object sender, DragEventArgs e)
        {
            object droppedData = e.Data.GetData(typeof(CardPanelCard));
            if (!((droppedData is CardPanelCard) && (sender is Panel)))
            {
                return;
            }

            CardPanelCard droppedCardPanelCard = (droppedData as CardPanelCard);
            Button droppedButton = (droppedCardPanelCard.DisplayedControl as Button);
            Card droppedCard = droppedCardPanelCard.Card;

            droppedCardPanelCard.OwnerPanel.RemoveCard(droppedCard);
            int dropIndex = GetIndexToInsertCardFromScreenPoint(pCards, e.X, e.Y);
            InsertCard(droppedCard, dropIndex);
        }

        private int GetIndexToInsertCardFromScreenPoint(Panel panel, int xPosition, int yPosition)
        {
            Point panelPoint = panel.PointToClient(new Point(xPosition, yPosition));

            int index = 0;
            foreach(Control c in panel.Controls)
            {
                int currentControlXPosition = c.Left + c.Width;
                if (currentControlXPosition >= panelPoint.X)
                {
                    return index == 0 ? index : index - 1;
                }

                index++;
            }

            return index;
        }
    }
}
