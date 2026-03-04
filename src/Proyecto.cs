using System;

/*
 * SISTEMA DE CLASIFICACIÓN DE PEDIDOS
 * Proyecto Integrador - Entrega 1
 * I.U. Pascual Bravo - Lógica de Programación
 * 
 * Equipo: Juan David Agudelo y Yeisson Gaviria
 * Modalidad: A (Problema propuesto por el docente)
 * 
 * Descripción: Programa que clasifica pedidos según monto, ciudad, 
 * tipo de cliente y cantidad de ítems para determinar categoría 
 * de despacho y costo de envío.
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

    static void Main()
    {
        Console.WriteLine("===== SISTEMA DE CLASIFICACIÓN DE PEDIDOS =====\n");

        // ===== ENTRADA =====
        Console.Write("Ingrese el monto del pedido ($): ");
        string entradaMonto = Console.ReadLine() ?? "";
        if (!ValidarDecimalPositivo(entradaMonto, out decimal monto, "Monto del pedido"))
        {
            return;
        }

        Console.Write("Ingrese la ciudad destino (interior/exterior): ");
        string entradaCiudad = Console.ReadLine() ?? "";
        if (!ValidarOpcionTexto(entradaCiudad, new string[] { "interior", "exterior" }, out string ciudad, "Ciudad destino"))
        {
            return;
        }

        Console.Write("Ingrese el tipo de cliente (nuevo/recurrente): ");
        string entradaCliente = Console.ReadLine() ?? "";
        if (!ValidarOpcionTexto(entradaCliente, new string[] { "nuevo", "recurrente" }, out string tipoCliente, "Tipo de cliente"))
        {
            return;
        }

        Console.Write("Ingrese la cantidad de ítems: ");
        string entradaItems = Console.ReadLine() ?? "";
        if (!int.TryParse(entradaItems, out int cantItems) || cantItems < 1)
        {
            Console.WriteLine("Error: Cantidad de ítems debe ser mayor a 0.");
            return;
        }

    // Método para determinar categoría y costo base según reglas de negocio
    static (string categoria, decimal costoBase) DeterminarCategoriaYCosto(decimal monto, string tipoCliente, int cantItems)
    {
        // Regla 1: Envío gratis si monto >= 150.000 Y cliente recurrente
        if (monto >= MONTO_GRATIS && tipoCliente == "recurrente")
        {
            return ("GRATIS", 0);
        }
        // Regla 2: Envío express si ítems >= 5 O monto >= 300.000
        else if (cantItems >= ITEMS_EXPRESS || monto >= MONTO_EXPRESS)
        {
            return ("EXPRESS", COSTO_BASE_EXPRESS);
        }
        // Regla 3: Envío estándar en todos los demás casos
        else
        {
            return ("ESTÁNDAR", COSTO_BASE_ESTANDAR);
        }
    }

        // ===== PROCESO =====
        var (categoria, costoBase) = DeterminarCategoriaYCosto(monto, tipoCliente, cantItems);

        // Regla 4: Costo adicional si ciudad es "exterior"
        decimal costoEnvio = costoBase;
        if (ciudad == "exterior")
        {
            costoEnvio += COSTO_EXTERIOR;
        }

    // Método para mostrar resumen del pedido
    static void MostrarResumenPedido(decimal monto, int cantItems, string tipoCliente, string ciudad, string categoria, decimal costoBase, decimal costoEnvio)
    {
        Console.WriteLine("\n===== RESUMEN DEL PEDIDO =====");
        Console.WriteLine($"Monto del pedido: ${monto:F2}");
        Console.WriteLine($"Cantidad de ítems: {cantItems}");
        Console.WriteLine($"Tipo de cliente: {tipoCliente}");
        Console.WriteLine($"Ciudad destino: {ciudad}");
        Console.WriteLine("\n----- RESULTADO -----");
        Console.WriteLine($"Categoría de despacho: {categoria}");
        Console.WriteLine($"Costo base: ${costoBase:F2}");
        if (ciudad == "exterior")
        {
            Console.WriteLine($"Recargo por exterior: ${COSTO_EXTERIOR:F2}");
        }
        Console.WriteLine($"\nCOSTO TOTAL DE ENVÍO: ${costoEnvio:F2}");
    }

        // ===== SALIDA =====
        MostrarResumenPedido(monto, cantItems, tipoCliente, ciudad, categoria, costoBase, costoEnvio);
        
        // Mensaje personalizado al cliente
        MostrarMensajeAlCliente(categoria, costoEnvio);
        
        Console.WriteLine("==============================");
    }

    // Método para mostrar mensaje personalizado según categoría
    static void MostrarMensajeAlCliente(string categoria, decimal costoEnvio)
    {
        Console.WriteLine("\n----- MENSAJE AL CLIENTE -----");
        switch (categoria)
        {
            case "GRATIS":
                Console.WriteLine($"¡Excelente! Tu envío es GRATIS. Total a pagar: ${costoEnvio:F2}");
                break;
            case "EXPRESS":
                Console.WriteLine($"Tu pedido será entregado con EXPRESS. Costo de envío: ${costoEnvio:F2}");
                break;
            default:
                Console.WriteLine($"Tu pedido será entregado de forma ESTÁNDAR. Costo de envío: ${costoEnvio:F2}");
                break;
        };
    }
}
