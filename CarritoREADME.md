# Carrito de Compras 📖

## Objetivos 📋
Desarrollar un sistema, que permita la administración del stock de productos a una PYME que tiene algunas sucursales de venta de ropa 
(de cara a los empleados): Empleados, Clientes, Productos, Categorias, Compras, Carritos, Sucursal, StockItem, etc., como así también, 
permitir a los clientes, realizar compras Online.
Utilizar Visual Studio 2019 preferentemente y crear una aplicación utilizando ASP.NET MVC Core (versión a definir por el docente 2.2 o 3.1).

<hr />

## Enunciado 📢
La idea principal de este trabajo práctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, 
el cual relevó e identificó la información aquí contenida. 
A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación, 

Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará 
a conseguir la información ya un poco más procesada. 
Es importante destacar, que este proceso, no debe esperar a ser en clase; es importante, que junten algunas consultas, sea de índole funcional 
o técnicas, en lugar de cada consulta enviarla de forma independiente.

Las consultas que sean realizadas por correo deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>

2.< xxxxxxxx>


# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relación del paciente con Turno es 1:1 o 1:N?

2.Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?

<hr />

### Proceso de ejecución en alto nivel ☑️
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. 
 [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Crear las relaciones entre las entidades
 - Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext. 
 - Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). 
 [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), 
 [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades en el DbContext.
 - Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Realizar un sistema de login con al menos los roles equivalentes a <Usuario Cliente> y <Usuario Administrador> (o con permisos elevados).
 - Si el proyecto lo requiere, generar el proceso de registración. 
 - Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio 
 (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente> sólo podrá tomar acción en el sistema, en base al rol que tiene.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos del lado de los permisos.
 - Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
 
<hr />

## Entidades 📄

- Usuario
- Cliente
- Empleado
- Producto
- Categoria
- Stock
- StockItem
- Carrito
- CarritoItem
- Compra

`Importante: Todas las entidades deben tener su identificador unico. Id o <ClassNameId>`

`
Las propiedades descriptas a continuación, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones.
`

**Usuario**
```
- Nombre
- Email
- FechaAlta
- Password
```

**Cliente**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Compras
- Carritos
```

**Empleado**
```
- Nombre
- Apellido
- Telefono
- Direccion
- FechaAlta
- Email
```

**Producto**
```
- Nombre
- Descripcion
- PrecioVigente
- Activo
- Categoria
```

**Categoria**
```
- Nombre
- Descripcion
- Productos
```

**Sucursal**
```
- Nombre
- Direccion
- Telefono
- Email
- StockItems
```

**StockItem**
```
- Sucursal
- Producto
- Cantidad
```

**Carrito**
```
- Activo
- Cliente
- CarritoItems
- Subtotal
```

**CarritoItem**
```
- Carrito 
- Producto
- ValorUnitario
- Cantidad
- Subtotal
```

**Compra**
```
- Cliente 
- Carrito
- Total
```


**NOTA:** aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades ⌨️
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

**Usuario**
- Los Clientes pueden auto registrarse.
- La autoregistración desde el sitio, es exclusiva para los clientes. Por lo cual, se le asignará dicho rol.
- Los empleados, deben ser agregados por otro empleado o administrador.
	- Al momento, del alta del empleado, se le definirá un username y password.
    - También se le asignará a estas cuentas el rol de Empleado.

**Cliente**
- Un cliente puede navegar los productos y sus descripciones sin iniciar sesión, de forma anonima. 
- Para agregar productos en cantidad al carrito, debe iniciar sesión primero.
- El cliente, puede agregar diferentes productos en el carrito, y por cada producto modificar la cantidad que quiere.
-- Esta acción, no implica validación en stock.
-- El ciente, verá el subtotal, por cada producto/cantidad.
-- También, verá el subtotal, del carrito.
- El cliente, una vez que está satisfecho con su carrito, puede finalizar la compra y elejirá un lugar para retirar.
- El cliente puede vaciar el carrito.
- Puede actualizar datos de contacto, como el telefono, dirección, Obra Social. Pero no puede modificar su DNI, Nombre, Apellido, etc.

**Empleado**
- El empleado, puede listar las compras realizadas en el mes, en modo listado, ordenado de forma descendente por valor de compra.
- Puede dar de alta otros empleados.
- Puede crear productos, categorias, Sucursales, agregar productos al stock de cada sucursal.
- Puede habilitar y/o deshabilitar productos.

**Producto y Categoria**
- No pueden eliminarse del sistema. 
- Solo los producto pueden dehabilitarse.

**Sucursal**
- Cada sucursal, tendrá su propio stock.
- Y sus datos de locación y contacto.
- Por el mercado tan volatil, las sucursales, pueden crearse y eliminarse en todo momento.
-- Para poder eliminar una sucursal, la misma no tiene que tener productos en su stock.

**StockItem**
- Pueden crearse, pero nunca pueden eliminarse desde el sistema. Son dependeintes de la surcursal.
- Puede modificarse la cantidad en todo momento que se dispone de dicho producto, en el stock.
- Se eliminaran, junto con la sucrusal, si esta fuese eliminada.

**Carrito**
- El carrito se crea automaticamente con la creación de un cliente, en estado activo.
- Solo puede haber un carrito activo por usuario en el sistema.
- Un carrito que no está activo, no puede modificarse en ningún aspecto.
- No se puede eliminar carritos.
- El carrito, se desactiva al momento de realizarse una compra de manera automatica.
- Al vaciar el carrito, se eliminan todos los CarritoItems y datos que sean necesarios.
- El subtotal, es un dato calculado.

**CarritoItem**
- El valor unitario del carritoItem, debe actualizarse, al realizar cualquier modificación, según el precio que tenga vigente el producto.
- El subtotal, debe ser una propiedad calculada, en base a la cantidad x el valor unitario.


**Compra**
- Al generarse la compra, el carrito que tiene asociado, pasa a estar en estado Inactivo.
- Al finalizar la compra, se validará si hay disponibles en el stock de la locación que seleccionó el cliente. 
-- Si hay stock, disminuye el mismo, y crea la compra.
-- Si no hay stock, verifica en otras locaciones, si hay stock. 
--- Si hay en alguna, propone las locaciones o indica que no hay en stock.
--- Si seleccionó una nueva locación, finaliza la compra.
- Al Finalizar la compra, se le muestra le da las gracias al cliente, se le dá el Id de compra y los datos de la Sucursal que eligió.
- No se pueden eliminar las compras.


**Aplicación General**
- Información institucional.
- Se deben mostrar los productos por categoría.
- Los productos que están deshabilitados, deben visualizarse como Pausados. Independientemente, de que haya o no en stock.
- Los accesos a las funcionalidades y/o capacidades, debe estar basada en los roles que tenga cada individuo.

`
Nota: El centro tiene consultorios ilimitados y cada consultorio está preparado para soportar cualquier prestación, 
por lo cual esto no implica restricciones.
`
