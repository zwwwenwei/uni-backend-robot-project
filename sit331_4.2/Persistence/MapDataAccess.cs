using Npgsql;

namespace sit331_4._1.Persistence;
    public class MapDataAccess
    {
        // Connection string is usually set in a config file for the ease of change.
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=Ku4Gh3Ts8An7;Database=tutorialdb";
        public static List<Map> GetMaps()
        {
            var robotMaps = new List<Map>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM map", conn);

            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var columns = dr.GetInt32(1);
                var rows = dr.GetInt32(2);
                var name = dr.GetString(3);
                var descr = dr.IsDBNull(4) ? null : dr.GetString(4);
                var createdate = dr.GetDateTime(5);
                var modifieddate = dr.GetDateTime(6);

                Map robotMap = new Map(id, columns, rows, name, createdate, modifieddate, descr);
                //read values off the data reader and create a new robotCommand here and then add it to the result list.
                robotMaps.Add(robotMap);
            }
            return robotMaps;

        }

        public static List<Map> InsertMaps(Map insertmap)
        {
            //var robotmaps = new List<Map>();
            var robotmaps = GetMaps();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("INSERT INTO map (Columns, Rows, Name, CreatedDate, ModifiedDate) VALUES (@columns, @rows, @name, @createddate, @modifieddate)", conn);

            cmd.Parameters.AddWithValue("columns", insertmap.Columns);
            cmd.Parameters.AddWithValue("rows", insertmap.Rows);
            cmd.Parameters.AddWithValue("name", insertmap.Name);
            cmd.Parameters.AddWithValue("createddate", insertmap.CreatedDate);
            cmd.Parameters.AddWithValue("modifieddate", insertmap.ModifiedDate);
            cmd.ExecuteNonQuery();
            robotmaps.Add(insertmap);
            return robotmaps;
        }

        public static List<Map> UpdateMaps(Map updatemap)
        {
            //var robotmaps = new List<Map>();
            var robotmaps = GetMaps();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE map set Columns = @columns, Rows = @rows, Name = @name, CreatedDate = @createddate, ModifiedDate = @modifieddate where Id = @id", conn);

            cmd.Parameters.AddWithValue("id", updatemap.Id);
            cmd.Parameters.AddWithValue("columns", updatemap.Columns);
            cmd.Parameters.AddWithValue("rows", updatemap.Rows);
            cmd.Parameters.AddWithValue("name", updatemap.Name);
            cmd.Parameters.AddWithValue("createddate", updatemap.CreatedDate);
            cmd.Parameters.AddWithValue("modifieddate", updatemap.ModifiedDate);
            cmd.ExecuteNonQuery();
            robotmaps.Add(updatemap);
            return robotmaps;
        }

        public static List<Map> DeleteMaps(int id)
        {
            //var robotmaps = new List<Map>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM map WHERE Id=(@id)", conn);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            var robotmaps = GetMaps();
            return robotmaps;
        }
}

