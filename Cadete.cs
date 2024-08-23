public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> pedidos;

    public Cadete(int id, string nombre, string direccion, string telefono, List<Pedido> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.pedidos = pedidos;
    }

    public float JornalACobrar()
    {
        // TODO
        return 0.0f;
    }

    public void MarcarCompletado(int numeroPedido) 
    {
        // TODO
    }

    public List<Pedido> BuscarPedidos(Estado estado)
    {
        // TODO
        return new List<Pedido>();
    }
}