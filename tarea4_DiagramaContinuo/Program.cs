using System;
using System.Threading;

class Program
{
    static void MostrarEstado(int piso, int personas, bool ocupado, int destino)
    {
        Console.WriteLine("\n========== ESTADO DEL ELEVADOR ==========");
        Console.WriteLine($"Piso actual: {piso}");
        Console.WriteLine($"Personas dentro: {personas}");
        Console.WriteLine($"Estado: {(ocupado ? "OCUPADO" : "LIBRE")}");
        if (ocupado)
        {
            Console.WriteLine($"Piso destino: {destino}");
        }
        Console.WriteLine("=========================================");
    }

    static void Main(string[] args)
    {
        Random random = new Random();

        int pisoActual = 0;
        int capacidadMaxima = 9;
        int personasDentro = 0;
        int pisoDestino = 0;
        bool ocupado = false;
        int numViajes = 3;

        Console.WriteLine("===== SIMULADOR DE ELEVADOR =====");
        MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);

        for (int viaje = 1; viaje <= numViajes; viaje++)
        {
            Console.WriteLine($"\n\n********** VIAJE #{viaje} **********");
            Console.WriteLine("Presiona Enter para iniciar el viaje...");
            Console.ReadLine();

            personasDentro = random.Next(1, capacidadMaxima + 1);

            // Asegurar que el piso destino sea diferente al actual
            do
            {
                pisoDestino = random.Next(0, 21);
            } while (pisoDestino == pisoActual);

            ocupado = true;

            Console.WriteLine("\n¡NUEVO VIAJE!");
            Console.WriteLine($"Entraron {personasDentro} personas");
            Console.WriteLine($"Destino: Piso {pisoDestino}");

            MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);

            // Mover el elevador piso por piso hasta el destino
            while (pisoActual != pisoDestino)
            {
                if (pisoActual < pisoDestino)
                {
                    pisoActual++;
                    Console.WriteLine($"↑ Subiendo... Piso {pisoActual}");
                }
                else
                {
                    pisoActual--;
                    Console.WriteLine($"↓ Bajando... Piso {pisoActual}");
                }
                
                Thread.Sleep(800); // Simular tiempo de movimiento entre pisos
            }

            // Llegada al destino - todos salen
            Console.WriteLine($"\n¡LLEGAMOS! Piso {pisoActual}");
            Console.WriteLine("Todas las personas salen del elevador...");
            personasDentro = 0;
            ocupado = false;

            MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);
        }

        Console.WriteLine("\n\n===== SIMULACIÓN FINALIZADA =====");
        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}