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

    public partial class FrmProductoEdit : Form
    {
        string cadenaConexion = "server=localhost; database=Parcial; Integrated security=true";
        private int? Id;

        public FrmProductoEdit(int? id = null)
        {
            InitializeComponent();
            this.Id = id;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cboDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmProductoEdit_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                var sql = "SELECT * FROM Categoria";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> CategoriaSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                CategoriaSource.Add(lector[0].ToString(), lector[1].ToString());

                            }
                            cboCategoria.DataSource = new BindingSource(CategoriaSource, null);
                            cboCategoria.DisplayMember = "Value";
                            cboCategoria.ValueMember = "Key";

                        }
                    }
                }

            }
        }

        private void btnSafe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                MessageBox.Show("El nombre es un dato obligatorio", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
            {
                MessageBox.Show("La marca es un dato obligatorio", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            if (string.IsNullOrEmpty(txtPrecio.Text.Trim()))
            {
                MessageBox.Show("El precio es un dato obligatorio", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            if (string.IsNullOrEmpty(txtStock.Text.Trim()))
            {
                MessageBox.Show("El stock es un dato obligatorio", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (string.IsNullOrEmpty(cboCategoria.Text.Trim()))
            {
                MessageBox.Show("La categoria es un dato obligatorio", "Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            {
                double preprecio;
                preprecio = double.Parse(txtPrecio.Text);
                txtPrecio.Text = preprecio.ToString();
                if (preprecio > 2500)
                {
                    MessageBox.Show("El precio máximo es 2500", "Sistema",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                {
                    double prestock;
                    prestock = double.Parse(txtStock.Text);
                    txtPrecio.Text = preprecio.ToString();
                    if (prestock < 5)
                    {
                        MessageBox.Show("El stock minimo debe ser 5", "Sistema",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
