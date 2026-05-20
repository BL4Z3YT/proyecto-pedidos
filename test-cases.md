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

## Caso 10: Reporte estadístico con coleción vacía
**Entrada:**
- Opción de menú: 2
- No se ha registrado ningún pedido

**Salida esperada:**
- Mensaje: No hay pedidos registrados.
- El programa debe continuar activo después del reporte.

## Caso 11: Salir del menú principal
**Entrada:**
- Opción de menú: 0

**Salida esperada:**
- Mensaje de despedida: "Saliendo del sistema. ¡Hasta pronto!"
- El programa finaliza sin registrar nuevos pedidos.

## Caso 12: Validación de reintentos - monto negativo
**Entrada:**
- Intento 1: Monto: -50000 (inválido)
- Intento 2: Monto: 100000 (válido)
- Continuar con entrada válida

**Salida esperada:**
- Error en intento 1
- Reintento automático sin salir de la función
- Aceptar entrada válida en intento 2

## Caso 13: Validación de reintentos - opción inválida
**Entrada:**
- Intento 1: Ciudad: "bogota" (inválido)
- Intento 2: Ciudad: "interior" (válido)

**Salida esperada:**
- Error mostrando opciones válidas
- Reintento automático
- Aceptar "interior"

## Caso 14: Múltiples pedidos con reporte completo
**Entrada:**
- Pedido 1: 200000, interior, recurrente, 2 ítems
- Pedido 2: 120000, exterior, nuevo, 4 ítems
- Pedido 3: 350000, interior, nuevo, 1 ítem
- Opción 2 para ver reporte

**Salida esperada:**
- Pedido 1: GRATIS, $0.00
- Pedido 2: ESTÁNDAR, $35000.00
- Pedido 3: EXPRESS, $25000.00
- Reporte: Total 3 pedidos, costo total $60000.00, promedio $20000.00
- Desglose por categoría, ciudad y tipo cliente

## Caso 10: Salir del menú principal
**Entrada:**
- Opción: 3

**Salida esperada:**
- Mensaje de despedida
- Programa finaliza sin procesar nuevo pedido.