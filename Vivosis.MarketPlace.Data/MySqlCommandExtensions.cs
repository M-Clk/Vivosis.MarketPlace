using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace Vivosis.MarketPlace.Data
{
    public static class MySqlCommandExtensions
    {
        public static void LoadScript(this MySqlCommand command, string scriptFileName, params string[] arguments)
        {
            var sqlScriptPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "Vivosis.MarketPlace.Data", "SqlSqripts", $"{scriptFileName}.sql");
            var script = File.ReadAllText(sqlScriptPath);
            if(arguments.Length > 0)
                script = string.Format(script, arguments);
            command.CommandText = script;
        }
    }
}
