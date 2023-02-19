using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkRebuild
{
    internal class Repository
    {
        InitProject initProject;
        SQLiteConnection sqlConnection;

        public Repository(InitProject initProject) 
        {
            this.initProject = initProject;
        }

        public SQLiteConnection getDbConnection()
        {
            String tablePath = "";
            if (!initProject.getPathToElevatorAndValuesTableRoot().Equals(""))
            {
                tablePath = initProject.getPathToElevatorAndValuesTableRoot();
            }
            else if (!initProject.getPathToElevatorTableRoot().Equals(""))
            {
                tablePath = initProject.getPathToElevatorTableRoot();
            }
            this.sqlConnection = new SQLiteConnection("Data Source=" + tablePath + ";Version=3;");
            this.sqlConnection.Open();
            return this.sqlConnection;

        }

        public String getTableNames()
        {
            String SQLQuery = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
            SQLiteCommand command = new SQLiteCommand(SQLQuery, sqlConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            String tableName = "";

            while (reader.Read())
            {
                tableName = reader.GetString(0);
            }

            return tableName;

        }

    }
}
