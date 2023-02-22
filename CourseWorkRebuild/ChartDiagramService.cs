using CourseWorkRebuild;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public class ChartDiagramService
{
	Calculations calculations = new Calculations();

    public Chart addLine(List<Double> listOfMValues, List<Double> listOfAValues, DataGridView dTable, Chart functionDiagrams, String serieName, ListBox listBox, ListBox listBox2)
    {
        listBox.Items.Clear();
        listBox2.Items.Clear();
        listOfMValues = calculations.calculateLineMValues(dTable);
        listOfAValues = calculations.calculateLineAValues(dTable, listOfMValues);
        foreach (Double value in listOfMValues)
        {
            listBox.Items.Add(value);
        }
        foreach (Double value in listOfAValues)
        {
            listBox2.Items.Add(value);
        }
        functionDiagrams.Series.Add(serieName);
        functionDiagrams.Series[serieName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < listOfMValues.Count; i++)
        {
            functionDiagrams.Series[serieName].Points.AddXY(listOfMValues[i], listOfAValues[i]);
        }
        return functionDiagrams;
    }
    public Chart removeLine(Chart functionDiagrams, String serieName)
    {
       functionDiagrams.Series[serieName].Points.Clear();
       functionDiagrams.Series.Remove(functionDiagrams.Series[serieName]);
       return functionDiagrams;
    }

    public Chart addResponseFunction(List<Double> listOfMValues, List<Double> listOfAlphaValues, DataGridView dTable, Chart functionDiagrams)
    {
        functionDiagrams.ChartAreas[0].AxisX.Title = "M";
        functionDiagrams.ChartAreas[0].AxisY.Title = "Alpha";
        functionDiagrams.ChartAreas[0].AxisX.Maximum = listOfMValues.Max();
        functionDiagrams.ChartAreas[0].AxisX.Minimum = listOfMValues.Min();
        functionDiagrams.ChartAreas[0].AxisY.Maximum = listOfAlphaValues.Max();
        functionDiagrams.ChartAreas[0].AxisY.Minimum = listOfAlphaValues.Min();

        functionDiagrams.Series.Add("Функция отклика");
        functionDiagrams.Series["Функция отклика"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < listOfMValues.Count; i++)
        {
            functionDiagrams.Series["Функция отклика"].Points.AddXY(listOfMValues[i], listOfAlphaValues[i]);
        }
        return functionDiagrams;

    }

    public Chart addforecastResponseFunction(List<Double> listOfMValues, List<Double> listOfAlphaValues, Chart functionDiagrams, ToolStripTextBox toolStripTextBox2, ListBox listBox, ListBox listBox2)
    {
        List<Double> forecastMValue = new List<Double>();
        List<Double> forecastAValue = new List<Double>();
        String a = toolStripTextBox2.Text.Split(' ')[1];
        forecastMValue = calculations.getForecastMValue(listOfMValues, Convert.ToDouble(a));
        forecastAValue = calculations.getForecastAValue(listOfAlphaValues, Convert.ToDouble(a));

        functionDiagrams.Series.Add("Прогнозное значение");
        functionDiagrams.Series["Прогнозное значение"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        functionDiagrams.Series["Прогнозное значение"].Points.AddXY(forecastMValue.Last(), forecastAValue.Last());
        foreach (Double value in forecastMValue)
        {
            listBox.Items.Add(value);
        }
        foreach (Double value in forecastAValue)
        {
            listBox2.Items.Add(value);
        }
        return functionDiagrams;
    }

    public Chart addforecastBottomFunction(DataTable dataTable, ToolStripTextBox toolStripTextBox1, Chart functionDiagrams, ToolStripTextBox toolStripTextBox2, DataGridView elevatorTable, ListBox listBox, ListBox listBox2)
    {
        listBox.Items.Clear();
        listBox2.Items.Clear();
        List<Double> listOfBottomMValues = calculations.calculateLineMValues(calculations.calculateBottomLine(dataTable, toolStripTextBox1, elevatorTable));
        List<Double> listOfBottomAlphaValues = calculations.calculateLineAValues(calculations.calculateBottomLine(dataTable, toolStripTextBox1, elevatorTable), listOfBottomMValues);
        List<Double> forecastMValue = new List<Double>();
        List<Double> forecastAValue = new List<Double>();
        String a = toolStripTextBox2.Text.Split(' ')[1];
        forecastMValue = calculations.getForecastMValue(listOfBottomMValues, Convert.ToDouble(a));
        forecastAValue = calculations.getForecastAValue(listOfBottomAlphaValues, Convert.ToDouble(a));

        functionDiagrams.Series.Add("Прогнозное значение для нижней границы");
        functionDiagrams.Series["Прогнозное значение для нижней границы"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        functionDiagrams.Series["Прогнозное значение для нижней границы"].Points.AddXY(forecastMValue.Last(), forecastAValue.Last());
        foreach (Double value in forecastMValue)
        {
            listBox.Items.Add(value);
        }
        foreach (Double value in forecastAValue)
        {
            listBox2.Items.Add(value);
        }
        return functionDiagrams;
    }

    public Chart addforecastTopFunction(DataTable dataTable, ToolStripTextBox toolStripTextBox1, Chart functionDiagrams, ToolStripTextBox toolStripTextBox2, DataGridView elevatorTable, ListBox listBox, ListBox listBox2)
    {
        listBox.Items.Clear();
        listBox2.Items.Clear();
        List<Double> listOfBottomMValues = calculations.calculateLineMValues(calculations.calculateTopLine(dataTable, toolStripTextBox1, elevatorTable));
        List<Double> listOfBottomAlphaValues = calculations.calculateLineAValues(calculations.calculateTopLine(dataTable, toolStripTextBox1, elevatorTable), listOfBottomMValues);
        List<Double> forecastMValue = new List<Double>();
        List<Double> forecastAValue = new List<Double>();
        String a = toolStripTextBox2.Text.Split(' ')[1];
        forecastMValue = calculations.getForecastMValue(listOfBottomMValues, Convert.ToDouble(a));
        forecastAValue = calculations.getForecastAValue(listOfBottomAlphaValues, Convert.ToDouble(a));

        functionDiagrams.Series.Add("Прогнозное значение для верхней границы");
        functionDiagrams.Series["Прогнозное значение для верхней границы"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
        functionDiagrams.Series["Прогнозное значение для верхней границы"].Points.AddXY(forecastMValue.Last(), forecastAValue.Last());
        foreach (Double value in forecastMValue)
        {
            listBox.Items.Add(value);
        }
        foreach (Double value in forecastAValue)
        {
            listBox2.Items.Add(value);
        }
        return functionDiagrams;
    }

}
