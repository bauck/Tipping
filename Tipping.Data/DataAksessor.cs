using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Tipping.Domain;

namespace Tipping.Data
{
    public static class DataAksessor
    {
        private static string HentTilkoblingsdata()
        {
            return ConfigurationManager.ConnectionStrings["TippingDB"].ConnectionString;
        }

        public static List<Kamp> HentAlleKamper()
        {
            var kamper = new List<Kamp>();
            try
            {
                using (var sqlConnection = new SqlConnection(HentTilkoblingsdata()))
                {
                    sqlConnection.Open();
                    const string kampCommandText = @"SELECT * FROM Kamp order by ID";
                    var kampCommand = new SqlCommand(kampCommandText, sqlConnection);

                    using (var adapter = new SqlDataAdapter(kampCommand))
                    {
                        var kampTabell = new DataTable();
                        adapter.Fill(kampTabell);
                        kamper.AddRange(from DataRow rad in kampTabell.Rows select new Kamp(Convert.ToInt32(rad["ID"]), Convert.ToString(rad["Hjemmelag"]), Convert.ToString(rad["Bortelag"]), Convert.ToString(rad["Avspark"]).FraDBStringTilDato(), Convert.ToInt32(rad["MaalHjemmelag"]), Convert.ToInt32(rad["MaalBortelag"]), Convert.ToBoolean(rad["ErFerdigspilt"])));
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kunne ikke hente kamper fra databasen", ex);
            }
            return kamper;
            
        }
        public static DateTime FraDBStringTilDato(this string dbString)
        {
            try
            {
                var år = Int32.Parse(dbString.Substring(0, 4));
                var måned = Int32.Parse(dbString.Substring(4, 2));
                var dag = Int32.Parse(dbString.Substring(6, 2));
                var time = Int32.Parse(dbString.Substring(8, 2));
                var minutt = Int32.Parse(dbString.Substring(10, 2));

                return new DateTime(år, måned, dag, time, minutt, 0);
            }
            catch (Exception e)
            {
                throw new Exception("Kunne ikke konvertere fra DB-string til dato", e);
            }
        }

        public static List<Tips> HentAlleTipsForBruker(string brukernavn)
        {
            var tips = new List<Tips>();
            try
            {
                using (var sqlConnection = new SqlConnection(HentTilkoblingsdata()))
                {
                    sqlConnection.Open();
                    const string tipsCommandText = @"SELECT * FROM Tips WHERE [TipperID] = @Brukernavn";
                    var tipsCommand = new SqlCommand(tipsCommandText, sqlConnection);
                    tipsCommand.Parameters.Add("@Brukernavn", SqlDbType.VarChar);
                    tipsCommand.Parameters["@Brukernavn"].Value = brukernavn;

                    using (var adapter = new SqlDataAdapter(tipsCommand))
                    {
                        var tipsTabell = new DataTable();
                        adapter.Fill(tipsTabell);
                        tips.AddRange(from DataRow rad in tipsTabell.Rows select new Tips(Convert.ToInt32(rad["KampID"]), brukernavn, Convert.ToInt32(rad["MaalHjemmelag"]), Convert.ToInt32(rad["MaalBortelag"]), Convert.ToBoolean(rad["ErLevert"]), Convert.ToBoolean(rad["ErBeregnet"]), Convert.ToInt32(rad["Poeng"])));
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kunne ikke hente tips fra databasen", ex);
            }
            return tips;
        }

        public static Kamp HentKamp(int kampID)
        {
            var kamper = new List<Kamp>();
            try
            {
                using (var sqlConnection = new SqlConnection(HentTilkoblingsdata()))
                {
                    sqlConnection.Open();
                    const string kampCommandText = @"SELECT * FROM Kamp WHERE [ID] = @KampID";
                    var kampCommand = new SqlCommand(kampCommandText, sqlConnection);
                    kampCommand.Parameters.Add("@KampID", SqlDbType.VarChar);
                    kampCommand.Parameters["@KampID"].Value = kampID;

                    using (var adapter = new SqlDataAdapter(kampCommand))
                    {
                        var kampTabell = new DataTable();
                        adapter.Fill(kampTabell);
                        kamper.AddRange(from DataRow rad in kampTabell.Rows select new Kamp(Convert.ToInt32(rad["ID"]), Convert.ToString(rad["Hjemmelag"]), Convert.ToString(rad["Bortelag"]), Convert.ToString(rad["Avspark"]).FraDBStringTilDato(), Convert.ToInt32(rad["MaalHjemmelag"]), Convert.ToInt32(rad["MaalBortelag"]), Convert.ToBoolean(rad["ErFerdigspilt"])));
                    }
                    sqlConnection.Close();
                    return kamper[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kunne ikke hente kamp fra databasen", ex);
            }
        }


        public static Tips LagreTips(int kampID, string tipperID, int målHjemmelag, int målBortelag)
        {
            var tips = new List<Tips>();
            try
            {
                using (var sqlConnection = new SqlConnection(HentTilkoblingsdata()))
                {
                    sqlConnection.Open();
                    const string kampCommandText = @"IF EXISTS (SELECT * FROM Tips WHERE KampID = @kampID AND TipperID = @tipperID) "
                        + "UPDATE Tips "
                        + "SET MaalHjemmelag = @maalHjemmelag, MaalBortelag = @maalBortelag WHERE KampID = @kampID AND TipperID = @tipperID "
                        + "ELSE INSERT INTO Tips VALUES (@kampID, @tipperID, @maalhjemmelag, @maalBortelag, 1, 0, 0) "
                        + "SELECT * FROM Tips WHERE KampID = @kampID AND TipperID = @tipperID";
                    var kampCommand = new SqlCommand(kampCommandText, sqlConnection);
                    kampCommand.Parameters.Add("@kampID", SqlDbType.Int);
                    kampCommand.Parameters["@kampID"].Value = kampID;
                    kampCommand.Parameters.Add("@tipperID", SqlDbType.VarChar);
                    kampCommand.Parameters["@tipperID"].Value = tipperID;
                    kampCommand.Parameters.Add("@maalHjemmelag", SqlDbType.Int);
                    kampCommand.Parameters["@maalHjemmelag"].Value = målHjemmelag;
                    kampCommand.Parameters.Add("@maalBortelag", SqlDbType.Int);
                    kampCommand.Parameters["@maalBortelag"].Value = målBortelag;

                    using (var adapter = new SqlDataAdapter(kampCommand))
                    {
                        var tipsTabell = new DataTable();
                        adapter.Fill(tipsTabell);
                        tips.AddRange(from DataRow rad in tipsTabell.Rows select new Tips(Convert.ToInt32(rad["KampID"]), Convert.ToString(rad["TipperID"]), Convert.ToInt32(rad["MaalHjemmelag"]), Convert.ToInt32(rad["MaalBortelag"]), Convert.ToBoolean(rad["ErLevert"]), Convert.ToBoolean(rad["ErBeregnet"]), Convert.ToInt32(rad["Poeng"])));
                    }
                    sqlConnection.Close();
                    return tips[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kunne ikke hente kamp fra databasen", ex);
            }
        }
    }
}
