using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;
using System.Windows.Input;

namespace sit331_4._1.Persistence;

public static class RobotCommandDataAccess
{
    // Connection string is usually set in a config file for the ease of change.
    private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=Ku4Gh3Ts8An7;Database=tutorialdb";
    public static List<RobotCommand> GetRobotCommands()
    {
        var robotCommands = new List<RobotCommand>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn);

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            var id = dr.GetInt32(0);
            var name = dr.GetString(1);
            var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
            var ismovecommand = dr.GetBoolean(3);
            var createdate = dr.GetDateTime(4);
            var modifieddate = dr.GetDateTime(5);

            RobotCommand robotCommand = new RobotCommand(id, name, ismovecommand, createdate, modifieddate, descr);
            //read values off the data reader and create a new robotCommand here and then add it to the result list.
            robotCommands.Add(robotCommand);
        }
        return robotCommands;

    }

    public static List<RobotCommand> InsertRobotCommands(RobotCommand insertCommand)
    {
        //var robotCommands = new List<RobotCommand>();
        var robotCommands = GetRobotCommands();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO robotcommand (name, IsMoveCommand, CreatedDate, ModifiedDate) VALUES (@Name, @ismovecommand, @createdate, @modifieddate)", conn);

        //cmd.Parameters.AddWithValue("id", insertCommand.Id);
        
        cmd.Parameters.AddWithValue("@Name", insertCommand.Name);
        cmd.Parameters.AddWithValue("@ismovecommand", insertCommand.IsMoveCommand);
        cmd.Parameters.AddWithValue("@createdate", insertCommand.CreateDate);
        cmd.Parameters.AddWithValue("@modifieddate", insertCommand.ModifiedDate);
        cmd.ExecuteNonQuery();

        robotCommands.Add(insertCommand);

        return robotCommands;
    }

    public static List<RobotCommand> UpdateRobotCommands(RobotCommand updatedCommand)
    {
        //var robotCommands = new List<RobotCommand>();
        var robotCommands = GetRobotCommands();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();

        using var cmd = new NpgsqlCommand("UPDATE robotcommand set Name = @name, IsMoveCommand = @ismovecommand, CreatedDate = @createddate, ModifiedDate = @modifieddate where Id = @id", conn);


        //using var dr = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("id", updatedCommand.Id);
            cmd.Parameters.AddWithValue("name", updatedCommand.Name);
            cmd.Parameters.AddWithValue("ismovecommand", updatedCommand.IsMoveCommand);
            cmd.Parameters.AddWithValue("createddate", updatedCommand.CreateDate);
            cmd.Parameters.AddWithValue("modifieddate", updatedCommand.ModifiedDate);
        cmd.ExecuteNonQuery();
            //RobotCommand robotCommand = new RobotCommand(id, name, ismovecommand, createdate, modifieddate, descr);
            //read values off the data reader and create a new robotCommand here and then add it to the result list.
            robotCommands.Add(updatedCommand);
        
        return robotCommands;
    }

    public static List<RobotCommand> DeleteRobotCommands(int id)
    {
        //var robotCommands = new List<RobotCommand>();
        using var conn = new NpgsqlConnection(CONNECTION_STRING);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM robotcommand WHERE Id=(@id)", conn);

        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();

        var robotCommands = GetRobotCommands();
        return robotCommands;
    }
}

