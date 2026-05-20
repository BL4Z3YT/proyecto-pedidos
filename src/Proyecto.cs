using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit { }
}

/*
 * SISTEMA DE CLASIFICACIÓN DE PEDIDOS
 * Proyecto Integrador - Entrega 3
 * I.U. Pascual Bravo - Lógica de Programación
 * 
 * Equipo: Juan David Agudelo (BL4Z3YT) y Yeisson Gaviria
 * Modalidad: A (Problema propuesto por el docente)
 * 
 * Descripción: Programa que clasifica pedidos según monto, ciudad, 
 * tipo de cliente y cantidad de ítems para determinar categoría 
 * de despacho y costo de envío. Versión modular con separación 
 * de responsabilidades: UI, validación, lógica de negocio.
 */

class Program
{
    // ===== CONSTANTES DE NEGOCIO =====
    private const decimal MONTO_GRATIS = 150000;
    private const decimal MONTO_EXPRESS = 300000;
    private const int ITEMS_EXPRESS = 5;
    private const decimal COSTO_BASE_EXPRESS = 25000;
    private const decimal COSTO_BASE_ESTANDAR = 15000;
    private const decimal COSTO_EXTERIOR = 20000;

    /// <summary>
    /// Registro que representa un pedido con todos sus atributos y resultado de clasificación.
    /// </summary>
    record Pedido(decimal Monto, string Ciudad, string TipoCliente, int CantItems, string Categoria, decimal CostoEnvio);

    // ===== PUNTO DE ENTRADA =====
    
    /// <summary>
    /// Función principal que orquesta el ciclo de vida del sistema.
    /// Responsabilidad única: coordinar el do-while, delegar a funciones especializadas.
    /// </summary>
    static void Main()
    {
        bool sistemaActivo = true;
        List<Pedido> pedidos = new List<Pedido>();

        do
        {
            MostrarEncabezado();
            MostrarMenu();
            string opcion = Console.ReadLine()?.Trim();

            switch (opcion)
            {
                case "1":
                    RegistrarPedido(pedidos);
                    break;

                case "2":
                    MostrarEstadisticas(pedidos);
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

    // ===== CAPA DE INTERFAZ DE USUARIO =====

    /// <summary>
    /// Limpia la pantalla y muestra el encabezado del sistema.
    /// </summary>
    static void MostrarEncabezado()
    {
        Console.Clear();
        Console.WriteLine("===== SISTEMA DE CLASIFICACIÓN DE PEDIDOS - ENTREGA 3 =====");
        Console.WriteLine("Unidad 3 · Refactorización modular con funciones especializadas\n");
    }

    /// <summary>
    /// Presenta las opciones del menú principal.
    /// </summary>
    static void MostrarMenu()
    {
        Console.WriteLine("----- MENÚ PRINCIPAL -----");
        Console.WriteLine("1. Registrar nuevo pedido");
        Console.WriteLine("2. Ver reporte estadístico");
        Console.WriteLine("0. Salir");
        Console.Write("Opción: ");
    }

    /// <summary>
    /// Solicita al usuario presionar Enter para continuar.
    /// Espera a que el usuario presione una tecla.
    /// </summary>
    static void PresionarEnterParaContinuar()
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }

    // ===== CAPA DE ENTRADA Y VALIDACIÓN =====

    /// <summary>
    /// Captura los datos de un pedido con reintentos en caso de entrada inválida.
    /// Responsabilidad única: leer y validar entrada del usuario.
    /// </summary>
    /// <returns>Un registro Pedido completamente validado y procesado.</returns>
    static Pedido CapturarPedidoCompleto()
    {
        Console.WriteLine("\n===== NUEVO PEDIDO =====");

        decimal monto = ObtenerDecimalValido("Ingrese el monto del pedido ($): ", "Monto del pedido");
        string ciudad = ObtenerOpcionValida("Ingrese la ciudad destino (interior/exterior): ", new string[] { "interior", "exterior" }, "Ciudad destino");
        string tipoCliente = ObtenerOpcionValida("Ingrese el tipo de cliente (nuevo/recurrente): ", new string[] { "nuevo", "recurrente" }, "Tipo de cliente");
        int cantItems = ObtenerEnteroValido("Ingrese la cantidad de ítems: ", "Cantidad de ítems");

        // Delegar al módulo de lógica de negocio
        var (categoria, costoBase) = DeterminarCategoriaYCosto(monto, tipoCliente, cantItems);
        decimal costoEnvio = CalcularCostoEnvio(costoBase, ciudad);

        // Mostrar resumen al usuario
        MostrarResumenPedido(monto, cantItems, tipoCliente, ciudad, categoria, costoBase, costoEnvio);

        return new Pedido(monto, ciudad, tipoCliente, cantItems, categoria, costoEnvio);
    }

    /// <summary>
    /// Obtiene un valor decimal válido del usuario con reintentos.
    /// </summary>
    /// <param name="prompt">Mensaje a mostrar al usuario.</param>
    /// <param name="campo">Nombre del campo para mensajes de error.</param>
    /// <returns>Un valor decimal positivo validado.</returns>
    static decimal ObtenerDecimalValido(string prompt, string campo)
    {
        while (true)
        {
            Console.Write(prompt);
            string entrada = Console.ReadLine() ?? "";
            if (decimal.TryParse(entrada, out decimal valor) && valor > 0)
            {
                return valor;
            }
            Console.WriteLine($"Error: {campo} debe ser un valor positivo mayor a cero.");
        }
    }

    /// <summary>
    /// Obtiene una opción válida del usuario de un conjunto permitido.
    /// </summary>
    /// <param name="prompt">Mensaje a mostrar al usuario.</param>
    /// <param name="opcionesValidas">Array de opciones permitidas.</param>
    /// <param name="campo">Nombre del campo para mensajes de error.</param>
    /// <returns>Una de las opciones válidas, en minúscula.</returns>
    static string ObtenerOpcionValida(string prompt, string[] opcionesValidas, string campo)
    {
        while (true)
        {
            Console.Write(prompt);
            string entrada = (Console.ReadLine() ?? "").ToLower().Trim();
            if (opcionesValidas.Contains(entrada))
            {
                return entrada;
            }
            Console.WriteLine($"Error: {campo} debe ser una de las siguientes opciones: {string.Join(", ", opcionesValidas)}");
        }
    }

    /// <summary>
    /// Obtiene un valor entero válido del usuario con reintentos.
    /// </summary>
    /// <param name="prompt">Mensaje a mostrar al usuario.</param>
    /// <param name="campo">Nombre del campo para mensajes de error.</param>
    /// <returns>Un valor entero positivo validado.</returns>
    static int ObtenerEnteroValido(string prompt, string campo)
    {
        while (true)
        {
            Console.Write(prompt);
            string entrada = Console.ReadLine() ?? "";
            if (int.TryParse(entrada, out int valor) && valor >= 1)
            {
                return valor;
            }
            Console.WriteLine($"Error: {campo} debe ser mayor a 0.");
        }
    }

    // ===== CAPA DE LÓGICA DE NEGOCIO =====

    /// <summary>
    /// Determina la categoría de despacho y el costo base según las reglas de negocio.
    /// </summary>
    /// <param name="monto">Valor del pedido en pesos.</param>
    /// <param name="tipoCliente">Tipo de cliente: "nuevo" o "recurrente".</param>
    /// <param name="cantItems">Cantidad de ítems en el pedido.</param>
    /// <returns>Tupla con categoría (GRATIS, EXPRESS, ESTÁNDAR) y costo base.</returns>
    static (string categoria, decimal costoBase) DeterminarCategoriaYCosto(decimal monto, string tipoCliente, int cantItems)
    {
        // Regla 1: Envío gratis si monto >= 150.000 Y cliente recurrente
        if (monto >= MONTO_GRATIS && tipoCliente == "recurrente")
        {
            return ("GRATIS", 0);
        }

        // Regla 2: Envío express si ítems >= 5 O monto >= 300.000
        if (cantItems >= ITEMS_EXPRESS || monto >= MONTO_EXPRESS)
        {
            return ("EXPRESS", COSTO_BASE_EXPRESS);
        }

        // Regla 3: Envío estándar en todos los demás casos
        return ("ESTÁNDAR", COSTO_BASE_ESTANDAR);
    }

    /// <summary>
    /// Calcula el costo final de envío sumando recargo por ciudad si aplica.
    /// </summary>
    /// <param name="costoBase">Costo base determinado por la categoría.</param>
    /// <param name="ciudad">Ciudad destino: "interior" o "exterior".</param>
    /// <returns>Costo total de envío.</returns>
    static decimal CalcularCostoEnvio(decimal costoBase, string ciudad)
    {
        decimal costoEnvio = costoBase;
        if (ciudad == "exterior")
        {
            costoEnvio += COSTO_EXTERIOR;
        }
        return costoEnvio;
    }

    /// <summary>
    /// Formatea un valor decimal como moneda en pesos colombianos.
    /// </summary>
    /// <param name="valor">Valor a formatear.</param>
    /// <returns>Cadena con formato de moneda (ej: $25000.00).</returns>
    static string FormatearMoneda(decimal valor)
    {
        return $"${valor:F2}";
    }

    // ===== CAPA DE ORQUESTACIÓN =====

    /// <summary>
    /// Orquesta el proceso completo de registrar un pedido: captura, validación y persistencia.
    /// </summary>
    /// <param name="pedidos">Colección de pedidos a la que se añadirá el nuevo registro.</param>
    static void RegistrarPedido(List<Pedido> pedidos)
    {
        Pedido nuevoPedido = CapturarPedidoCompleto();
        pedidos.Add(nuevoPedido);
        Console.WriteLine("Pedido registrado y almacenado exitosamente.");
        MostrarMensajeAlCliente(nuevoPedido.Categoria, nuevoPedido.CostoEnvio);
        PresionarEnterParaContinuar();
    }

    // ===== CAPA DE PRESENTACIÓN DE RESULTADOS =====

    /// <summary>
    /// Muestra el resumen detallado de un pedido procesado.
    /// </summary>
    /// <param name="monto">Monto del pedido.</param>
    /// <param name="cantItems">Cantidad de ítems.</param>
    /// <param name="tipoCliente">Tipo de cliente.</param>
    /// <param name="ciudad">Ciudad destino.</param>
    /// <param name="categoria">Categoría asignada.</param>
    /// <param name="costoBase">Costo base de la categoría.</param>
    /// <param name="costoEnvio">Costo total de envío.</param>
    static void MostrarResumenPedido(decimal monto, int cantItems, string tipoCliente, string ciudad, string categoria, decimal costoBase, decimal costoEnvio)
    {
        Console.WriteLine("\n===== RESUMEN DEL PEDIDO =====");
        Console.WriteLine($"Monto del pedido:      {FormatearMoneda(monto)}");
        Console.WriteLine($"Cantidad de ítems:     {cantItems}");
        Console.WriteLine($"Tipo de cliente:       {tipoCliente}");
        Console.WriteLine($"Ciudad destino:        {ciudad}");
        Console.WriteLine("\n----- RESULTADO -----");
        Console.WriteLine($"Categoría de despacho: {categoria}");
        Console.WriteLine($"Costo base:            {FormatearMoneda(costoBase)}");
        if (ciudad == "exterior")
        {
            Console.WriteLine($"Recargo por exterior:  {FormatearMoneda(COSTO_EXTERIOR)}");
        }
        Console.WriteLine($"\nCOSTO TOTAL DE ENVÍO:  {FormatearMoneda(costoEnvio)}");
    }

    /// <summary>
    /// Muestra un mensaje personalizado al cliente según la categoría de despacho.
    /// </summary>
    /// <param name="categoria">Categoría del envío: GRATIS, EXPRESS o ESTÁNDAR.</param>
    /// <param name="costoEnvio">Costo final del envío.</param>
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
        }
    }

    /// <summary>
    /// Calcula y muestra estadísticas detalladas de todos los pedidos registrados.
    /// Reinicia acumuladores en cada invocación; no reutiliza estado previo.
    /// </summary>
    /// <param name="pedidos">Colección de pedidos a analizar.</param>
    static void MostrarEstadisticas(List<Pedido> pedidos)
    {
        Console.WriteLine("\n===== ESTADÍSTICAS DE PEDIDOS =====");
        
        // Validar colección vacía
        if (pedidos.Count == 0)
        {
            Console.WriteLine("No hay pedidos registrados.");
            PresionarEnterParaContinuar();
            return;
        }

        // Calcular métricas generales (acumuladores reiniciados)
        int totalPedidos = pedidos.Count;
        decimal totalCostoEnvio = 0;
        foreach (var p in pedidos)
        {
            totalCostoEnvio += p.CostoEnvio;
        }
        decimal promedioCosto = totalCostoEnvio / totalPedidos;

        Console.WriteLine($"Total de pedidos procesados: {totalPedidos}");
        Console.WriteLine($"Costo total de envíos:       {FormatearMoneda(totalCostoEnvio)}");
        Console.WriteLine($"Costo promedio de envío:     {FormatearMoneda(promedioCosto)}");

        // Estadísticas por categoría
        Console.WriteLine("\nEstadísticas por categoría:");
        var categorias = new[] { "GRATIS", "EXPRESS", "ESTÁNDAR" };
        foreach (var cat in categorias)
        {
            int cantidad = 0;
            decimal totalCat = 0;
            foreach (var p in pedidos)
            {
                if (p.Categoria == cat)
                {
                    cantidad++;
                    totalCat += p.CostoEnvio;
                }
            }
            if (cantidad > 0)
            {
                Console.WriteLine($"- {cat}: {cantidad} pedidos, Total: {FormatearMoneda(totalCat)}");
            }
        }

        // Estadísticas por ciudad
        Console.WriteLine("\nEstadísticas por ciudad destino:");
        var ciudades = new[] { "interior", "exterior" };
        foreach (var ciu in ciudades)
        {
            int cantidad = pedidos.Count(p => p.Ciudad == ciu);
            if (cantidad > 0)
            {
                Console.WriteLine($"- {ciu}: {cantidad} pedidos");
            }
        }

        // Estadísticas por tipo de cliente
        Console.WriteLine("\nEstadísticas por tipo de cliente:");
        var tipos = new[] { "nuevo", "recurrente" };
        foreach (var tipo in tipos)
        {
            int cantidad = pedidos.Count(p => p.TipoCliente == tipo);
            if (cantidad > 0)
            {
                Console.WriteLine($"- {tipo}: {cantidad} pedidos");
            }
        }

        PresionarEnterParaContinuar();
    }
}
