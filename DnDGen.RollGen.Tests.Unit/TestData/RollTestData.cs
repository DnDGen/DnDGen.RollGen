using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.RollGen.Tests.Unit.TestData
{
    public class RollTestData
    {
        //INFO: Each of these is exactly one roll, with nothing else in the string
        public static IEnumerable<string> SingleRolls =>
            [
                " 1 d 2 ",
                " 1 d 2 ! ",
                " 1 d 2 ! k 3 ",
                " 1 d 2 k 3 ",
                " 1 d 2 k 3 ! ",
                " d 2 ",
                "1d2",
                "1d2!",
                "1d2!k3",
                "1d2k3",
                "1d2k3!",
                "3d4!",
                "3d4!e3",
                "3d4!k2",
                "3d4e1e2k2",
                "3d4k2",
                "3d6!t1k2",
                "3d6t1",
                "3d6t1t2",
                "3d6t1t5",
                "4d3!k1t2",
                "4d3!t2k1",
                "4d3k1!t2",
                "4d3k1t2",
                "4d3k1t2!",
                "4d3t2!k1",
                "4d3t2k1",
                "4d3t2k1!",
                "4d6",
                "4d6!",
                "4d6k3",
                "92d66",
                "d2",
            ];

        public static IEnumerable SingleRollsTestCases => SingleRolls.Select(r => new TestCaseData(r));
    }
}
