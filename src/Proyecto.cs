using System;
using System.Collections.Generic;
using System.Linq;

/*
 * SISTEMA DE CLASIFICACIÓN DE PEDIDOS
 * Proyecto Integrador - Entrega 2
 * I.U. Pascual Bravo - Lógica de Programación
 * 
 * Equipo: Juan David Agudelo y Yeisson Gaviria
 * Modalidad: A (Problema propuesto por el docente)
 * 
 * Descripción: Programa que clasifica pedidos según monto, ciudad, 
 * tipo de cliente y cantidad de ítems para determinar categoría 
 * de despacho y costo de envío. Versión con menú continuo, 
 * registros múltiples y reportes estadísticos.
 */

class Program
{
    // Constantes para las reglas de negocio
    private const decimal MONTO_GRATIS = 150000;
    private const decimal MONTO_EXPRESS = 300000;
    private const int ITEMS_EXPRESS = 5;
    private const decimal COSTO_BASE_EXPRESS = 25000;
    private const decimal COSTO_BASE_ESTANDAR = 15000;
    private const decimal COSTO_EXTERIOR = 20000;

    // Record para almacenar los datos de un pedido
    record Pedido(decimal Monto, string Ciudad, string TipoCliente, int CantItems, string Categoria, decimal CostoEnvio);

    // Método para validar entrada decimal positiva
    static bool ValidarDecimalPositivo(string entrada, out decimal valor, string campo)
    {
        if (!decimal.TryParse(entrada, out valor) || valor <= 0)
        {
            Console.WriteLine($"Error: {campo} debe ser un valor positivo mayor a cero.");
            return false;
        }
        return true;
    }

    // Método para validar entrada de texto con opciones específicas
    static bool ValidarOpcionTexto(string entrada, string[] opcionesValidas, out string valor, string campo)
    {
        valor = (entrada ?? "").ToLower().Trim();
        foreach (string opcion in opcionesValidas)
        {
            if (valor == opcion) return true;
        }
        Console.WriteLine($"Error: {campo} debe ser una de las siguientes opciones: {string.Join(", ", opcionesValidas)}");
        return false;
    }

    // Método para capturar un pedido desde la consola
    static Pedido? CapturarPedido()
    {
        Console.WriteLine("\n===== NUEVO PEDIDO =====");

        // Entrada del monto
        Console.Write("Ingrese el monto del pedido ($): ");
        string entradaMonto = Console.ReadLine() ?? "";
        if (!ValidarDecimalPositivo(entradaMonto, out decimal monto, "Monto del pedido"))
        {
            return null;
        }

        // Entrada de la ciudad
        Console.Write("Ingrese la ciudad destino (interior/exterior): ");
        string entradaCiudad = Console.ReadLine() ?? "";
        if (!ValidarOpcionTexto(entradaCiudad, new string[] { "interior", "exterior" }, out string ciudad, "Ciudad destino"))
        {
            return null;
        }

        // Entrada del tipo de cliente
        Console.Write("Ingrese el tipo de cliente (nuevo/recurrente): ");
        string entradaCliente = Console.ReadLine() ?? "";
        if (!ValidarOpcionTexto(entradaCliente, new string[] { "nuevo", "recurrente" }, out string tipoCliente, "Tipo de cliente"))
        {
            return null;
        }

        // Entrada de la cantidad de ítems
        Console.Write("Ingrese la cantidad de ítems: ");
        string entradaItems = Console.ReadLine() ?? "";
        if (!int.TryParse(entradaItems, out int cantItems) || cantItems < 1)
        {
            Console.WriteLine("Error: Cantidad de ítems debe ser mayor a 0.");
            return null;
        }

        // Procesar el pedido
        var (categoria, costoBase) = DeterminarCategoriaYCosto(monto, tipoCliente, cantItems);
        decimal costoEnvio = costoBase;
        if (ciudad == "exterior")
        {
            costoEnvio += COSTO_EXTERIOR;
        }

        // Mostrar resumen
        MostrarResumenPedido(monto, cantItems, tipoCliente, ciudad, categoria, costoBase, costoEnvio);
        MostrarMensajeAlCliente(categoria, costoEnvio);

        // Retornar el pedido
        return new Pedido(monto, ciudad, tipoCliente, cantItems, categoria, costoEnvio);
    }

    static void Main()
    {
        bool sistemaActivo = true;
        List<Pedido> pedidos = new List<Pedido>();

        do
        {
            Console.Clear();
            MostrarEncabezado();
            MostrarMenu();

            string opcion = Console.ReadLine()?.Trim();

            switch (opcion)
            {
                case "1":
                    Pedido? nuevoPedido = CapturarPedido();
                    if (nuevoPedido != null)
                    {
                        pedidos.Add(nuevoPedido.Value);
                        Console.WriteLine("Pedido agregado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el pedido debido a datos inválidos.");
                    }
                    PresionarEnterParaContinuar();
                    break;

                case "2":
                    MostrarEstadisticas(pedidos);
                    PresionarEnterParaContinuar();
                    break;

                case "0":
                    Console.WriteLine("Saliendo del sistema. ¡Hasta pronto!");
                    sistemaActivo = false;
                    break;

                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    PresionarEnterParaContinuar();
                    break;
            }
        } while (sistemaActivo);
    }

    static void MostrarEncabezado()
    {
        Console.WriteLine("===== SISTEMA DE CLASIFICACIÓN DE PEDIDOS - ENTREGA 2 =====");
        Console.WriteLine("Unidad 2 · Escalado iterativo: menú continuo, registros múltiples y reportes estadísticos\n");
    }

    static void MostrarMenu()
    {
        Console.WriteLine("----- MENÚ PRINCIPAL -----");
        Console.WriteLine("1. Registrar nuevo pedido");
        Console.WriteLine("2. Ver reporte estadístico");
        Console.WriteLine("0. Salir");
        Console.Write("Opción: ");
    }

    static void PresionarEnterParaContinuar()
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }

    // Método para mostrar mensaje personalizado según categoría
    static void MostrarMensajeAlCliente(string categoria, decimal costoEnvio)
    {
        Console.WriteLine("\n----- MENSAJE AL CLIENTE -----");
        switch (categoria)
        {
            case "GRATIS":
                Console.WriteLine($"¡Excelente! Tu envío es GRATIS. Total a pagar: {FormatearMoneda(costoEnvio)}");
                break;
            case "EXPRESS":
                Console.WriteLine($"Tu pedido será entregado con EXPRESS. Costo de envío: {FormatearMoneda(costoEnvio)}");
                break;
            default:
                Console.WriteLine($"Tu pedido será entregado de forma ESTÁNDAR. Costo de envío: {FormatearMoneda(costoEnvio)}");
                break;
        };
    }

    static string FormatearMoneda(decimal valor)
    {
        return $"${valor:F2}";
    }

    // Método para mostrar resumen del pedido
    static void MostrarResumenPedido(decimal monto, int cantItems, string tipoCliente, string ciudad, string categoria, decimal costoBase, decimal costoEnvio)
    {
        Console.WriteLine("\n===== RESUMEN DEL PEDIDO =====");
        Console.WriteLine($"Monto del pedido: {FormatearMoneda(monto)}");
        Console.WriteLine($"Cantidad de ítems: {cantItems}");
        Console.WriteLine($"Tipo de cliente: {tipoCliente}");
        Console.WriteLine($"Ciudad destino: {ciudad}");
        Console.WriteLine("\n----- RESULTADO -----");
        Console.WriteLine($"Categoría de despacho: {categoria}");
        Console.WriteLine($"Costo base: {FormatearMoneda(costoBase)}");
        if (ciudad == "exterior")
        {
            Console.WriteLine($"Recargo por exterior: {FormatearMoneda(COSTO_EXTERIOR)}");
        }
        Console.WriteLine($"\nCOSTO TOTAL DE ENVÍO: {FormatearMoneda(costoEnvio)}");
    }

    static (string categoria, decimal costoBase) DeterminarCategoriaYCosto(decimal monto, string tipoCliente, int cantItems)
    {
        if (monto >= MONTO_GRATIS && tipoCliente == "recurrente")
        {
            return ("GRATIS", 0);
        }
        else if (cantItems >= ITEMS_EXPRESS || monto >= MONTO_EXPRESS)
        {
            return ("EXPRESS", COSTO_BASE_EXPRESS);
        }
        else
        {
            return ("ESTÁNDAR", COSTO_BASE_ESTANDAR);
        }
    }

    // Método para mostrar estadísticas de los pedidos
    static void MostrarEstadisticas(List<Pedido> pedidos)
    {
        Console.WriteLine("\n===== ESTADÍSTICAS DE PEDIDOS =====");
        if (pedidos.Count == 0)
        {
            Console.WriteLine("No hay pedidos registrados.");
            return;
        }

        int totalPedidos = pedidos.Count;
        decimal totalCostoEnvio = pedidos.Sum(p => p.CostoEnvio);
        decimal promedioCosto = totalCostoEnvio / totalPedidos;

        Console.WriteLine($"Total de pedidos procesados: {totalPedidos}");
        Console.WriteLine($"Costo total de envíos: {FormatearMoneda(totalCostoEnvio)}");
        Console.WriteLine($"Costo promedio de envío: {FormatearMoneda(promedioCosto)}");

        // Estadísticas por categoría
        var categorias = pedidos.GroupBy(p => p.Categoria).Select(g => new { Categoria = g.Key, Cantidad = g.Count(), TotalCosto = g.Sum(p => p.CostoEnvio) });
        Console.WriteLine("\nEstadísticas por categoría:");
        foreach (var cat in categorias)
        {
            Console.WriteLine($"- {cat.Categoria}: {cat.Cantidad} pedidos, Total: {FormatearMoneda(cat.TotalCosto)}");
        }

        // Estadísticas por ciudad
        var ciudades = pedidos.GroupBy(p => p.Ciudad).Select(g => new { Ciudad = g.Key, Cantidad = g.Count() });
        Console.WriteLine("\nEstadísticas por ciudad destino:");
        foreach (var ciu in ciudades)
        {
            Console.WriteLine($"- {ciu.Ciudad}: {ciu.Cantidad} pedidos");
        }

        // Estadísticas por tipo de cliente
        var tiposCliente = pedidos.GroupBy(p => p.TipoCliente).Select(g => new { Tipo = g.Key, Cantidad = g.Count() });
        Console.WriteLine("\nEstadísticas por tipo de cliente:");
        foreach (var tipo in tiposCliente)
        {
            Console.WriteLine($"- {tipo.Tipo}: {tipo.Cantidad} pedidos");
        }
    }
