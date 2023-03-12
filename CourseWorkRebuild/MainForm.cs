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
using System.Data.Entity;

namespace CourseWorkRebuild
{
    public partial class MainForm : Form
    {
        private Rectangle topLineSelectBoxRectangle;
        private Rectangle bottomLineSelectBoxRectangle;
        private Rectangle forecastResponseFunctionSelectBoxRectangle;
        private Rectangle forecastBottomLineSelectBoxRectangle;
        private Rectangle forecastTopLineSelectBoxRectangle;
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
        private List<Double> listOfMValues = new List<Double>();
        private List<Double> listOfAValues = new List<Double>();
        private List<Double> listOfBottomLineMValues = new List<Double>();
        private List<Double> listOfBottomLineAValues = new List<Double>();
        private List<Double> listOfTopLineMValues = new List<Double>();
        private List<Double> listOfTopLineAValues = new List<Double>();
        private List<Double> forecastTopLineMValue = new List<Double>();
        private List<Double> forecastBottomLineMValue = new List<Double>();
        private List<Double> forecastTopLineAValue = new List<Double>();
        private List<Double> forecastBottomLineAValue = new List<Double>();
        private List<Double> forecastMValue = new List<Double>();
        private List<Double> forecastAValue = new List<Double>();
        private ChartDiagramService chartDiagramService = new ChartDiagramService();
        private Repository db;
        private Calculations calculations;
        private Double Alpha;
        private Double T;
        private int markCount = 0;
        private int buildingCount = 0;
        private int needMarkCount;
        private String hintLabelForMarks;
        private int panelShowedCount;
        DataGridView bottomLineTable = new DataGridView();
        DataGridView topLineTable = new DataGridView();

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
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
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
                
                Close();
            }

        }

        private void startProgramm()
        {
            isContinue = true;

            db = new Repository(initProject);
            sqlConnection = db.getDbConnection(); 
            db.showTable(dataTable, elevatorTable);

            loadObjectDiagram();
            showTAndAValues();

        }

        private void firstLevel()
        {
            bottomLineTable = calculations.calculateBottomLine(dataTable, T, elevatorTable);
            topLineTable = calculations.calculateTopLine(dataTable, T, elevatorTable);
            listOfBottomLineMValues = calculations.calculateLineMValues(bottomLineTable);
            listOfBottomLineAValues = calculations.calculateLineAValues(bottomLineTable, listOfBottomLineMValues);
            listOfTopLineMValues = calculations.calculateLineMValues(topLineTable);
            listOfTopLineAValues = calculations.calculateLineAValues(topLineTable, listOfTopLineMValues);
            forecastTopLineMValue = calculations.getForecastValue(listOfTopLineMValues, Alpha);
            forecastBottomLineMValue = calculations.getForecastValue(listOfBottomLineMValues, Alpha);
            forecastTopLineAValue = calculations.getForecastValue(listOfTopLineMValues, Alpha);
            forecastBottomLineAValue = calculations.getForecastValue(listOfBottomLineMValues, Alpha);
            listOfMValues = calculations.calculateMValues(elevatorTable);
            listOfAValues = calculations.calculateAValues(elevatorTable, listOfMValues);
            forecastMValue = calculations.getForecastValue(listOfMValues, Alpha);
            forecastAValue = calculations.getForecastValue(listOfAValues, Alpha);

            foreach (Double value in listOfBottomLineMValues)
            {
                listBox13.Items.Add(value);
            }
            foreach (Double value in listOfTopLineMValues)
            {
                listBox15.Items.Add(value);
            }
            foreach (Double value in listOfMValues)
            {
                listBox14.Items.Add(value);
            }
            listBox13.Items.Add(forecastBottomLineMValue.Last());
            listBox14.Items.Add(forecastMValue.Last());
            listBox15.Items.Add(forecastTopLineMValue.Last());
            for (int i = 0; i < listBox13.Items.Count; i++)
            {
                listBox16.Items.Add(Math.Abs(Convert.ToDouble(listBox13.Items[i]) - Convert.ToDouble(listBox15.Items[i])));
            }
            for (int i = 0; i < listBox14.Items.Count; i++)
            {
                listBox17.Items.Add(Math.Abs(Convert.ToDouble(listBox14.Items[i]) - Convert.ToDouble(listBox14.Items[0])));
            }
            for (int i = 0; i < listBox17.Items.Count; i++)
            {
                if (Convert.ToDouble(listBox17.Items[i]) < (Convert.ToDouble(listBox16.Items[i]) / 2))
                {
                    listBox18.Items.Add("В пределе");
                }
                else if (Convert.ToDouble(listBox17.Items[i]) == (Convert.ToDouble(listBox16.Items[i]) / 2))
                {
                    listBox18.Items.Add("Точка бифуркации");
                }
                else listBox18.Items.Add("Выход за границу");
            }
        }

        private void secondLevel()
        {
            try
            {
                if (initProject.getBuildingCount() == null)
                {
                    MessageBox.Show("Не указано количество объектов");
                    throw new NullBuildingsCountException("Не указано количество объектов");
                }
                buildingCount = Convert.ToInt32(initProject.getBuildingCount());
                switch (buildingCount)
                {
                    case 0:
                        break;
                    case 1:
                        panel1.Show();
                        panelShowedCount = 1;
                        break;
                    case 2:
                        panel1.Show();
                        panel2.Show();
                        panelShowedCount = 2;
                        break;
                    case 3:
                        panel1.Show();
                        panel2.Show();
                        panel3.Show();
                        panelShowedCount = 3;
                        break;
                    case 4:
                        panel1.Show();
                        panel2.Show();
                        panel3.Show();
                        panel4.Show();
                        panelShowedCount = 4;
                        break;
                    case 5:
                        panel1.Show();
                        panel2.Show();
                        panel3.Show();
                        panel4.Show();
                        panel5.Show();
                        panelShowedCount = 5;
                        break;
                    case 6:
                        panel1.Show();
                        panel2.Show();
                        panel3.Show();
                        panel4.Show();
                        panel5.Show();
                        panel6.Show();
                        panelShowedCount = 6;
                        break;
                }


                markCount = Convert.ToInt32(initProject.getMarkCount());
                Double value = Math.Floor(Convert.ToDouble(markCount / Convert.ToDouble(buildingCount)));
                needMarkCount = Convert.ToInt32(value);
                hintLabelForMarks = Convert.ToString(needMarkCount);
                label27.Text = "Распределите на каждый\nобъект по " + hintLabelForMarks.ToString() + " марки";
                for (int i = 1; i <= markCount; i++)
                {
                    listBox19.Items.Add(i);
                }
            }
            
            catch (NullBuildingsCountException ex)
            {

            }
        }

        private void loadObjectDiagram()
        {
            objectDiagramPicture.Load(initProject.getPathToObjectDiagramRoot());
            objectDiagramPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            objectDiagramPicture2.Load(initProject.getPathToObjectDiagramRoot());
            objectDiagramPicture2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void showTAndAValues()
        {
            this.toolStripTextBox1.Text = "T: " + initProject.getValueOfT();
            this.toolStripTextBox2.Text = "a: " + initProject.getValueOfa();
            initTAndAlphaValues();
        }

        private void initTAndAlphaValues()
        {
            Alpha = Convert.ToDouble(toolStripTextBox2.Text.Split(' ')[1]);
            T = Convert.ToDouble(toolStripTextBox1.Text.Split(' ')[1]);
        }
        public void updateTAndAlphaValues(TextBox textBox, TextBox textBox2, TextBox textBox3)
        {
            T = Convert.ToDouble(textBox.Text);
            Alpha = Convert.ToDouble(textBox2.Text);
            buildingCount = Convert.ToInt32(textBox3.Text);
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            
        }

        private void showResponseFunctionSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Функция отклика";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addLine(listOfMValues, listOfAValues, functionDiagrams, serieName, listBox1, listBox2);
        }
        private void forecastResponseFunctionSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastFunction(serieName, forecastMValue, forecastAValue, functionDiagrams, listBox3, listBox4);
        }

        private void bottomLineSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Нижняя граница";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addLine(listOfBottomLineMValues, listOfBottomLineAValues, functionDiagrams, serieName, listBox5, listBox6);
        }
        private void forecastBottomValues_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение для нижней границы";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastFunction(serieName, forecastBottomLineMValue, forecastBottomLineAValue, functionDiagrams, listBox7, listBox8);
        }

        private void topLineSelectBox_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Верхняя граница";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addLine(listOfTopLineMValues, listOfTopLineAValues, functionDiagrams, serieName, listBox9, listBox10);
        }

        private void forecastTopLineValues_CheckedChanged(object sender, EventArgs e)
        {
            String serieName = "Прогнозное значение для верхней границы";
            if (functionDiagrams.Series.IndexOf(serieName) != -1) chartDiagramService.removeLine(functionDiagrams, serieName);
            else chartDiagramService.addforecastFunction(serieName, forecastTopLineMValue, forecastTopLineAValue ,functionDiagrams, listBox11, listBox12);
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
            resizeControl(forecastBottomLineSelectBoxRectangle, forecastBottomLineValues);
            resizeControl(forecastTopLineSelectBoxRectangle, forecastTopLineValues);
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
            forecastBottomLineSelectBoxRectangle = new Rectangle(forecastBottomLineValues.Location.X, forecastBottomLineValues.Location.Y, forecastBottomLineValues.Width, forecastBottomLineValues.Height); ;
            forecastTopLineSelectBoxRectangle = new Rectangle(forecastTopLineValues.Location.X, forecastTopLineValues.Location.Y, forecastTopLineValues.Width, forecastTopLineValues.Height); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox19.Items.Count > 0)
            {
                listBox20.Items.Add(listBox19.Items[0]);
                listBox19.Items.Remove(listBox19.Items[0]);
                chechMarkCountIsCorrect();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox20.Items.Count > 0)
            {
                listBox19.Items.Insert(0, listBox20.Items[listBox20.Items.Count - 1]);
                listBox20.Items.Remove(listBox20.Items[listBox20.Items.Count - 1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox19.Items.Count > 0)
            {
                listBox21.Items.Add(listBox19.Items[0]);
                listBox19.Items.Remove(listBox19.Items[0]);
                chechMarkCountIsCorrect();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox21.Items.Count > 0)
            {
                listBox19.Items.Insert(0, listBox21.Items[listBox21.Items.Count - 1]);
                listBox21.Items.Remove(listBox21.Items[listBox21.Items.Count - 1]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox19.Items.Count > 0)
            {
                listBox22.Items.Add(listBox19.Items[0]);
                listBox19.Items.Remove(listBox19.Items[0]);
                chechMarkCountIsCorrect();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox22.Items.Count > 0)
            {
                listBox19.Items.Insert(0, listBox22.Items[listBox22.Items.Count - 1]);
                listBox22.Items.Remove(listBox22.Items[listBox22.Items.Count - 1]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox19.Items.Count > 0)
            {
                listBox23.Items.Add(listBox19.Items[0]);
                listBox19.Items.Remove(listBox19.Items[0]);
                chechMarkCountIsCorrect();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox23.Items.Count > 0)
            {
                listBox19.Items.Insert(0, listBox23.Items[listBox23.Items.Count - 1]);
                listBox23.Items.Remove(listBox23.Items[listBox23.Items.Count - 1]);
            }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox24.Items.Count > 0)
            {
                listBox19.Items.Insert(0, listBox24.Items[listBox24.Items.Count - 1]);
                listBox24.Items.Remove(listBox24.Items[listBox24.Items.Count - 1]);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox19.Items.Count > 0)
            {
                listBox24.Items.Add(listBox19.Items[0]);
                listBox19.Items.Remove(listBox19.Items[0]);
                chechMarkCountIsCorrect();
            }

        }

        private void secondLevelStart()
        {
            MessageBox.Show("Отлично, переходим к расчетам");
        }

        private void chechMarkCountIsCorrect()
        {
            switch (panelShowedCount)
            {
                case 1:
                    if (listBox20.Items.Count == needMarkCount)
                    {
                        secondLevelStart();
                    }
                    break;
                case 2:
                    if ((listBox20.Items.Count == needMarkCount) & (listBox21.Items.Count == needMarkCount))
                    {
                        secondLevelStart();
                    }
                    break;
                case 3:
                    if ((listBox20.Items.Count == needMarkCount) & (listBox21.Items.Count == needMarkCount) & (listBox22.Items.Count == needMarkCount))
                    {
                        secondLevelStart();
                    }
                    break;
                case 4:
                    if ((listBox20.Items.Count == needMarkCount) & (listBox21.Items.Count == needMarkCount) & (listBox22.Items.Count == needMarkCount) & (listBox23.Items.Count == needMarkCount))
                    {
                        secondLevelStart();
                    }
                    break;
                case 5:
                    if ((listBox20.Items.Count == needMarkCount) & (listBox21.Items.Count == needMarkCount) & (listBox22.Items.Count == needMarkCount) & (listBox23.Items.Count == needMarkCount) & (listBox24.Items.Count == needMarkCount))
                    {
                        secondLevelStart();
                    }
                    break;
                case 6:
                    if ((listBox20.Items.Count == needMarkCount) & (listBox21.Items.Count == needMarkCount) & (listBox22.Items.Count == needMarkCount) & (listBox23.Items.Count == needMarkCount) & (listBox24.Items.Count == needMarkCount) & (listBox25.Items.Count == needMarkCount))
                    {
                        secondLevelStart();
                    }
                    break;
            }
        }

        private void elevatorTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int currentColumn =  e.ColumnIndex;
            int currentRow = e.RowIndex;
            Double newValue = Convert.ToDouble(elevatorTable.Rows[currentRow].Cells[currentColumn].Value);
            elevatorTable = db.updateValue(elevatorTable, currentColumn, currentRow, newValue);
        }

        private void AddNewRow_Click(object sender, EventArgs e)
        {
            elevatorTable.Rows.Add();
            
            Double delta = 0;
            Double averageDelta = 0;
            Double newCellValue = 0;
            int newRow = elevatorTable.RowCount;
            Random random= new Random();
            elevatorTable.Rows[newRow-1].Cells[0].Value = Convert.ToInt32(elevatorTable.Rows[newRow-2].Cells[0].Value) + 1;
            db.addNewRow(Convert.ToDouble(elevatorTable.Rows[newRow - 1].Cells[0].Value));
            for (int i = 1; i < elevatorTable.Columns.Count; i++)
            {
                
                for (int j = 0; j < elevatorTable.Rows.Count-1; j++)
                {
                    if (Convert.ToDouble(elevatorTable.Rows[j + 1].Cells[i].Value) != 0) {
                        delta = Math.Abs(Convert.ToDouble(elevatorTable.Rows[j].Cells[i].Value) - Convert.ToDouble(elevatorTable.Rows[j + 1].Cells[i].Value));
                    }
                    
                    averageDelta += delta;
                    delta = 0;
                }
                
                averageDelta /= elevatorTable.Rows.Count;
                newCellValue = random.NextDouble() * (averageDelta - (-averageDelta)) + averageDelta;
                elevatorTable.Rows[newRow-1].Cells[i].Value = Math.Round(newCellValue + Convert.ToDouble(elevatorTable.Rows[newRow-2].Cells[i].Value), 4);
                db.addNewValuesInRow(i, newRow-1, Convert.ToDouble(elevatorTable.Rows[newRow - 1].Cells[i].Value));
                averageDelta = 0;
            }
            
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            firstLevel();
            
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            secondLevel();
        }
        public class NullBuildingsCountException : Exception
        {
            public NullBuildingsCountException(string message)
                : base(message) { }

        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.initValues(T, Alpha, buildingCount);
            settingsForm.ShowDialog();
        }
    }

}
