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
    public partial class FrmNuevo : Form
    {
        private int? Id;
        public FrmNuevo(int? id=null)
        {
            InitializeComponent();
            this.Id = id;
            if (this.Id != null)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            ClientesBD cliente = new ClientesBD();
            Cliente clienteDB=cliente.Get((int)Id);
            txtName.Text = clienteDB.Nombre;
            txtTipoUsuario.Text = clienteDB.Tipo_Usuario.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClientesBD cliente = new ClientesBD();
            try
            {
                if (Id == null)
                {

                    cliente.Add(txtName.Text, int.Parse(txtTipoUsuario.Text));
                }
                else
                {
                    cliente.Update(txtName.Text, int.Parse(txtTipoUsuario.Text), (int)Id);
                }
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
        }
    }
}
