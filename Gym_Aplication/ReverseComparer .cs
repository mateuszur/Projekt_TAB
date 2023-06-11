using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Aplication
{
    public class ReverseComparer : IComparer
    {
        private readonly IComparer _baseComparer;

        public ReverseComparer(IComparer baseComparer)
        {
            _baseComparer = baseComparer;
        }

        public int Compare(object x, object y)
        {
            return _baseComparer.Compare(y, x); // Odwrócenie kolejności argumentów
        }
    }

}
