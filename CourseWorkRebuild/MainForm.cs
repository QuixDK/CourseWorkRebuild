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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CourseWorkRebuild
{
    public partial class MainForm : Form
    {
        private Rectangle topLineSelectBoxRectangle;
        private Rectangle bottomLineSelectBoxRectangle;
        private Rectangle forecastResponseFunctionSelectBoxRectangle;
        private Rectangle showResponseFunctionSelectBoxRectangle;
        private Rectangle originalFormSize;
        private Rectangle tabControl1Rectangle;
        private Rectangle objectDiagramPictureRectangle;
        private Rectangle responseFunctionDiagramRectangle;
        private Rectangle elevatorTableRectangle;
        private InitProject initProject;
        private SetUpProject setUpProject;
        private SQLiteConnection sqlConnection;
        private DataTable dataTable = new DataTable();
        private SetFieldsForNewProject setFieldsForNewProject;
        private bool isContinue = false;
        private List<Double> listOfMValues;
        private List<Double> listOfAlphaValues;
        private bool responseFunctionIsShow = false;
        Repository db;
        Calculations calculations;

        public MainForm()
        {
            InitializeComponent();
            calculations = new Calculations();
            setFieldsForNewProject = new SetFieldsForNewProject();
            setUpProject = new SetUpProject(setFieldsForNewProject);
            setUpProject.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            initRectangleSettings();

            try
            {

                if (setFieldsForNewProject.getInitProject() != null)
                {
                    isContinue = true;
                    this.initProject = setFieldsForNewProject.getInitProject();
                    startProgramm();
                }

                if (setUpProject.getInitProject() != null)
                {
                    isContinue = true;
                    this.initProject = setUpProject.getInitProject();
                    startProgramm();
                    
                }
                if (!isContinue)
                {
                    throw new Exception();
                }
                for (int column = 0; column < elevatorTable.ColumnCount; column++)
                {
                    elevatorTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            db = new Repository(initProject);
            sqlConnection = db.getDbConnection();
            
            loadObjectDiagram();
            showTable(SQL_AllTable());
            initTAndAValues();
            showMValues();
            showAValues();
        }

        private void loadObjectDiagram()
        {
            objectDiagramPicture.Load(initProject.getPathToObjectDiagramRoot());
            objectDiagramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void showMValues()
        {
            listOfMValues = calculations.calculateMValues(elevatorTable);
            for (int i = 0; i < elevatorTable.Rows.Count; i++)
            {
                listBox1.Items.Add(listOfMValues[i]);
            }

        }

        private void showAValues()
        {
            listOfAlphaValues = calculations.calculateAValues(elevatorTable, listOfMValues);
            for (int i = 0;i < elevatorTable.Rows.Count;i++)
            {
                listBox2.Items.Add(listOfAlphaValues[i]);
            }
        }

        private void initTAndAValues()
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
                String replaceCommosToDots = "UPDATE [" + db.getTableNames() + "] SET[" + i + "] = REPLACE([" + i + "],',','.')";
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
            return "SELECT * FROM [" + db.getTableNames() + "]";
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            initProject.setTValue(this.toolStripTextBox1.Text);
            initProject.setAValue(this.toolStripTextBox2.Text);
        }
        private void showResponseFunction()
        {
            responseFunctionDiagram.Series.Add("Функция отклика");
            responseFunctionDiagram.Series["Функция отклика"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < listOfMValues.Count; i++)
            {
                responseFunctionDiagram.Series["Функция отклика"].Points.AddXY(listOfMValues[i], listOfAlphaValues[i]);
            }

        }

        private void hideResponseFunction()
        {
            responseFunctionDiagram.Series["Функция отклика"].Points.Clear();
            responseFunctionDiagram.Series.Remove(responseFunctionDiagram.Series["Функция отклика"]);
        }

        private void showResponseFunctionSelectBox_CheckedChanged(object sender, EventArgs e)
        {

            if (responseFunctionIsShow == false)
            {
                showResponseFunction();
                responseFunctionIsShow = true;
            } 
            else
            {
                hideResponseFunction();
                responseFunctionIsShow = false;
            }
                

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

            resizeControl(topLineSelectBoxRectangle, topLineSelectBox);
            resizeControl(bottomLineSelectBoxRectangle, bottomLineSelectBox);
            resizeControl(forecastResponseFunctionSelectBoxRectangle, forecastResponseFunctionSelectBox);
            resizeControl(showResponseFunctionSelectBoxRectangle, showResponseFunctionSelectBox);
            resizeControl(tabControl1Rectangle, tabControl1);
            resizeControl(elevatorTableRectangle, elevatorTable);
            resizeControl(objectDiagramPictureRectangle, objectDiagramPicture);
            resizeControl(responseFunctionDiagramRectangle, responseFunctionDiagram);
            for (int column = 0; column < elevatorTable.ColumnCount; column++)
            {
                elevatorTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }

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

        private void initRectangleSettings()
        {
            topLineSelectBoxRectangle = new Rectangle(topLineSelectBox.Location.X, topLineSelectBox.Location.Y, topLineSelectBox.Width, topLineSelectBox.Height);
            bottomLineSelectBoxRectangle = new Rectangle(bottomLineSelectBox.Location.X, bottomLineSelectBox.Location.Y, bottomLineSelectBox.Width, bottomLineSelectBox.Height);
            forecastResponseFunctionSelectBoxRectangle = new Rectangle(forecastResponseFunctionSelectBox.Location.X, forecastResponseFunctionSelectBox.Location.Y, forecastResponseFunctionSelectBox.Width, forecastResponseFunctionSelectBox.Height);
            showResponseFunctionSelectBoxRectangle = new Rectangle(showResponseFunctionSelectBox.Location.X, showResponseFunctionSelectBox.Location.Y, showResponseFunctionSelectBox.Width, showResponseFunctionSelectBox.Height);
            tabControl1Rectangle = new Rectangle(tabControl1.Location.X, tabControl1.Location.Y, tabControl1.Width, tabControl1.Height);
            elevatorTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            elevatorTableRectangle = new Rectangle(elevatorTable.Location.X, elevatorTable.Location.Y, elevatorTable.Width, elevatorTable.Height);
            responseFunctionDiagramRectangle = new Rectangle(responseFunctionDiagram.Location.X, responseFunctionDiagram.Location.Y, responseFunctionDiagram.Width, responseFunctionDiagram.Height);
            objectDiagramPictureRectangle = new Rectangle(objectDiagramPicture.Location.X, objectDiagramPicture.Location.Y, objectDiagramPicture.Width, objectDiagramPicture.Height);
        }

    }
}
