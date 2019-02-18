using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static gin_rummy.Cards.Card;
using static gin_rummy.Cards.SuitColourScheme;

namespace gin_rummy.ControlsHelpers
{
    public class ButtonCardDisplayer
    {
        private const double CardWidthRelativeToHeight = 0.66;

        private Dictionary<Suit, Color> _suitColourMap { get; set; }
        public SuitColourScheme SuitColourScheme { get; set; }

        public ButtonCardDisplayer(SuitColourScheme suitColourScheme)
        {
            SuitColourScheme = suitColourScheme;

            Dictionary<SuitColour, Color> colourMap = GetColourMapping(suitColourScheme);

            _suitColourMap = new Dictionary<Suit, Color>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                _suitColourMap.Add(suit, colourMap[suitColourScheme.GetColour(suit)]);
            }
        }

        private Dictionary<SuitColour, Color> GetColourMapping(SuitColourScheme suitColourScheme)
        {
            var mapping = new Dictionary<SuitColour, Color>();

            mapping.Add(SuitColour.Black, Color.LightSlateGray);
            mapping.Add(SuitColour.Blue, Color.LightSkyBlue);
            mapping.Add(SuitColour.Green, Color.LightGreen);
            mapping.Add(SuitColour.Red, Color.OrangeRed);

            return mapping;
        }

        public void SetButtonSize(Button button)
        {
            button.Height = button.Parent.Height;
            button.Width = (int)(button.Height * CardWidthRelativeToHeight);
        }

        public void DisplayCardFaceUp(Button button, Card card, int cardsUnderneath)
        {
            SetButtonSize(button);
            button.BackColor = _suitColourMap[card.SuitValue];
            button.Font = new Font("Arial", 16, FontStyle.Bold);
            button.Text = card.ToString();
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        public void DisplayCardFaceDown(Button button, int cardsUnderneath)
        {
            SetButtonSize(button);
            button.BackColor = Color.DarkSlateGray; // TODO: variablise back-of-card display
            button.Text = "";
            //button.BorderThickness = 
        }

        public void DisplayNothing(Button button)
        {
            SetButtonSize(button);
            button.Text = "";
            button.BackColor = Color.LightGray; // TODO: variablise nothing display
        }
    }
}
