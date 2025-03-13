/*Clase que sustituirá a los structs del ejercicio anterior.
 * La clase JuegoMesa tendrá como atributos (privados) el nombre del juego, 
 * el precio, el tipo, la edad mínima para jugar, el número mínimo de 
 * jugadores y el número máximo */
class JuegoMesa {
    public enum tipoJuego {ROL = 1, INFANTIL, AZAR, PUZZLE, OTROS}

    private string nombre;
    private float precio;
    private int minEdad;
    private int maxJugadores;
    private int minJugadores;
    private tipoJuego tipo;

    public JuegoMesa(string nombre, float precio, int minEdad, 
        int minJugadores, int maxJugadores, tipoJuego tipo) {
        Nombre = nombre;
        Precio = precio;
        MinEdad = minEdad;
        MinJugadores = minJugadores;
        MaxJugadores = maxJugadores;
        Tipo = tipo;
    }
    public string Nombre {
        get {
            return nombre;
        }
        set {
            nombre = value;
        }
    }
    public float Precio {
        get {
            return precio;
        }
        set {
            if (value < 0) {
                precio = 1;
            } else {
                precio = value;
            }
        }
    }
    public int MinEdad {
        get {
            return minEdad;
        }
        set {
            if (value < 0) {
                minEdad = 0;
            }
        }
    }
    public int MinJugadores {
        get {
            return minJugadores;
        }
        set {
            if (value < 0) {
                minJugadores = 0;
            }
        }
    }
    public int MaxJugadores {
        get {
            return maxJugadores;
        }
        set {
            if (value < 0) {
                maxJugadores = 0;
            }
        }
    }
    public tipoJuego Tipo {
        get {
            return tipo;
        }
        set { 
            if(value < tipoJuego.ROL || value > tipoJuego.OTROS) {
                tipo = tipoJuego.OTROS;
            } else {
                tipo = value;
            }
        }
    }
    public void Mostrar() {
        Console.WriteLine("{0} ({1}): {2}, min {3} años, Jugadores {4}-{5}", 
            nombre, tipo, precio,  minEdad,  minJugadores, maxJugadores );
    }
}