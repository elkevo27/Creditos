using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Creditos
{
    public partial class frm_producto : Form
    {
        public frm_producto()
        {
            InitializeComponent();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            
            Conexion.Conectar();
            String insertar = "INSERT INTO articulo(nombre) VALUES (@textnombre)";
            SqlCommand sqlCommand = new SqlCommand(insertar, Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@textnombre", textnombre.Text);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Registro Completado");
            dataGridView1.DataSource = llenarGrid();

        }

        private void Producto_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenarGrid();
        }

        public DataTable llenarGrid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM articulo";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            return dt;

        }


    }
}
