using gin_rummy.Actors;
using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.GameStructures
{
    /// <summary>
    /// Model class for storing the information assocaited with a game. Generally should only be manipulated by an instance of GameMaster.
    /// </summary>
    public class Game
    {
        public const int InitialHandSize = 10;
        public const int MinimumDeadWoodForKnock = 10;

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        private Deck _deck;
        private Table _table;

        public Game(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            _deck = new Deck(Deck.DeckType.Standard);
            _table = new Table();
        }

        public void ShuffleDeck()
        {
            _deck.Shuffle();
        }

        public void DealHand(Player player, int cardCount)
        {
            player.ClearHand();
            foreach (Card card in _deck.RemoveTop(cardCount))
            {
                player.DrawCard(card);
            }
        }

        public void CreateStacks()
        {
            _table.ClearTable();
            foreach (Card card in _deck.RemoveAll())
            {
                _table.PlaceDiscard(card);
            }
            _table.RestockFromDiscard();
            _table.PlaceDiscard(_table.DrawStock());
        }

        public void RestockFromDiscard()
        {
            _table.RestockFromDiscard();
        }

        public Card DrawDiscard()
        {
            return _table.DrawDiscard();
        }

        public Card DrawStock()
        {
            return _table.DrawStock();
        }

        public void PlaceDiscard(Card card)
        {
            _table.PlaceDiscard(card);
        }

        public int GetDiscardCount()
        {
            return _table.DiscardCount;
        }

        public int GetStockCount()
        {
            return _table.StockCount;
        }

        public Card GetVisibleDiscard()
        {
            return _table.PeekDiscard();
        }

    }
}
