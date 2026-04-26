using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentación
{
    public partial class FormCategoria : Form
    {
        private List<Categoria> listaCategoria;

        public FormCategoria()
        {
            InitializeComponent();
        }

        private void FormCategoria_Load(object sender, EventArgs e)
        {
            cargar(); 
        }
       
        private void cargar()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                listaCategoria = categoriaNegocio.listarCategorias();
                dgvCategorias.DataSource = listaCategoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            tbFiltroRapido.Clear();
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = listaCategoria;
        }
    }
}
