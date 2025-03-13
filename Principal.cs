/* Programa que gestiona un conjunto de juegos de mesa, permitiendo
 * hacer diferentes operaciones con funciones en C# */

using System;

class Practica {
    enum opciones {SALIR, NUEVO, BORRAR, CARO, TIPO, ORDENAR}
    static opciones MostrarMenu () {
        opciones opcionUsuario;
        Console.WriteLine("1. Nuevo Juego");
        Console.WriteLine("2. Borrar juego");
        Console.WriteLine("3. Juego más caro");
        Console.WriteLine("4. Juegos por tipo");
        Console.WriteLine("5. Ordenar juegos");
        Console.WriteLine("0. Salir");
        Console.Write("Elige una opción del menú: ");
        opcionUsuario = (opciones) Convert.ToInt32(Console.ReadLine());
        return opcionUsuario;
    }
    static int NuevoJuego (JuegoMesa[] juegos, ref int cantidad) {
        int result;
        if (cantidad < juegos.Length) {
            Console.WriteLine("Nombre del juego: ");
            string nombre = Console.ReadLine();
            Console.WriteLine("Información básica: ");
            Console.WriteLine("--Edad mínima: ");
            int minEdad = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--Mínimo número de jugadores: ");
            int minJug =
            Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("--Máximo número de jugadores: ");
            int maxJug =
            Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tipo de juego: ");
            Console.WriteLine("--1. ROL\n--2. INFANTIL\n" +
            "--3. AZAR\n--4. PUZZLE\n--5. OTRO");
            JuegoMesa.tipoJuego tipo =
            (JuegoMesa.tipoJuego) Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Precio: ");
            try {
                float precio = Convert.ToSingle(Console.ReadLine());
                if (precio <= 0) {
                    result = 1;
                }
                else {
                    juegos[cantidad] = new JuegoMesa(nombre, precio, minEdad, 
                        minJug, maxJug, tipo);
                    cantidad++;
                    result = 0;
                }
            }
            catch (Exception) {
                result = 1;
            }
        }
        else {
            result = 2;
        }
        return result;
    }
    static void BorrarJuego (JuegoMesa[] juegos, ref int cantidad) {
        int posicion = 0;
        if (cantidad > 0) {
            for (int i = 0; i < cantidad; i++) {
                JuegoMesa juegoMostrado = juegos[i];
                juegoMostrado.Mostrar();
            }
            Console.WriteLine("Posición del juego a borrar:");
            posicion = Convert.ToInt32(Console.ReadLine());
            posicion--;
            if (posicion >= 0 && posicion < cantidad) {
                Console.WriteLine("¿Estás seguro? S/N:");
                char confirmar = Convert.ToChar(Console.ReadLine());
                if (confirmar == 'S' || confirmar == 's') {
                    for (int i = posicion; i < cantidad - 1; i++) {
                        juegos[i] = juegos[i + 1];
                    }
                    cantidad--;
                }
                else {
                    Console.WriteLine("Cancelado");
                }
            }
            else {
                Console.WriteLine("Posición incorrecta");
            }
        }
        else {
            Console.WriteLine("No hay juegos que borrar");
        }
    }
    static void JuegosMasCaro (JuegoMesa[] juegos, int cantidad) {
        if (cantidad > 0) {
            JuegoMesa juegoMasCaro = juegos[0];
            for (int i = 1; i < cantidad; i++) {
                if (juegos[i].Precio > juegoMasCaro.Precio) {
                    juegoMasCaro = juegos[i];
                }
            }
            juegoMasCaro.Mostrar();
        }
        else {
            Console.WriteLine("No hay juegos");
        }
    }
    static void JuegosPorTipo (JuegoMesa[] juegos, int cantidad,
        JuegoMesa.tipoJuego mostrarTipo) {
        bool encontrado = false;
        if (mostrarTipo >= JuegoMesa.tipoJuego.ROL &&
        mostrarTipo <= JuegoMesa.tipoJuego.OTROS) {
            encontrado = false;
            for (int i = 0; i < cantidad; i++) {
                if (mostrarTipo == juegos[i].Tipo) {
                    JuegoMesa juegoAMostrar = juegos[i];
                    juegoAMostrar.Mostrar();
                    encontrado = true;
                }
            }
            if (!encontrado) {
                Console.WriteLine("No hay juegos de ese tipo");
            }
        }
        else {
            Console.WriteLine("Tipo inválido");
        }
    }
    static void OrdenarJuegos (JuegoMesa[] juegos, int cantidad) {
        if (cantidad > 0) {
            for (int i = 0; i < cantidad - 1; i++) {
                for (int j = i + 1; j < cantidad; j++) {
                    if (string.Compare(juegos[i].Nombre,
                        juegos[j].Nombre) > 0) {
                        JuegoMesa auxiliar = juegos[i];
                        juegos[i] = juegos[j];
                        juegos[j] = auxiliar;
                    }
                }
            }
            Console.WriteLine("Los primeros 5 juegos ordenados:");
            for (int i = 0; i < Math.Min(5, cantidad); i++) {
                juegos[i].Mostrar();
            }
        }
        else {
            Console.WriteLine("No hay juegos");
        }
        Console.WriteLine();
    }
    static void Main () {
        opciones opcionUsuario;
        JuegoMesa[] juegos = new JuegoMesa[50];
        int cantidad = 0;
        JuegoMesa.tipoJuego mostrarTipo;
        int resultJuego;

        do {
            opcionUsuario = MostrarMenu();
            switch (opcionUsuario) {
                case opciones.NUEVO:
                    Console.WriteLine("---NUEVO JUEGO---");
                    resultJuego = NuevoJuego(juegos, ref cantidad);
                    switch (resultJuego) {
                        case 0:
                            Console.WriteLine("Se ha insertado correctamente");
                            break;
                        case 1:
                            Console.WriteLine("Precio incorrecto");
                            break;
                        case 2:
                            Console.WriteLine("El catálogo está completo");
                            break;
                    } 
                    break;

                case opciones.BORRAR:
                    Console.WriteLine("---BORRAR JUEGO---");
                    BorrarJuego(juegos, ref cantidad);
                    break;

                case opciones.CARO:
                    Console.WriteLine("---JUEGO MÁS CARO---");
                    JuegosMasCaro(juegos, cantidad);
                    break;

                case opciones.TIPO:
                    Console.WriteLine("---JUEGOS POR TIPO---");
                    if (cantidad > 0) {
                        Console.WriteLine("--1. ROL\n--2. INFANTIL\n" +
                                "--3. AZAR\n--4. PUZZLE\n--5. OTRO");
                        Console.Write("Elige tipo: ");

                        mostrarTipo =
                            (JuegoMesa.tipoJuego) 
                            Convert.ToInt32(Console.ReadLine());
                        JuegosPorTipo(juegos, cantidad, mostrarTipo);
                    }
                    else {
                        Console.WriteLine("No hay juegos");
                    }
                    break;

                case opciones.ORDENAR:
                    Console.WriteLine("---ORDENAR JUEGOS POR NOMBRE---");
                    OrdenarJuegos(juegos, cantidad);
                    break;
                case opciones.SALIR:
                    Console.WriteLine("FIN DE PROGRAMA");
                    break;
            }
        }
        while (opcionUsuario != opciones.SALIR);
    }
}