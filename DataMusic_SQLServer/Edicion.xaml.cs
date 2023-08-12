using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using System.IO;

namespace DataMusic_SQLServer
{
    /// <summary>
    /// Lógica de interacción para Edicion.xaml
    /// </summary>
    public partial class Edicion : Window
    {   
        DataClasses1DataContext dataContext;

        List<Cancion> listaCanciones;

        public int IdAlbum { get; set; }
        public int IdAutor { get; set; }
        public int IdCancion { get; set; }
        public string Autor { get; set; }
        public string Album { get; set; }
        public int Año { get; set; }
        public string selectedFilePath;        

        public Edicion()
        {
            InitializeComponent();

            string conexion = ConfigurationManager.ConnectionStrings["DataMusic_SQLServer.Properties.Settings.ConnectionString"].ConnectionString;

            dataContext = new DataClasses1DataContext(conexion);

            gridCanciones.PreviewMouseDoubleClick += DataGrid_PreviewMouseDoubleClick;
        }        

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtAutor.Text.Trim() != "" && txtAlbum.Text.Trim() != "" && txtAño.Text.Trim() != "")
            {
                EditarAutor();
                EditarAlbum();
                this.Close();
            }
            else
            {
                MessageBox.Show("Introduce los datos que faltan.");
            }
        }

        private void EditarAutor()
        {
            if (txtAutor.Text != Autor)
            {
                Autor autor = dataContext.Autor.First(a => a.Id == IdAutor);

                autor.Nombre = txtAutor.Text;

                dataContext.SubmitChanges();
            }            
            else
            {

            }
        }

        private void EditarAlbum()
        {
            if (txtAlbum.Text.Trim() != Album || txtAño.Text.Trim() != Convert.ToString(Año) || selectedFilePath != null)
            {
                Album album = dataContext.Album.First(a => a.Id == IdAlbum);                
                
                if (selectedFilePath != null)
                {
                    album.Titulo = txtAlbum.Text;
                    album.Año = Convert.ToInt32(txtAño.Text);
                    album.Portada = GuardarPortada();                    
                }
                else
                {
                    album.Titulo = txtAlbum.Text;
                    album.Año = Convert.ToInt32(txtAño.Text);                    
                }

                dataContext.SubmitChanges();
            }            
            else
            {
                
            }            
        }

        private void MostrarPortada(int albumId)
        {
            var album = dataContext.Album.FirstOrDefault(a => a.Id == albumId);

            if (album != null && album.Portada != null)
            {
                // Convertir el campo Portada (tipo Binary) a un arreglo de bytes (byte[])
                byte[] imageBytes = album.Portada.ToArray();

                if (imageBytes.Length > 0)
                {
                    // Convertir los datos binarios en un BitmapImage
                    BitmapImage image = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = memoryStream;
                        image.EndInit();
                    }

                    // Mostrar la imagen en el control Image
                    imgPortada.Source = image;
                }
                else
                {
                    // Si no hay imagen o es nula, puedes mostrar una imagen predeterminada o dejar el control Image vacío
                    imgPortada.Source = null;
                }
            }
            else
            {
                // Si el álbum no existe o no tiene portada, deja el control Image vacío
                imgPortada.Source = null;
            }
        }

        private void SeleccionarPortada_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos|*.*",
                Title = "Seleccionar imagen"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                selectedFilePath = openFileDialog.FileName;

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(selectedFilePath);
                image.EndInit();

                imgPortada.Source = image;
            }
        }

        private byte[] GuardarPortada()
        {
            // Cargar la imagen desde la ruta del archivo
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(selectedFilePath);
            image.EndInit();

            // Convierte la imagen a un arreglo de bytes
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // Puedes ajustar el encoder según el formato que estés utilizando
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(ms);
                imageBytes = ms.ToArray();
            }

            return imageBytes;
        }

        private void gridCanciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridCanciones.SelectedItem != null)
            {
                var cancionSeleccionada = gridCanciones.SelectedItem as dynamic;

                txtNumeroCancion.Text = Convert.ToString(cancionSeleccionada.Pista);
                txtTituloCancion.Text = cancionSeleccionada.Título;
                IdCancion = cancionSeleccionada.IdCancion;
            }
        }        

        private void btnAgregarCancion_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumeroCancion.Text.Trim() != "" && txtTituloCancion.Text.Trim() != "")
            {
                int num = Convert.ToInt32(txtNumeroCancion.Text);
                string tittle = txtTituloCancion.Text.Trim();

                dataContext.Cancion.InsertOnSubmit(new Cancion { Numero = num, Titulo = tittle, AlbumId = IdAlbum });

                dataContext.SubmitChanges();

                MostrarCanciones();

                txtNumeroCancion.Text = "";
                txtTituloCancion.Text = "";
            }
            else
            {
                MessageBox.Show("Introduce los datos necesarios antes de agregar la canción.");
            }
            
        }

        private void btnBorrarCancion_Click(object sender, RoutedEventArgs e)
        {
            if(gridCanciones.SelectedItem != null)
            {
                var cancion = gridCanciones.SelectedItem as dynamic;

                int id = cancion.IdCancion;

                var cancionAEliminar = dataContext.Cancion.First(c => c.Id == id);

                dataContext.Cancion.DeleteOnSubmit(cancionAEliminar);

                dataContext.SubmitChanges();

                MostrarCanciones();

                txtNumeroCancion.Text = "";
                txtTituloCancion.Text = "";
            }
            else
            {
                MessageBox.Show("Debes seleccionar una canción para eliminarla.");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            MostrarCanciones();
            MostrarPortada(IdAlbum);
        }

        private void MostrarCanciones()
        {
            gridCanciones.ItemsSource = dataContext.Cancion.Where(c => c.AlbumId == IdAlbum).Select(c => new { Pista = c.Numero, Título = c.Titulo, IdCancion = c.Id });
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verificar si el texto ingresado es un número
            if (!EsNumero(e.Text))
            {
                e.Handled = true; // Si no es un número, se ignora el texto ingresado
            }
        }

        private bool EsNumero(string texto)
        {
            // Utilizar la función TryParse de int para comprobar si el texto es un número
            return int.TryParse(texto, out _);
        }

        //El código de abajo sirve para controlar cuando se hace clic en una fila del datagrid, evitar bloqueos y errores

        private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Marcar el evento como "manejado" para evitar que se propague al evento MouseDoubleClick
            e.Handled = true;
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Indicar que hemos manejado el evento, evitando que se propague más allá.
            }
        }

        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verificar si el clic es el botón izquierdo del ratón (opcional, para asegurarnos de que solo respondemos a clics izquierdos).
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Obtener el elemento de la fila en la que se hizo clic
                var clickedElement = e.OriginalSource as FrameworkElement;
                if (clickedElement != null)
                {
                    // Obtener la fila que contiene el elemento
                    var clickedRow = FindVisualParent<DataGridRow>(clickedElement);
                    if (clickedRow != null)
                    {
                        // Verificar si la fila ya está seleccionada
                        if (clickedRow.IsSelected)
                        {
                            // Deseleccionar la fila
                            clickedRow.IsSelected = false;
                        }
                    }
                }
            }
        }

        // Método auxiliar para encontrar el elemento visual padre de un cierto tipo
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (true)
            {
                // Obtener el padre visual del elemento actual
                var parentObject = VisualTreeHelper.GetParent(child);
                if (parentObject == null)
                {
                    return null;
                }

                // Verificar si el padre es del tipo deseado
                if (parentObject is T parent)
                {
                    return parent;
                }

                // Continuar buscando en el padre del padre
                child = parentObject;
            }
        }
    }   


}
