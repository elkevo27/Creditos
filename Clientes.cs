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
    public partial class frm_clientes : Form
    {
        public frm_clientes()
        {
            InitializeComponent();
        }

        private void frm_clientes_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = llenarGrid();

        }

        public DataTable llenarGrid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM clientes";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            return dt;


        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            string solvente = "";
            if (checkBox1.Checked == true)
            {
                solvente = solvente + checkBox1.Text;
            }
            if (checkBox2.Checked == true)
            {
                solvente = solvente + checkBox2.Text;
            }

            Conexion.Conectar();
            String insertar = "INSERT INTO clientes(nombre,cedula,estado) VALUES (@textnombre,@textcedula,@estado)";
            SqlCommand sqlCommand = new SqlCommand(insertar, Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@textnombre", textnombre.Text);
            sqlCommand.Parameters.AddWithValue("@textcedula", textcedula.Text);
            sqlCommand.Parameters.AddWithValue("@estado", solvente);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Registro Completado");
            dataGridView1.DataSource = llenarGrid();


        }
    }
}
