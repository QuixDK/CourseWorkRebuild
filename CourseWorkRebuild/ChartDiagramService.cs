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
    public Chart addLine(List<Double> listOfTopLineMValues, List<Double> listOfTopLineAValues, DataGridView dTable, Chart functionDiagrams, String serieName)
    {

        listOfTopLineMValues = calculations.calculateMValues(dTable);
        listOfTopLineAValues = calculations.calculateAValues(dTable, listOfTopLineMValues);
        functionDiagrams.Series.Add(serieName);
        functionDiagrams.Series[serieName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        for (int i = 0; i < listOfTopLineMValues.Count; i++)
        {
            functionDiagrams.Series[serieName].Points.AddXY(listOfTopLineMValues[i], listOfTopLineAValues[i]);
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

}
