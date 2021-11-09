using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    internal class SimulatedAnnealing
    {
        private SingleTour actualState;
        private SingleTour nextState;
        private SingleTour bestState;

        internal SingleTour BestState { get => bestState; }

        public void Simulate()
        {
            Random random = new Random();
            double temperature = Constants.MAX_TEMPERATURE;
            actualState = new SingleTour();
            actualState.GenerateIndividual();
            bestState = new SingleTour(actualState.Tour);

            Console.WriteLine(String.Format("Initial solution distance: {0}", actualState.GetDistance()));

            while (temperature > Constants.MIN_TEMPERATURE)
            {
                nextState = GenerateNeighborState(actualState);

                double currentEnergy = actualState.GetDistance();
                double neighborEnergy = nextState.GetDistance();
                
                if(AcceptanceProbability(currentEnergy, neighborEnergy, temperature) > random.NextDouble())
                {
                    actualState = new SingleTour(nextState.Tour);
                }
                int x = actualState.GetDistance();
                int y = bestState.GetDistance();

                if (actualState.GetDistance() < bestState.GetDistance())
                {
                    bestState = new SingleTour(actualState.Tour);
                }

                temperature *= (1-Constants.COOLING_RATE);
            }
        }
        private SingleTour GenerateNeighborState(SingleTour ActualState)
        {
            SingleTour newState = new SingleTour(ActualState.Tour);
            
            Random random = new Random();
            int randomIndex1 = random.Next(newState.GetTourSize());
            int randomIndex2 = random.Next(newState.GetTourSize());

            City? city1 = newState.GetCity(randomIndex1);
            City? city2 = newState.GetCity(randomIndex2);

            newState.SetCity(randomIndex1, city2);
            newState.SetCity(randomIndex2, city1);

            return newState;
        }

        private double AcceptanceProbability(double CurrentEnergy, double NeighborEnergy, double Temperature)
        {
            if (NeighborEnergy < CurrentEnergy)
                return 1;
            return Math.Exp((CurrentEnergy - NeighborEnergy) / Temperature);
        }
    }
}
