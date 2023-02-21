using System;
using System.Collections.Generic;
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
            List<Double> listOfAlphaValues = new List<Double>();
            listOfAlphaValues.Add(0);
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



        public List<Double> getForecastMValue(List<Double> listOfMValues, Double a)
        {
            
            List<Double> forecastMValues = new List<Double>();
            Double summMValues = 0;
            for (int i = 0; i < listOfMValues.Count; i++)
            {
                summMValues += listOfMValues[i];
            }
            summMValues /= listOfMValues.Count;
            Double m = a * listOfMValues[0] + (1 - a) * summMValues;
            forecastMValues.Add(m);

            for (int i = 1; i < listOfMValues.Count; i++)
            {
                m = 0;
                m = a * listOfMValues[i] + (1- a) * forecastMValues[i-1];
                forecastMValues.Add(m);
            }

            return forecastMValues;
        }

        public List<Double> getForecastAValue(List<Double> listOfAValues, Double a)
        {
            List<Double> forecastAValues = new List<Double>();
            Double summAValues = 0;
            for (int i = 0; i < listOfAValues.Count; i++)
            {
                summAValues += listOfAValues[i];
            }
            summAValues /= listOfAValues.Count;
            Double A = a * listOfAValues[0] + (1 - a) * summAValues;
            forecastAValues.Add(A);
            for (int i = 1; i < listOfAValues.Count; i++)
            {
                A = 0;
                A = a * listOfAValues[i] + (1 - a) * forecastAValues[i - 1];
                forecastAValues.Add(A);
            }

            return forecastAValues;
        }

        

    }
}
