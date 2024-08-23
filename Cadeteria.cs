public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;

        listadoCadetes = new List<Cadete>();
    }

    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }
}