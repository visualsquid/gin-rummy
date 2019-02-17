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
        private List<Card> _cards;
        private ButtonCardDisplayer _cardDisplayer;
        private SuitColourScheme _suitColourScheme;

        public delegate void OnCardSelected(Card card, out bool removeCard);

        public OnCardSelected CardSelected { get; set; }
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
        public bool ShowCards { get; set; }
        public bool AllowSelection { get; set; }
        public bool AllowReordering { get; set; }

        public CardPanel()
        {
            InitializeComponent();
            _cards = new List<Card>();
            InitialiseDefaultProperties();
        }

        public void Clear()
        {
            pCards.Controls.Clear();
            _cards.Clear();
        }

        private void InitialiseDefaultProperties()
        {
            ColourScheme = new TwoColourScheme();
            ShowCards = false;
            AllowSelection = false;
            AllowReordering = false;
        }

        public void AddCard(Card c)
        {
            _cards.Add(c);
            AddCardToDisplay(c, ShowCards);
        }

        private void AddCardToDisplay(Card card, bool showCards)
        {

            Button button = new Button();
            button.Parent = pCards;
            if (showCards)
            {
                _cardDisplayer.DisplayCardFaceUp(button, card, 0);
            }
            else
            {
                _cardDisplayer.DisplayCardFaceDown(button, 0);
            }

            if (AllowReordering)
            {
                button.MouseDown += CardPanelMouseDown;
                button.AllowDrop = true;
                button.DragDrop += CardPanelDragDrop;
                button.DragOver += CardPanelDragOver;
            }

            if (AllowSelection)
            {
                button.MouseUp += CardPanelMouseUp;
            }

            if (AllowSelection || AllowReordering)
            {
                button.PreviewKeyDown += CardPanelPreviewKeyDown;
                button.KeyDown += CardPanelKeyDown;
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
                _cards.RemoveAt(pCards.Controls.IndexOf(button));
                pCards.Controls.Remove(button);
            }
        }

        private void CardPanelMouseDown(object sender, MouseEventArgs e)
        {
            if (!AllowReordering || e.Button != MouseButtons.Left)
            {
                return;
            }

            Button b = (sender as Button);
            b.DoDragDrop(b, DragDropEffects.Move);
        }

        private void CardPanelDragDrop(object sender, DragEventArgs e)
        {
            Button dragged = (e.Data.GetData(typeof(Button)) as Button);
            Button draggedTo = (sender as Button);
            if (dragged != draggedTo)
            {
                SwapCards(pCards, dragged, draggedTo);
            }
        }

        private void CardPanelDragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void SwapCards(Panel p, Button x, Button y)
        {
            if (x == y)
            {
                return;
            }

            int indexOfX = p.Controls.IndexOf(x);
            int indexOfY = p.Controls.IndexOf(y);

            Card cardX = _cards[indexOfX];
            Card cardY = _cards[indexOfY];

            if (indexOfX < indexOfY)
            {
                p.Controls.SetChildIndex(x, indexOfY);
                p.Controls.SetChildIndex(y, indexOfX);
                _cards.Remove(cardY);
                _cards.Insert(indexOfY, cardX);
                _cards.Remove(cardX);
                _cards.Insert(indexOfX, cardY);
            }
            else
            {
                p.Controls.SetChildIndex(y, indexOfX);
                p.Controls.SetChildIndex(x, indexOfY);
                _cards.Remove(cardX);
                _cards.Insert(indexOfX, cardY);
                _cards.Remove(cardY);
                _cards.Insert(indexOfY, cardX);
            }

        }
    }
}
