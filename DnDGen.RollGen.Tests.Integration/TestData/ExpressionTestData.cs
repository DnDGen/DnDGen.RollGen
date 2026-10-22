using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.RollGen.Tests.Integration.TestData
{
    public class ExpressionTestData
    {
        public static IEnumerable<(string Expression, double Value)> Numbers =>
            [
                ("90210", 90210),
                ("4.2", 4.2),
                ("-8245", -8245),
                ("-10", -10),
                ("-2", -2),
                ("-1", -1),
                ("0", 0),
                ("1", 1),
                ("2", 2),
                ("10", 10),
                ("-0.9", -0.9),
            ];

        public static IEnumerable NumbersTestCases => Numbers.Select(n => new TestCaseData(n.Expression, n.Value));

        //INFO: Exploding dice have no limit on how many times they explode, so their individual rolls have no maximum count
        public static IEnumerable<(string Expression, double Minimum, double Maximum, int MinimumRolls, int MaximumRolls)> Expressions =>
            [
                ("4d6k3", 3, 18, 3, 3),
                ("3d6", 3, 18, 3, 3),
                ("3d6t1", 6, 18, 3, 3),
                ("3d6t1:2", 6, 18, 3, 3),
                ("1d1", 1, 1, 1, 1),
                ("10000d10000", 10_000, 100_000_000, 10_000, 10_000),
                ("1d20!", 1, 200, 1, int.MaxValue),
                ("3d4!e3", 3, 120, 3, int.MaxValue),
                ("4d3!t2k1", 1, 30, 1, 1),
                ("600d1337!t96:1336k78", 78, 1_042_860, 78, 78),
                ("1d2d3d4", 1, 24, 1, 1),
                ("2d3d4", 2, 24, 1, 1),
                ("(2d3)d4", 2, 24, 1, 1),
                ("2d(3d4)", 2, 24, 1, 1),
                ("(1d3+1)d(2d3)", 2, 24, 1, 1),
                ("1d2+3d4", 4, 14, 1, 1),
                ("1d8+3", 4, 11, 1, 1),
                ("2d6+1d4+3", 6, 19, 1, 1),
                ("1d6+1d6+1d6", 3, 18, 1, 1),
                ("5+3d4*2", 11, 29, 1, 1),
                ("9d10*11d12", 99, 11_880, 1, 1),
                ("0.5*4d6", 2, 12, 1, 1),
                ("4d6/2", 2, 12, 1, 1),
                //HACK: Subtracting or dividing by a roll inverts the potential minimum and maximum. Uncomment once that is fixed (#110)
                //("5d6-7d8", 5 - 7 * 8, 5 * 6 - 7, 1, 1),
                //("16d15/14d13", 16d / (14 * 13), 16d * 15 / 14, 1, 1),
                ("min(4d6,10)", 4, 10, 1, 1),
                ("max(1d2, 3d4)", 3, 12, 1, 1),
                ("avg(1d12, 2d6)", 1.5, 12, 1, 1),
                ("9266+90210", 99_476, 99_476, 1, 1),
            ];

        public static IEnumerable ExpressionsTestCases => Expressions.Select(e => new TestCaseData(e.Expression, e.Minimum, e.Maximum, e.MinimumRolls, e.MaximumRolls));
    }
}
