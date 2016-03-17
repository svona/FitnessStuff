using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessStuff
{
    public static class ConversionHelper
    {
        public static decimal CM_to_IN(decimal cm)
        {
            return cm * 0.393701M;
        }

        public static decimal IN_to_CM(decimal inches)
        {
            return inches * 2.54000137161M;
        }

        public static decimal KG_to_pounds(decimal kg)
        {
            return kg * 2.20462M;
        }

        public static decimal Pounds_to_kg(decimal pounds)
        {
            return pounds * 0.453592M;
        }
    }
}