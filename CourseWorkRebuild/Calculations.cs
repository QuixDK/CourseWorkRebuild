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

    }
}
