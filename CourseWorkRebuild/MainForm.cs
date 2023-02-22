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
using System.Windows.Forms.DataVisualization.Charting;

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
        private List<Double> listOfBottomLineMValues = new List<Double>();
        private List<Double> listOfBottomLineAValues = new List<Double>();
        private List<Double> listOfTopLineMValues = new List<Double>();
        private List<Double> listOfTopLineAValues = new List<Double>();
        ChartDiagramService chartDiagramService = new ChartDiagramService();
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
                for (int column = 0; column < elevatorTable.ColumnCount; column++)
                {
                    elevatorTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Не выбран ни один проект");
                Close();
            }

        }

        private void startProgramm()
        {
            isContinue = true;

            db = new Repository(initProject);
            sqlConnection = db.getDbConnection();
            
            loadObjectDiagram();
            db.showTable(SQL_AllTable(), dataTable, elevatorTable);
            initTAndAValues();
            showMValues();
            showAValues();
        }

        private void chartScale()
        { 
            functionDiagrams.ChartAreas[0].AxisX.Maximum = Math.Max(Math.Max(listOfBottomLineMValues.Max(),listOfMValues.Max()),listOfTopLineMValues.Max());
            functionDiagrams.ChartAreas[0].AxisX.Minimum = Math.Min(Math.Min(listOfMValues.Min(), listOfTopLineMValues.Min()),listOfBottomLineMValues.Min());
            functionDiagrams.ChartAreas[0].AxisY.Maximum = Math.Max(Math.Max(listOfBottomLineMValues.Max(), listOfMValues.Max()), listOfTopLineMValues.Max());
            functionDiagrams.ChartAreas[0].AxisY.Minimum = Math.Min(Math.Min(listOfMValues.Min(), listOfTopLineMValues.Min()), listOfBottomLineMValues.Min());
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

        private String SQL_AllTable()
        {
            return "SELECT * FROM [" + db.getTableNames() + "]";
            
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            initProject.setTValue(this.toolStripTextBox1.Text);
            initProject.setAValue(this.toolStripTextBox2.Text);
        }

        private void topLineSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Верхняя граница";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addLine(listOfTopLineMValues, listOfTopLineAValues, calculations.calculateTopLine(dataTable, toolStripTextBox1, elevatorTable), functionDiagrams, serieName, listBox9, listBox10);
        }

        private void bottomLineSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Нижняя граница";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addLine(listOfBottomLineMValues, listOfBottomLineAValues, calculations.calculateBottomLine(dataTable, toolStripTextBox1, elevatorTable), functionDiagrams, serieName, listBox5, listBox6);
        }

        private void showResponseFunctionSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Функция отклика";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addResponseFunction(listOfMValues, listOfAlphaValues, elevatorTable, functionDiagrams);
        }

        private void forecastResponseFunctionSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastResponseFunction(listOfMValues, listOfAlphaValues, functionDiagrams, toolStripTextBox2, listBox3, listBox4);
        }
        private void forecastBottomValues_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение для нижней границы";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastBottomFunction(dataTable, toolStripTextBox1, functionDiagrams, toolStripTextBox2, elevatorTable, listBox7, listBox8);
        }
        private void forecastTopLineValues_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение для верхней границы";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastTopFunction(dataTable, toolStripTextBox1, functionDiagrams, toolStripTextBox2, elevatorTable, listBox11, listBox12);
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
            resizeControl(responseFunctionDiagramRectangle, functionDiagrams);
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
            responseFunctionDiagramRectangle = new Rectangle(functionDiagrams.Location.X, functionDiagrams.Location.Y, functionDiagrams.Width, functionDiagrams.Height);
            objectDiagramPictureRectangle = new Rectangle(objectDiagramPicture.Location.X, objectDiagramPicture.Location.Y, objectDiagramPicture.Width, objectDiagramPicture.Height);
        }


    }
}
