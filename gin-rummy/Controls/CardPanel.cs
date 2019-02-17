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

namespace gin_rummy.Controls
{
    /// <summary>
    ///   GUI class for displaying a hand to the user that can be manipulated.
    /// </summary>
    public partial class CardPanel : UserControl
    {
        private const double CardWidthRelativeToHeight = 0.66;

        private List<Card> _cards;

        public delegate void OnCardSelected(Card card, out bool removeCard);

        public OnCardSelected CardSelected { get; set; }
        public SuitColourScheme ColourScheme { get; set; }
        public Dictionary<SuitColour, Color> ColourMap { get; set; }
        public bool ShowCards { get; set; }
        public bool AllowSelection { get; set; }
        public bool AllowReordering { get; set; }

        public CardPanel()
        {
            InitializeComponent();
            _cards = new List<Card>();
            InitialiseDefaultProperties();
        }

        private void InitialiseDefaultProperties()
        {
            ColourScheme = new TwoColourScheme();
            ColourMap = new Dictionary<SuitColour, Color>();
            ColourMap.Add(SuitColour.Black, Color.LightSlateGray);
            ColourMap.Add(SuitColour.Red, Color.OrangeRed);
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
            button.Height = button.Parent.Height;
            button.Width = (int)(button.Height * CardWidthRelativeToHeight);

            if (showCards)
            {
                SetButtonAsFrontOfCard(button, card);
            }
            else
            {
                SetButtonAsBackOfCard(button);
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
        }

        private void SetButtonAsFrontOfCard(Button button, Card card)
        {
            button.BackColor = ColourMap[ColourScheme.GetColour(card.SuitValue)];
            button.Font = new Font("Arial", 16, FontStyle.Bold);
            button.Text = card.ToString();
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void SetButtonAsBackOfCard(Button button)
        {
            button.BackColor = Color.DarkSlateGray; // TODO: variablise back-of-card display
            button.Text = "";
        }

        private void CardPanelMouseUp(object sender, MouseEventArgs e)
        {
            if (!AllowSelection || e.Button != MouseButtons.Right || CardSelected == null)
            {
                return;
            }

            Button clickedButton = (sender as Button);
            string cardIdentifier = clickedButton.Text;
            Card card = new Card(cardIdentifier);
            bool removeCard;
            CardSelected(card, out removeCard);
            if (removeCard)
            {
                _cards.RemoveAt(pCards.Controls.IndexOf(clickedButton));
                pCards.Controls.Remove(clickedButton);
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
