/*Clase Ciclista con la información de los participantes*/
class Ciclista {
    protected int numDorsal;
    protected string nombre;
    protected string equipo;

    public Ciclista(int numDorsal, string nombre, string equipo) {
        this.numDorsal = numDorsal;
        this.nombre = nombre;
        this.equipo = equipo;
    }

    public string GetNombre() {
        return nombre;
    }

    public override string ToString() {
        return nombre + " || Dorsal: " + numDorsal + " || Equipo: "
            + equipo;
    }
}