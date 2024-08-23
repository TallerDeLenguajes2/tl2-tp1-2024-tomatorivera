public class Pedido
{
    private int numero;
    private string observaciones;
    private Cliente cliente;
    private Estado estadoPedido;

    public Pedido(int numero, string observaciones, Cliente cliente, Estado estadoPedido)
    {
        this.numero = numero;
        this.observaciones = observaciones;
        this.cliente = cliente;
        this.estadoPedido = estadoPedido;
    }

    public string VerDatosCliente()
    {
        // TODO
        return "";
    }

    public string VerDireccionCliente()
    {
        // TODO
        return "";
    }
}