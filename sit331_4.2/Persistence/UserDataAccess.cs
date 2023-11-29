using Microsoft.AspNetCore.Identity;
using Npgsql;
using NpgsqlTypes;
using sit331_4._2;

namespace sit331_4._2.Persistence
{
    public class UserDataAccess
    {
        // Connection string is usually set in a config file for the ease of change.
        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=Ku4Gh3Ts8An7;Database=tutorialdb";
        public static List<UserModel> Getusermodels()
        {
            var usermodels = new List<UserModel>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM usermodel", conn);

            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var email = dr.GetString(1);
                var firstname = dr.GetString(2);
                var lastname = dr.GetString(3);
                var passwordhash = dr.GetString(4);
                var descr = dr.IsDBNull(5) ? null : dr.GetString(5);
                var role = dr.IsDBNull(6) ? null : dr.GetString(6);
                var createddate = dr.GetDateTime(7);
                var modifieddate = dr.GetDateTime(8);

                UserModel usermodel = new UserModel(id, email, firstname, lastname, passwordhash, createddate, modifieddate, descr, role);
                //read values off the data reader and create a new robotCommand here and then add it to the result list.
                usermodels.Add(usermodel);
            }
            return usermodels;

        }

        public static List<UserModel> GetUserModelsByID(int id)
        {
            var usermodels = new List<UserModel>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM usermodel WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("id", id);

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var userId = dr.GetInt32(0);
                var email = dr.GetString(1);
                var firstName = dr.GetString(2);
                var lastName = dr.GetString(3);
                var passwordHash = dr.GetString(4);
                var description = dr.IsDBNull(5) ? null : dr.GetString(5);
                var role = dr.IsDBNull(6) ? null : dr.GetString(6);
                var createdDate = dr.GetDateTime(7);
                var modifiedDate = dr.GetDateTime(8);

                UserModel usermodel = new UserModel(userId, email, firstName, lastName, passwordHash, createdDate, modifiedDate, description, role);
                usermodels.Add(usermodel);
            }
            return usermodels;
        }

        public static List<UserModel> GetUserModelsByAdmin(string Role)
        {
            var usermodels = new List<UserModel>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM usermodel WHERE role = @role", conn);

            cmd.Parameters.AddWithValue("role", Role);

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var userId = dr.GetInt32(0);
                var email = dr.GetString(1);
                var firstName = dr.GetString(2);
                var lastName = dr.GetString(3);
                var passwordHash = dr.GetString(4);
                var description = dr.IsDBNull(5) ? null : dr.GetString(5);
                var role = dr.IsDBNull(6) ? null : dr.GetString(6);
                var createdDate = dr.GetDateTime(7);
                var modifiedDate = dr.GetDateTime(8);

                UserModel usermodel = new UserModel(userId, email, firstName, lastName, passwordHash, createdDate, modifiedDate, description, role);
                usermodels.Add(usermodel);
            }
            return usermodels;
        }

        public static List<UserModel> GetUserModelsByEmail(string email)
        {
            var usermodels = new List<UserModel>();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM usermodel WHERE email = @email", conn);

            cmd.Parameters.AddWithValue("email", email);

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var id = dr.GetInt32(0);
                var useremail = dr.GetString(1);
                var firstName = dr.GetString(2);
                var lastName = dr.GetString(3);
                var passwordHash = dr.GetString(4);
                var description = dr.IsDBNull(5) ? null : dr.GetString(5);
                var role = dr.IsDBNull(6) ? null : dr.GetString(6);
                var createdDate = dr.GetDateTime(7);
                var modifiedDate = dr.GetDateTime(8);

                UserModel usermodel = new UserModel(id, useremail, firstName, lastName, passwordHash, createdDate, modifiedDate, description, role);
                usermodels.Add(usermodel);
            }
            return usermodels;
        }


        public static List<UserModel> Insertusermodels(UserModel insertusermodel)
        {
            var usermodels = Getusermodels();
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO usermodel (Email, FirstName, LastName, PasswordHash, CreatedDate, ModifiedDate) VALUES (@email, @firstName, @lastName, @passwordHash, @createddate, @modifieddate)", conn);

            cmd.Parameters.AddWithValue("@email", insertusermodel.Email);
            cmd.Parameters.AddWithValue("@firstName", insertusermodel.FirstName);
            cmd.Parameters.AddWithValue("@lastName", insertusermodel.LastName);
            cmd.Parameters.AddWithValue("@passwordHash", insertusermodel.PasswordHash);
            cmd.Parameters.AddWithValue("@createddate", insertusermodel.CreatedDate);
            cmd.Parameters.AddWithValue("@modifieddate", insertusermodel.ModifiedDate);
            
            cmd.ExecuteNonQuery();

            usermodels.Add(insertusermodel);

            return usermodels;
        }

        public static List<UserModel> Updateusermodels(UserModel updatedusermodel)
        {
            var usermodels = Getusermodels();

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE usermodel set FirstName = @firstname, LastName = @lastname, CreatedDate = @createddate, ModifiedDate = @modifieddate where Id = @id", conn);


            //using var dr = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("id", updatedusermodel.Id);
            //cmd.Parameters.AddWithValue("email", );
            cmd.Parameters.AddWithValue("firstname", updatedusermodel.FirstName);
            cmd.Parameters.AddWithValue("lastname", updatedusermodel.LastName);
            //cmd.Parameters.AddWithValue("passwordhash", );
            cmd.Parameters.AddWithValue("createddate", updatedusermodel.CreatedDate);
            cmd.Parameters.AddWithValue("modifieddate", updatedusermodel.ModifiedDate);
            cmd.ExecuteNonQuery();

            //RobotCommand robotCommand = new RobotCommand(id, name, ismovecommand, createdate, modifieddate, descr);
            //read values off the data reader and create a new robotCommand here and then add it to the result list.
            usermodels.Add(updatedusermodel);

            return usermodels;
        }

        public static List<UserModel> Deleteusermodels(int id)
        {
            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM usermodel WHERE Id=(@id)", conn);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            var usermodels = Getusermodels();
            return usermodels;
        }

        public static List<UserModel> Updatelogin(UserModel updatedusermodel)
        {
            var usermodels = Getusermodels();

            using var conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE usermodel set Email = @email, Passwordhash = @passwordhash where Id = @id", conn);


            //using var dr = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("email", updatedusermodel.Email);
            cmd.Parameters.AddWithValue("passwordhash", updatedusermodel.PasswordHash);
            /*cmd.Parameters.AddWithValue("id", updatedusermodel.Id);
            cmd.Parameters.AddWithValue("firstname", updatedusermodel.FirstName);
            cmd.Parameters.AddWithValue("lastname", updatedusermodel.LastName); 
            cmd.Parameters.AddWithValue("createddate", updatedusermodel.CreatedDate);
            cmd.Parameters.AddWithValue("modifieddate", updatedusermodel.ModifiedDate);
            cmd.ExecuteNonQuery();*/

            //RobotCommand robotCommand = new RobotCommand(id, name, ismovecommand, createdate, modifieddate, descr);
            //read values off the data reader and create a new robotCommand here and then add it to the result list.
            usermodels.Add(updatedusermodel);

            return usermodels;
        }
    }
}
