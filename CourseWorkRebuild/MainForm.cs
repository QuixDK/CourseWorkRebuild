using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;

namespace CourseWorkRebuild
{
    public partial class MainForm : Form
    {
        private InitProject initProject;
        private SetUpProject setUpProject;
        private SQLiteConnection sqlConnection;
        private DataTable dataTable;
        private SetFieldsForNewProject setFieldsForNewProject;
        private bool isContinue = false;
        private List<Double> listOfMValues;
        private List<Double> listOfAlphaValues;

        public MainForm()
        {
            InitializeComponent();
            setFieldsForNewProject = new SetFieldsForNewProject();
            setUpProject = new SetUpProject(setFieldsForNewProject);
            setUpProject.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                if (setFieldsForNewProject.getInitProject() != null)
                {
                    this.initProject = setFieldsForNewProject.getInitProject();
                    loadObjectDiagram();
                    getDbConnection();
                    showTable(SQL_AllTable());
                    initValues();
                    isContinue = true;
                    calculateMValues();
                }

                if (setUpProject.getInitProject() != null)
                {
                    this.initProject = setUpProject.getInitProject();
                    loadObjectDiagram();
                    getDbConnection();
                    showTable(SQL_AllTable());
                    isContinue = true;
                    calculateMValues();
                }
                if (!isContinue)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть проект");
                Close();
            }

        }

        private void loadObjectDiagram()
        {
            objectDiagramPicture.Load(initProject.getPathToObjectDiagramRoot());
            objectDiagramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void getDbConnection()
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
            dataTable = new DataTable();
            sqlConnection = new SQLiteConnection("Data Source=" + tablePath + ";Version=3;");
            sqlConnection.Open();
            SQLiteCommand sqlCommand = new SQLiteCommand();
            sqlCommand.Connection = sqlConnection;

        }

        private void calculateMValues()
        {
            double M = 0;
            List<Double> values = new List<double>();
            listOfMValues = new List<double>();
            listOfAlphaValues= new List<double>();
            listOfAlphaValues.Add(0);
            for (int i = 0; i < elevatorTable.Rows.Count-1; i++)
            {
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    values.Add(Convert.ToDouble(elevatorTable.Rows[i].Cells[j].Value));
                }
                foreach (double c in values)
                {
                    M += (c * c);
                }
                listOfMValues.Add(Math.Sqrt(M));
                listBox1.Items.Add(listOfMValues[i]);
                M = 0;
                values.Clear();

            }
            
        }

        private String getTableNames()
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

        private void initValues()
        {
            this.toolStripTextBox1.Text = "T: " + initProject.getValueOfT();
            this.toolStripTextBox2.Text = "a: " + initProject.getValueOfa();
        }

        private void showTable(String SQLQuery)
        {

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

        }

        private String SQL_AllTable()
        {
            return "SELECT * FROM [" + getTableNames() + "]";
        }

        private void showTableButton_Click(object sender, EventArgs e)
        {
            showTable(SQL_AllTable());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
