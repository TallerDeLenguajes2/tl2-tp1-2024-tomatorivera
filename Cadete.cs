public class Cadete
{
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

    public override string ToString()
    {
        return $"CLIENTE: {id} - {nombre} - {direccion} - {telefono}";
    }
}