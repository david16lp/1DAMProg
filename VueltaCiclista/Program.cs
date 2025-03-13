/*Clase principal donde se crean las etapas y los ciclistas, y las demás 
 funciones*/ 

class Program {
    static Ciclista[] RellenarCiclistas() {
        Ciclista[] ciclistas = new Ciclista[5];
        ciclistas[0] = new Ciclista(10, "Chema", "Equipo 1");
        ciclistas[1] = new Ciclista(60, "Pepe", "Equipo 3");
        ciclistas[2] = new Ciclista(16, "Mireya", "Equipo 2");
        ciclistas[3] = new Ciclista(89, "Hector", "Equipo 6");
        ciclistas[4] = new Ciclista(69, "David", "Equipo 9");


        return ciclistas;
    }

    static void MostrarCiclistas(List<Ciclista> ciclistas) {
        int i = 1;

        Console.WriteLine("Lista de Ciclistas: ");
        foreach (Ciclista c in ciclistas) {
            Console.WriteLine("{0}. {1}", i, c);
            i++;
        }
        i = 1;
    }


    static void ComprobarFecha(string fecha, SortedList<string, Etapa> etapas){
        string[] partes = fecha.Split('-');

        if(partes.Length != 3) {
            throw new Exception("El formato debe ser aaaa-mm-dd");
        }

        if (partes[0].Length != 4 || partes[1].Length != 2 
            || partes[2].Length != 2) {
            throw new Exception("Fecha Incorrecta");
        }

        if (!int.TryParse(partes[0], out int year) || !int.TryParse(partes[1],
            out int month) || !int.TryParse(partes[2], out int dia)) {
            throw new Exception("Fecha incorrecta.");
        }

        if(etapas.ContainsKey(fecha)) {
            throw new Exception("La fecha ya existe.");
        }
    }

    static void RellenarEtapa(SortedList<string, Etapa> etapas, 
        Ciclista[] ciclistas) {
        string fecha;
        int posicionUsuario;
        Ciclista[] ganadores = new Ciclista[3];

        do {
            Console.Write("Introduce la fecha de la etapa (aaaa-mm-dd): ");
            fecha = Console.ReadLine();

            if (fecha != "") {                                                 
                ComprobarFecha(fecha, etapas);
                
                Etapa nueva = new Etapa(fecha);

                List<Ciclista> ciclistasPodio = new List<Ciclista>(ciclistas);

                for (int i = 0; i < 3; i++) {
                    MostrarCiclistas(ciclistasPodio);

                    Console.WriteLine("Introduce el puesto {0}: ", i + 1);
                    posicionUsuario = Convert.ToInt32(Console.ReadLine());

                    nueva.SetPodio(i, ciclistasPodio[posicionUsuario - 1]);

                    ciclistasPodio.RemoveAt(posicionUsuario - 1);
                }
                etapas.Add(fecha, nueva);
            }
        } while (fecha != "");
    }

    static void MostrarEtapas(SortedList<string, Etapa> etapas){
        Console.WriteLine("Histórico Etapas:");
        foreach (Etapa e in etapas.Values) {
            Console.WriteLine(e);  
        }
        Console.WriteLine();
    }

    static void MostrarGanadores(SortedList<string, Etapa> etapas) {
        HashSet<Ciclista> ganadoresHistorico = new HashSet<Ciclista>();

        foreach (Etapa e in etapas.Values) {
            Ciclista ganador = e.GetPodio()[0];
            ganadoresHistorico.Add(ganador);
        }

        Console.WriteLine("Ganadores históricos: ");
        foreach (Ciclista c in ganadoresHistorico) {
            Console.WriteLine(c.GetNombre());
        }
    }

    public static void Main() {
        Ciclista[] ciclistas = RellenarCiclistas();
        SortedList<string, Etapa> etapas = new SortedList<string, Etapa>();

        try {
            RellenarEtapa(etapas, ciclistas);
            MostrarEtapas(etapas);
            MostrarGanadores(etapas);
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }
}
