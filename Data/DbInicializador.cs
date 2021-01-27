using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProyectoGrupo3NT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Data
{
    public class DbInicializador
    {


        private const string passwordDefault = "Password1!";

        private const string clienteNombre = "Martín";
        private const string clienteApellido = "Perez";
        private const string clienteMail = "cliente@cliente";
        private const int clienteDni = 36345709;
        private const int clienteTelefono = 47448890;
        private const string clienteDireccion = "H. Wineberg 2320";

        private const string adminMail = "admin@ort.edu.ar";
        private const string adminNombre = "Marcelo";
        private const string adminApellido = "Morales";
        private const int adminTelefono = 47095687;
        private const int adminDni = 20657211;
        private const string adminDireccion = "Piedrabuena 6362";

        private const string empleadoMail = "empleado@empleado";
        private const string empNombre = "Jackie";
        private const string empApellido = "McCabe";
        private const int empTelefono = 48056985;
        private const int empDni = 22657211;
        private const string empDireccion = "Jean Jaures 632";

        private const string rolCliente = "Usuario";
        private const string rolEmpleado = "Empleado";
        private const string rolAdministrador = "Administrador";

        private const string nombreSuc = "Herencia Unicenter";
        private const string direccionSuc = "Paraná 3745, Martínez";
        private const string emailSuc = "herenciaunicenter@herencia.com.ar";
        private const string telefonoSuc = "4794-3589";

        private const string nombreSuc2 = "Herencia II";
        private const string telefonoSuc2 = "4711-0967";
        private const string direccionSuc2 = "Charcas 1214, CABA";
        private const string emailSuc2 = "herenciaII@herencia.com.ar";

        private const string Categoria1 = "Buzos";
        private const string Categoria2 = "Remeras";
        private const string Categoria3 = "Pantalones";

        private const string Producto1 = "Buzo gris canguro";
        private const int cantProducto1 = 3;
        private const string Producto1Foto = "griscanguro.png";
        private const string Producto1Desc = "Buzo con capucha, 80% algodón, 20% poliéster, liso sin estampa. Calce slim.";

        private const string Producto2 = "Buzo amarillo sin capucha";
        private const int cantProducto2 = 1;
        private const string Producto2Foto = "sincapucha.png";
        private const string Producto2Desc = "Buzo cuello redondo 100% algodón, liso sin estampa. Calce regular.";

        private const string Producto3 = "Remera naranja melange";
        private const int cantProducto3 = 2;
        private const string Producto3Foto = "naranja.png";
        private const string Producto3Desc = "Remera de jersey melange manga corta, 100% algodón pima. Calce slim.";

        private const string Producto4 = "Remera de los Rollin";
        private const int cantProducto4 = 1;
        private const string Producto4Foto = "rollin.png";
        private const string Producto4Desc = "Remera de manga corta, 100% algodón pima, con estampa en delantero. Calce slim."; 

        private const string Producto5 = "Jean oxford azul";
        private const int cantProducto5 = 2;
        private const string Producto5Foto = "oxford.png";
        private const string Producto5Desc = "Jean tiro medio tipo oxford desgastado. Calce ajustado.";

        private const string Producto6 = "Chupin slimfit roto";
        private const int cantProducto6 = 3;
        private const string Producto6Foto = "roto.png";
        private const string Producto6Desc = "Chupin tiro medio, super skinny. Lavado negro gastado con roturas en rodillas.";

        public static void Inicializar(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var miContexto = serviceScope.ServiceProvider.GetService<MiContexto>();

                miContexto.Database.EnsureCreated();

                var _usermgr = serviceScope.ServiceProvider.GetService<UserManager<Usuario>>();
                var _rolmgr = serviceScope.ServiceProvider.GetService<RoleManager<Rol>>();

                Producto producto1 = new Producto()
                {
                    Nombre = Producto1,
                    Descripcion = Producto1Desc,
                    Activo = true,
                    PrecioVigente = 1600,
                    Foto = Producto1Foto
                };

                Producto producto2 = new Producto()
                {
                    Nombre = Producto2,
                    Descripcion = Producto2Desc,
                    Activo = true,
                    PrecioVigente = 2600,
                    Foto = Producto2Foto
                };

                Producto producto3 = new Producto()
                {
                    Nombre = Producto3,
                    Descripcion = Producto3Desc,
                    Activo = true,
                    PrecioVigente = 600,
                    Foto = Producto3Foto

                };

                Producto producto4 = new Producto()
                {
                    Nombre = Producto4,
                    Descripcion = Producto4Desc,
                    Activo = true,
                    PrecioVigente = 6600,
                    Foto = Producto4Foto
                };

                Producto producto5 = new Producto()
                {
                    Nombre = Producto5,
                    Descripcion = Producto5Desc,
                    Activo = true,
                    PrecioVigente = 600,
                    Foto = Producto5Foto
                };

                Producto producto6 = new Producto()
                {
                    Nombre = Producto6,
                    Descripcion = Producto6Desc,
                    Activo = true,
                    PrecioVigente = 600,
                    Foto = Producto6Foto
                };


                if (!_rolmgr.RoleExistsAsync(rolAdministrador).Result)
                {
                    var rol = _rolmgr.CreateAsync(new Rol { Name = rolAdministrador }).Result;

                }

                if (!_rolmgr.RoleExistsAsync(rolCliente).Result)
                {
                    var rol = _rolmgr.CreateAsync(new Rol { Name = rolCliente }).Result;
                }

                if (!_rolmgr.RoleExistsAsync(rolEmpleado).Result)
                {
                    var rol = _rolmgr.CreateAsync(new Rol { Name = rolEmpleado }).Result;
                }

                if (!miContexto.Usuarios.Any(c => c.UserName == clienteMail))
                {
                    Cliente cliente = new Cliente()
                    {
                        Nombre = clienteNombre,
                        Email = clienteMail,
                        UserName = clienteMail,
                        NormalizedUserName = clienteMail.ToUpper(),
                        Dni = clienteDni,
                        Apellido = clienteApellido,
                        Telefono = clienteTelefono,
                        Direccion = clienteDireccion
                    };

                    var resultado = _usermgr.CreateAsync(cliente, passwordDefault).Result;
                    if (resultado.Succeeded)
                    {
                        var clienteRol = _usermgr.AddToRoleAsync(cliente, rolCliente);
                        Carrito carrito = new Carrito(cliente.Id);
                        miContexto.Carritos.Add(carrito);
                        miContexto.SaveChangesAsync();
                    }

                }

                if (!miContexto.Usuarios.Any(u => u.UserName == adminMail))
                {
                    var user = new Usuario()
                    {
                        UserName = adminMail,
                        NormalizedUserName = adminMail.ToUpper(),
                        Email = adminMail,
                        NormalizedEmail = adminMail.ToUpper(),
                        Apellido = adminApellido,
                        Nombre = adminNombre,
                        Telefono = adminTelefono,
                        Dni = adminDni,
                        Direccion = adminDireccion
                    };

                    var resultado = _usermgr.CreateAsync(user, passwordDefault).Result;
                    if (resultado.Succeeded)
                    {
                        var adminRol = _usermgr.AddToRoleAsync(user, rolAdministrador);
                    }


                }

                if (!miContexto.Usuarios.Any(u => u.UserName == empleadoMail))
                {
                    var user = new Usuario()
                    {
                        UserName = empleadoMail,
                        NormalizedUserName = empleadoMail.ToUpper(),
                        Email = empleadoMail,
                        NormalizedEmail = empleadoMail.ToUpper(),
                        Apellido = empApellido,
                        Nombre = empNombre,
                        Telefono = empTelefono,
                        Dni = empDni,
                        Direccion = empDireccion
                    };

                    var resultado = _usermgr.CreateAsync(user, passwordDefault).Result;
                    if (resultado.Succeeded)
                    {
                        var adminRol = _usermgr.AddToRoleAsync(user, rolEmpleado);
                    }
                }

                if (!miContexto.Sucursales.Any(s => s.Nombre == nombreSuc))
                {
                    Sucursal sucursal = new Sucursal()
                    {
                        Nombre = nombreSuc,
                        Direccion = direccionSuc,
                        Telefono = telefonoSuc,
                        Email = emailSuc
                    };

                    miContexto.Sucursales.Add(sucursal);
                    miContexto.SaveChangesAsync();

                    StockItem si1 = new StockItem()
                    {
                        Sucursal = sucursal,
                        Producto = producto1,
                        Cantidad = cantProducto1
                    };
                    StockItem si2 = new StockItem()
                    {
                        Producto = producto2,
                        Cantidad = cantProducto2
                    };
                    StockItem si3 = new StockItem()
                    {
                        Producto = producto3,
                        Cantidad = cantProducto3
                    };
                    StockItem si4 = new StockItem()
                    {
                        Producto = producto4,
                        Cantidad = cantProducto4
                    };
                    StockItem si5 = new StockItem()
                    {
                        Producto = producto5,
                        Cantidad = cantProducto5
                    };
                    StockItem si6 = new StockItem()
                    {
                        Producto = producto6,
                        Cantidad = cantProducto6
                    };

                    sucursal.StockItems.Add(si1);
                    sucursal.StockItems.Add(si2);
                    sucursal.StockItems.Add(si3);
                    sucursal.StockItems.Add(si4);
                    sucursal.StockItems.Add(si5);
                    sucursal.StockItems.Add(si6);
                    miContexto.SaveChangesAsync();
                }

                if (!miContexto.Sucursales.Any(s => s.Nombre == nombreSuc2))
                {
                    Sucursal suc2 = new Sucursal()
                    {
                        Nombre = nombreSuc2,
                        Direccion = direccionSuc2,
                        Telefono = telefonoSuc2,
                        Email = emailSuc2
                    };
                    miContexto.Sucursales.Add(suc2);
                    miContexto.SaveChangesAsync();

                    StockItem si7 = new StockItem()
                    {
                        Producto = producto1,
                        Cantidad = 5
                    };

                    suc2.StockItems.Add(si7);
                    miContexto.SaveChangesAsync();
                }


                if (!miContexto.Categorias.Any(c => c.Nombre == Categoria1))
                {
                    var cat = new Categoria();
                    {
                        cat.Nombre = Categoria1;
                    }

                    miContexto.Categorias.Add(cat);
                    miContexto.SaveChangesAsync();

                    cat.Productos.Add(producto1);
                    cat.Productos.Add(producto2);
                    miContexto.SaveChangesAsync();

                }

                if (!miContexto.Categorias.Any(c => c.Nombre == Categoria2))
                {
                    var cat = new Categoria();
                    {
                        cat.Nombre = Categoria2;
                    }
                    miContexto.Categorias.Add(cat);
                    miContexto.SaveChangesAsync();

                    cat.Productos.Add(producto3);
                    cat.Productos.Add(producto4);
                    miContexto.SaveChangesAsync();

                }

                if (!miContexto.Categorias.Any(c => c.Nombre == Categoria3))
                {
                    var cat = new Categoria();
                    {
                        cat.Nombre = Categoria3;
                    }
                    miContexto.Categorias.Add(cat);
                    miContexto.SaveChangesAsync();

                    cat.Productos.Add(producto5);

                    cat.Productos.Add(producto6);

                    miContexto.SaveChangesAsync();

                }
                miContexto.SaveChanges();
            }
        }
    }
}

