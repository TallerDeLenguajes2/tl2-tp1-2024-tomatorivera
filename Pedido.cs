public class Pedido
{
    private static int ultimoNumeroGenerado = 0;
    
    private int numero;
    private string observaciones;
    private Cliente cliente;
    private Estado estado;

    public Pedido(string observaciones, Cliente cliente)
    {
        numero = ++ultimoNumeroGenerado; // El ID se auto genera de manera consecutiva
        estado = Estado.PENDIENTE; // El estado de un pedido al instanciarse siempre es pendiente

        this.observaciones = observaciones;
        this.cliente = cliente;
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

    public override string ToString()
    {
        return $"PEDIDO NRO. {numero} - Obs.: {observaciones} - Cliente: {cliente.Nombre} - Estado: {estado}";
    }
}