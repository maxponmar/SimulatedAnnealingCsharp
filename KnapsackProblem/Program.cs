using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ejemplo KnapSack

            // Objetos para guardar
            int[] pesos = { 1, 4, 2, 5, 7 };
            int[] valores = { 10, 14, 5, 18, 3 };

            // Objetos que se aceptan en la mochila
            // false indica que se descarta, true indica que se acepta en la mochila
            // NOTA: Este es un estado inicial, no es la mejor solucion.
            bool[] aceptados = {false, false, false, false, false};

            // Peso maximo que la mochila puede soportar
            int pesoMaximoDeMochila = 10;

            // Por ejemplo,
            // El objeto 2 tiene un peso de 4 (pesos[1])
            // **los indices comienzan en 0 por lo que en el indice 1 esta el segundo elemento)**
            // y un valor de 14 (valores[1])

            // El objeto 5 tiene un peso de 7 (pesos[4]) y un valor de 3 (valores[7])

            // NOTA: este problema tiene una complejidad de O(2^n)  (donde n es el numero de objetos)
            // por lo que resolverlo por fuerza bruta conlleva mucho tiempo, por eso
            // el recocido simulado u otros algoritmos metaheuristicos son una mejor opcion para resolver el prbolema
            // aceptable y sin que lleve mucho tiempo de ejecucion.
            // NOTA: Por ejemplo, si se tienen 30 objetos habrian 1,073,741,824. Con 50 objetos serian 1,125,899,906,842,624.

            RecocidoSimulado recocidoSimulado = new RecocidoSimulado();

            recocidoSimulado.CorrerAlgoritmo(aceptados, pesos, valores, pesoMaximoDeMochila);

            Console.ReadLine();
        }
    }
}
