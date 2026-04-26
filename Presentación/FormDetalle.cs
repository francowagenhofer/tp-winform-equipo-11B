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
    public partial class FormDetalle : Form
    {
        private Articulo articulo = null;
        private List<Imagen> imagenesArticulo = new List<Imagen>();
        private int indiceImagenActual = 0;

        public FormDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FormDetalle_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                cbMarca.DataSource = marcaNegocio.listarMarcas();
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";

                cbCategoria.DataSource = categoriaNegocio.listarCategorias();
                cbCategoria.ValueMember = "Id";
                cbCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    tbCodigo.Text = articulo.Codigo;
                    tbNombre.Text = articulo.Nombre;
                    tbDescripcion.Text = articulo.Descripcion;
                    tbPrecio.Text = articulo.Precio.ToString();

                    cbMarca.SelectedValue = articulo.Marca.Id;
                    cbCategoria.SelectedValue = articulo.Categoria.Id;

                    imagenesArticulo = imagenNegocio.listarImagenesPorArticulo(articulo.Id);
                    refrescarListaImagenes();
                    
                    if (imagenesArticulo.Count > 0)
                    {
                        indiceImagenActual = 0;
                        mostrarImagenActual();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbImagenes.Load(imagen);
            }
            catch
            {
                pbImagenes.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSk8RLjeIEybu1xwZigumVersvGJXzhmG8-0Q&s");
            }
        }
       
        private void refrescarListaImagenes()
        {
            listaImagenes.DataSource = null;
            listaImagenes.DataSource = imagenesArticulo;
            listaImagenes.DisplayMember = "ImagenUrl";
        }
    
        private void listaImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listaImagenes.SelectedIndex >= 0)
                {
                    indiceImagenActual = listaImagenes.SelectedIndex;
                    mostrarImagenActual();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagenesArticulo.Count == 0)
                    return;

                if (indiceImagenActual > 0)
                    indiceImagenActual--;
                else
                    indiceImagenActual = imagenesArticulo.Count - 1; // va al final

                mostrarImagenActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagenesArticulo.Count == 0)
                    return;

                if (indiceImagenActual < imagenesArticulo.Count - 1)
                    indiceImagenActual++;
                else
                    indiceImagenActual = 0; // vuelve al inicio

                mostrarImagenActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void mostrarImagenActual()
        {
            if (imagenesArticulo.Count == 0)
            {
                pbImagenes.Image = null;
                lblContadorImagen.Text = "0 / 0";
                return;
            }

            cargarImagen(imagenesArticulo[indiceImagenActual].ImagenUrl);

            lblContadorImagen.Text =
                (indiceImagenActual + 1).ToString() +
                " / " +
                imagenesArticulo.Count.ToString();

            listaImagenes.SelectedIndex = indiceImagenActual;
        }

    }
}
