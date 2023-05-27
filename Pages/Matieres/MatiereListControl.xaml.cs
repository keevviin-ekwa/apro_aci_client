using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Produits;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Matieres
{
    /// <summary>
    /// Logique d'interaction pour MatiereListControl.xaml
    /// </summary>
    public partial class MatiereListControl : UserControl
    {
        public List<Matiere> Matieres { get; set; }
       
        public MatiereListControl()
        {
            InitializeComponent();
            getData();
        }

        public async void getData()
        {
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Matiere>> matiereData = await MatiereService.GetAllMatieres();
                   
                    Matieres = matiereData.Data;
                    
                    listeMatieres.ItemsSource = Matieres;
                    

                });
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
            }
            catch (Exception ex)
            {
                var pes = ex.Message;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
                MessageBox.Show("Echec de connexion au serveur. Impossible d'obtenir la liste des agences. Veuillez re-essayer plus tard.");
            }
        }

        private void searchMatiereAction(object sender, KeyEventArgs e)
        {
            try
            {
                var matieres = Matieres.Where(M => M.Designation.Contains(searchMatiere.Text));
                listeMatieres.ItemsSource = matieres;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite.");
            }
        }

        private void open_create_form(object sender, RoutedEventArgs e)
        {
            create_form.IsOpen = true;
           


        }

        private void cancel_create_form(object sender, RoutedEventArgs e)
        {
            create_form.IsOpen = false;
            Reference.Clear();
            Designation.Clear();
            Origine.Clear();
            PrixUnitaire.Clear();
            DelaisLivraison.Clear();
            Colissage.Clear();

        }

        public void refreshMatier()
        {
            InitializeComponent();
            getData();
        }

        private async void create_matiere_click(object sender, RoutedEventArgs e)
        {
            var matiere = new CreateMatiereDTO
            {
                Reference = Reference.Text,
                Designation = Designation.Text,
                Origine = Origine.Text,
                DelaisAppro=int.Parse(DelaisLivraison.Text),
                Colissage=double.Parse(Colissage.Text),
                PrixUnitaire=double.Parse(PrixUnitaire.Text),
                DateCreation=DateTime.Now,
                DateModification=DateTime.Now,
                
            };

            ResponseObject<Matiere> result = await MatiereService.SaveMatiere(matiere);
            if (result.Status == ResponseStatus.SUCCESSFUL.ToString())
            {
                create_form.IsOpen = false;

                snack_bar_message.Message.Content = result.Message;
                snack_bar_message.IsActive = true;
                snack_bar_message.Background = Brushes.Green;
                refreshMatier();
                for (int i=0;i<500;i++)
                {

                }
                snack_bar_message.IsActive = false;


            }
            else
            {
                create_form.IsOpen = false;
                snack_bar_message.Message.Content = result.Message;
                snack_bar_message.IsActive = true;
                snack_bar_message.Background = Brushes.Red;
                for (int i = 0; i < 50; i++)
                {

                }
                snack_bar_message.IsActive = false;


            }

        }

        private void opent_detail_click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
