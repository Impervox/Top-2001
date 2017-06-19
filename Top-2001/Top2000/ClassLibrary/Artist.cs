using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Artist
    {
        private string name;
        private string biography;
        private string url;
        private int artistId;
        

        public Artist(string name, int artistId, string biography = null, string url = null)
        {
            this.name = name;
            this.biography = biography;
            this.url = url;
            this.artistId = artistId;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Biography
        {
            get
            {
                return biography;
            }

            set
            {
                biography = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public int ArtistId
        {
            get
            {
                return artistId;
            }
        }
    }
}
