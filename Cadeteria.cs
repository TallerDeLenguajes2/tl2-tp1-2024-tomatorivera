using System.IO.Compression;
using System.Text.Json.Serialization;

public class Cadeteria
{
    [JsonPropertyName("nombre")]
    private string nombre;
    [JsonPropertyName("telefono")]
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> pedidosAsignados;
    private List<Pedido> pedidosTomados;

    public Cadeteria()
    {
        nombre = string.Empty;
        telefono = string.Empty;
        listadoCadetes = new List<Cadete>();
        pedidosAsignados = new List<Pedido>();
        pedidosTomados = new List<Pedido>();
    }

    public Cadeteria(string nombre, string telefono) : this()
    {
        this.nombre = nombre;
        this.telefono = telefono;
    }

    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> PedidosAsignados { get => pedidosAsignados; set => pedidosAsignados = value; }
    public List<Pedido> PedidosTomados { get => pedidosTomados; set => pedidosTomados = value; }
    public string Nombre { get => nombre; set => nombre = value; }

    public void TomarPedido(Pedido pedido)
    {
        pedidosTomados.Add(pedido);
    }

    public void AsignarCadete(Cadete cadete, Pedido pedido)
    {
        pedido.Cadete = cadete;

        /*
        cadete.Pedidos.Add(pedido);

        // si el pedido le pertenecía a otro cliente, se lo remuevo
        var cadeteReasignado = listadoCadetes.Where(c => c.Id != cadete.Id && c.Pedidos.Contains(pedido)).FirstOrDefault();
        if (cadeteReasignado != null) cadeteReasignado.Pedidos.Remove(pedido);
        */

        // también remuevo el pedido de la lista de pedidos para que no se duplique
        pedidosTomados.Remove(pedido);
        pedidosAsignados.Add(pedido);
    }

    public void AltaCadete(Cadete cadete)
    {
        listadoCadetes.Add(cadete);
    }

    public List<Pedido> ObtenerTodosLosPedidos()
    {
        return pedidosTomados.Concat(pedidosAsignados).ToList();
    }

    public float JornalACobrar(int idCadete)
    {
        float precioPorPedido = 500;
        return precioPorPedido * pedidosAsignados.Where(p => p.Cadete.Id == idCadete).Count();
    }

    public List<Pedido> BuscarPedidos(int idCadete)
    {
        return pedidosAsignados.Where(p => p.Cadete.Id == idCadete).ToList();
    }

    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }
}