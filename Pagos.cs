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
using System.Data.SqlClient;

namespace Creditos
{
    public partial class frm_Pagos : Form
    {
        public frm_Pagos()
        {
            InitializeComponent();
        }

        private void frm_Pagos_Load(object sender, EventArgs e)
        {

            DateTime fecha = DateTime.Now;
            textfecha.Text = fecha.ToShortDateString();
            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM credito", Conexion.Conectar());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBoxcredito.Items.Add((dr.GetString(0)));
            }

            dataGridView1.DataSource = llenarGrid();
        }

        private void comboBoxcredito_SelectedIndexChanged(object sender, EventArgs e)
        {

            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM credito WHERE idcredito='" + comboBoxcredito.Text + "'", Conexion.Conectar());
            SqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read() == true)
            {
                textcliente.Text = leer["cliente"].ToString();
            }
        }

        public DataTable llenarGrid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM pagos";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            return dt;


        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String insertar = "INSERT INTO pagos(idpago,idcredito,cliente,fecha,valor) VALUES (@textcodigo,@idcredito,@cliente,@fecha,@valor)";
            SqlCommand sqlCommand = new SqlCommand(insertar, Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@textcodigo", textcodigo.Text);
            sqlCommand.Parameters.AddWithValue("@idcredito", comboBoxcredito.Text);
            sqlCommand.Parameters.AddWithValue("@cliente", textcliente.Text);
            sqlCommand.Parameters.AddWithValue("@fecha", textfecha.Text);
            sqlCommand.Parameters.AddWithValue("@valor", textabono.Text);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Registro Completado");
            dataGridView1.DataSource = llenarGrid();
        }
    }
}
