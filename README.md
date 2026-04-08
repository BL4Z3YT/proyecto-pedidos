# Sistema de Clasificación de Pedidos

**Equipo:** Juan David Agudelo y Yeisson Gaviria  
**Modalidad:** A  
**Fecha de entrega:** 08/04/2026  
**Entrega:** Entrega 2 - Menú continuo, registros múltiples y reportes estadísticos

---

## Descripción del Problema

Una tienda en línea necesita un programa que, dados los datos de un pedido, determine su **categoría de despacho** y el **costo de envío**. El sistema debe evaluar criterios como el monto del pedido, la ciudad destino, el tipo de cliente y la cantidad de ítems para asignar correctamente la categoría y calcular el costo final.

---

## IPO (Entrada - Proceso - Salida)

### **ENTRADAS**

| Variable | Tipo | Descripción | Restricción |
|----------|------|--------|-------------|
| `monto` | decimal | Valor del pedido en pesos | ≥ 0 |
| `ciudad` | string | Destino del envío | "interior" o "exterior" |
| `tipoCliente` | string | Tipo de cliente | "nuevo" o "recurrente" |
| `cantItems` | int | Cantidad de ítems en el pedido | ≥ 1 |

### **PROCESO**

1. **Regla 1:** Si monto ≥ $150.000 **Y** cliente es "recurrente" → categoría "GRATIS"
2. **Regla 2:** Si ítems ≥ 5 **O** monto ≥ $300.000 → categoría "EXPRESS"
3. **Regla 3:** En todos los demás casos → categoría "ESTÁNDAR"
4. **Regla 4:** Si ciudad es "exterior" → agregar $20.000 al costo base
5. **Costos base:** Gratis = $0, Express = $25.000, Estándar = $15.000

### **SALIDAS**

| Variable | Tipo | Descripción |
|----------|------|-------------|
| `categoria` | string | Tipo de despacho (GRATIS/EXPRESS/ESTÁNDAR) |
| `costoEnvio` | decimal | Costo final del envío |
| Mensaje | string | Resumen para el cliente |

---

## Tabla de Variables

| Nombre | Tipo C# | Propósito |
|--------|---------|----------|
| `monto` | decimal | Almacena el valor del pedido |
| `ciudad` | string | Almacena la ciudad destino |
| `tipoCliente` | string | Almacena el tipo de cliente (nuevo/recurrente) |
| `cantItems` | int | Almacena la cantidad de ítems |
| `categoria` | string | Almacena la categoría asignada |
| `costoEnvio` | decimal | Almacena el costo calculado del envío |
| `costoBase` | decimal | Variable para el costo base según categoría |

---

## Tipos de Datos Utilizados

✅ **decimal** - Para valores monetarios (monto, costoEnvio, costoBase)  
✅ **string** - Para datos textuales (ciudad, tipoCliente, categoria)  
✅ **int** - Para cantidad de ítems (cantItems)

---

## Condicionales y Operadores Lógicos

1. Condicional 1: `if (monto >= 150000 && tipoCliente == "recurrente")`
2. Condicional 2: `else if (cantItems >= 5 || monto >= 300000)`
3. Condicional 3: `else`
4. Condicional 4: `if (ciudad == "exterior")`

---

## Casos de Prueba

Se incluyen casos de prueba detallados en el archivo [`test-cases.md`](test-cases.md) que cubren:

- Todos los escenarios de clasificación (GRATIS, EXPRESS, ESTÁNDAR)
- Validaciones de entrada
- Casos borde y errores
- Recargos por ciudad exterior

### **Ejemplos principales:**

### **Caso Normal (Envío Estándar)**
- **Entrada:** monto = $100.000, ciudad = "interior", tipoCliente = "nuevo", cantItems = 2
- **Salida esperada:** Categoría = "ESTÁNDAR", Costo = $15.000

### **Caso Borde (Envío Gratis + Exterior)**
- **Entrada:** monto = $180.000, ciudad = "exterior", tipoCliente = "recurrente", cantItems = 3
- **Salida esperada:** Categoría = "GRATIS", Costo = $20.000

### **Caso Express**
- **Entrada:** monto = $50.000, ciudad = "interior", tipoCliente = "nuevo", cantItems = 6
- **Salida esperada:** Categoría = "EXPRESS", Costo = $25.000

---

## Instrucciones de Compilación y Ejecución

### **Requisitos previos**
- .NET SDK 6.0 o superior instalado
- Git configurado en tu máquina

### **Compilar**
```bash
cd c:\Users\JUAND\proyecto-pedidos
dotnet new console -n ProyectoPedidos
cd ProyectoPedidos
# Reemplazar el contenido de Program.cs con el código incluido
```

### **Ejecutar**
```bash
dotnet run
```

---

## Estado del Proyecto

✓ **Proyecto completado** - Sistema de clasificación de pedidos funcionando  
✓ **Equipo colaborando** - Ambos integrantes con commits verificados  
✓ **20%** de mejoras y refactorización implementadas por Yeisson Gaviria  

---

## Commits Realizados

1. **init:** README con descripción del problema y diseño IPO (Juan David Agudelo)
2. **feat:** Declaración de variables y lectura de entradas (Juan David Agudelo)
3. **fix:** Ajustes según casos de prueba (Juan David Agudelo)
4. **refactor:** Mejorar código con constantes y método separado para mensajes (Yeisson Gaviria)
5. **docs:** Agregar información de commits y mejoras (Yeisson Gaviria)
6. **docs:** Agregar comentario de documentación al inicio del archivo (Yeisson Gaviria)
7. **docs:** Actualizar README con fecha de entrega 2 y descripción de Entrega 2 (BL4Z3YT)
8. **refactor:** Añadir método para formatear moneda en salidas de consola (BL4Z3YT)
9. **test:** Agregar casos de prueba para múltiples pedidos y estadísticas (Yeisson Gaviria)

---

## Mejoras Implementadas

### Por Yeisson Gaviria
- Refactorización del código usando constantes para valores mágicos
- Creación de método separado `MostrarMensajeAlCliente()` para mejor modularidad
- Mejora en validaciones (verificación de monto positivo)
- Uso de switch statement para lógica de mensajes más limpia

### Validación de Entrada
- Uso de `TryParse` para conversiones seguras
- Validación de rango para todos los inputs
- Mensajes de error descriptivos

---

## Notas

- El programa solicita entrada por consola usando `Console.ReadLine()`
- Se recomienda el uso de `decimal.TryParse()` para conversión robusta
- Cada integrante ha contribuido con commits que representan al menos 5% del avance
- Esta entrega incluye 4 commits a nombre de BL4Z3YT y 4 commits a nombre de Yeisson Gaviria
