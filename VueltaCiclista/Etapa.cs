/*Clase Etapa que almacena la información relacionada con la etapa*/
class Etapa {
    protected string fecha;
    protected Ciclista[] podio;

    public Etapa(string fecha) {
        this.fecha = fecha;
        this.podio = new Ciclista[3];
    }

    public void SetPodio(int posicion, Ciclista ciclista) {
        if (posicion >= 0 && posicion < 3) {
            podio[posicion] = ciclista;
        }
    }

    public Ciclista[] GetPodio() {
        return podio;
    }

    public override string ToString() {
        string resultado = fecha + "\n";

        for (int i = 0; i < podio.Length; i++) {
            resultado += "\t" + podio[i].GetNombre() + "\n";
        }
        return resultado;
    }
}
