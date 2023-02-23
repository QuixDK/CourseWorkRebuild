using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkRebuild
{
    internal class Calculations
    {

        public Calculations() 
        {
            
        }

        public List<Double> calculateMValues(DataGridView elevatorTable)
        {
            Double M = 0;

            List<Double> values = new List<Double>();

            List<Double> listOfMValues = new List<Double> ();

            for (int i = 0; i < elevatorTable.Rows.Count; i++)
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
                M = 0;
                values.Clear();

            }
            return listOfMValues;

        }

        public List<Double> calculateLineMValues(DataGridView elevatorTable)
        {
            Double M = 0;

            List<Double> values = new List<Double>();

            List<Double> listOfMValues = new List<Double>();

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
                M = 0;
                values.Clear();

            }
            return listOfMValues;

        }
        public List<Double> calculateLineAValues(DataGridView elevatorTable, List<Double> listOfMValues)
        {
            Double calculateAcos = 0;
            Double calculateDegree = 0;
            Double summPr = 0;
            Double firstValue = 0;
            Double secondValue = 0;
            List<Double> listOfAlphaValues = new List<Double>();
            listOfAlphaValues.Add(0);

            for (int i = 0; i < elevatorTable.Rows.Count - 2; i++)
            {
                summPr = 0;
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    firstValue = Convert.ToDouble(elevatorTable.Rows[0].Cells[j].Value);
                    secondValue = Convert.ToDouble(elevatorTable.Rows[i + 1].Cells[j].Value);
                    summPr += firstValue * secondValue;
                }
                summPr /= listOfMValues[0];
                summPr /= listOfMValues[i + 1];
                calculateAcos = Math.Acos(summPr);
                calculateDegree = 180 * calculateAcos / Math.PI;
                listOfAlphaValues.Add(calculateDegree);

            }
            return listOfAlphaValues;

        }


        public List<Double> calculateAValues(DataGridView elevatorTable, List<Double> listOfMValues)
        {
            Double calculateAcos = 0;
            Double calculateDegree = 0;
            Double summPr = 0;
            Double firstValue = 0;
            Double secondValue = 0;
            List<Double> listOfAlphaValues = new List<Double>();
            listOfAlphaValues.Add(0);

            for (int i = 0; i < elevatorTable.Rows.Count -1; i++)
            {
                summPr = 0;
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    firstValue = Convert.ToDouble(elevatorTable.Rows[0].Cells[j].Value);
                    secondValue = Convert.ToDouble(elevatorTable.Rows[i + 1].Cells[j].Value);
                    summPr += firstValue * secondValue;
                }
                summPr /= listOfMValues[0];
                summPr /= listOfMValues[i + 1];
                calculateAcos = Math.Acos(summPr);
                calculateDegree = 180 * calculateAcos / Math.PI;
                listOfAlphaValues.Add(calculateDegree);

            }
            return listOfAlphaValues;

        }

        public List<Double> getForecastValue(List<Double> listOfValues, Double a)
        {
            List<Double> forecastValues = new List<Double>();
            Double summValues = 0;
            for (int i = 0; i < listOfValues.Count; i++)
            {
                summValues += listOfValues[i];
            }
            summValues /= listOfValues.Count;
            Double value = a * listOfValues[0] + (1 - a) * summValues;
            forecastValues.Add(value);
            for (int i = 1; i < listOfValues.Count; i++)
            {
                value = 0;
                value = a * listOfValues[i] + (1 - a) * forecastValues[i - 1];
                forecastValues.Add(value);
            }
            value = 0;
            summValues = 0;
            for (int i = 0; i < forecastValues.Count; i++)
            {
                summValues += forecastValues[i];
            }
            summValues /= forecastValues.Count;
            value = a * forecastValues.Average() + (1 - a) * forecastValues.Last();
            forecastValues.Add(value);

            return forecastValues;
        }

        public DataGridView calculateBottomLine(DataTable dataTable, Double T, DataGridView elevatorTable)
        {
            DataGridView bottomLineTable = calculateNewTable(dataTable);
            
            for (int i = 0; i < elevatorTable.Rows.Count; i++)
            {
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    bottomLineTable.Rows[i].Cells[j].Value = Convert.ToDouble(elevatorTable.Rows[i].Cells[j].Value) - T;
                }
            }
            return bottomLineTable;
        }
        public DataGridView calculateTopLine(DataTable dataTable, Double T, DataGridView elevatorTable)
        {
            DataGridView topLineTable = calculateNewTable(dataTable);
            
            for (int i = 0; i < elevatorTable.Rows.Count; i++)
            {
                for (int j = 1; j < elevatorTable.ColumnCount; j++)
                {
                    topLineTable.Rows[i].Cells[j].Value = Convert.ToDouble(elevatorTable.Rows[i].Cells[j].Value) + T;
                }
            }
            return topLineTable;
        }

        private DataGridView calculateNewTable(DataTable dataTable)
        {
            DataGridView newTable = new DataGridView();
            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                String ColName = dataTable.Columns[column].ColumnName;
                newTable.Columns.Add(ColName, ColName);
                newTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                newTable.Rows.Add(dataTable.Rows[row].ItemArray);
            }
            return newTable;
        }


    }
}
