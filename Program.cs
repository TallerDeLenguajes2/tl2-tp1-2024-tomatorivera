using System.IO.Compression;
using persistencia;

internal class Program
{
    private static Cadeteria cadeteria;

    private static void Main(string[] args)
    {
        try
        {
            // Selecciono el tipo de acceso a los datos
            AccesoDatos? acceso;
            System.Console.WriteLine("--- Seleccione el tipo de acceso a los datos ---");
            System.Console.WriteLine("\n\t1. CSV");
            System.Console.WriteLine("\t2. JSON");
            System.Console.Write("\n> Digite su opcion: ");
            string strOpcion = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(strOpcion, out int opcion))
            {
                MostrarError("Opción inválida. No se puede proceder con el cargado de datos.");
                return;
            }

            acceso = opcion switch
            {
                1 => new AccesoCsv(),
                2 => new AccesoJson(),
                _ => null
            };

            if (acceso == null)
            {
                MostrarError("Opción inválida. No se puede proceder con el cargado de datos.");
                return;
            }

            // Cargo los datos de los csv
            cadeteria = CrearCadeteria(acceso);
            CargarCadetes(acceso, cadeteria);
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
            return;
        }

        int opcionSeleccionada = 0;
        int opcionSalida = 5;
        do
        {
            System.Console.WriteLine($"\n\n\n### MENU PRINCIPAL - {cadeteria?.Nombre} ###\n");
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
                            System.Console.WriteLine("\n\n*** INGRESANDO NUEVO PEDIDO ***\n");
                            var cliente = SolicitarDatosCliente();
                            var pedidoA = SolicitarDatosPedido(cliente);
                            cadeteria.TomarPedido(pedidoA);
                            MostrarResultadoExitoso($"Nuevo pedido generado con éxito (NRO.: {pedidoA.Numero} - Cliente: {cliente.Nombre})");
                            break;

                        case 2:
                            if (!cadeteria.ListadoCadetes.Any()) throw new Exception("No hay cadetes a los cuales asignarles pedidos");
                            if (!cadeteria.PedidosTomados.Any()) throw new Exception("No hay pedidos sin asignar");

                            System.Console.WriteLine("\n\n*** ASIGNANDO UN PEDIDO ***\n");
                            var pedidoB = SolicitarSeleccionPedido(cadeteria.PedidosTomados);
                            System.Console.WriteLine();
                            var cadete = SolicitarSeleccionCadete();
                            cadeteria.AsignarCadete(cadete, pedidoB);
                            MostrarResultadoExitoso($"El pedido nro. {pedidoB.Numero} ha sido asignado al cadete {cadete.Nombre} ({cadete.Id})");
                            break;

                        case 3:
                            var pedidos = cadeteria.ObtenerTodosLosPedidos();
                            if (!pedidos.Any()) throw new Exception("No hay pedidos a los cuales modificarles el estado");

                            System.Console.WriteLine("\n\n*** MODIFICANDO ESTADO DE UN PEDIDO ***\n");

                            var pedidoC = SolicitarSeleccionPedido(pedidos);
                            System.Console.WriteLine();
                            var nuevoEstado = SolicitarSeleccionEstado();
                            pedidoC.Estado = nuevoEstado;

                            MostrarResultadoExitoso($"El estado del pedido nro. {pedidoC.Numero} ha sido modificado a: {nuevoEstado}");
                            break;

                        case 4:
                            var pedidosAsignados = cadeteria.PedidosAsignados;
                            if (!pedidosAsignados.Any()) throw new Exception("No hay pedidos para reasignar");

                            System.Console.WriteLine("\n\n*** REASIGNANDO UN PEDIDO ***\n");

                            var pedidoD = SolicitarSeleccionPedido(pedidosAsignados);
                            System.Console.WriteLine();
                            var cadeteB = SolicitarSeleccionCadete();

                            pedidoD.Cadete = cadeteB;
                            MostrarResultadoExitoso($"El pedido nro. {pedidoD.Numero} ha sido re-asignado al cadete {cadeteB.Nombre} ({cadeteB.Id})");
                            break;

                        default:
                            Console.WriteLine("\nSaliendo...");
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

        System.Console.WriteLine("\n\n\n*** INFORME ***\n");

        System.Console.WriteLine("* Envíos de cada cadete:");
        int totalEnvios = 0;
        foreach (var cadete in cadeteria.ListadoCadetes)
        {
            int nEnviosCompletados = cadeteria.BuscarPedidos(cadete.Id)
                                              .Where(p => p.Estado == Estado.COMPLETADO)
                                              .Count();
            int nEnviosPendientes = cadeteria.BuscarPedidos(cadete.Id)
                                             .Where(p => p.Estado == Estado.PENDIENTE)
                                             .Count();
            int totalPedidos = nEnviosCompletados + nEnviosPendientes;

            System.Console.WriteLine($"\t> CADETE ID {cadete.Id} ({cadete.Nombre}) - Envíos terminados: {nEnviosCompletados} Envíos pendientes: {nEnviosPendientes}");
            totalEnvios += totalPedidos;
        }

        System.Console.WriteLine($"\n* Envíos totales del día: {totalEnvios}");
    }

    private static Cadeteria CrearCadeteria(AccesoDatos acceso)
    {
        var datosCsv = acceso.LeerArchivo("datos_cadeteria.csv");
        var datos = datosCsv[0].Split(",");

        if (datos.Count() < 2) throw new Exception("No hay datos suficientes para instanciar la cadeteria");

        return new Cadeteria(datos[0], datos[1]);
    }

    private static void CargarCadetes(AccesoDatos acceso, Cadeteria cadeteria)
    {
        var datosCsv = acceso.LeerArchivo("datos_cadetes.csv");

        foreach (var linea in datosCsv)
        {
            var datos = linea.Split(",");

            if (datos.Count() < 4)
            {
                System.Console.WriteLine($"\n[!] No se pudo cargar el cadete: {linea} - {datos}");
                continue;
            }
            if (!int.TryParse(datos[0], out int id))
                continue;

            cadeteria.AltaCadete(new Cadete(id, datos[1], datos[2], datos[3]));
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

    private static void MostrarResultadoExitoso(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"\n[/] {mensaje}\n");
        Console.ResetColor();
    }

    private static Cliente SolicitarDatosCliente()
    {
        System.Console.Write("> Ingrese el DNI del cliente (sin puntos ni espacios): ");
        var dni = 0;
        var strDni = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(strDni)) throw new Exception("El DNI no puede estar vacio");
        if (!int.TryParse(strDni, out dni)) throw new Exception("El DNI debe ser un número");

        System.Console.Write("> Ingrese el nombre del cliente: ");
        var nombre = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("El nombre no puede estar vacío");

        System.Console.Write("> Ingrese el telefono del cliente: ");
        var telefono = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(telefono)) throw new Exception("El teléfono no puede estar vacío");

        System.Console.Write("> Ingrese la dirección del cliente: ");
        var direccion = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(direccion)) throw new Exception("La dirección no puede estar vacía");

        System.Console.Write("> Ingrese datos o referencias de la dirección del cliente (opcional): ");
        var datosRerencia = Console.ReadLine() ?? string.Empty;

        return new Cliente(dni, nombre, direccion, telefono, datosRerencia);
    }

    private static Pedido SolicitarDatosPedido(Cliente cliente)
    {
        System.Console.Write("> Ingrese los detalles del pedido (obligatorio): ");
        var detalles = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(detalles)) throw new Exception("Debe incluir los detalles del pedido");

        return new Pedido(detalles, cliente);
    }

    private static Pedido SolicitarSeleccionPedido(List<Pedido> pedidos)
    {
        var detallesPedidos = pedidos.Select(pedido => pedido.ToString());
        foreach (var detallePedido in detallesPedidos)
        {
            Console.WriteLine($"\t* {detallePedido}");
        }

        System.Console.Write("\n> Ingrese el número del pedido a asignar: ");
        var strNro = Console.ReadLine() ?? string.Empty;
        var nroPedido = 0;

        if (!int.TryParse(strNro, out nroPedido))
            throw new Exception("Debe ingresar un número entero para seleccionar el pedido");

        var pedidoSeleccionado = pedidos.Where(p => p.Numero == nroPedido).FirstOrDefault();
        if (pedidoSeleccionado == null)
            throw new Exception($"El número de pedido ingresado es inválido ({nroPedido})");

        return pedidoSeleccionado;
    }

    private static Cadete SolicitarSeleccionCadete()
    {
        var detallesCadetes = cadeteria.ListadoCadetes.Select(cadete => cadete.ToString());
        foreach (var detalle in detallesCadetes)
        {
            System.Console.WriteLine($"\t* {detalle}");
        }

        System.Console.Write("\n> Ingrese el ID del cadete al cual asignarle el pedido: ");
        var strId = Console.ReadLine() ?? string.Empty;
        var id = 0;

        if (!int.TryParse(strId, out id))
            throw new Exception("El ID debe ser un número");

        var cadeteSeleccionado = cadeteria.ListadoCadetes.Where(cadete => cadete.Id == id).FirstOrDefault();
        if (cadeteSeleccionado == null)
            throw new Exception("$No existe ningún cadete con el ID {id}");

        return cadeteSeleccionado;
    }

    private static Estado SolicitarSeleccionEstado()
    {
        int contador = 0;
        foreach (var estado in Enum.GetValues(typeof(Estado)))
        {
            System.Console.WriteLine($"> ID {++contador}. {estado}");
        }

        System.Console.Write("> Selecciona el ID del nuevo estado para el pedido: ");
        var strOpcion = Console.ReadLine() ?? string.Empty;

        Estado nuevoEstado;
        if (!Enum.TryParse(strOpcion, out nuevoEstado))
            throw new Exception("Seleccione un ID válido");

        return nuevoEstado;
    }
}