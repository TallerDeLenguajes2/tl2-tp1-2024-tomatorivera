using System.Runtime.InteropServices;

public class Cadete
{
    private const float pagoPorPedidoEntregado = 500f;

    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    public Cadete()
    {
        id = -1;
        nombre = string.Empty;
        direccion = string.Empty;
        telefono = string.Empty;
    }

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }

    /*public float JornalACobrar()
    {
        return pagoPorPedidoEntregado * pedidos.Where(p => p.Estado == Estado.COMPLETADO).Count();
    }*/

    /*public List<Pedido> BuscarPedidos(Estado estado)
    {
        return pedidos.Where(p => p.Estado == estado).ToList();
    }*/

    public override string ToString()
    {
        return $"CADETE: {id} - {nombre} - {direccion} - {telefono}";
    }
}