# Divide And Conquer

Es una técnica para resolver algoritmos en el que un problema puede ser dividido en partes más pequeñas que sí tienen solución, para luego usarlas para resolver el problema mayor. Tiene la ventaja que su uso es eficiente.

- **Divide** el problema en subproblemas, porciones más pequeñas del problema original.
- **Conquer** los subproblemas resolviéndolos de manera recursiva. En caso contrario, resuélvelos de manera directa.
- **Combine**. Combinar las soluciones de los subproblemas en la solución del problema original.

## Merge Sort usando Divide And Conquer

- *Divide*: Dividir la secuencia de `n` elementos para ser ordenadas en dos subsecuencias de `n/2` elementos, respectivamente.
- *Conquer*: Ordenar las dos subsecuencias recursivamente, usando merge sort.
- *Combine*: Une las dos subsecuencias ya ordenadas para formar la secuencia final con todos los elementos ordenados.

El proceso de recursión hace "bottoms out" o toca fondo cuando la secuencia para ser ordenada tiene 1 de longitud, ya que una secuencia de un elemento ya se encuentra ordenada.


### Implementación

```python

```

## Términos

- **Recursive Case**. Es aquel caso dentro del algoritmo que es resuelto de manera recursiva.
- **Base case**. Es el caso o casos con resolución trivial o no recursiva. Se llega a ellos luego de haber pasado por los recursive case. A este proceso de pasar por los recursive case hasta el base case se le dice *bottoms out*, "toque fondo", en español.
