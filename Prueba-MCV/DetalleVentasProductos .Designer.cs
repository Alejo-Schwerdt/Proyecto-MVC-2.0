namespace Prueba_MCV
{
    partial class DetalleVentasProductos
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private System.Windows.Forms.DataGridView dataGridViewDetalles;
        private System.Windows.Forms.Button btnVolver;

        private void InitializeComponent()
        {
            this.dataGridViewDetalles = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDetalles
            // 
            this.dataGridViewDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetalles.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewDetalles.Name = "dataGridViewDetalles";
            this.dataGridViewDetalles.Size = new System.Drawing.Size(560, 300);
            this.dataGridViewDetalles.TabIndex = 0;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 325);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 30);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver al menú";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // DetalleVentasProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dataGridViewDetalles);
            this.Name = "DetalleVentasProductos";
            this.Text = "Detalle de Ventas";
            this.Load += new System.EventHandler(this.DetalleVentasProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
