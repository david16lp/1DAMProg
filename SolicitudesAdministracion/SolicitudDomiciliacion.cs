/*
 * Tipo de solicitud: domiciliación bancaria
 */
class SolicitudDomiciliacion : Solicitud {
    private string cuenta;

    public string Cuenta {
        get { return cuenta; }
        set { cuenta = value; }
    }

    public SolicitudDomiciliacion(string id, string fecha,
        Administrativo administrativo, string cuenta)
        : base(id, fecha, administrativo) {
        this.Cuenta = cuenta;
    }

    public override string ToString() {
        return "Cambio Domiciliación. " + base.ToString() +
            "Núm cuenta " + cuenta + ". Admin " + administrativo.Dni;
    }
}