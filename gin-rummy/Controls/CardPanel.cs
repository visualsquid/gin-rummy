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
        public delegate void OnCardSelected(Card card, out bool removeCard);

        public OnCardSelected CardSelected { get; set; }
        public SuitColourScheme ColourScheme { get; set; }
        public Dictionary<SuitColour, Color> ColourMap { get; set; }

        public CardPanel()
        {
            InitializeComponent();
            InitialiseDefaultProperties();
        }

        private void InitialiseDefaultProperties()
        {
            ColourScheme = new TwoColourScheme();
            ColourMap = new Dictionary<SuitColour, Color>();
            ColourMap.Add(SuitColour.Black, Color.LightSlateGray);
            ColourMap.Add(SuitColour.Red, Color.OrangeRed);
        }

        public void AddCard(Card c)
        {
            Button b = new Button();
            b.Parent = pCards;
            b.Height = b.Parent.Height;
            b.Width = b.Height;
            b.BackColor = ColourMap[ColourScheme.GetColour(c.SuitValue)];
            b.Font = new Font("Arial", 16, FontStyle.Bold);
            b.Text = c.ToString();
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.MouseDown += CardPanelMouseDown;
            b.MouseUp += CardPanelMouseUp;
            b.AllowDrop = true;
            b.DragDrop += CardPanelDragDrop;
            b.DragOver += CardPanelDragOver;
            
        }

        private void CardPanelMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || CardSelected == null)
            {
                return;
            }

            string cardIdentifier = (sender as Button).Text;
            Card card = new Card(cardIdentifier);
            bool removeCard;
            CardSelected(card, out removeCard);
            if (removeCard)
            {
                pCards.Controls.Remove(sender as Control);
            }
        }

        private void CardPanelMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
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
            int indexOfX = p.Controls.IndexOf(x);
            int indexOfY = p.Controls.IndexOf(y);

            if (indexOfX < indexOfY)
            {
                p.Controls.SetChildIndex(x, indexOfY);
                p.Controls.SetChildIndex(y, indexOfX);
            }
            else
            {
                p.Controls.SetChildIndex(y, indexOfX);
                p.Controls.SetChildIndex(x, indexOfY);
            }



        }
    }
}
