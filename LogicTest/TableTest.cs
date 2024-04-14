using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    internal class TableTest
    {
        [TestMethod]
        public void TableTestMethod()
        {
            Table table = new Table(150,200);
            table.AddBalls(2);
            double prev_x = table.balls[0].x;
            double prev_y = table.balls[0].y;

            table.MovingBalls();

            Assert.AreNotEqual(prev_x, table.balls[0].x);
            Assert.AreNotEqual(prev_y, table.balls[0].y);
            Assert.AreEqual(table.balls.Count, 2);
        }
    }
}
