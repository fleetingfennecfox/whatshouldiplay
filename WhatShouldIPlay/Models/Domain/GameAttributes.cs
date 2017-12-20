using System.Collections.Generic;

namespace WhatShouldIPlay.Models.Domain
{
    public class GameAttributes
    {
        private List<Platforms> _platforms = new List<Platforms>();
        public List<Platforms> Platforms
        {
            get { return this._platforms; }
            set { this._platforms = value; }
        }

        private List<Genres> _genres = new List<Genres>();
        public List<Genres> Genres
        {
            get { return this._genres; }
            set { this._genres = value; }
        }

        private List<Studios> _studios = new List<Studios>();
        public List<Studios> Studios
        {
            get { return this._studios; }
            set { this._studios = value; }
        }

        private List<Directors> _directors = new List<Directors>();
        public List<Directors> Directors
        {
            get { return this._directors; }
            set { this._directors = value; }
        }
    }
}