/*
 * Tipo de solicitud: reserva de espacios públicos
 */
class SolicitudReserva : Solicitud {
    private string espacio;
    private string fechaReserva;
    private string horaInicio;
    private int horas;

    public string Espacio {
        get { return espacio; }
        set { espacio = value; }
    }

    public string FechaReserva {
        get { return fechaReserva; }
        set { fechaReserva = value; }
    }

    public string HoraInicio {
        get { return horaInicio; }
        set { horaInicio = value; }
    }

    public int Horas {
        get { return horas; }
        set { horas = value; }
    }

    public SolicitudReserva(string id, string fecha,
        Administrativo administrativo, string espacio, string fechaReserva,
        string horaInicio, int horas) : base(id, fecha, administrativo) {
        this.Espacio = espacio;
        this.FechaReserva = fechaReserva;
        this.HoraInicio = horaInicio;
        this.Horas = horas;
    }

    public override string ToString() {
        return "Reserva Espacio. " + base.ToString() + espacio + " el " +
            fecha + " a las " + horaInicio + " durante " + horas + " horas" +
            ". Admin " + administrativo.Dni;
    }
}