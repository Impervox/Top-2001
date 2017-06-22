using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class DataProvider
    {
        //select your database, comment the line you don't use.
        static SqlConnection conn = new SqlConnection(@"Server=(LocalDb)\MSSQLLocalDB;Database=TOP2000;Trusted_Connection=True;");
        //static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-0ABOFA3\SQLEXPRESS;Database=TOP2000;Trusted_Connection=True;");
        static List<Record> currentlyShownRecords = new List<Record>();
        public static string errorException = "Er is iets fout gegaan, probeer het later opnieuw.";
        public static List<int> allYears = GetAllYears();
        public static List<Artist> allArtists = GetAllArtists();
        public static List<Song> allSongs = GetAllSongs();

        public static List<int> GetAllYears()
        {
            List<int> list = new List<int>();
            SqlCommand cmd = new SqlCommand("spGetAllYears", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    list.Add(reader.GetInt32(0));
                }
                return list;
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Artist> GetAllArtists()
        {
            List<Artist> list = new List<Artist>();
            SqlCommand cmd = new SqlCommand("spGetAllArtists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Artist artist = new Artist(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    list.Add(artist);
                }
                return list;
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Song> GetAllSongs()
        {
            List<Song> list = new List<Song>();
            SqlCommand cmd = new SqlCommand("spSongLijst", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Song song = new Song(reader.GetString(0), reader.GetInt32(1), (byte[])reader.GetValue(2), reader.GetString(3));
                    list.Add(song);
                }
                return list;
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataView loadData(int year, int first, int last)
        {
            SqlCommand cmd = new SqlCommand("spSongsByPosition", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@lowestSong", first);
            cmd.Parameters.AddWithValue("@higestSong", last);
            cmd.Parameters.AddWithValue("@year", year);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                return table.DefaultView;
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void CreateSong(string artist, string title, int year, string lyrics)
        {
            SqlCommand cmd = new SqlCommand("spAddSong", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ArtistName", artist);
            cmd.Parameters.AddWithValue("@SongTitle", title);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Lyrics", lyrics);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void CreateArtist(string artist, string biography, string url)
        {
            foreach (Artist art in GetAllArtists())
            {
                if (art.Name == artist)
                {
                    throw new Exception();
                }
            }
            SqlCommand cmd = new SqlCommand("spAddArtist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Artist", artist);
            cmd.Parameters.AddWithValue("@Biografie", biography);
            cmd.Parameters.AddWithValue("@Url", url);
            allArtists.Add(new Artist(artist, biography, url));
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<string> SongsOfArtist(string artist)
        {
            List<string> list = new List<string>();

            SqlCommand cmd = new SqlCommand("spSongsByArtist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@artiest", artist);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetString(1));
                }
                return list;
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static char[] GetFirstCharacters()
        {
            string characters = "";
            SqlCommand cmd = new SqlCommand("spGetAllFirstCharactersFromArtists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    characters += reader.GetString(0);
                }
                return characters.ToArray();
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void RemoveArtist(string artist)
        {
            SqlCommand cmd = new SqlCommand("spRemoveArtist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Artist", artist);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                foreach(Artist a in allArtists.OrderBy(x => x.Name).ToList())
                {
                    if (a.Name == artist)
                        allArtists.Remove(a);
                }
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}