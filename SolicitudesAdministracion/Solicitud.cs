/*
 * Clase padre de todos los tipos de solicitudes
 */
class Solicitud {
    protected string id;
    protected string fecha;
    protected Administrativo administrativo;

    public string Id {
        get { return id; }
        set { id = value; }
    }

    public string Fecha {
        get { return fecha; }
        set { fecha = value; }
    }

    public Administrativo Administrativo {
        get { return administrativo; }
        set { administrativo = value; }
    }

    public Solicitud(string id, string fecha, Administrativo administrativo) {
        this.Id = id;
        this.Fecha = fecha;
        this.Administrativo = administrativo;
    }

    public override string ToString() {
        return "ID " + id + ". Realizada el " + fecha + ". ";
    }
}