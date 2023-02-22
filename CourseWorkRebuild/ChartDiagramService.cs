using CourseWorkRebuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public class ChartDiagramService
{
	Calculations calculations = new Calculations();
	public ChartDiagramService()
	{
		
	}
    public Chart addTopLine(List<Double> listOfTopLineMValues, List<Double> listOfTopLineAValues, DataGridView dTable, Chart functionDiagrams)
    {

        listOfTopLineMValues = calculations.calculateMValues(dTable);
        listOfTopLineAValues = calculations.calculateAValues(dTable, listOfTopLineMValues);
        functionDiagrams.Series.Add("Верхняя граница");
        functionDiagrams.Series["Верхняя граница"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < listOfTopLineMValues.Count; i++)
        {
            functionDiagrams.Series["Верхняя граница"].Points.AddXY(listOfTopLineMValues[i], listOfTopLineAValues[i]);
        }
        return functionDiagrams;
    }
    public Chart removeTopLine(Chart functionDiagrams)
    {
       functionDiagrams.Series["Верхняя граница"].Points.Clear();
       functionDiagrams.Series.Remove(functionDiagrams.Series["Верхняя граница"]);
       return functionDiagrams;
    }

    public Chart addBottomLine(List<Double> listOfBottomLineMValues, List<Double> listOfBottomLineAValues, DataGridView dTable, Chart functionDiagrams)
    {
        listOfBottomLineMValues = calculations.calculateMValues(dTable);
        listOfBottomLineAValues = calculations.calculateAValues(dTable, listOfBottomLineMValues);
        functionDiagrams.Series.Add("Нижняя граница");
        functionDiagrams.Series["Нижняя граница"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < listOfBottomLineMValues.Count; i++)
        {
            functionDiagrams.Series["Нижняя граница"].Points.AddXY(listOfBottomLineMValues[i], listOfBottomLineAValues[i]);
        }
        return functionDiagrams;
    }
    public Chart removeBottomLine(Chart functionDiagrams)
    {
        functionDiagrams.Series["Нижняя граница"].Points.Clear();
        functionDiagrams.Series.Remove(functionDiagrams.Series["Нижняя граница"]);
        return functionDiagrams;
    }
    public Chart addResponseFunction(List<Double> listOfMValues, List<Double> listOfAlphaValues, DataGridView dTable, Chart functionDiagrams)
    {
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

    public Chart removeResponseFunction(Chart functionDiagrams)
    {
        functionDiagrams.Series["Функция отклика"].Points.Clear();
        functionDiagrams.Series.Remove(functionDiagrams.Series["Функция отклика"]);
        return functionDiagrams;
    }

    public Chart addforecastResponseFunction(List<Double> listOfMValues, List<Double> listOfAlphaValues, Chart functionDiagrams, ToolStripTextBox toolStripTextBox2)
    {
        List<Double> forecastMValue = new List<Double>();
        List<Double> forecastAValue = new List<Double>();
        String a = toolStripTextBox2.Text.Split(' ')[1];
        forecastMValue = calculations.getForecastMValue(listOfMValues, Convert.ToDouble(a));
        forecastAValue = calculations.getForecastAValue(listOfAlphaValues, Convert.ToDouble(a));

        functionDiagrams.Series.Add("Прогнозное значение");
        functionDiagrams.Series["Прогнозное значение"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < forecastMValue.Count - 1; i++)
        {
            functionDiagrams.Series["Прогнозное значение"].Points.AddXY(forecastMValue[i], forecastAValue[i]);
        }
        return functionDiagrams;
    }

    public Chart removeforecastResponseFunction(Chart functionDiagrams)
    {
        functionDiagrams.Series["Прогнозное значение"].Points.Clear();
        functionDiagrams.Series.Remove(functionDiagrams.Series["Прогнозное значение"]);
        return functionDiagrams;
    }

}
