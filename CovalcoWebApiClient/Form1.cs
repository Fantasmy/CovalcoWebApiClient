using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace CovalcoWebApiClient
{
    public partial class Form1 : Form
    {
        HttpApiControler controller;
        public Form1()
        {
            InitializeComponent();
            //GridAlumnos.AutoGenerateColumns = true;
            Console.WriteLine(Resource1.ReqMsgHeader);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GridAlumnos.AutoGenerateColumns = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller = new HttpApiControler();
            List<AlumnoViewModel> alumnos = new List<AlumnoViewModel>();
            alumnos = controller.GetCall().Result;  // devuelve el resultado de una tarea (=lista de alumnos)
            GridAlumnos.DataSource = alumnos;  //importante hacer el datasource
            GridAlumnos.Refresh(); // si no haces refresh no hace nada, queda en gris
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
