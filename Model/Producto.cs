using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Producto
    {
        public int idproducto { get; set; }
        public string nombreproducto { get; set; }
        public string descripcionproducto { get; set; }
        public string tipoproducto { get; set; }
        public int valorproducto { get; set; }
        public int stockproducto { get; set; }
        public string marcaproducto { get; set; }


        // Constructor por defecto
        public Producto() { }

        // Constructor con parámetros para inicializar las propiedades
        public Producto(int IdProducto, string NombreProducto, string DescripcionProducto, string TipoProducto, int ValorProducto, int StockProducto, string MarcaProducto)
        {
            this.idproducto = IdProducto;
            this.nombreproducto = NombreProducto;
            this.descripcionproducto = DescripcionProducto;
            this.tipoproducto = TipoProducto;
            this.valorproducto = ValorProducto;
            this.stockproducto = StockProducto;
            this.marcaproducto = MarcaProducto;
        }

        // Método ToString para mostrar la información del producto en formato de texto
        public override string ToString()
        {
            return $"ID: {idproducto}, Nombre del producto: {nombreproducto} ,Descripcion del producto:{descripcionproducto} ,Tipo de producto: {tipoproducto},Valor del producto: {valorproducto},Stock: {stockproducto},Marca del producto:{marcaproducto}" ;
        }
    }
}
