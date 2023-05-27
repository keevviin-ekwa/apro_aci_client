using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.MatiereProduits;
using Sign_Up_Form.Pages.Matieres;
using Sign_Up_Form.Pages.Objectifs;
using Sign_Up_Form.Pages.Produits;
using Sign_Up_Form.Pages.Stocks;
using Sign_Up_Form.Pages.UserControls;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;

namespace Sign_Up_Form
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {

      
        public HomePage()
        {
            InitializeComponent();
            getData();
            UserControl userControl = new UserListControl();
            
            
         Main.Children.Add(userControl);
        }


        public async void getData()
        {
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Produit>> produitData = await ProductService.GetAllProduits();
                  
                 
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





        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void DashBoard_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl dashboard= new DashbordControl();
            Main.Children.Add(dashboard);
 
        }

        private void Previson_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl prevision = new PrevisionControl();
            Main.Children.Add(prevision);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl user = new UserListControl();
            Main.Children.Add(user);
        }

        private void Product_click(object sender, RoutedEventArgs e)
        {

            Main.Children.Clear();
            UserControl Product = new ProductListControl();
            Main.Children.Add(Product);
        }

        private void matiere_btn_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl matiere = new MatiereListControl();
            Main.Children.Add(matiere);
        }

        private void nomenclature_btn_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl matiereProduit = new MatiereProduitList();
            Main.Children.Add(matiereProduit);
        }

        private void stcok_btn_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl stock = new StockListUserControl();
            Main.Children.Add(stock);
        }

        private void Objectif_click(object sender, RoutedEventArgs e)
        {
            Main.Children.Clear();
            UserControl objectif = new ObjectifListControl();
            Main.Children.Add(objectif);
        }
    }

    
}

