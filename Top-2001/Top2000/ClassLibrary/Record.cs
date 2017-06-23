using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Record
    {
        /// <summary>
        /// The position
        /// </summary>
        private int position;
        /// <summary>
        /// The title
        /// </summary>
        private string title;
        /// <summary>
        /// The artist
        /// </summary>
        private string artist;
        /// <summary>
        /// The year
        /// </summary>
        private int year;

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="title">The title.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="year">The year.</param>
        public Record(int position, string title, string artist, int year)
        {
            this.position = position;
            this.title = title;
            this.artist = artist;
            this.year = year;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
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

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>
        /// The artist.
        /// </value>
        public string Artist
        {
            get
            {
                return artist;
            }

            set
            {
                artist = value;
            }
        }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
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
    }
}
