internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Cargo los datos de los csv
            var cadeteria = CrearCadeteria();
            CargarCadetes(cadeteria);
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
        }

        int opcionSeleccionada = 0;
        int opcionSalida = 5;
        do
        {
            System.Console.WriteLine("### MENU PRINCIPAL ###\n");
            System.Console.WriteLine("\t1. Dar de alta un pedido");
            System.Console.WriteLine("\t2. Asignar un pedido a un cadete");
            System.Console.WriteLine("\t3. Cambiar el estado de un pedido");
            System.Console.WriteLine("\t4. Reasignar el cadete en un pedido");
            System.Console.WriteLine("\t5. Salir del programa");
            System.Console.Write("\n> Digite su opción: ");

            var strSeleccion = Console.ReadLine() ?? string.Empty;

            try
            {
                if (!int.TryParse(strSeleccion, out opcionSeleccionada))
                {
                    throw new Exception("Debe ingresar un número entero");
                }
                else if (opcionSeleccionada < 1 || opcionSeleccionada > opcionSalida)
                {
                    throw new Exception("Debe ingresar una opción válida");
                }
                else
                {
                    switch (opcionSeleccionada)
                    {
                        case 1:
                            break;

                        case 2:
                            break;

                        case 3:
                            break;

                        case 4:
                            break;

                        default:
                            Console.WriteLine("\n\n*** Saliendo...");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
        while (opcionSeleccionada != opcionSalida);
    }

    private static Cadeteria CrearCadeteria()
    {
        var datosCsv = LeerCsv("datos_cadeteria.csv");
        var datos = datosCsv[0].Split(",");

        if (datos.Count() < 2) throw new Exception("No hay datos suficientes para instanciar la cadeteria");

        return new Cadeteria(datos[0], datos[1]);
    }

    private static void CargarCadetes(Cadeteria cadeteria)
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

            cadeteria.AltaCadete(new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]));
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

    private static void MostrarError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"\n[!] Error: {error}\n");
        Console.ResetColor();
    }
}   