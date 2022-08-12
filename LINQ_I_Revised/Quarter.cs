using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_I_Revised
{
    public class Quarter
    {
        public static long GetQuarters(DateTime dt1, DateTime dt2)
        {
            double d1Quarter = GetQuarter(dt1.Month);
            double d2Quarter = GetQuarter(dt2.Month);
            double d1 = d2Quarter - d1Quarter;
            double d2 = (4 * (dt2.Year - dt1.Year));
            return Round(d1 + d2);
        }

        public static DateTime GetQuarterStartDate (DateTime dt)
        {
            if (dt.Month <= 3)
                return new DateTime(dt.Year, 1, 1);
            if (dt.Month <= 6)
                return new DateTime(dt.Year, 4, 1);
            if (dt.Month <= 9)
                return new DateTime(dt.Year, 7, 1);
            return new DateTime(dt.Year, 10, 1);
        }

        public static DateTime GetQuarterEndDate(DateTime dt)
        {
            if (dt.Month <= 3)
                return new DateTime(dt.Year, 3, 31);
            if (dt.Month <= 6)
                return new DateTime(dt.Year, 6, 30);
            if (dt.Month <= 9)
                return new DateTime(dt.Year, 9, 30);
            return new DateTime(dt.Year, 12, 31);
        }

        public static int GetQuarterFromDate(DateTime dt)
        {
            if (dt.Month <= 3)
                return 1;
            if (dt.Month <= 6)
                return 2;
            if (dt.Month <= 9)
                return 3;
            return 4;
        }

        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }

        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
    }
}
