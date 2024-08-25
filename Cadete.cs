public class Cadete
{
    private const float pagoPorPedidoEntregado = 500f;

    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> pedidos;


    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.pedidos = new List<Pedido>();
    }

    public int Id { get => id; set => id = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public float JornalACobrar()
    {
        return pagoPorPedidoEntregado * pedidos.Where(p => p.Estado == Estado.COMPLETADO).Count();
    }

    public List<Pedido> BuscarPedidos(Estado estado)
    {
        return pedidos.Where(p => p.Estado == estado).ToList();
    }

    public void ModificarEstadoPedido(int nroPedido, Estado nuevoEstado)
    {
        var p = pedidos.Where(p => p.Numero == nroPedido).FirstOrDefault();
        if (p != null)
        {
            p.Estado = nuevoEstado;
        }
        else
            throw new Exception($"No se ha encontrado un pedido con número: {nroPedido} (cadete id {id})");
    }

    public override string ToString()
    {
        return $"CLIENTE: {id} - {nombre} - {direccion} - {telefono}";
    }
}