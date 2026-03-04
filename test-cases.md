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