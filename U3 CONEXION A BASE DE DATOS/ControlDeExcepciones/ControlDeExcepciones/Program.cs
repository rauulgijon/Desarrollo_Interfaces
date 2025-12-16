using System;
using System.Collections.Generic;

namespace ControlDeExcepciones
{
    // Parte 2: excepción personalizada
    public class NumeroNegativoException : Exception
    {
        public NumeroNegativoException(string message) : base(message) { }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numeros = new List<int>();
            double[] resultados = new double[10];

            try
            {
                // Pedir 20 números
                for (int i = 0; i < 20; i++)
                {
                    int num;
                    while (true)
                    {
                        Console.Write($"Introduce un número ({i + 1}/20): ");
                        string entrada = Console.ReadLine();
                        if (!int.TryParse(entrada, out num))
                        {
                            Console.WriteLine("Entrada no válida. Introduce un entero.");
                            continue;
                        }

                        // Parte 2: lanzar excepción si el número es negativo
                        if (num < 0)
                            throw new NumeroNegativoException($"Número negativo detectado en la posición {i + 1}: {num}");

                        numeros.Add(num);
                        break;
                    }
                }

                // Realizar 10 divisiones (pares: 0/1, 2/3, 4/5, ...)
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        int indiceDividendo = i * 2;
                        int indiceDivisor = i * 2 + 1;

                        // Comprobación de índices por si ocurre IndexOutOfRangeException
                        if (indiceDividendo < 0 || indiceDivisor < 0 || indiceDividendo >= numeros.Count || indiceDivisor >= numeros.Count)
                            throw new IndexOutOfRangeException($"Índices inválidos para la división {i + 1}: {indiceDividendo}, {indiceDivisor}");

                        int dividendo = numeros[indiceDividendo];
                        int divisor = numeros[indiceDivisor];

                        // Forzar DivideByZeroException si divisor es 0
                        if (divisor == 0)
                            throw new DivideByZeroException($"Divisor cero en la división {i + 1} (índice {indiceDivisor}).");

                        resultados[i] = (double)dividendo / divisor;
                    }
                    catch (DivideByZeroException dbzEx)
                    {
                        Console.WriteLine($"DivideByZeroException: {dbzEx.Message}");
                        resultados[i] = double.PositiveInfinity; // decisión: registrar como infinito
                    }
                    catch (IndexOutOfRangeException idxEx)
                    {
                        Console.WriteLine($"IndexOutOfRangeException: {idxEx.Message}");
                        resultados[i] = double.NaN;
                    }
                }

                // Mostrar resultados
                Console.WriteLine("\nResultados de las 10 divisiones:");
                for (int i = 0; i < resultados.Length; i++)
                {
                    Console.WriteLine($"División {i + 1}: {resultados[i]}");
                }
            }
            catch (NumeroNegativoException nnEx)
            {
                Console.WriteLine($"NumeroNegativoException: {nnEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción inesperada: {ex.GetType().Name} - {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nEl programa ha finalizado.");
                Console.WriteLine("Pulsa una tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}
