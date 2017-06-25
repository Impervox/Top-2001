using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataProvider
    {
        //select your database, comment the line you don't use.
        /// <summary>
        /// The connection
        /// </summary>
        static SqlConnection conn = new SqlConnection(@"Server=(LocalDb)\MSSQLLocalDB;Database=TOP2000;Trusted_Connection=True;");
        //static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-0ABOFA3\SQLEXPRESS;Database=TOP2000;Trusted_Connection=True;");
        /// <summary>
        /// The currently shown records
        /// </summary>
        static List<Record> currentlyShownRecords = new List<Record>();
        /// <summary>
        /// The error exception
        /// </summary>
        public static string errorException = "Er is iets fout gegaan, probeer het later opnieuw.";
        /// <summary>
        /// All years
        /// </summary>
        public static List<int> allYears = GetAllYears();
        /// <summary>
        /// All artists
        /// </summary>
        public static List<Artist> allArtists = GetAllArtists();
        /// <summary>
        /// All songs
        /// </summary>
        public static List<Song> allSongs = GetAllSongs();

        /// <summary>
        /// Gets all years.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets all artists.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets all songs.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static List<Song> GetAllSongs()
        {
            List<Song> list = new List<Song>();
            SqlCommand cmd = new SqlCommand("spSongLijst", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Song song = new Song(reader.GetString(0), reader.GetInt32(1), reader.GetString(3), (byte[])reader.GetValue(2));
                    list.Add(song);
                }
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
                //throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Edits the song.
        /// </summary>
        /// <param name="thisSong">The this song.</param>
        /// <exception cref="System.Exception"></exception>
        public static void EditSong(string year, string title, string lyrics, Song thisSong)
        {
            int newYear;
            if (!int.TryParse(year, out newYear))
                newYear = thisSong.Year;
            
            SqlCommand cmd = new SqlCommand("spEditSong", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@title", thisSong.Title);
            cmd.Parameters.AddWithValue("@newTitle", title);
            cmd.Parameters.AddWithValue("@lyrics", lyrics);
            cmd.Parameters.AddWithValue("@year", thisSong.Year);
            cmd.Parameters.AddWithValue("@newYear", newYear);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                foreach (Song s in allSongs.OrderBy(x => x.Title).ToList())
                    if (s.Title == thisSong.Title)
                    {
                        s.Title = title;
                        s.Lyrics = lyrics;
                        s.Year = newYear;
                    }
            }
            catch
            {
                throw new Exception(errorException);
            }
            finally
            {
                conn.Close();
                allSongs = GetAllSongs();
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Removes the song.
        /// </summary>
        /// <param name="thisSong">The this song.</param>
        /// <param name="thisArtist">The this artist.</param>
        /// <exception cref="System.Exception"></exception>
        public static void RemoveSong(Song thisSong, Artist thisArtist)
        {
            SqlCommand cmd = new SqlCommand("spRemoveSong", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@song", thisSong.Title);
            cmd.Parameters.AddWithValue("@artist", thisArtist.Name);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                List<Song> newSongList = GetAllSongs();
                if (newSongList.Count != allSongs.Count)
                    foreach (Song s in allSongs.OrderBy(x => x.Title).ToList())
                        if (s.Title == thisSong.Title)
                            allSongs.Remove(s);
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

        /// <summary>
        /// Creates the song.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="title">The title.</param>
        /// <param name="year">The year.</param>
        /// <param name="lyrics">The lyrics.</param>
        /// <exception cref="System.Exception">
        /// </exception>
        public static void CreateSong(string artist, string title, int year, string lyrics)
        {
            foreach (Song s in GetAllSongs())
            {
                if (s.Title == title)
                {
                    throw new Exception();
                }
            }
            SqlCommand cmd = new SqlCommand("spAddSong", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@artist", artist);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@lyrics", lyrics);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                allSongs.Add(new Song(title, year, lyrics));
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

        /// <summary>
        /// Creates the artist.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="biography">The biography.</param>
        /// <param name="url">The URL.</param>
        /// <exception cref="System.Exception">
        /// </exception>
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
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                allArtists.Add(new Artist(artist, biography, url));
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

        /// <summary>
        /// Songses the of artist.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the first characters.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Removes the artist.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Edits the artist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="biography">The biography.</param>
        /// <exception cref="System.Exception"></exception>
        public static void EditArtist(string name, string newName, string url = null, string biography = null)
        {
            SqlCommand cmd = new SqlCommand("spEditArtist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Artist", name);
            cmd.Parameters.AddWithValue("@NewArtist", newName);
            cmd.Parameters.AddWithValue("@Url", url);
            cmd.Parameters.AddWithValue("@Biography", biography);
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
                allArtists = GetAllArtists();
            }
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="song">The song.</param>
        /// <param name="position">The position.</param>
        /// <param name="year">The year.</param>
        /// <exception cref="System.Exception"></exception>
        public static void AddRecord(string artist, string song, int position, int year)
        {
            SqlCommand cmd = new SqlCommand("spAddRecord", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Artist", artist);
            cmd.Parameters.AddWithValue("@Song", song);
            cmd.Parameters.AddWithValue("@Position", position);
            cmd.Parameters.AddWithValue("@Year", year);
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
                allYears = GetAllYears();
            }
        }

        /// <summary>
        /// Gets the years and song count.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static List<int> GetYearsAndSongCount()
        {
            List<int> returnValue = new List<int>();
            List<int> years = new List<int>();
            List<int> count = new List<int>();
            SqlCommand cmd = new SqlCommand("spGetYearAndAmountOfNumbers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    years.Add(reader.GetInt32(0));
                    count.Add(reader.GetInt32(1));
                }
                for (int i = years.OrderBy(x => x).ToList()[0]; i <= DateTime.Today.Year; i++)
                {
                    if (!years.Contains(i))
                        returnValue.Add(i);
                    else
                        for(int b = 0; b < years.Count; b++)
                            if(years[b] == i)
                                if(count[b] < 2000)
                                    returnValue.Add(i);
                }
                return returnValue;
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