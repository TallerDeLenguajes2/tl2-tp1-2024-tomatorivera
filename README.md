# TL2 - Trabajo práctico 1

## Respuestas punto 2 a

**¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?**


1. *Cliente-Pedido* es una relación de composición. Un cliente es parte de un pedido y si se elimina un pedido se elimina también su cliente.
2. *Pedido-Cadete* es una relación de agregación. Un Pedido tiene un Cadete pero pueden existir Cadetes sin pedidos.
3. *Cadete-Cadetería* es una relación de composición. Un cadete es parte de una cadetería.

**¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?**

Algunos métodos que podrían implementar estas clases:

*Cadetería*
- TomarPedido(Pedido): Toma un pedido y se lo asigna a un cadete
- AsignarCadete(Id, Pedido): Dado el Id de un cliente y un pedido, le asigna
- AltaCadete(Cadete): Registra un nuevo cadete

*Cadete*
- ModificarEstado(nro, pedido): Dado el numero de un pedido lo marca modifica su estado
- BuscarPedidos(Estado): Obtiene una lista de pedidos en cierto estado asignados al cadete

**Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados**

Siguiendo estos principios, todos los atributos en este ejemplo particular deberían ser privados y accesibles con sus respectivas propiedades (getters y setters) que deberían ser públicas.

En cuánto a los métodos, en este ejemplo haría todos públicos.

**¿Cómo diseñaría los constructores de cada una de las clases?**

Realizaría constructores completos, excepto en las clases *Cadeteria* y *Cadete* puesto que las listas las iniciaría en el mismo constructor.

**¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?**

Lo único que se me ocurre que se podría haber agregado, es una generalización de *Cadete*  y *Cliente* en *Persona* para no repetir atributos.