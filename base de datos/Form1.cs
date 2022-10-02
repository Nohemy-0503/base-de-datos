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

namespace base_de_datos
{
    public partial class Form1 : Form
    {
        Empleados Empleado = new Empleados();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "No ingreso el nombre");
                txtNombre.Focus();
                return;
            }

            errorProvider1.SetError(txtNombre, "");

            if (txtDUI.Text == "")
            {
                errorProvider1.SetError(txtDUI, "No ingreso el DUI");
                txtDUI.Focus();
                return;
            }

            errorProvider1.SetError(txtDUI, "");

            double Salario;

            if (!double.TryParse(txtSalario.Text, out Salario))
            {
                errorProvider1.SetError(txtSalario, "No ingreso el salario de forma correcta");
                txtSalario.Focus();
                return;
            }

            errorProvider1.SetError(txtSalario, "");


            Empleado.Nombre = txtNombre.Text;
            Empleado.Dui = txtDUI.Text;
            Empleado.Salario = Convert.ToDouble(txtSalario.Text);
            txtAFP.Text = Empleado.AFP(Empleado.Salario).ToString();
        }

        private void btInsertar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string nombre = txtNombre.Text;
            string dui = txtDUI.Text;
            string salario = txtSalario.Text;
            string afp = txtAFP.Text;
            string cadena = "insert into [Empleados] (Nombre, DUI, Salario, AFP) values ('" + nombre + "','" + dui + "','" + salario + "','" + afp + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Los datos se guardaron correctamente");

            conexion.Close();
        }

        private void btConsultar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cod = txtConsulta.Text;
            string cadena = "select nombre, dui from Empleados where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);

            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                labelnombre.Text = registro["Nombre"].ToString();
                labeldui.Text = registro["DUI"].ToString();
            }
            else
                MessageBox.Show("No existe un Empleado con el código ingresado");

            conexion.Close();

        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDUI.Clear();
            txtSalario.Clear();
            txtAFP.Clear();
            txtConsulta.Clear();
            labelnombre.Text = "";
            labeldui.Text = "";
        }
    }
}
