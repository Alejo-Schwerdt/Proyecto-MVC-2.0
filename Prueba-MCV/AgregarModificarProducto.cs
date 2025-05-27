using System;
using System.Windows.Forms;
using Controler;
using Model;

namespace Prueba_MCV
{
    public partial class AgregarModificarProducto: UserControl
    {
        private ProductoController productoController;
        // Variables internas para almacenar el producto actual
        private Producto producto;

        // Evento que otros pueden suscribirse
        public event Action VolverAListar;

        public AgregarModificarProducto()
        {
            InitializeComponent();
            productoController = new ProductoController();
        }

        // Propiedad para establecer y obtener un producto completo
        public void Inicializar(Producto producto = null)
        {
            this.producto = producto;

            if (this.producto != null)
            {
                // Modo modificar
                nombreTextBox.Text = this.producto.nombreproducto;
                descripcionTextBox.Text = this.producto.descripcionproducto;
                tipoTextBox.Text = this.producto.tipoproducto;
                valorTextBox.Text = this.producto.valorproducto.ToString();
                stockTextBox.Text = this.producto.stockproducto.ToString();
                marcaTextBox.Text = this.producto.marcaproducto;
                btnAgregarModificar.Text = "Modificar";
            }
            else
            {
                // Modo agregar: limpiar campos
                nombreTextBox.Text = "";
                descripcionTextBox.Text = "";
                tipoTextBox.Text = "";
                valorTextBox.Text = "";
                stockTextBox.Text = "";
                marcaTextBox.Text = "";
                btnAgregarModificar.Text = "Agregar";
            }
        }


        private void agregarModificarButton_Click(object sender, EventArgs e)
        {
            if (producto == null) // Si Producto es null, estamos en modo agregar
            {
                AgregarProducto();
            }
            else // Si Producto no es null, estamos en modo modificar
            {
                ModificarProducto();
            }
        }

        // Método para agregar un producto
        private void AgregarProducto()
        {
            try
            {
                // Validar campos
                if (string.IsNullOrWhiteSpace(nombreTextBox.Text) ||
                    string.IsNullOrWhiteSpace(descripcionTextBox.Text) ||
                    string.IsNullOrWhiteSpace(tipoTextBox.Text) ||
                    string.IsNullOrWhiteSpace(marcaTextBox.Text) ||
                    string.IsNullOrWhiteSpace(stockTextBox.Text) ||
                    string.IsNullOrWhiteSpace(valorTextBox.Text))
                {
                    MessageBox.Show("Todos los campos deben estar completos.");
                    return;
                }

                if (!int.TryParse(stockTextBox.Text, out int stockproducto))
                {
                    MessageBox.Show("Stock no es un número válido.");
                    return;
                }

                if (!int.TryParse(valorTextBox.Text, out int valorproducto))
                {
                    MessageBox.Show("Valor no es un número válido.");
                    return;
                }

                // Crear producto
                Producto nuevoProducto = new Producto(0, nombreTextBox.Text, descripcionTextBox.Text, tipoTextBox.Text, valorproducto, stockproducto, marcaTextBox.Text);

                // Guardar
                bool resultado = productoController.GuardarProducto(nuevoProducto);
                MessageBox.Show(resultado ? "Producto agregado exitosamente." : "Error al agregar el producto. Verifica los datos y la conexión.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
            finally
            {
                VolverAListar?.Invoke();
            }
        }

        // Método para modificar un producto
        private void ModificarProducto()
        {
            try
            {
                // Obtener los valores de los campos de texto
                string nombre = nombreTextBox.Text;
                string descripcion = descripcionTextBox.Text;
                string tipo = tipoTextBox.Text;

                // Modificar los datos del producto existente
                producto.nombreproducto = nombre;
                producto.descripcionproducto = descripcion;
                producto.tipoproducto = tipo;

                bool resultado = productoController.ModificarProducto(producto);

                // Mostrar mensaje de éxito o error
                MessageBox.Show(resultado ? "Producto modificado exitosamente." : "Error al modificar el producto.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                VolverAListar?.Invoke();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            VolverAListar?.Invoke();
        }

        private void lblValor_Click(object sender, EventArgs e)
        {

        }

        private void nombreTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }
    }
}
