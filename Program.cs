using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static int izquierda = 0;
    static int derecha = 1;
    static int arriba = 2;
    static int abajo = 3;

    static int puntajeJugador1 = 0;
    static int direccionJugador1 = derecha;
    static int columnaJugador1 = 0; // columna
    static int filaJugador1 = 0; // fila

    static int puntajeJugador2 = 0;
    static int direccionJugador2 = izquierda;
    static int columnaJugador2 = 40; // columna
    static int filaJugador2 = 5; // fila

    static bool[,] estaEnUso;

    static void Main(string[] args)
    {
        EstablecerCampoDeJuego();
        PantallaDeInicio();

        estaEnUso = new bool[Console.WindowWidth, Console.WindowHeight];

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                CambiarDireccionJugador(tecla);
            }

            MoverJugadores();

            bool jugador1Pierde = PierdeJugador(filaJugador1, columnaJugador1);
            bool jugador2Pierde = PierdeJugador(filaJugador2, columnaJugador2);

            if (jugador1Pierde && jugador2Pierde)
            {
                puntajeJugador1++;
                puntajeJugador2++;
                MostrarResultado("¡Empate!");
                ReiniciarJuego();
            }
            if (jugador1Pierde)
            {
                puntajeJugador2++;
                MostrarResultado("¡El jugador 2 gana!");
                ReiniciarJuego();
            }
            if (jugador2Pierde)
            {
                puntajeJugador1++;
                MostrarResultado("¡El jugador 1 gana!");
                ReiniciarJuego();
            }

            estaEnUso[columnaJugador1, filaJugador1] = true;
            estaEnUso[columnaJugador2, filaJugador2] = true;

            EscribirEnPosicion(columnaJugador1, filaJugador1, '*', ConsoleColor.Yellow);
            EscribirEnPosicion(columnaJugador2, filaJugador2, '*', ConsoleColor.Cyan);

            Thread.Sleep(100);
        }
    }

    static void PantallaDeInicio()
    {
        Console.Clear();
        string titulo = "ARCADE MURCIA";
        Console.CursorLeft = Console.BufferWidth / 2 - titulo.Length / 2;
        Console.WriteLine(titulo);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Controles del Jugador 1:\n");
        Console.WriteLine("W - Arriba");
        Console.WriteLine("A - Izquierda");
        Console.WriteLine("S - Abajo");
        Console.WriteLine("D - Derecha");

        string cadenaMasLarga = "Controles del Jugador 2:";
        int cursorIzquierdo = Console.BufferWidth - cadenaMasLarga.Length;

        Console.CursorTop = 1;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.CursorLeft = cursorIzquierdo;
        Console.WriteLine("Controles del Jugador 2:\n");
        Console.CursorLeft = cursorIzquierdo;
        Console.WriteLine("Flecha Arriba - Arriba");
        Console.CursorLeft = cursorIzquierdo;
        Console.WriteLine("Flecha - Izquierda");
        Console.CursorLeft = cursorIzquierdo;
        Console.WriteLine("Flecha Abajo - Abajo");
        Console.CursorLeft = cursorIzquierdo;
        Console.WriteLine("Flecha Derecha - Derecha");

        Console.ReadKey();
    }

    static void MostrarResultado(string mensaje)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Fin del juego");
        Console.WriteLine(mensaje);
        Console.WriteLine("Puntaje actual: {0} - {1}", puntajeJugador1, puntajeJugador2);
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void ReiniciarJuego()
    {
        estaEnUso = new bool[Console.WindowWidth, Console.WindowHeight];
        EstablecerCampoDeJuego();
        direccionJugador1 = derecha;
        direccionJugador2 = izquierda;
        MostrarResultado("Presiona cualquier tecla para comenzar de nuevo...");
        Console.Clear();
        MoverJugadores();
    }


    static bool PierdeJugador(int fila, int columna)
    {
        if (fila < 0)
        {
            return true;
        }
        if (columna < 0)
        {
            return true;
        }
        if (fila >= Console.WindowHeight)
        {
            return true;
        }
        if (columna >= Console.WindowWidth)
        {
            return true;
        }

        if (estaEnUso[columna, fila])
        {
            return true;
        }

        return false;
    }

    static void EstablecerCampoDeJuego()
    {
        Console.WindowHeight = 30;
        Console.BufferHeight = 30;

        Console.WindowWidth = 100;
        Console.BufferWidth = 100;

        /*
         * 
         * ->>>>            <<<<-
         * 
         */
        columnaJugador1 = 0;
        filaJugador1 = Console.WindowHeight / 2;

        columnaJugador2 = Console.WindowWidth - 1;
        filaJugador2 = Console.WindowHeight / 2;
    }

    static void MoverJugadores()
    {
        if (direccionJugador1 == derecha)
        {
            columnaJugador1++;
        }
        if (direccionJugador1 == izquierda)
        {
            columnaJugador1--;
        }
        if (direccionJugador1 == arriba)
        {
            filaJugador1--;
        }
        if (direccionJugador1 == abajo)
        {
            filaJugador1++;
        }

        if (direccionJugador2 == derecha)
        {
            columnaJugador2++;
        }
        if (direccionJugador2 == izquierda)
        {
            columnaJugador2--;
        }
        if (direccionJugador2 == arriba)
        {
            filaJugador2--;
        }
        if (direccionJugador2 == abajo)
        {
            filaJugador2++;
        }
    }

    static void EscribirEnPosicion(int x, int y, char ch, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(x, y);
        Console.Write(ch);
    }


    static void CambiarDireccionJugador(ConsoleKeyInfo tecla)
    {
        if (tecla.Key == ConsoleKey.W && direccionJugador1 != abajo)
        {
            direccionJugador1 = arriba;
        }
        if (tecla.Key == ConsoleKey.A && direccionJugador1 != derecha)
        {
            direccionJugador1 = izquierda;
        }
        if (tecla.Key == ConsoleKey.D && direccionJugador1 != izquierda)
        {
            direccionJugador1 = derecha;
        }
        if (tecla.Key == ConsoleKey.S && direccionJugador1 != arriba)
        {
            direccionJugador1 = abajo;
        }

        if (tecla.Key == ConsoleKey.UpArrow && direccionJugador2 != abajo)
        {
            direccionJugador2 = arriba;
        }
        if (tecla.Key == ConsoleKey.LeftArrow && direccionJugador2 != derecha)
        {
            direccionJugador2 = izquierda;
        }
        if (tecla.Key == ConsoleKey.RightArrow && direccionJugador2 != izquierda)
        {
            direccionJugador2 = derecha;
        }
        if (tecla.Key == ConsoleKey.DownArrow && direccionJugador2 != arriba)
        {
            direccionJugador2 = abajo;
        }
    }
}
