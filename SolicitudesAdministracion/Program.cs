/*Programa principal en el que tenemos la función para crear una nueva 
solicitud, ver las solicitudes por administrativo, mostrar el listado de 
solicitudes por colores según el tipo de solicitud y ver el pendiente de pago
de las tasas*/
using System.Data;
using System.Runtime.InteropServices;

class Principal {

    enum opciones { SALIR, NUEVA, SOLICITUDES, LISTADO, TOTAL }
    static opciones mostrarMenu() {
        opciones opcionUsuario;

        Console.WriteLine("1. Nueva Solicitud");
        Console.WriteLine("2. Solicitudes por administrativo");
        Console.WriteLine("3. Listado de solicitudes");
        Console.WriteLine("4. Total pendiente de pago");
        Console.WriteLine("0. Salir");
        Console.Write("Elige una opción: ");
        opcionUsuario = (opciones) Convert.ToInt32(Console.ReadLine());

        return opcionUsuario;
    }

    static void MostrarAdmin(Administrativo[] administrativo) {
        foreach (Administrativo a in administrativo) {
            Console.WriteLine("Nombre: " + a.GetNombre() + " - DNI: " +
                a.GetDni());
        }
    }

    static Administrativo BuscarAdmin(Administrativo[] administrativo,
        string codigo) {
        Administrativo admin = null;

        foreach (Administrativo a in administrativo) {
            if (a.GetDni() == codigo) {
                admin = a;
            }
        }

        return admin;
    }
    static bool ComprobarID(Solicitud[] solicitudes, string id) {
        bool existe = false;

        if (solicitudes != null) {
            foreach (Solicitud s in solicitudes) {
                if (s != null && s.GetId() == id) {
                    existe = true;
                    break;
                }
            }
        }

        return existe;
    }
    static bool ComprobarNumCuenta(string numCuenta) {
        bool correcto = true;
        if (numCuenta.Length != 20) {
            correcto = false;
        }

        foreach (char c in numCuenta) {
            if (c < '0' || c > '9') {
                correcto = false;
            }
        }
        return correcto;
    }

    static Solicitud NuevaDomiciliacion(string id, string fecha,
        Administrativo adm) {
        string numCuenta;

        Console.WriteLine("Número de Cuenta: ");
        numCuenta = Console.ReadLine();

        if (!ComprobarNumCuenta(numCuenta)) {
            throw new Exception("Error: Solo 20 dígitos del 0 al 9");
        }

        Console.WriteLine("Cambio de Domiciliación correcto. ");

        SolicitudDomiciliacion nueva = new SolicitudDomiciliacion(id, fecha,
            numCuenta, adm);
        return nueva;
    }
    static Solicitud NuevaReserva(string id, string fecha,
        Administrativo adm) {
        string espacio, fechaReserva, horaInicio;
        int duracion;

        Console.Write("Espacio a reservar: ");
        espacio = Console.ReadLine();
        Console.Write("Fecha de reserva: ");
        fechaReserva = Console.ReadLine();
        Console.Write("Hora de incio: ");
        horaInicio = Console.ReadLine();
        Console.Write("Duración en minutos: ");
        duracion = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Reserva completada con éxito.");
        SolicitudReserva nueva = new SolicitudReserva(id, fecha, espacio,
            horaInicio, duracion, adm);

        return nueva;
    }

    static Solicitud NuevaTasa(string id, string fecha, Administrativo adm) {
        string concepto;
        float importe;

        Console.Write("Concepto: ");
        concepto = Console.ReadLine();

        Console.Write("Importe: ");
        importe = Convert.ToSingle(Console.ReadLine());

        if (importe < 0) {
            throw new Exception("Error: El importe debe ser mayor que 0");
        }

        Console.WriteLine("Tasa correcta.");

        SolicitudTasas nueva = new SolicitudTasas(id, fecha, concepto,
            importe, adm);

        return nueva;
    }
    static Solicitud NuevaSolicitud(Solicitud[] solicitudes, ref int cantidad,
        Administrativo[] adm) {
        Solicitud nueva = null;

        int tipo, duracion;
        float importe;
        string id, numCuenta, fecha, dniUsuario, concepto, espacio,
            fechaReserva, horaInicio;

        Console.WriteLine("1. Cambio domiciliación bancaria");
        Console.WriteLine("2. Pago de tasas");
        Console.WriteLine("3. Reserva de espacios públicos");
        Console.Write("Elige una opción: ");
        tipo = Convert.ToInt32(Console.ReadLine());

        try {
            Console.Write("ID: ");
            id = Console.ReadLine();

            if (ComprobarID(solicitudes, id)) {
                throw new Exception("Error : El ID ya existe");
            }
            else {
                Console.Write("Fecha: ");
                fecha = Console.ReadLine();
            }

            MostrarAdmin(adm);

            Administrativo ad;
            Console.WriteLine("Elige un administrativo por su DNI: ");
            dniUsuario = Console.ReadLine();
            ad = BuscarAdmin(adm, dniUsuario);

            if (ad == null) {
                throw new Exception("No se ha encontrado un administrativo" +
                    "con el DNI indicado.");
            }

            switch (tipo) {
                case 1:
                    nueva = NuevaDomiciliacion(id, fecha, ad);
                    solicitudes[cantidad] = nueva;
                    cantidad++;
                    break;
                case 2:
                    nueva = NuevaTasa(id, fecha, ad);
                    solicitudes[cantidad] = nueva;
                    cantidad++;
                    break;
                case 3:
                    nueva = NuevaReserva(id, fecha, ad);
                    solicitudes[cantidad] = nueva;
                    cantidad++;
                    break;
            }
        }
        catch (Exception e) {
            Console.WriteLine("Datos incorrectos para la solicitud.");
            Console.WriteLine(e.Message);
        }
        return nueva;
    }


    static void SolicitudesAdministrativo(Administrativo[] administrativo,
        Solicitud[] solicitudes, int cantidad) {

        MostrarAdmin(administrativo);
        Administrativo ad;
        bool encontrado = false;

        Console.Write("Elige un administrativo por su DNI para ver sus " +
            "solicitudes: ");
        string dniUsuario = Console.ReadLine();

        ad = BuscarAdmin(administrativo, dniUsuario);

        if (ad == null) {
            Console.WriteLine("No se ha encontrado un administrativo con el" +
                " DNI indicado");
        }
        else {
            for (int i = 0; i < cantidad; i++) {
                if (solicitudes[i].GetAdmAsociado() == ad) {
                    Console.WriteLine(solicitudes[i]);
                    encontrado = true;
                }
            }
            if (!encontrado) {
                Console.WriteLine("El administrativo seleccionado no tiene " +
                    "solicitudes a su cargo");
            }
        }
    }

    static void ListadoSolicitudes(Solicitud[] solicitudes, int cantidad) {
        for (int i = 0; i < cantidad; i++) {
            if (solicitudes[i] is SolicitudDomiciliacion) {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (solicitudes[i] is SolicitudReserva) {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(solicitudes[i]);
            Console.ResetColor();
        }
    }

    static void PendientePago(Solicitud[] solicitudes) {
        float total = 0;
        // Creamos objeto de SolicitudTasas para obtener el importe.
        foreach (Solicitud s in solicitudes) {
            if (s is SolicitudTasas st) {
                total += st.GetImporte();
            }
        }
        Console.WriteLine("Importe total de las tasas: " + total);
    }
    static void Main(String[] args) {
        Administrativo[] administrativos = new Administrativo[2];
        Solicitud[] solicitudes = new Solicitud[20];
        int cantidad = 0;
        opciones opcionUsuario;

        administrativos[0] = new Administrativo("David", "123456789J",
            611223344);
        administrativos[1] = new Administrativo("Chema", "987654321A",
            644112233);

        do {
            opcionUsuario = mostrarMenu();
            switch (opcionUsuario) {
                case opciones.NUEVA:
                    if (cantidad < solicitudes.Length) {
                        NuevaSolicitud(solicitudes, ref cantidad,
                            administrativos);
                    }
                    else {
                        Console.WriteLine("Error: Espacio Completo.");
                    }
                    break;

                case opciones.SOLICITUDES:
                    if(cantidad == 0) {
                        Console.WriteLine("Error: No existen solicitudes. ");
                    } 
                    else {
                        SolicitudesAdministrativo(administrativos, solicitudes,
                            cantidad);
                    }
                    break;

                case opciones.LISTADO:
                    if (cantidad == 0) {
                        Console.WriteLine("Error: No existen solicitudes. ");
                    }
                    else {
                        ListadoSolicitudes(solicitudes, cantidad);
                    };
                    break;

                case opciones.TOTAL:
                    if (cantidad == 0) {
                        Console.WriteLine("Error: No existen tasas. ");
                    }
                    else {
                        PendientePago(solicitudes);
                    }
                    break;

                case opciones.SALIR:
                    break;
            }
        }
        while (opcionUsuario != opciones.SALIR);
    }
}