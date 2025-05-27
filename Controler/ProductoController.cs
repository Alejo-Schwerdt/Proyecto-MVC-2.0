﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Controler
{
    public class ProductoController
    {
        // Ya no se requiere tener un miembro de instancia de Database,
        // ya que todos sus métodos son estáticos.
        public ProductoController()
        {
            // Constructor vacío o cualquier inicialización necesaria
        }

        public bool GuardarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.nombreproducto))
                throw new ArgumentException("El nombre del producto es obligatorio");

            string query = "INSERT INTO Productos (NombreProducto, DescripcionProducto, TipoProducto, ValorProducto, StockProducto, MarcaProducto) " +
                           "VALUES (@NombreProducto, @DescripcionProducto, @TipoProducto, @ValorProducto, @StockProducto, @MarcaProducto)";
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@NombreProducto", producto.nombreproducto),
        new SqlParameter("@DescripcionProducto", producto.descripcionproducto ?? (object)DBNull.Value),
        new SqlParameter("@TipoProducto", producto.tipoproducto ?? (object)DBNull.Value),
        new SqlParameter("@ValorProducto", producto.valorproducto),
        new SqlParameter("@StockProducto", producto.stockproducto),
        new SqlParameter("@MarcaProducto", producto.marcaproducto ?? (object)DBNull.Value)
            };

            try
            {
                Database.ExecuteNonQuery(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el producto: " + ex.Message);
            }
        }

        public bool ModificarProducto(Producto producto)
        {
            string query = "UPDATE Productos SET NombreProducto = @NombreProducto, DescripcionProducto = @DescripcionProducto, " +
                           "TipoProducto = @TipoProducto, ValorProducto = @ValorProducto, StockProducto = @StockProducto, " +
                           "MarcaProducto = @MarcaProducto WHERE IdProducto = @IdProducto";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@NombreProducto", producto.nombreproducto),
                new SqlParameter("@DescripcionProducto", producto.descripcionproducto),
                new SqlParameter("@TipoProducto", producto.tipoproducto),
                new SqlParameter("@ValorProducto", producto.valorproducto),
                new SqlParameter("@StockProducto", producto.stockproducto),
                new SqlParameter("@MarcaProducto", producto.marcaproducto),
                new SqlParameter("@IdProducto", producto.idproducto)
            };

            try
            {
                Database.ExecuteNonQuery(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto: " + ex.Message);
            }
        }

        public bool EliminarProducto(int idproducto)
        {
            string query = "DELETE FROM Productos WHERE IdProducto = @IdProducto";
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@IdProducto", idproducto)
            };

            try
            {
                int filasAfectadas = Database.ExecuteNonQuery(query, parametros);

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se encontró ningún producto con el ID especificado.");
                }
            }
            catch (SqlException ex)
            {
                // Captura de error por si quiere eliminar un producto que ya esta mostrado en las ventas 
                if (ex.Number == 547)
                {
                    // Mostrar un mensaje más claro para el usuario
                    throw new Exception("No se puede eliminar el producto porque está asociado a una venta o detalle de venta.");
                }
                else
                {
                    // Otros errores de SQL
                    throw new Exception("Error SQL al eliminar el producto: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                // Otros errores generales
                throw new Exception("Error general al eliminar el producto: " + ex.Message);
            }
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            string query = "SELECT * FROM Productos";
            // Llamamos al método estático Select
            DataTable tabla = Database.Select(query);

            List<Producto> productos = new List<Producto>();

            foreach (DataRow row in tabla.Rows)
            {
                productos.Add(new Producto
                {
                    idproducto = Convert.ToInt32(row["IdProducto"]),
                    nombreproducto = row["NombreProducto"].ToString(),
                    descripcionproducto = row["DescripcionProducto"].ToString(),
                    tipoproducto = row["TipoProducto"].ToString(),
                    valorproducto = Convert.ToInt32(row["ValorProducto"]),
                    stockproducto = Convert.ToInt32(row["StockProducto"]),
                    marcaproducto = row["MarcaProducto"].ToString()
                });
            }

            return productos;
        }
    }
}