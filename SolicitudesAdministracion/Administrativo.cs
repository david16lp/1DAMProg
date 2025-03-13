/*
 * Personal administrativo que atiende las solicitudes
 */
class Administrativo {
    private string dni;
    private string nombre;
    private string telefono;

    public string Dni {
        get { return dni; }
        set { dni = value; }
    }

    public string Nombre {
        get { return nombre; }
        set { nombre = value; }
    }

    public string Telefono {
        get { return telefono; }
        set { telefono = value; }
    }

    public Administrativo(string dni, string nombre, string telefono) {
        this.Dni = dni;
        this.Nombre = nombre;
        this.Telefono = telefono;
    }

    public override string ToString() {
        return dni + ": " + nombre + ", " + telefono;
    }
}