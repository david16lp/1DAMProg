/*Clase hija de Solicitud. Con atributos para indicar donde quieren cobrar 
su nómina(Número de cuenta) más los atributos del padre(Solicitud).
*/
class SolicitudDomiciliacion : Solicitud {
    protected string numCuenta;

    public SolicitudDomiciliacion(string id, string fecha, string numCuenta, 
        Administrativo admAsociado)
        : base(id, fecha, admAsociado) {
        this.numCuenta = numCuenta;
    }

    public override string ToString() { 
        return "Cambio Domiciliación. " + base.ToString() + " Núm cuenta: " + 
            numCuenta + " DNI: " + admAsociado.GetDni();
    }
}