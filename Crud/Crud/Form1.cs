using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Actualizar();
        }
        private void Actualizar()
        {
            ClientesBD clientedb= new ClientesBD();
            dataGridView1.DataSource = clientedb.Get();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Actualizar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmNuevo frm = new FrmNuevo();
            frm.ShowDialog();
            Actualizar();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                FrmNuevo frmEdit = new FrmNuevo(id);
                frmEdit.ShowDialog();
                Actualizar();

            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            int? id = GetId();
            try
            {
                if (id != null)
                {
                    ClientesBD cliente = new ClientesBD();
                    cliente.Eliminar((int)id);
                    Actualizar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al quererlo eliminar: " + ex.Message);
            }
            
        }
        #region HELPER
        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
            
        }

        #endregion


    }
}
