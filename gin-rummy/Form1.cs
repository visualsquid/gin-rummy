using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gin_rummy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bNewHand_Click(object sender, EventArgs e)
        {
            Deck deck = new Deck();
            deck.Shuffle();
            Hand hand = new Hand();
            while (hand.Size < 10)
            {
                hand.AddCard(deck.RemoveTop());
            }
            eYourHand.Text = hand.ToString();
        }
    }
}
