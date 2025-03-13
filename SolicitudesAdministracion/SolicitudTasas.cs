/*Clase hija de Solicitud. Con atributos para conocer el importa y el nombre 
de las tasas más los atributos del padre(Solicitud).
*/
class SolicitudTasas : Solicitud {
    protected string concepto;
    protected float importe;
    public SolicitudTasas(string id, string fecha, string concepto, 
        float importe, Administrativo admAsociado )
        : base(id, fecha, admAsociado) {
        this.concepto = concepto;
        this.importe = importe;
    }

    public float GetImporte() { 
        return importe; 
    }

    public void SetImporte(float importe) {
        this.importe= importe;
    }

    public override string ToString() {
        return "Pago de tasas. " + base.ToString() + " Concepto: " + 
            concepto + " Importe total a pagar: " + importe + " DNI: " + 
            admAsociado.GetDni();
    }
}