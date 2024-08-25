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
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Direccion { get => direccion; set => direccion = value; }
    public string Nombre { get => nombre; set => nombre = value; }

    public override string ToString()
    {
        return $"CLIENTE: \n\t* dni: {dni} \n\t* nombre: {nombre} \n\t* teléfono: {telefono} \n\t* dirección: {direccion} ({datosReferenciaDireccion})";
    }
}