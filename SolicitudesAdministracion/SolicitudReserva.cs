/*Clase hija de Solicitud. Con atributos para reservar un espacio público más 
los atributos del padre(Solicitud).
*/
class SolicitudReserva : Solicitud {
    protected string espacioReserva;
    protected string fecha;
    protected string horaInicio;
    protected int duracion;

    public SolicitudReserva(string id, string fecha, string espacioReserva,
        string horaInicio, int duracion, Administrativo admAsociado)
        : base(id, fecha, admAsociado) {
        this.espacioReserva = espacioReserva;
        this.horaInicio = horaInicio;
        this.duracion = duracion;
    }

    public override string ToString() {
        return "Reserva de Espacios. " + base.ToString() + " Espacio: " +
            espacioReserva + " Fecha de Reserva: " + fecha + "Hora: " +
            horaInicio + " Duración: " + duracion + " DNI: " +
            admAsociado.GetDni();
    }
}