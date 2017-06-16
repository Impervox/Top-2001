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
        private SqlConnection conn;
        private string name;
        private string biography;
        private string url;
        private string picture;
        

        public Artist(string name, string biography = null, string url = null, string picture = null)
        {
            conn = DataProvider.conn;
            this.name = name;
            this.biography = biography;
            this.url = url;
            this.picture = picture;
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

        public string Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
            }
        }

        public void AddArtist(string artistName, string artistBiography = null, string artistUrl = null, object artistPicture = null)
        {
            //do send name to artist table as new row artist.
            List<int> list = new List<int>();
            SqlCommand cmd = new SqlCommand("spAddArtist", conn);
            cmd.Parameters.AddWithValue("@Artist", artistName);
            cmd.Parameters.AddWithValue("@Biografie", artistBiography);
            cmd.Parameters.AddWithValue("@Url", artistUrl);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                throw new Exception(DataProvider.errorException);
            }
        }
    }
}
