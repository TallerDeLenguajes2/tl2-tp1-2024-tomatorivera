internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Cargo los datos de los csv
            var cadeteria = crearCadeteria();
            cargarCadetes(cadeteria);

            // TEMPORAL: para ver los datos cargados del csv
            System.Console.WriteLine(cadeteria);
            foreach (var dato in cadeteria.ListadoCadetes)
            {
                System.Console.WriteLine(dato);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"\n[!] Error: {ex.Message}\n");
            Console.ResetColor();
        }
    }

    private static Cadeteria crearCadeteria()
    {
        var datosCsv = LeerCsv("datos_cadeteria.csv");
        var datos = datosCsv[0].Split(",");

        if (datos.Count() < 2) throw new Exception("No hay datos suficientes para instanciar la cadeteria");

        return new Cadeteria(datos[0], datos[1]);
    }

    private static void cargarCadetes(Cadeteria cadeteria)
    {
        var datosCsv = LeerCsv("datos_cadetes.csv");

        foreach (var linea in datosCsv)
        {
            var datos = linea.Split(",");
            if (datos.Count() < 4)
            {
                System.Console.WriteLine($"\n[!] No se pudo cargar el cadete: {linea} - {datos}");
                continue;
            }

            cadeteria.ListadoCadetes.Add(new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]));
        }
    }

    private static List<string> LeerCsv(string nombreArchivo, bool tieneCabecera = true)
    {
        var lineas = new List<string>();

        using (FileStream archivoCsv = new FileStream(nombreArchivo, FileMode.Open))
        {
            using (StreamReader readerCsv = new StreamReader(archivoCsv))
            {
                // Salto la cabecera
                if (tieneCabecera) readerCsv.ReadLine();
                
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