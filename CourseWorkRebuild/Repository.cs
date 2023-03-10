using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public DataGridView showTable(DataTable dataTable, DataGridView elevatorTable)
        {
            String SQLQuery = "SELECT * FROM [" + getTableNames() + "]";
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
            SQLiteCommand command = new SQLiteCommand(sqlConnection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(SQLQuery, sqlConnection);
            adapter.Fill(dataTable);

            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                String replaceCommosToDots = "UPDATE [" + getTableNames() + "] SET[" + i + "] = REPLACE([" + i + "],',','.')";
                command.CommandText = replaceCommosToDots;
                command.ExecuteNonQuery();
                Thread.Sleep(10);
            }

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            adapter = new SQLiteDataAdapter(SQLQuery, sqlConnection);
            adapter.Fill(dataTable);

            elevatorTable.Columns.Clear();
            elevatorTable.Rows.Clear();

            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                String ColName = dataTable.Columns[column].ColumnName;
                elevatorTable.Columns.Add(ColName, ColName);
                elevatorTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                elevatorTable.Rows.Add(dataTable.Rows[row].ItemArray);
            }
            return elevatorTable;

        }

        public DataGridView updateValue(DataGridView elevatorTable, int currentColumn, int currentRow, Double newValue)
        {
            String updateQuery = "UPDATE [" + getTableNames() + "] SET \"" + currentColumn + "\" = \"" + newValue + "\" WHERE Эпоха = \'" + currentRow +  "\'";
            SQLiteCommand command = new SQLiteCommand(sqlConnection);
            command.CommandText = updateQuery;
            command.ExecuteNonQuery();
            return elevatorTable;
        }

        public void addNewValuesInRow(int column, int row, Double value)
        {
            String SQLQuery = "UPDATE [" + getTableNames() + "] SET \"" + column + "\" = \"" + value + "\" WHERE Эпоха = \'" + row + "\'";
            SQLiteCommand command = new SQLiteCommand(sqlConnection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }
        public void addNewRow(Double value)
        {
            String SQLQuery = "INSERT INTO [" + getTableNames() + "] (Эпоха) VALUES (\"" + value + "\")";
            SQLiteCommand command = new SQLiteCommand(sqlConnection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }

    }
}
