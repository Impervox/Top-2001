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
        //static SqlConnection conn = new SqlConnection(@"Server=(localdb)\MSSQLLocaldb;Database=TOP2000;Trusted_Connection=True;");
        //static SqlConnection conn = new SqlConnection(@"Server=(LocalDb)\MSSQLLocalDB;Database=TOP2000;Trusted_Connection=True;");
        static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-0ABOFA3\SQLEXPRESS;Database=TOP2000;Trusted_Connection=True;");
        static List<Record> currentlyShownRecords = new List<Record>();
        static string errorException = "Er is iets fout gegaan, probeer het later opnieuw.";
        public static List<int> allYears = GetAllYears();
        public static List<Artist> allArtists = GetAllArtists();

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
                conn.Close();
                return list;
            }
            catch
            {
                throw new Exception(errorException);
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
                    Artist artist = new Artist(reader.GetString(0), reader.GetString(1), reader.GetString(2), (byte[])reader.GetValue(3));
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

        public static DataView loadData(int year, int first, int last)
        {
            SqlCommand cmd = new SqlCommand("spSongsBYPositiON", conn);
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
            finally
            {
                conn.Close();
            }
        }

        public static void CreateArtist()
        {

        }
    }
}