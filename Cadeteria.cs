public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;

        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedido>();
    }

    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public void TomarPedido(Pedido pedido)
    {
        listadoPedidos.Add(pedido);
    }

    public void AsignarCadete(Cadete cadete, Pedido pedido)
    {
        cadete.Pedidos.Add(pedido);

        // si el pedido le pertenecía a otro cliente, se lo remuevo
        var cadeteReasignado = listadoCadetes.Where(c => c.Id != cadete.Id && c.Pedidos.Contains(pedido)).FirstOrDefault();
        if (cadeteReasignado != null) cadeteReasignado.Pedidos.Remove(pedido);

        // también remuevo el pedido de la lista de pedidos para que no se duplique
        listadoPedidos.Remove(pedido);
    }

    public void AltaCadete(Cadete cadete)
    {
        listadoCadetes.Add(cadete);
    }

    public List<Pedido> ObtenerPedidosAsignados()
    {
        return listadoCadetes.SelectMany(cadete => cadete.Pedidos).Where(pedido => pedido.Estado == Estado.PENDIENTE).ToList();
    }

    public List<Pedido> ObtenerTodosLosPedidos()
    {
        return listadoPedidos.Concat(ObtenerPedidosAsignados()).ToList();
    }

    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }
}