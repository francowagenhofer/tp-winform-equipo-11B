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
        private BindingSource bindingSource;

        public FormCategoria()
        {
            InitializeComponent();
            bindingSource = new BindingSource();
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


                bindingSource.DataSource = listaCategoria;
                dgvCategorias.DataSource = bindingSource;

                dgvCategorias.AllowUserToAddRows = true;
                dgvCategorias.ReadOnly = false;
                dgvCategorias.Columns["Id"].ReadOnly = true;
                dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria nuevaCategoria = new Categoria();

            try
            {
                if (dgvCategorias.CurrentRow == null)
                    return;

                if (dgvCategorias.CurrentRow.IsNewRow)
                {
                    MessageBox.Show("Seleccioná la fila que querés agregar.");
                    return;
                }

                string descripcion =
                    dgvCategorias.CurrentRow.Cells["Descripcion"]
                    .Value?.ToString();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingresá una descripción para la categoría");
                    return;
                }

                nuevaCategoria.Descripcion = descripcion;

                negocio.agregarCategoria(nuevaCategoria);

                MessageBox.Show("Categoría agregada exitosamente");

                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionado;

            try
            {
                if (dgvCategorias.CurrentRow == null)
                    return;

                if (dgvCategorias.CurrentRow.IsNewRow)
                {
                    MessageBox.Show("No se puede modificar una fila vacía.");
                    return;
                }

                seleccionado =
                    (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

                if (seleccionado.Id == 0)
                {
                    MessageBox.Show(
                        "No se puede modificar una categoría que no existe.");
                    return;
                }

                negocio.modificarCategoria(seleccionado);

                MessageBox.Show("Modificado exitosamente");

                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionado;

            try
            {
                if (dgvCategorias.CurrentRow == null)
                    return;

                if (dgvCategorias.CurrentRow.IsNewRow)
                {
                    MessageBox.Show("No se puede eliminar una fila vacía.");
                    return;
                }

                seleccionado = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

                if (seleccionado.Id == 0)
                {
                    MessageBox.Show("No se puede eliminar una categoría que no existe.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿De verdad querés eliminar la categoría?",
                    "Eliminando",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminarCategoria(seleccionado.Id);

                    MessageBox.Show("Eliminado exitosamente");

                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            tbFiltroRapido.Clear();
            bindingSource.DataSource = listaCategoria;
        }

        private void tbFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Categoria> listaFiltrada;
            string filtro = tbFiltroRapido.Text;

            if (filtro.Length >= 1)
            {
                listaFiltrada = listaCategoria.FindAll(x =>
                    x.Descripcion.ToUpper().Contains(filtro.ToUpper()) ||
                    x.Id.ToString().Contains(filtro));
            }
            else
            {
                listaFiltrada = listaCategoria;
            }

            bindingSource.DataSource = listaFiltrada;
        }
    }
}
