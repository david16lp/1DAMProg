/*
 * Tipo de solicitud: pago de tasas
 */
class SolicitudTasas : Solicitud {
    private string concepto;
    private float importe;

    public string Concepto {
        get { return concepto; }
        set { concepto = value; }
    }

    public float Importe {
        get { return importe; }
        set { importe = value; }
    }

    public SolicitudTasas(string id, string fecha, Administrativo
        administrativo, string concepto, float importe) :
        base(id, fecha, administrativo) {
        this.Concepto = concepto;
        this.Importe = importe;
    }

    public override string ToString() {
        return "Pago Tasas. " + base.ToString() + concepto + ", " +
            importe.ToString("N2") + " eur" + ". Admin " + administrativo.Dni;
    }
}