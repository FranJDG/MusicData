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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.IO;
using Microsoft.Win32;
using System.Data.Linq;
using System.Runtime.Remoting.Contexts;

namespace DataMusic_SQLServer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataContext;               

        public MainWindow()
        {
            InitializeComponent();

            Conectar();

            MostrarAlbunes();

            //desactivar doble clic
            gridAlbunes.PreviewMouseDoubleClick += DataGrid_PreviewMouseDoubleClick;
            gridCanciones.PreviewMouseDoubleClick += DataGrid_PreviewMouseDoubleClick;
        }

        private void Conectar()
        {
            string conexion = ConfigurationManager.ConnectionStrings["DataMusic_SQLServer.Properties.Settings.ConnectionString"].ConnectionString;

            dataContext = new DataClasses1DataContext(conexion);            
        }

        private void MostrarAlbunes()
        {
            gridAlbunes.ItemsSource = dataContext.Album.Select(a => new
            {
                IdAlbum = a.Id,
                IdAutor = a.Autor.Id,
                Título = a.Titulo,
                Autor = a.Autor.Nombre,
                a.Año
            });            
        }

        private void MostrarCanciones(int albumId)
        {
            gridCanciones.ItemsSource = dataContext.Cancion.Where(c => c.AlbumId == albumId).Select(c => new
            {
                Pista = c.Numero,
                Título = c.Titulo
            });
        }

        private void MostrarPortada(int albumId)
        {   
            Conectar();
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

        private void gridAlbunes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (gridAlbunes.SelectedItem != null)
            {
                // Obtener el álbum seleccionado del DataGrid
                var albumSeleccionado = gridAlbunes.SelectedItem as dynamic;                

                // Obtener el ID del álbum seleccionado
                int idAlbumSeleccionado = albumSeleccionado.IdAlbum;

                // Cargar las canciones y la portada del álbum seleccionado en el DataGrid de canciones
                MostrarCanciones(idAlbumSeleccionado);
                MostrarPortada(idAlbumSeleccionado);
            } 
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Nuevo ventana = new Nuevo();

            ventana.txtAutor.Focus();

            ventana.ShowDialog();

            gridCanciones.ItemsSource = null;
            gridAlbunes.ItemsSource = null;
            imgPortada.Source = null;
            MostrarAlbunes();           
            
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(gridAlbunes.SelectedItem != null)
            {
                var albumSeleccionado = gridAlbunes.SelectedItem as dynamic;

                Edicion ventana = new Edicion();

                ventana.txtAutor.Text = albumSeleccionado.Autor;
                ventana.txtAlbum.Text = albumSeleccionado.Título;
                ventana.txtAño.Text = Convert.ToString(albumSeleccionado.Año);
                ventana.IdAlbum = albumSeleccionado.IdAlbum;
                ventana.IdAutor = albumSeleccionado.IdAutor;
                ventana.Autor = albumSeleccionado.Autor;
                ventana.Album = albumSeleccionado.Título;
                ventana.Año = albumSeleccionado.Año;

                ventana.ShowDialog();

                gridCanciones.ItemsSource = null;
                gridAlbunes.ItemsSource = null;
                imgPortada.Source = null;
                MostrarAlbunes();
            }
            else
            {
                MessageBox.Show("Debes seleccionar un álbum para poder editar y agregar canciones.");
            }
            
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (gridAlbunes.SelectedItem != null)
            {
                var albumSeleccionado = gridAlbunes.SelectedItem as dynamic;

                Eliminar ventana = new Eliminar();

                ventana.IdAlbum = albumSeleccionado.IdAlbum;
                ventana.IdAutor = albumSeleccionado.IdAutor;

                ventana.ShowDialog();

                gridCanciones.ItemsSource = null;
                gridAlbunes.ItemsSource = null;
                imgPortada.Source = null;
                MostrarAlbunes();
            }
            else
            {
                MessageBox.Show("Debes seleccionar un álbum para poder eliminarlo.");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string busqueda = txtBuscar.Text.ToLower(); // Texto de búsqueda en minúsculas para hacer la búsqueda insensible a mayúsculas.            

            List<Album> albunes = dataContext.Album.ToList();            
                       
            List<Album> albunesFiltrados = albunes.Where(a =>
                a.Titulo.ToLower().Contains(busqueda) ||
                a.Autor.Nombre.ToLower().Contains(busqueda) ||
                a.Año.ToString().Contains(busqueda)
            ).ToList();

            // Actualizar la vista del DataGrid con los resultados filtrados
            gridAlbunes.ItemsSource = albunesFiltrados.Select(a => new
            {
                IdAlbum = a.Id,
                IdAutor = a.Autor.Id,
                Título = a.Titulo,
                Autor = a.Autor.Nombre,
                a.Año
            });
        }

        private void btnAcerca_Click(object sender, RoutedEventArgs e)
        {            
            MessageBox.Show("Versión 1.0 \n29/07/2023 \n\nCreado con WPF (.Net FrameWork) \n\n©Fran Díaz", "Acerca de Music Data");            
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
