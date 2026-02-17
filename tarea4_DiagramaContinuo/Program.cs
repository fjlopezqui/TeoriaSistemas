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
        if (ocupado) Console.WriteLine($"Piso destino: {destino}");
        Console.WriteLine("=========================================");
    }

    static void FallaTecnica()
    {
        Random random = new Random();

        // 1 de cada 4 posibilidades de que ocurra una falla tecnica
        if (random.Next(1, 5) == 4)
        {
            Console.WriteLine("ESTADO: Falla tecnica - Espere 3 segundos...");
            Thread.Sleep(3000);
        }
    }

    static void Main(string[] args)
    {
        Random random = new Random();

        int pisoActual = 0;
        int capacidadMaxima = 10;
        int personasDentro = 0;
        int pisoDestino = 0;
        bool ocupado = false;
        int numViajes = 3;

        Console.WriteLine("===== SIMULADOR DE ELEVADOR =====");
        MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);

        for (int viaje = 1; viaje <= numViajes; viaje++)
        {
            Console.WriteLine($"\n********** VIAJE #{viaje} **********");
            Console.WriteLine("Presiona Enter para iniciar...");
            Console.ReadLine();

            // Generar personas y destino aleatorios para este viaje
            personasDentro = random.Next(1, capacidadMaxima + 1);
            do { pisoDestino = random.Next(0, 21); } while (pisoDestino == pisoActual);
            ocupado = true;

            // Determinar si el elevador se equivocara de piso (1 de cada 8 viajes)
            // Si hay error, el piso final sera diferente al destino solicitado
            int pisoFinal = pisoDestino;
            if (random.Next(1, 9) == 1)
            {
                // Desviarse entre 1 y 2 pisos del destino real
                int desviacion = random.Next(1, 3);
                pisoFinal = Math.Clamp(pisoDestino + desviacion, 0, 20);
            }

            Console.WriteLine($"Entraron {personasDentro} personas. Destino: Piso {pisoDestino}");
            MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);

            // Mover el elevador hacia el piso final (que puede ser incorrecto)
            while (pisoActual != pisoFinal)
            {
                FallaTecnica();
                pisoActual += pisoActual < pisoFinal ? 1 : -1;
                Console.WriteLine($"{(pisoActual < pisoFinal ? "Subiendo" : "Bajando")}... Piso {pisoActual}");
                Thread.Sleep(800);
            }

            // Evaluar si el elevador llego al piso correcto o no
            if (pisoActual == pisoDestino)
            {
                Console.WriteLine($"\nCORRECTO: Llego al piso {pisoActual} sin errores.");
            }
            else
            {
                // El elevador se detuvo en el piso equivocado, hay que corregir
                Console.WriteLine($"\nERROR: Se detuvo en piso {pisoActual}, el destino era {pisoDestino}.");
                Console.WriteLine("Corrigiendo ruta...");
                Thread.Sleep(1500);

                // Mover el elevador al destino correcto
                while (pisoActual != pisoDestino)
                {
                    pisoActual += pisoActual < pisoDestino ? 1 : -1;
                    Console.WriteLine($"Corrigiendo... Piso {pisoActual}");
                    Thread.Sleep(600);
                }

                Console.WriteLine($"Correccion completada. Piso {pisoActual}.");
            }

            // Vaciar el elevador al llegar al destino final
            personasDentro = 0;
            ocupado = false;
            MostrarEstado(pisoActual, personasDentro, ocupado, pisoDestino);
        }

        Console.WriteLine("\n===== SIMULACION FINALIZADA =====");
        Console.ReadLine();
    }
}