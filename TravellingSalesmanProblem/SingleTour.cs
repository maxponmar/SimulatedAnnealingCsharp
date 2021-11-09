using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    internal class SingleTour
    {
        private List<City?> tour;
        private int distance;

        internal List<City?> Tour { get => tour; }

        public SingleTour()
        {
            tour = new List<City?>();
            for (int i = 0; i < Repository.GetNumberOfCities(); i++)
            {
                tour.Add(null);
            }
        }
        public SingleTour(List<City?> cities)
        {
            tour = new List<City?>();
            foreach (City c in cities)
                tour.Add(c);
        }
        public City? GetCity(int index)
        {
            return tour[index];
        }
        public void SetCity(int index, City? city)
        {
            tour[index] = city;
        }
        public int GetTourSize()
        {
            return tour.Count;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (City city in tour)
            {
                sb.Append(city + " - ");
            }
            return sb.ToString();
        }
        public void GenerateIndividual()
        {
            for (int cityIndex = 0; cityIndex < Repository.GetNumberOfCities(); cityIndex++)
            {
                SetCity(cityIndex, Repository.GetCity(cityIndex));
            }
            tour.Shuffle();
        }
        public int GetDistance()
        {
            if (distance==0)
            {
                int tourDistance = 0;
                for (int cityIndex = 0; cityIndex < tour.Count; cityIndex++)
                {
                    City? fromCity = tour[cityIndex];
                    City? destinationCity = null;
                    if (cityIndex+1 < tour.Count)
                        destinationCity = tour[cityIndex+1];
                    else
                        destinationCity = tour[0];

                    tourDistance += (int)fromCity.DistanceTo(destinationCity);
                }
                distance = tourDistance;
            }
            return distance;
        }
    }
}
