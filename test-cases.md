# Casos de Prueba - Sistema de Clasificación de Pedidos

## Caso 1: Envío GRATIS
**Entrada:**
- Monto: 200000
- Ciudad: interior
- Tipo cliente: recurrente
- Cantidad ítems: 2

**Salida esperada:**
- Categoría: GRATIS
- Costo envío: $0.00

## Caso 2: Envío EXPRESS (por ítems)
**Entrada:**
- Monto: 50000
- Ciudad: interior
- Tipo cliente: nuevo
- Cantidad ítems: 6

**Salida esperada:**
- Categoría: EXPRESS
- Costo envío: $25000.00

## Caso 3: Envío EXPRESS (por monto)
**Entrada:**
- Monto: 350000
- Ciudad: interior
- Tipo cliente: nuevo
- Cantidad ítems: 1

**Salida esperada:**
- Categoría: EXPRESS
- Costo envío: $25000.00

## Caso 4: Envío ESTÁNDAR
**Entrada:**
- Monto: 100000
- Ciudad: interior
- Tipo cliente: nuevo
- Cantidad ítems: 3

**Salida esperada:**
- Categoría: ESTÁNDAR
- Costo envío: $15000.00

## Caso 5: Envío con recargo exterior
**Entrada:**
- Monto: 100000
- Ciudad: exterior
- Tipo cliente: nuevo
- Cantidad ítems: 3

**Salida esperada:**
- Categoría: ESTÁNDAR
- Costo envío: $45000.00 (15000 + 20000 recargo)

## Caso 6: Validación de entrada inválida
**Entrada:**
- Monto: -50000
- Ciudad: interior
- Tipo cliente: recurrente
- Cantidad ítems: 2

**Salida esperada:**
- Error: Monto debe ser un valor positivo mayor a cero.

## Caso 7: Validación de ciudad inválida
**Entrada:**
- Monto: 100000
- Ciudad: bogota
- Tipo cliente: recurrente
- Cantidad ítems: 2

**Salida esperada:**
- Error: Ciudad destino debe ser una de las siguientes opciones: interior, exterior

## Caso 8: Múltiples pedidos y reporte estadístico
**Entrada:**
- Pedido 1: monto 200000, ciudad interior, tipo cliente recurrente, ítems 2
- Pedido 2: monto 120000, ciudad exterior, tipo cliente nuevo, ítems 4
- Pedido 3: monto 350000, ciudad interior, tipo cliente nuevo, ítems 1

**Salida esperada:**
- Pedido 1: GRATIS, costo envío $0.00
- Pedido 2: ESTÁNDAR, costo envío $35000.00
- Pedido 3: EXPRESS, costo envío $25000.00
- Total de pedidos: 3
- Costo total de envíos: $60000.00
- Costo promedio de envío: $20000.00

## Caso 9: Validación de cantidad de ítems inválida
**Entrada:**
- Monto: 50000
- Ciudad: interior
- Tipo cliente: nuevo
- Cantidad ítems: 0

**Salida esperada:**
- Error: Cantidad de ítems debe ser mayor a 0.

## Caso 10: Salir del menú principal
**Entrada:**
- Opción: 3

**Salida esperada:**
- Mensaje de despedida
- Programa finaliza sin procesar nuevo pedido.