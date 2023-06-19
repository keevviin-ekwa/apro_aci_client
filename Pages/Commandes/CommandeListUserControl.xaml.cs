using Sign_Up_Form.Models;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
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

namespace Sign_Up_Form.Pages.Commandes
{
    /// <summary>
    /// Logique d'interaction pour CommandeListUserControl.xaml
    /// </summary>
    public partial class CommandeListUserControl : UserControl
    {

        public List<Commande> CommandesList { get; set; }
        public CommandeListUserControl()
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
                    ResponseObject<List<Commande>> CommandeData = await CommandeService.GetAllCommandes();

                    CommandesList = CommandeData.Data;

                    listeCommandes.ItemsSource = CommandesList;


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

        private async void delete_commande(object sender, RoutedEventArgs e)
        {
            Commande commande = (Commande)((Button)e.Source).DataContext;
            ResponseObject<Commande> response = await CommandeService.deleteCommande(commande.Id);
            if (response.Status.ToString() == ResponseStatus.SUCCESSFUL.ToString()) {

                InitializeComponent();
                getData();
                MessageBox.Show(response.Message);
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }
    }
}
