using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing_Udemy
{
    internal class SimulatedAnnealing
    {
        private Random RandomGenerator;
        // Actual state (state = x)
        private double currentCoordinateX;
        // Next state (neighboor)
        private double nextCoordinateX;
        // Solution
        private double bestCoordinateX;

        public SimulatedAnnealing()
        {
            RandomGenerator = new Random();
            // Initial State
            currentCoordinateX = 0;
        }

        public void FindOptimum()
        {
            double Temperature = Constants.MAX_TEMPERATURE;

            while (Temperature > Constants.MIN_TEMPERATURE)
            {
                nextCoordinateX = GenerateRandomNeighboor();

                // Calculate energy values
                double CurrentEnergy = GetEnergy(currentCoordinateX);
                double NewEnergy = GetEnergy(nextCoordinateX);

                if(AcceptanceProbability(CurrentEnergy, NewEnergy, Temperature) > RandomGenerator.NextDouble())
                {
                    currentCoordinateX = nextCoordinateX;
                }
                
                if(f(currentCoordinateX)<f(bestCoordinateX))
                {
                    bestCoordinateX = currentCoordinateX;
                }

                Temperature *= (1 - Constants.COOLING_RATE);
            }
            Console.WriteLine("Global max is: x="+ bestCoordinateX + " f(x)=" + f(bestCoordinateX));
        }

        private double GenerateRandomNeighboor()
        {
            // Return a neightboor in the range [-2,2] <- problem
           return RandomGenerator.NextDouble()*(Constants.MAX_CORDINATE_X - Constants.MIN_CORDINATE_X) + Constants.MIN_CORDINATE_X;
        }

        private double GetEnergy(double x)
        {
            return f(x);
        }

        private double f(double x)
        {
            return (x-0.3)*(x-0.3)*(x-0.3)-5*x+x*x-2;
        }

        // Metropolis Function
        private double AcceptanceProbability(double ActualEnergy, double NewEnergy, double Temperature)
        {
            // If the new state is better, accept it
            if (NewEnergy < ActualEnergy)
                return 1;
            // If the new state is worse, calculate an acceptance probability
            // For small values of T => We accept worse solutions with lower probability
            return Math.Exp((ActualEnergy - NewEnergy) / Temperature);
        }
    }
}
