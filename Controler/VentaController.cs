using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Model;

namespace Controler
{
    public class VentaController
    {
        public bool RegistrarVenta(Venta venta)
        {
            try
            {
                // Calcular el total de la venta a partir de los detalles
                decimal total = 0;
                foreach (var detalle in venta.Detalles)
                {
                    total += detalle.Cantidad * detalle.PrecioUnitario;
                }
                venta.TotalVenta = total;

                // INSERT EN Ventas 
                string insertVentaQuery = "INSERT INTO Ventas (FechaVenta, TotalVenta) VALUES (@fecha, @total); SELECT SCOPE_IDENTITY();";
                SqlParameter[] parametrosVenta = new SqlParameter[]
                {
            new SqlParameter("@fecha", venta.FechaVenta),
            new SqlParameter("@total", venta.TotalVenta)
                };

                object result = Database.ExecuteScalar(insertVentaQuery, parametrosVenta);
                int idVenta = Convert.ToInt32(result);
                venta.IDVenta = idVenta;

                // Para cada detalle, validar stock, insertar el detalle y actualizar el stock del producto
                foreach (var detalle in venta.Detalles)
                {
                    // Validar stock
                    string consultaStock = "SELECT stockproducto FROM productos WHERE idproducto = @idproducto";
                    SqlParameter[] parametrosConsultaStock = new SqlParameter[]
                    {
                new SqlParameter("@idproducto", detalle.IdProducto)
                    };
                    DataTable stockResult = Database.Select(consultaStock, parametrosConsultaStock);
                    if (stockResult.Rows.Count == 0)
                    {
                        Console.WriteLine($"El producto con ID {detalle.IdProducto} no existe.");
                        return false;
                    }

                    int stockActual = Convert.ToInt32(stockResult.Rows[0]["stockproducto"]);
                    if (detalle.Cantidad > stockActual)
                    {
                        Console.WriteLine($"Stock insuficiente para el producto con ID {detalle.IdProducto}.");
                        return false;
                    }

                    // Insertar en DetalleVenta (nombre correcto de la tabla)
                    string insertDetalleQuery = @"INSERT INTO DetalleVenta (IDVenta, IDProducto, Cantidad, PrecioUnitario)
                                          VALUES (@idventa, @idproducto, @cantidad, @precio)";
                    SqlParameter[] parametrosDetalle = new SqlParameter[]
                    {
                new SqlParameter("@idventa", idVenta),
                new SqlParameter("@idproducto", detalle.IdProducto),
                new SqlParameter("@cantidad", detalle.Cantidad),
                new SqlParameter("@precio", detalle.PrecioUnitario)
                    };
                    Database.ExecuteNonQuery(insertDetalleQuery, parametrosDetalle);

                    // Actualizar el stock del producto
                    string actualizarStockQuery = "UPDATE productos SET stockproducto = stockproducto - @cantidad WHERE idproducto = @idproducto";
                    SqlParameter[] parametrosActualizarStock = new SqlParameter[]
                    {
                new SqlParameter("@cantidad", detalle.Cantidad),
                new SqlParameter("@idproducto", detalle.IdProducto)
                    };
                    Database.ExecuteNonQuery(actualizarStockQuery, parametrosActualizarStock);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar venta: " + ex.Message);
                return false;
            }
        }

        public List<Venta> ObtenerTodasLasVentasConDetalles()
        {
            string query = @"
        SELECT v.IDVenta, v.FechaVenta, v.TotalVenta,
               d.IDDetalleVenta, d.IDProducto, d.Cantidad, d.PrecioUnitario,
               p.NombreProducto, p.DescripcionProducto, p.TipoProducto, p.MarcaProducto, p.ValorProducto, p.StockProducto
        FROM Ventas v
        INNER JOIN DetalleVenta d ON v.IDVenta = d.IDVenta
        INNER JOIN Productos p ON d.IDProducto = p.idproducto
    ";

            DataTable tabla = Database.Select(query);

            var ventas = tabla.Rows.Cast<DataRow>()
                .GroupBy(row => new
                {
                    IDVenta = (int)row["IDVenta"],
                    FechaVenta = (DateTime)row["FechaVenta"],
                    TotalVenta = (decimal)row["TotalVenta"]
                })
                .Select(g => new Venta
                {
                    IDVenta = g.Key.IDVenta,
                    FechaVenta = g.Key.FechaVenta,
                    TotalVenta = g.Key.TotalVenta,
                    Detalles = g.Select(row => new DetalleVenta
                    {
                        IDDetalleVenta = (int)row["IDDetalleVenta"],
                        IDVenta = (int)row["IDVenta"],
                        IdProducto = (int)row["IDProducto"],
                        Cantidad = (int)row["Cantidad"],
                        PrecioUnitario = (decimal)row["PrecioUnitario"],
                        Producto = new Producto
                        {
                            idproducto = (int)row["IDProducto"],
                            nombreproducto = row["NombreProducto"].ToString(),
                            descripcionproducto = row["DescripcionProducto"].ToString(),
                            tipoproducto = row["TipoProducto"].ToString(),
                            marcaproducto = row["MarcaProducto"].ToString(),
                            valorproducto = Convert.ToInt32(row["ValorProducto"]),
                            stockproducto = Convert.ToInt32(row["StockProducto"])
                        }
                    }).ToList()
                }).ToList();

            return ventas;
        }
    }
}