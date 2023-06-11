using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Aplication
{
    public class NumericComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null || y == null)
            {
                return 0;
            }

            if (int.TryParse(x.ToString(), out int xValue) && int.TryParse(y.ToString(), out int yValue))
            {
                return xValue.CompareTo(yValue);
            }

            return 0;
        }
    }

}
