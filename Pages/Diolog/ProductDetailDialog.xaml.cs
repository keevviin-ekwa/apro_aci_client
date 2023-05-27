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
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Diolog
{
    /// <summary>
    /// Logique d'interaction pour ProductDetail.xaml
    /// </summary>
    public partial class ProductDetailDialog : Window
    {
        Produit Produit { get; set; }
        List<MatiereProduit> matiereProduits { get; set; }
        public ProductDetailDialog(Produit produit)
        {
            Produit = produit;
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
                    ResponseObject<List<MatiereProduit>> matiereProduitData = await MatiereProduitService.GetAllMatieresByProduct(Produit.id);

                    matiereProduits = matiereProduitData.Data;

                    listeMatieres.ItemsSource = matiereProduits;


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
    }
}
