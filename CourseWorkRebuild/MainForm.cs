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
using System.Reflection.Emit;

namespace CourseWorkRebuild
{
    public partial class MainForm : Form
    {
        private Rectangle originalFormSize;
        private Rectangle objectDiagramPictureRectangle;
        private Rectangle responseFunctionDiagramRectangle;
        private Rectangle elevatorTableRectangle;
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
            elevatorTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            elevatorTableRectangle = new Rectangle(elevatorTable.Location.X, elevatorTable.Location.Y, elevatorTable.Width, elevatorTable.Height);
            responseFunctionDiagramRectangle = new Rectangle(responseFunctionDiagram.Location.X, responseFunctionDiagram.Location.Y, responseFunctionDiagram.Width, responseFunctionDiagram.Height);
            objectDiagramPictureRectangle = new Rectangle(objectDiagramPicture.Location.X, objectDiagramPicture.Location.Y, objectDiagramPicture.Width, objectDiagramPicture.Height);


            try
            {

                if (setFieldsForNewProject.getInitProject() != null)
                {
                    this.initProject = setFieldsForNewProject.getInitProject();
                    startProgramm();
                }

                if (setUpProject.getInitProject() != null)
                {
                    this.initProject = setUpProject.getInitProject();
                    startProgramm();
                    
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

        private void startProgramm()
        {
            loadObjectDiagram();
            getDbConnection();
            showTable(SQL_AllTable());
            initValues();
            isContinue = true;
            calculateMValues();
            calculateAValues();
            showResponseFunction();
        }

        private void showResponseFunction()
        {
            responseFunctionDiagram.Series["Функция отклика"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < listOfMValues.Count; i++) 
            {
                responseFunctionDiagram.Series["Функция отклика"].Points.AddXY(listOfMValues[i], listOfAlphaValues[i]);
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
            double summPr = 0;
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

        private void calculateAValues()
        {
            double calculateAcos = 0;
            double calculateDegree = 0;
            double summPr = 0;
            double firstValue = 0;
            double secondValue = 0;
            
            listBox2.Items.Add(listOfAlphaValues[0]);
            for (int i = 0; i < elevatorTable.Rows.Count-2; i++)
            {
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    firstValue = Convert.ToDouble(elevatorTable.Rows[0].Cells[j].Value);
                    secondValue = Convert.ToDouble(elevatorTable.Rows[i+1].Cells[j].Value);
                    summPr += firstValue * secondValue;
                }
                summPr = summPr / listOfMValues[0] / listOfMValues[i + 1];
                calculateAcos = Math.Acos(summPr);
                calculateDegree = 180 * calculateAcos / Math.PI;
                listOfAlphaValues.Add(calculateDegree);
                listBox2.Items.Add(listOfAlphaValues[i+1]);
                
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

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            initProject.setTValue(this.toolStripTextBox1.Text);
            initProject.setAValue(this.toolStripTextBox2.Text);
        }

        private void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);

            int newX = (int)(r.Location.X * xRatio);
            int newY = (int)(r.Location.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            resizeControl(elevatorTableRectangle, elevatorTable);
            resizeControl(objectDiagramPictureRectangle, objectDiagramPicture);
            resizeControl(responseFunctionDiagramRectangle, responseFunctionDiagram);
            for (int column = 0; column < elevatorTable.ColumnCount; column++)
            {
                elevatorTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
            }
            
        }

    }
}
