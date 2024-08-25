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

    public void TomarPedido(Pedido pedido)
    {
        listadoPedidos.Add(pedido);
    }

    public void AsignarCadete(int id, Pedido pedido)
    {
        var cadete = listadoCadetes.Where(cadete => cadete.Id == id).FirstOrDefault();
        if (cadete != null)
        {
            cadete.Pedidos.Add(pedido);
            listadoPedidos.Remove(pedido);
        }
        else
            throw new Exception($"No existe un cadete con el ID: {id}");
    }

    public void AltaCadete(Cadete cadete)
    {
        listadoCadetes.Add(cadete);
    }

    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }
}