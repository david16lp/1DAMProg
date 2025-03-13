/* Clase con los datos del personal administrativo, asociación uno a muchos
con la clase Solicitud. Asociación unidireccional desde Solicitud.*/
class Administrativo {
    protected string nombre;
    protected string dni;
    protected int telefono;

    public Administrativo(string nombre, string dni, int telefono) {
        this.nombre = nombre;
        this.dni = dni;
        this.telefono = telefono;
    }

    public string GetDni() {
        return dni;
    }

    public void SetDni(string dni) {
        this.dni = dni;
    }

    public string GetNombre() {
        return nombre;
    }

    public int GetTelefono() {
        return telefono;
    }
}