using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;
using Controler;

namespace Prueba_MCV
{
    public partial class DetalleVentasProductos : Form
    {
        private VentaController ventaController = new VentaController();

        public DetalleVentasProductos()
        {
            InitializeComponent();
            this.Load += DetalleVentasProductos_Load;

            // Abre el formulario maximizado
            this.WindowState = FormWindowState.Maximized;
        }

        private void DetalleVentasProductos_Load(object sender, EventArgs e)
        {
            try
            {
                var ventas = ventaController.ObtenerTodasLasVentasConDetalles();

                var listaParaMostrar = ventas
                    .SelectMany(v => v.Detalles.Select(d => new
                    {
                        IDVenta = v.IDVenta,
                        FechaVenta = v.FechaVenta,
                        TotalVenta = v.TotalVenta,
                        IDDetalleVenta = d.IDDetalleVenta,
                        IDProducto = d.IdProducto,
                        NombreProducto = d.Producto.nombreproducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        TotalProducto = d.Cantidad * d.PrecioUnitario
                    }))
                    .ToList();

                dataGridViewDetalles.DataSource = listaParaMostrar;

                // Configurar encabezados (opcional)
                dataGridViewDetalles.Columns["IDVenta"].HeaderText = "ID Venta";
                dataGridViewDetalles.Columns["FechaVenta"].HeaderText = "Fecha";
                dataGridViewDetalles.Columns["TotalVenta"].HeaderText = "Total Venta";
                dataGridViewDetalles.Columns["IDDetalleVenta"].HeaderText = "ID Detalle";
                dataGridViewDetalles.Columns["IDProducto"].HeaderText = "ID Producto";
                dataGridViewDetalles.Columns["NombreProducto"].HeaderText = "Producto";
                dataGridViewDetalles.Columns["Cantidad"].HeaderText = "Cantidad";
                dataGridViewDetalles.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                dataGridViewDetalles.Columns["TotalProducto"].HeaderText = "Total por Producto";

                // Ajustar tamaño de columnas y filas
                dataGridViewDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewDetalles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewDetalles.Dock = DockStyle.Fill;  // Ocupa todo el espacio del formulario
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra este formulario y vuelve al principal
        }
    }
}
