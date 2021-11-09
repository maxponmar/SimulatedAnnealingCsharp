using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    internal class Repository
    {
        public static List<City> cities = new List<City>();
        public static void AddCity(City city)
        {
            cities.Add(city);
        }
        public static City GetCity(int index)
        {
            return cities[index];
        }
        public static int GetNumberOfCities()
        {
            return cities.Count;
        }
    }
}
