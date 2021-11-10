using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    internal class RecocidoSimulado
    {       
        bool[] estadoActual;
        bool[] siguienteEstado;
        bool[] solucion;
        Random GeneradorAleatorio = new Random();

        public void CorrerAlgoritmo(bool[] EstadoInicial, int[] Pesos, int[]Valores)
        {            
            double temperatura = Constantes.TEMPERATURE_MAXIMA;

            // Se inicializan todos los estados, con el estado inicial
            estadoActual = (bool[])EstadoInicial.Clone();
            siguienteEstado = (bool[])EstadoInicial.Clone();
            solucion = (bool[])EstadoInicial.Clone();

            // Se inicializan los valores de la mochila, con el estado inicial
            Console.WriteLine("Estado inicial");
            int ValorDeMochilaSolucion = CalcularValorDeMochila(Pesos, Valores, estadoActual);
            int ValorDeMochilaActual = CalcularValorDeMochila(Pesos, Valores, estadoActual);
            int ValorDeMochilaVecino = CalcularValorDeMochila(Pesos, Valores, estadoActual);

            // Constante de iteracion para mostrar mensajes
            int iteracion = 1;
            // Inicia el algoritmo
            while (temperatura > Constantes.TEMPERATURE_MINIMA)
            {
                Console.WriteLine("ITERACION: " + iteracion);
                Console.WriteLine("==> Temperatura: " + temperatura);
                // Se genera un vecino aleatorio
                siguienteEstado = GenerarVecinoAleatorio(estadoActual);

                // Se calcula el valor de la mochila para el estado actual y el estado aleatorio (vecino)
                ValorDeMochilaActual = CalcularValorDeMochila(Pesos,Valores, estadoActual);
                Console.WriteLine("==> Valor de mochila actual: " + ValorDeMochilaActual);
                ValorDeMochilaVecino = CalcularValorDeMochila(Pesos, Valores, siguienteEstado);
                Console.WriteLine("==> Valor de mochila del vecino: " + ValorDeMochilaVecino);

                // Se determina si se acepta el vecino como siguiente solucion actual
                if (ProbabilidadDeAceptar(ValorDeMochilaActual, ValorDeMochilaVecino, temperatura) > GeneradorAleatorio.NextDouble())
                {
                    // el (bool[]) y .Clone() sirven para hacer una copia real, 
                    // .Clone() regresa un objeto y (bool[]) lo convierte en un arrreglo de booleanos nuevamente
                    estadoActual = (bool[])siguienteEstado.Clone();
                }

                // Se calcula el valor de la mochila para el estado actual y la solucion actual
                ValorDeMochilaActual = CalcularValorDeMochila(Pesos, Valores, estadoActual);
                ValorDeMochilaSolucion = CalcularValorDeMochila(Pesos, Valores, solucion);

                // Si el estado actual es mejor que la solucion, entonces la solucion se actualiza
                if (ValorDeMochilaActual > ValorDeMochilaSolucion)
                {
                    Console.WriteLine("Se actualizo la solucion: " + ValorDeMochilaSolucion);
                    solucion = (bool[])estadoActual.Clone();
                }

                // Se actualiza temperatura (se enfria)
                temperatura *= (1-Constantes.RAZON_ENFRIAMIENTO);

                // Se incremente en 1 la iteracion
                iteracion++;
            }

            Console.WriteLine("=====================");
            Console.WriteLine("La solucion es: ");
            Console.Write("[");
            for (int i = 0; i < solucion.Length; i++)
            {
                if (i < solucion.Length-1)
                {
                    Console.Write(solucion[i] + ", ");
                }
                else
                {
                    Console.Write(solucion[i]+"]\r\n");
                }
            }
            Console.WriteLine("Valor de mochila: " + ValorDeMochilaSolucion);
            Console.WriteLine("=====================");
        }

        private int CalcularValorDeMochila(int[] Pesos, int[] Valores, bool[] Aceptados)
        {
            int resultado = 0;
            if (Pesos.Length == Valores.Length && Pesos.Length == Aceptados.Length)
            {
                for (int i = 0; i < Pesos.Length; i++)
                {
                    if (Aceptados[i])
                    {
                        resultado += Pesos[i]*Valores[i];
                    }
                }
                //Console.WriteLine("=> Evaluacion de mochila: " + resultado);
            }
            else
            {
                Console.WriteLine("Pesos, Valores y arreglo de Aceptados deben ser del mismo tamano");
            }
            return resultado;
        }

        private bool[] GenerarVecinoAleatorio(bool[] EstadoActual)
        {
            bool[] resultado = new bool[EstadoActual.Length];

            // Para generar un vecino aleatorio se elimina un objeto y se agrega otro a la mochila
            int indiceObjeto1 = GeneradorAleatorio.Next(EstadoActual.Length);
            int indiceObjeto2 = GeneradorAleatorio.Next(EstadoActual.Length);

            for (int i = 0; i < EstadoActual.Length; i++)
            {
                resultado[i] = EstadoActual[i];
                if (i == indiceObjeto1 || i == indiceObjeto2)
                {
                    // Agregar un ! en un booleano significa que lo va a negar: !true = false, como una compuerta NOT
                    resultado[i] = !EstadoActual[i];
                }
            }

            return resultado;
        }

        private double ProbabilidadDeAceptar(double ValorDeMochilaActual, double ValorDeMochilaVecino, double Temperatura)
        {
            // Si el vecino es mejor que el estado actual se acepta inmediatamente
            if (ValorDeMochilaVecino > ValorDeMochilaActual)
            {
                return 1;
            }
            // Si el vecino es peor se determina con probabilidad si se debe de aceptar o no
            // Es como si se lanzara un dado, puede que se acepte, puede que no.
            // Entre mayor es la temperatura se aceptara con mayor probabilidad.
            // De igual manera si la diferencia entre los dos estados es muy grande tambien afectara en la probabilidad
            return Math.Exp((ValorDeMochilaActual - ValorDeMochilaVecino) / Temperatura);
        }
    }
}
