using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRACTICA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;

            DataTable tabla = new DataTable();
            Class1 personas = new Class1();
            string sql = "select * from BARRIOS";
            tabla = personas.personas(sql);

            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "barrioID";
            comboBox1.DataSource = tabla;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string sql = "select * from PERSONAS";
            Class1 personas = new Class1();
            dt = personas.personas(sql);
           
            foreach (DataRow fila in dt.Rows)
            {
                if (textBox1.Text == fila["personaID"].ToString())
                {
                    if (fila["sexo"].ToString() == "f")
                    {
                        radioButton1.Checked = true;
                        if (Convert.ToInt32(fila["trabaja"]) == 1)
                        {
                            checkBox1.Checked = true;
                            textBox1.Text = fila["personaID"].ToString();
                            textBox2.Text = fila["nombre"].ToString();
                            comboBox1.SelectedValue = fila["barrioID"];
                        }
                    }

                    if (fila["sexo"].ToString() == "m")
                    {
                        radioButton2.Checked = true;
                        if (Convert.ToInt32(fila["trabaja"]) == 1)
                        {
                            checkBox1.Checked = true;
                            textBox1.Text = fila["personaID"].ToString();
                            textBox2.Text = fila["nombre"].ToString();
                            comboBox1.SelectedValue = fila["barrioID"];

                        }
                    }
                }
                if (fila["sexo"].ToString() == "f")
                {
                    radioButton2.Checked = true;
                    if (Convert.ToInt32(fila["trabaja"]) == 1)
                    {
                        checkBox1.Checked = true;
                        textBox1.Text = fila["personaID"].ToString();
                        textBox2.Text = fila["nombre"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(fila["nacio"]);
                        comboBox1.SelectedValue = fila["barrioID"];

                    }
                    if (Convert.ToInt32(fila["trabaja"]) == 0)
                    {
                        checkBox1.Checked = false;
                        textBox1.Text = fila["personaID"].ToString();
                        textBox2.Text = fila["nombre"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(fila["nacio"]);
                        comboBox1.SelectedValue = fila["barrioID"];

                    }
                }

            }

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string nombre = textBox2.Text;
            string sexo="";
            int trabaja;
            DateTime nacio = dateTimePicker1.Value;
            int barrio =Convert.ToInt32(comboBox1.SelectedValue);

            if (radioButton1.Checked)
            {
                sexo = "f";
            }
            if(radioButton2.Checked)
            {
                 sexo = "m";
            }

            if (checkBox1.Checked)
            {
                trabaja = 1;
            }
            else
            {
                trabaja = 0;
            }

            string sql = $"INSERT INTO PERSONAS (personaID, nombre, sexo, trabaja, nacio, barrioID) VALUES ('{id}','{nombre}', '{sexo}', '{trabaja}', '{nacio.ToString("dd/MM/yy")}', '{barrio}') ";
            Class1 personas = new Class1();
            dt = personas.personas(sql);


            textBox1.Clear();
            textBox2.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string sql = $"DELETE FROM PPERSONAS WHERE personaID = {id}";
            Class1 personas = new Class1();
            dt = personas.personas(sql);


            textBox1.Clear();
            textBox2.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string nombre = textBox2.Text;
            string sexo = "";
            int trabaja = 0;
            DateTime nacio = dateTimePicker1.Value;
            int barrio = Convert.ToInt32(comboBox1.SelectedValue);

            if (radioButton1.Checked)
            {
                sexo = "f";
            }
            else if (radioButton2.Checked)
            {
                sexo = "m";
            }


            string sql = $@"UPDATE personas SET nombre = '{nombre}', sexo = '{sexo}', trabaja = {trabaja}, nacio = '{nacio.ToString("dd/MM/yy")}, barrio= '{barrio}'
                            WHERE idpersonas = {id}";

            Class1 personas = new Class1();
            dt = personas.personas(sql);

            textBox1.Clear();
            textBox2.Clear();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT  personas.personaID, personas.nombre, personas.sexo, personas.trabaja, personas.nacio, barrios.nombre  
                            FROM personas, barrios
                            WHERE personas.barrioID=barrios.barrioID";
            Class1 personas = new Class1();
            dt = personas.personas(sql);



            dataGridView1.Rows.Clear();
            foreach (DataRow fila in dt.Rows)
            {

                if (listBox1.SelectedIndex == 0)
                {
                    if (radioButton3.Checked == true)
                    {

                        if (fila["sexo"].ToString() == "f" && fila["trabaja"].ToString() == "1")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "si", fila["nacio"], fila[5]);

                        }
                    }

                }
                if (listBox1.SelectedIndex == 0)
                {
                    if (radioButton4.Checked == true)
                    {

                        if (fila["sexo"].ToString() == "f" && fila["trabaja"].ToString() == "0")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "no", fila["nacio"], fila[5]);

                        }
                    }

                }
                if (listBox1.SelectedIndex == 1)
                {
                    if (radioButton3.Checked == true)
                    {

                        if (fila["sexo"].ToString() == "m" && fila["trabaja"].ToString() == "1")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "si", fila["nacio"], fila[5]);

                        }
                    }

                }
                if (listBox1.SelectedIndex == 1)
                {
                    if (radioButton4.Checked == true)
                    {

                        if (fila["sexo"].ToString() == "m" && fila["trabaja"].ToString() == "0")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "no", fila["nacio"], fila[5]);

                        }
                    }

                }
                if (listBox1.SelectedIndex == 2)
                {
                    if (radioButton3.Checked == true)
                    {

                        if (fila["trabaja"].ToString() == "1")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "si", fila["nacio"], fila[5]);

                        }
                    }
                }
                if (listBox1.SelectedIndex == 2)
                {
                    if (radioButton4.Checked == true)
                    {

                        if (fila["trabaja"].ToString() == "0")
                        {

                            dataGridView1.Rows.Add(fila["personaID"], fila["nombre"], fila["sexo"], "no", fila["nacio"], fila[5]);

                        }
                    }
                }
            }

        }
    }
    
}
