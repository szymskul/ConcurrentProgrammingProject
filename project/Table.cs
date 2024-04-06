using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class TableInterface
    {
        public abstract int height { get; set; }
        public abstract int widht { get; set; }

        public static TableInterface createBoard(int h, int w)
        {
            return new Table(h, w);
        }

        public class Table : TableInterface
        {
            public override int height { get; set; }
            public override int widht { get; set; }
            public Table(int w, int h)
            {
                this.widht = w;
                this.height = h;
            }
        }
    }
}
