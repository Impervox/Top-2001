using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Song
    {
        private string title;
        private string lyrics;
        private int year;
        private byte[] intro;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Lyrics
        {
            get
            {
                return lyrics;
            }

            set
            {
                lyrics = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public byte[] Intro
        {
            get
            {
                return intro;
            }

            set
            {
                intro = value;
            }
        }

        public Song(string title, int year, string lyrics = null, byte[] intro = null)
        {
            this.title = title;
            this.lyrics = lyrics;
            this.year = year;
            this.intro = intro;
        }
    }
}
