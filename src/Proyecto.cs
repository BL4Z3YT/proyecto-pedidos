using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== SISTEMA DE CLASIFICACIÓN DE PEDIDOS =====\n");

        // ===== ENTRADA =====
        Console.Write("Ingrese el monto del pedido ($): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal monto))
        {
            Console.WriteLine("Error: Monto inválido.");
            return;
        }

        Console.Write("Ingrese la ciudad destino (interior/exterior): ");
        string ciudad = (Console.ReadLine() ?? "").ToLower().Trim();
        if (ciudad != "interior" && ciudad != "exterior")
        {
            Console.WriteLine("Error: Ciudad debe ser 'interior' o 'exterior'.");
            return;
        }

        Console.Write("Ingrese el tipo de cliente (nuevo/recurrente): ");
        string tipoCliente = (Console.ReadLine() ?? "").ToLower().Trim();
        if (tipoCliente != "nuevo" && tipoCliente != "recurrente")
        {
            Console.WriteLine("Error: Tipo de cliente debe ser 'nuevo' o 'recurrente'.");
            return;
        }

        Console.Write("Ingrese la cantidad de ítems: ");
        if (!int.TryParse(Console.ReadLine(), out int cantItems) || cantItems < 1)
        {
            Console.WriteLine("Error: Cantidad de ítems debe ser mayor a 0.");
            return;
        }

        // ===== PROCESO =====
        string categoria;
        decimal costoBase;

        // Regla 1: Envío gratis si monto >= 150.000 Y cliente recurrente
        if (monto >= 150000 && tipoCliente == "recurrente")
        {
            categoria = "GRATIS";
            costoBase = 0;
        }
        // Regla 2: Envío express si ítems >= 5 O monto >= 300.000
        else if (cantItems >= 5 || monto >= 300000)
        {
            categoria = "EXPRESS";
            costoBase = 25000;
        }
        // Regla 3: Envío estándar en todos los demás casos
        else
        {
            categoria = "ESTÁNDAR";
            costoBase = 15000;
        }

        // Regla 4: Costo adicional si ciudad es "exterior"
        decimal costoEnvio = costoBase;
        if (ciudad == "exterior")
        {
            costoEnvio += 20000;
        }

        // ===== SALIDA =====
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
            Console.WriteLine($"Recargo por exterior: ${20000:F2}");
        }
        Console.WriteLine($"\nCOSTO TOTAL DE ENVÍO: ${costoEnvio:F2}");
        
        // Mensaje personalizado
        Console.WriteLine("\n----- MENSAJE AL CLIENTE -----");
        if (categoria == "GRATIS")
        {
            Console.WriteLine($"¡Excelente! Tu envío es GRATIS. Total a pagar: ${costoEnvio:F2}");
        }
        else if (categoria == "EXPRESS")
        {
            Console.WriteLine($"Tu pedido será entregado con EXPRESS. Costo de envío: ${costoEnvio:F2}");
        }
        else
        {
            Console.WriteLine($"Tu pedido será entregado de forma ESTÁNDAR. Costo de envío: ${costoEnvio:F2}");
        }
        
        Console.WriteLine("==============================");
    }
}
