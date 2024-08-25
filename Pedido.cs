public class Pedido
{
    private int numero;
    private string observaciones;
    private Cliente cliente;
    private Estado estado;

    public Pedido(int numero, string observaciones, Cliente cliente, Estado estado)
    {
        this.numero = numero;
        this.observaciones = observaciones;
        this.cliente = cliente;
        this.estado = estado;
    }

    public int Numero { get => numero; set => numero = value; }
    public Estado Estado { get => estado; set => estado = value; }

    public string VerDatosCliente()
    {
        return $"*** CLIENTE ASOCIADO AL PEDIDO NRO. {numero} *** \n{cliente}";
    }

    public string VerDireccionCliente()
    {
        return $"Pedido NRO. {numero} - Dirección del cliente: {cliente.Direccion}";
    }
}