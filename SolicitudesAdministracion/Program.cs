/*
 * Programa principal de gestión de solicitudes
 */

enum opciones { SALIR, NUEVA, ADMINISTRATIVO, LISTADO, TOTAL, ORDENAR, ELIMINAR
     }

class Program {
    // Comprueba si el id ya existe en el array o no. Lanza excepción si existe
    static void ComprobarId(List<Solicitud> solicitudes, string id) {
        for (int i = 0; i < solicitudes.Count; i++) {
            if (solicitudes[i].Id == id)
                throw new Exception("El Id de solicitud ya existe");
        }
    }

    // Devuelve el administrativo con el DNI indicado,
    // o lanza excepción si no está
    static Administrativo ComprobarAdmin(List<Administrativo> administrativos,
        string dni) {
        Administrativo resultado = null;

        foreach (Administrativo a in administrativos) {
            if (a.Dni == dni)
                resultado = a;
        }

        if (resultado == null)
            throw new Exception("No se ha encontrado un administrativo con " +
                "el DNI indicado");
        return resultado;
    }

    // Comprueba si el número de cuenta es correcto o no (lanza excepción)
    static void ComprobarNumCuenta(string cuenta) {
        if (cuenta.Length != 20)
            throw new Exception("Número de cuenta incorrecto");
        else {
            foreach (char digito in cuenta) {
                if (digito < '0' || digito > '9')
                    throw new Exception("Número de cuenta incorrecto");
            }
        }
    }

    // Resto de funciones para las opciones del programa principal

    static List<Administrativo> RellenarAdministrativos() {
        List<Administrativo> administrativos = new List<Administrativo>();

        administrativos.Add(new Administrativo("11223344A", "Juan Pérez",
            "611223344"));
        administrativos.Add(new Administrativo("44332211B", "Elisa López",
            "644332211"));
        administrativos.Add(new Administrativo("55667788C", "Raquel Sánchez",
            "655667788"));
        administrativos.Add(new Administrativo("88776655D", "Sergio Zamora",
            "688776655"));

        return administrativos;
    }

    static void ListarAdministrativos(List<Administrativo> administrativos) {
        Console.WriteLine("Listado de administrativos:");
        foreach (Administrativo a in administrativos)
            Console.WriteLine(a);
    }

    static opciones Menu() {
        opciones opcion;
        Console.Clear();
        Console.WriteLine("{0}. Nueva solicitud", (int)opciones.NUEVA);
        Console.WriteLine("{0}. Solicitudes por administrativo",
            (int)opciones.ADMINISTRATIVO);
        Console.WriteLine("{0}. Listado de solicitudes",
            (int)opciones.LISTADO);
        Console.WriteLine("{0}. Total pendiente de pago",
            (int)opciones.TOTAL);
        Console.WriteLine("{0}. Ordenar solicitudes por ID",
            (int)opciones.ORDENAR);
        Console.WriteLine("{0}. Eliminar solicitud ID",
            (int)opciones.ELIMINAR);
        Console.WriteLine("{0}. Salir", (int)opciones.SALIR);
        Console.Write("Elige una opción: ");
        opcion = (opciones)Convert.ToInt32(Console.ReadLine());
        return opcion;
    }

    static void NuevaSolicitud(List<Solicitud> solicitudes,
        List<Administrativo> administrativos) {
        Solicitud nueva;
        Administrativo admin;
        int tipo, horas;
        float importe;
        string id, fecha, dni, cuenta, concepto, espacio, fechaReserva,
            horaInicio;

        Console.WriteLine("Elige tipo de solicitud:");
        Console.WriteLine("1. Domiciliación / 2. Tasas / 3. Reserva");
        tipo = Convert.ToInt32(Console.ReadLine());

        // Pedimos datos comunes

        Console.Write("Id. de solicitud: ");
        id = Console.ReadLine();
        ComprobarId(solicitudes, id);

        Console.Write("Fecha solicitud: ");
        fecha = Console.ReadLine();

        ListarAdministrativos(administrativos);
        Console.Write("DNI administrativo: ");
        dni = Console.ReadLine();
        admin = ComprobarAdmin(administrativos, dni);

        // Distinguimos tipo de solicitud
        if (tipo == 1) {
            Console.Write("Núm. cuenta: ");
            cuenta = Console.ReadLine();
            ComprobarNumCuenta(cuenta);

            solicitudes.Add(new SolicitudDomiciliacion(id, fecha,
                admin, cuenta));
        }
        else if (tipo == 2) {
            Console.Write("Concepto: ");
            concepto = Console.ReadLine();

            Console.Write("Importe a pagar: ");
            if (!Single.TryParse(Console.ReadLine(), out importe) ||
                importe <= 0)
                throw new Exception("Importe incorrecto");
            solicitudes.Add(new SolicitudTasas(id, fecha, admin,
                concepto, importe));
        }
        else {
            Console.Write("Espacio: ");
            espacio = Console.ReadLine();
            Console.Write("Fecha reserva: ");
            fechaReserva = Console.ReadLine();
            Console.Write("Hora inicio reserva: ");
            horaInicio = Console.ReadLine();
            Console.Write("Duración reserva (horas): ");
            horas = Convert.ToInt32(Console.ReadLine());
            solicitudes.Add(new SolicitudReserva(id, fecha, admin,
                espacio, fechaReserva, horaInicio, horas));
        }
    }

    static void SolicitudesPorAdmin(List<Solicitud> solicitudes,
        List<Administrativo> administrativos) {
        string dni;
        bool resultados = false;

        ListarAdministrativos(administrativos);
        Console.Write("DNI administrativo: ");
        dni = Console.ReadLine();
        ComprobarAdmin(administrativos, dni);

        for (int i = 0; i < solicitudes.Count; i++) {
            if (solicitudes[i].Administrativo.Dni == dni) {
                resultados = true;
                Console.WriteLine(solicitudes[i]);
            }
        }

        if (!resultados)
            Console.WriteLine("El administrativo seleccionado no tiene " +
                "solicitudes a su cargo");
    }

    static void ListadoSolicitudes(List<Solicitud> solicitudes) {
        for (int i = 0; i < solicitudes.Count; i++) {
            if (solicitudes[i] is SolicitudDomiciliacion)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (solicitudes[i] is SolicitudTasas)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(solicitudes[i]);
        }

        Console.ResetColor();
    }

    static void TotalPagar(List<Solicitud> solicitudes) {
        float total = 0;

        for (int i = 0; i < solicitudes.Count; i++) {
            if (solicitudes[i] is SolicitudTasas) {
                total += ((SolicitudTasas)solicitudes[i]).Importe;
            }
        }

        Console.WriteLine("Total a pagar: {0} eur.", total.ToString("N2"));
    }

    static void OrdenarSolicitudes(List<Solicitud> solicitudes) {
        solicitudes.Sort((s1, s2) => s1.Id.CompareTo(s2.Id));

        foreach(Solicitud s in solicitudes) {
            Console.WriteLine(s);
        }
    }


    static bool Existe(List<Solicitud> solicitudes, string id) {
        bool existe = false;
        foreach (Solicitud s in solicitudes) {
            if (s.Id == id) {
                return true;
            }
        }

        return existe;
    }

    static void EliminarSolicitudes(List<Solicitud> solicitudes) {
        ListadoSolicitudes(solicitudes);
        if(solicitudes.Count == 0) {
            Console.WriteLine("No existen solicitudes");
        } else {
            Console.Write("Elige el ID para borrar la solicitud: ");
            string id = Console.ReadLine();
            

            if(!Existe(solicitudes, id)) {
                Console.WriteLine("No se ha encontrado una solicitud con" +
                    " el ID indicado");
            } else {
                solicitudes.RemoveAll(s => s.Id == id);
                Console.WriteLine("Solicitud eliminada correctamente.");
            }
        }
    }

    static void Main() {
        List<Administrativo> administrativos = RellenarAdministrativos();

        List<Solicitud> solicitudes = new List<Solicitud>();
        opciones opcion;

        do {
            opcion = Menu();
            switch (opcion) {
                case opciones.NUEVA:
                    try {
                        NuevaSolicitud(solicitudes, administrativos);
                    }
                    catch (Exception e) {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    break;
                case opciones.ADMINISTRATIVO:
                    try {
                        SolicitudesPorAdmin(solicitudes, administrativos);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case opciones.LISTADO:
                    ListadoSolicitudes(solicitudes);
                    break;
                case opciones.TOTAL:
                    TotalPagar(solicitudes);
                    break;
                case opciones.ORDENAR:
                    OrdenarSolicitudes(solicitudes);
                    break;
                case opciones.ELIMINAR:
                    EliminarSolicitudes(solicitudes);
                    break;
            }
            Console.WriteLine("Pulsa cualquier tecla para continuar...");
            Console.ReadKey(true);
        }
        while (opcion != opciones.SALIR);
    }
}