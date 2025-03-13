/* Clase padre de SolicitudDomiciliacion, SolicitudTasas y SolicitudReserva.
 Con redefinición del método ToString para imprimir la información de 
 cada solicitud. También incluye el Administrativo asociado.*/
class Solicitud {
    protected string id;
    protected string fecha;
    protected Administrativo admAsociado;

    public string GetId() {
        return id;
    }
    public void SetID(string id) {
        this.id = id;
    }

    public string GetFecha() {
        return fecha;
    }

    public void SetFecha(string fecha) {
        this.fecha = fecha;
    }

    public Solicitud(string id, string fecha, Administrativo admAsociado) {
        this.id = id;
        this.fecha = fecha;
        this.admAsociado = admAsociado;
    }

    public Administrativo GetAdmAsociado() {
        return admAsociado;
    }

    public void SetAdmAsociado(Administrativo admAsociado) {
        this.admAsociado = admAsociado;
    }

    public override string ToString() {
        return id + ". " + "Realizada el " + fecha;
    }
}