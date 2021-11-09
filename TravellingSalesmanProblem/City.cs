using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    internal class City
    {
        private int x;
        private int y;

        public City()
        {
            Random random = new Random();
            x = random.Next(100);
            y = random.Next(100);
        }

        public int X { get => x; set => x=value; }
        public int Y { get => y; set => y=value; }
        public double DistanceTo(City city)
        {
            int horizontalDistance = this.X - city.X;
            int verticalDistance = this.Y - city.Y;

            return Math.Sqrt(horizontalDistance*horizontalDistance + verticalDistance*verticalDistance);
        }

        public override string ToString()
        {
            return String.Format("[x={0}, y={1}]",X,y);
        }
    }
}
