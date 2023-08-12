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
using Microsoft.Win32;
using System.IO;


namespace DataMusic_SQLServer
{
    /// <summary>
    /// Lógica de interacción para Nuevo.xaml
    /// </summary>
    public partial class Nuevo : Window
    {
        DataClasses1DataContext dataContext;
        string selectedFilePath;

        public Nuevo()
        {
            InitializeComponent();

            string conexion = ConfigurationManager.ConnectionStrings["DataMusic_SQLServer.Properties.Settings.ConnectionString"].ConnectionString;

            dataContext = new DataClasses1DataContext(conexion);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtAutor.Text.Trim() != "" && txtAlbum.Text.Trim() != "" && txtAño.Text.Trim() != "")
            {
                InsertarAutor();
                InsertarAlbum();
                this.Close();
            }
            else
            {
                MessageBox.Show("Introduce los datos que faltan.");
            }
        }

        private void InsertarAutor()
        {     
            if (dataContext.Autor.FirstOrDefault(a => a.Nombre == txtAutor.Text) == null)
            {               
                dataContext.Autor.InsertOnSubmit(new Autor { Nombre = txtAutor.Text });

                dataContext.SubmitChanges();
            }
            else
            {
                
            }            
        }

        private void InsertarAlbum()
        {
            var nAutor = dataContext.Autor.First(a => a.Nombre == txtAutor.Text);

            if (nAutor != null)
            {
                if(dataContext.Album.FirstOrDefault(a => a.Titulo == txtAlbum.Text) == null)
                {
                    if (selectedFilePath != null)
                    {
                        dataContext.Album.InsertOnSubmit(new Album
                        {
                            Titulo = txtAlbum.Text,
                            Año = Convert.ToInt32(txtAño.Text),
                            AutorId = nAutor.Id,
                            Portada = GuardarPortada()
                        });
                    }
                    else
                    {
                        dataContext.Album.InsertOnSubmit(new Album
                        {
                            Titulo = txtAlbum.Text,
                            Año = Convert.ToInt32(txtAño.Text),
                            AutorId = nAutor.Id
                        });
                    }                   

                    dataContext.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("El álbum ya existe.");
                }
                
            }
            else
            {
                MessageBox.Show("El autor no fue encontrado.");
            }            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
