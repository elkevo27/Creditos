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
    public partial class Form1 : Form
    {

        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-0MQU2P1\\MSSQLSERVER01;Initial Catalog=Creditos;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
            cargar_clientes();
            cargar_articulo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conexion.Open();
            MessageBox.Show("Conexion Exitosa");
            dataGridView1.DataSource = llenarGrid();
            Sumarventas();
            DateTime fecha = DateTime.Now;
            textBoxfecha.Text = fecha.ToShortDateString();
            
            

        }

        public DataTable llenarGrid()
        {

            
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM factura";
            string total = "SELECT SUM(monto) FROM factura";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            return dt;


        }


        private void Sumarventas()
        {

            decimal total = 0;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDecimal(row.Cells["monto"].Value);
            }

            labeltotal.Text = total.ToString();
        }



        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form form = new frm_clientes();
            form.Show();
            
        }

        public void cargar_clientes()
        {
            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM clientes where estado='Solvente'", Conexion.Conectar());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            conexion.Close();

            DataRow fila = dataTable.NewRow();
            fila["nombre"] = "Seleciones un Cliente";
            dataTable.Rows.InsertAt(fila, 0);
            comboBoxcliente.ValueMember = "idcliente";
            comboBoxcliente.DisplayMember = "nombre";
            comboBoxcliente.DataSource = dataTable;
            // permite el autocomplete

        }

        public void cargar_articulo()
        {
            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM articulo", Conexion.Conectar());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            conexion.Close();

            DataRow fila = dataTable.NewRow();
            fila["nombre"] = "Seleciones un Articulo";
            dataTable.Rows.InsertAt(fila, 0);
            comboBoxarticulo.ValueMember = "idarticulo";
            comboBoxarticulo.DisplayMember = "nombre";
            comboBoxarticulo.DataSource = dataTable;
            // permite el autocomplete

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            Conexion.Conectar();
            String insertar = "INSERT INTO factura(idfactura,cliente,articulo,fechacompra,monto) VALUES (@textcodigo,@cliente,@articulo,@fecha,@monto)";
            SqlCommand sqlCommand = new SqlCommand(insertar, Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@textcodigo", textcodigo.Text);
            sqlCommand.Parameters.AddWithValue("@cliente", comboBoxcliente.Text);
            sqlCommand.Parameters.AddWithValue("@articulo", comboBoxarticulo.Text);
            sqlCommand.Parameters.AddWithValue("@fecha", textBoxfecha.Text);
            sqlCommand.Parameters.AddWithValue("@monto", textmonto.Text);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Registro Completado");
            dataGridView1.DataSource = llenarGrid();


        }

        private void ingresarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form form = new frm_producto();
            form.Show();

        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frm_Pagos();
            form.Show();
        }

        private void abonosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frm_Cuenta();
            form.Show();
        }
    }
}
