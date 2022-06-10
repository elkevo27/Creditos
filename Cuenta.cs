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
    public partial class frm_Cuenta : Form
    {
        public frm_Cuenta()
        {
            InitializeComponent();
        }

        private void frm_Cuenta_Load(object sender, EventArgs e)
        {
            
            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM factura", Conexion.Conectar());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBoxfactura.Items.Add((dr.GetString(0)));
            }

            dataGridView1.DataSource = llenarGrid();
        }

        private void comboBoxfactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion.Conectar();
            SqlCommand cmd = new SqlCommand("SELECT * FROM factura WHERE idfactura='"+ comboBoxfactura.Text +"'", Conexion.Conectar());
            SqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read() == true)
            {
                textnombre.Text = leer["cliente"].ToString();
                textmonto.Text = leer["monto"].ToString();
            }
        }

        public DataTable llenarGrid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT c.idcredito as Codigo, c.cliente as Cliente, c.idfactura as Factura, c.monto as Debe, p.valor as Abona,c.monto-p.valor as Saldo, p.fecha as Fecha FROM ((credito as c INNER JOIN pagos as p ON c.idcredito = p.idcredito))";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            return dt;


        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string solvente = "";
            if (radioButton1.Checked == true)
            {
                solvente = solvente + radioButton1.Text;
            }
            if (radioButton2.Checked == true)
            {
                solvente = solvente + radioButton2.Text;

            }
                Conexion.Conectar();
            String insertar = "INSERT INTO credito(cliente,idfactura,monto,estado) VALUES (@textnombre,@factura,@monto,@estado)";
            SqlCommand sqlCommand = new SqlCommand(insertar, Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@textnombre", textnombre.Text);
            sqlCommand.Parameters.AddWithValue("@factura", comboBoxfactura.Text);
            sqlCommand.Parameters.AddWithValue("@monto", textmonto.Text);
            sqlCommand.Parameters.AddWithValue("@estado", solvente);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Registro Completado");
            dataGridView1.DataSource = llenarGrid();

        }
    }
}
