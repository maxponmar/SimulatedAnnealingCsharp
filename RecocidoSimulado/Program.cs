using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecocidoSimulado
{
    class Program
    {
        static void Main(string[] args)
        {
            double temperatura = 1000;
            double temperaturaFinal = 10;

            // Solucion inicial aleatoria'
            int[] solucion = { 0, 0, 0, 0, 0 };

            // Generador de numeros aleatoreos
            Random aleatorio = new Random();

            while (temperatura > temperaturaFinal)
            {
                bool encontrado = false;

                // Copiamos la solucion en mejor vecino
                int[] mejorVecino = (int[])solucion.Clone();

                // Calculo el costo de la funcion con el vecino actual
                double costoActual = FuncionDeCosto(BinarioADecimal(mejorVecino));

                // Copiamos el vecino actual para modificarlo
                int[] vecinoCopia = (int[])solucion.Clone();

                for (int i = 0; i < solucion.Length && !encontrado; i++)
                {
                    // Encontramos los vecinos con una funcion que altera los bits 
                    // por ejemplo los vecinos de {0,0,1,0,1} serian:
                    // {1,0,1,0,1}, {0,1,1,0,1}, {0,0,0,0,1}, {0,0,1,1,1} y {0,0,1,0,0}
                    if (vecinoCopia[i] == 1)
                        vecinoCopia[i] = 0;
                    else
                        vecinoCopia[i] = 1;

                    // Comparamos el costo del vecino actual con el nuevo vecino y lo aceptamos
                    // si el coste es mayor o la probabilidad de aceptacion es mayor, lo aceptamos
                    double costoVecino = FuncionDeCosto(BinarioADecimal(vecinoCopia));
                    if (costoActual < costoVecino || ProbabilidadDeAceptacion(costoActual, costoVecino, temperatura) > aleatorio.NextDouble())
                    {
                        mejorVecino = vecinoCopia;
                        costoActual = FuncionDeCosto(BinarioADecimal(vecinoCopia));
                        encontrado = true;
                    }
                    vecinoCopia = new int[solucion.Length];
                    solucion = (int[])vecinoCopia.Clone();
                }
                solucion = mejorVecino;

                // Enfriamos la temperatura en cada iteracion
                temperatura *= 0.9;
            }

            // Mostramos los resultados por pantalla
            Console.Write("Numero binario: [");
            for (int i = 0; i < solucion.Length; i++)
            {
                Console.Write(solucion[i]);
            }
            Console.Write("]");
            double valor = BinarioADecimal(solucion);
            Console.WriteLine(", Valor en decimal: " + valor + ", Coste: " + FuncionDeCosto(valor));
            Console.ReadLine();
        }

        static public double FuncionDeCosto(double x)
        {
            // Realizamos el costo segun el problema propuesto
            double Resultado = Math.Pow(x, 3) - 60 * Math.Pow(x, 2) + 900 * x + 100;
            return Resultado;
        }

        static private double BinarioADecimal(int[] binario)
        {
            // Realizamos una funcion para convertir el array de binarios en un valor decimal
            double resultado = 0;
            int contador = 0;
            for (int i = binario.Length - 1; i >= 0 ; i--)
            {
                if (binario[i] == 1)
                {
                    resultado += Math.Pow(2, contador);
                }
                contador++;
            }
            return resultado;
        }

        static public double ProbabilidadDeAceptacion(double costoActual, double costoVecino, double temperatura)
        {
            // Si la nueva solucion es peor se calcula una probabilidad de aceptacion
            return Math.Exp((costoVecino - costoActual) / temperatura);
        }
    }
}
