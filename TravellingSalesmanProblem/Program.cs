// See https://aka.ms/new-console-template for more information
using TravellingSalesmanProblem;

Console.WriteLine("Hello, World!");

SimulatedAnnealing travelSalesManProblem = new SimulatedAnnealing();

for (int i = 0; i < 500; i++)
{
    City city = new City();
    Repository.AddCity(city);
}

travelSalesManProblem.Simulate();

Console.WriteLine("Best solution: " + travelSalesManProblem.BestState.GetDistance());
Console.WriteLine(travelSalesManProblem.BestState);