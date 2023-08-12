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

namespace DataMusic_SQLServer
{
    /// <summary>
    /// Lógica de interacción para Eliminar.xaml
    /// </summary>
    public partial class Eliminar : Window
    {
        public int IdAlbum { get; set; }
        public int IdAutor { get; set; }

        DataClasses1DataContext dataContext;

        public Eliminar()
        {
            InitializeComponent();

            string conexion = ConfigurationManager.ConnectionStrings["DataMusic_SQLServer.Properties.Settings.ConnectionString"].ConnectionString;

            dataContext = new DataClasses1DataContext(conexion);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(checkEliminar.IsChecked == true)
            {  
                List<Album> albunes = dataContext.Album.Where(a => a.AutorId == IdAutor).ToList();
                foreach(Album album in albunes)
                {
                    List<Cancion> canciones = dataContext.Cancion.Where(c => c.AlbumId == album.Id).ToList();
                    foreach (Cancion cancion in canciones)
                    {
                        dataContext.Cancion.DeleteOnSubmit(cancion);
                    }

                    dataContext.Album.DeleteOnSubmit(album);
                }                

                Autor autor = dataContext.Autor.First(a => a.Id == IdAutor);
                dataContext.Autor.DeleteOnSubmit(autor);
                
                dataContext.SubmitChanges();

                this.Close();
            }
            else
            {
                List<Cancion> canciones = dataContext.Cancion.Where(c => c.AlbumId == IdAlbum).ToList();

                foreach (Cancion cancion in canciones)
                {
                    dataContext.Cancion.DeleteOnSubmit(cancion);
                }

                Album album = dataContext.Album.First(a => a.Id == IdAlbum);

                dataContext.Album.DeleteOnSubmit(album);

                dataContext.SubmitChanges();

                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
