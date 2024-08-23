public class Cliente
{
    private int dni;
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public Cliente(int dni, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.dni = dni;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        this.DatosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
}