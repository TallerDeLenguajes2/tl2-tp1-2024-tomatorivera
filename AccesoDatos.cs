namespace persistencia;

public interface AccesoDatos
{
    List<string> LeerArchivo(string nombreArchivo);
}

public class AccesoCsv : AccesoDatos
{
    public List<string> LeerArchivo(string nombreArchivo)
    {
        var lineas = new List<string>();

        using (FileStream archivoCsv = new FileStream(nombreArchivo, FileMode.Open))
        {
            using (StreamReader readerCsv = new StreamReader(archivoCsv))
            {
                // salto la cabecera
                readerCsv.ReadLine();

                while (readerCsv.Peek() != -1)
                {
                    var linea = readerCsv.ReadLine();
                    if (!string.IsNullOrWhiteSpace(linea)) lineas.Add(linea);
                }
            }
        }

        return lineas;
    }
}

public class AccesoJson : AccesoDatos
{
    public List<string> LeerArchivo(string nombreArchivo)
    {
        string cadetesDeserealizado = string.Empty;
        using (FileStream json = new FileStream(nombreArchivo, FileMode.Create))
        {
            using (StreamReader reader = new StreamReader(json))
            {
                cadetesDeserealizado = reader.ReadToEnd();
            }
        }

        return new List<string>() { cadetesDeserealizado };
    }
}
