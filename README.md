# Integrantes

.58212-Tártalo Aguirre Franco Emanuel-Franco.TartaloAguirre@alu.frt.utn.edu.ar

.57873-Campos Lucas Gonzalo-Lucas.Campos@alu.frt.utn.edu.ar

.58185-Rodriguez Hector Martin-HectorMartin.Rodriguez@alu.frt.utn.edu.ar

## Instrucciones claras y concisas para concisas para configurar y ejecutar el proyecto localmente



### instrucción los endpoints y como usarlos 

OrderController (api/orders)

POST /api/orders
•	Crea una nueva orden.
•	Recibe un objeto OrderModel.CreateOrderRequest en el cuerpo de la solicitud.
•	Devuelve la orden creada y un código 201 si es exitosa.

GET /api/orders/{id}
•	Obtiene una orden por su identificador (GUID).
•	Devuelve la orden si existe, o 404 si no se encuentra.

ProductController (api/products)

POST /api/products
•	Crea un nuevo producto.
•	Recibe un objeto ProductModel.request en el cuerpo de la solicitud.
•	Devuelve el producto creado y un código 201.

GET /api/products
•	Obtiene la lista de todos los productos.
•	Devuelve un array de productos o 204 si no hay productos.

GET /api/products/{id}
•	Obtiene un producto por su identificador (GUID).
•	Devuelve el producto si existe.

PUT /api/products/{id}
•	Actualiza un producto existente.
•	Recibe los datos actualizados en el cuerpo de la solicitud.
•	Devuelve el producto actualizado.

PATCH /api/products/{id}
•	Deshabilita un producto (no elimina, solo lo marca como inactivo).
•	No requiere body, solo el id en la URL.
•	Devuelve 204 si es exitoso.

Notas adicionales

•Todos los endpoints devuelven respuestas estándar HTTP (200, 201, 204, 400, 404).
•El manejo de errores está centralizado mediante un middleware de excepciones.
•Utiliza Swagger (si está en desarrollo) para probar los endpoints visualmente.
