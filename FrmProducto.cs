using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial
{
    public partial class FrmProducto : Form
    {
        string cadenaConexion = "server=localhost; database=Parcial; Integrated security=true";
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var frm = new FrmProductoEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                var marca = ((TextBox)frm.Controls["txtMarca"]).Text;
                var precio = ((TextBox)frm.Controls["txtPrecio"]).Text;
                var categoria = ((ComboBox)frm.Controls["cboCategoria"]).SelectedValue.ToString();
                var stock = ((TextBox)frm.Controls["txtStock"]).Text;
                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var sql = "INSERT INTO Producto (Nombre ,Marca, Precio, " +
                        "Stock,IdCategoria) " +
                        "VALUES(@nombre, @marca, @precio, @stock, @idcategoria)";
                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@precio", precio);
                        comando.Parameters.AddWithValue("@idcategoria", categoria);
                        comando.Parameters.AddWithValue("@stock", stock);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El cliente ha sido registrado", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("El proceso de creación del cliente ha fallado.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            dgvListado.Rows.Clear();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var sql = "SELECT A.IDProducto,A.Nombre,A.Marca,A.Precio,A.Stock,A.Idcategoria,B.Nombre " +
                    "FROM Producto A INNER JOIN Categoria B ON A.Idcategoria=B.Idcategoria ";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvListado.Rows.Add(lector[0], lector[1], lector[2], lector[3], lector[5], lector[4], lector[6]);
                            }
                        }
                    }
                }
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id > 0)
            {
                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var sql = "DELETE FROM Producto WHERE IdProducto= @IdProducto";
                    using (var comando = new SqlCommand(sql, conexion))
                    {

                        comando.Parameters.AddWithValue("@IdProducto", id);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El cliente ha sido eliminado", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("El proceso de la eliminación del cliente ha fallado.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private int getId()

        {
            try
            {
                DataGridViewRow filaActual = dgvListado.CurrentRow;
                if (filaActual == null)
                {
                    return 0;
                }
                return int.Parse(dgvListado.Rows[filaActual.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }


        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int id = getId();
            if (id > 0)
            {
                var frm = new FrmProductoEdit();
                if (frm.ShowDialog() == DialogResult.OK)

                {
                    var nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                    var marca = ((TextBox)frm.Controls["txtMarca"]).Text;
                    var precio = ((TextBox)frm.Controls["txtPrecio"]).Text;
                    var categoria = ((ComboBox)frm.Controls["cboCategoria"]).SelectedValue.ToString();
                    var stock = ((TextBox)frm.Controls["txtStock"]).Text;
                    using (var conexion = new SqlConnection(cadenaConexion))
                    {
                        conexion.Open();
                        var sql = "UPDATE Producto SET Nombre=@nombre, Marca=@marca, Precio=@precio, Stock=@stock,IdCategoria=@idcategoria  WHERE IdProducto=@IdProducto";
                        using (var comando = new SqlCommand(sql, conexion))
                        {
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@marca", marca);
                            comando.Parameters.AddWithValue("@precio", precio);
                            comando.Parameters.AddWithValue("@idcategoria", categoria);
                            comando.Parameters.AddWithValue("@stock", stock);
                            comando.Parameters.AddWithValue("@IdProducto", id);

                            int resultado = comando.ExecuteNonQuery();
                            if (resultado > 0)
                            {
                                MessageBox.Show("El cliente ha sido actualizado", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cargarDatos();
                            }
                            else
                            {
                                MessageBox.Show("El proceso de actualización del cliente ha fallado.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}
            










            
    

