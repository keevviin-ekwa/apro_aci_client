using MaterialDesignThemes.Wpf;
using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Diolog;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sign_Up_Form.Pages.Produits
{
    /// <summary>
    /// Logique d'interaction pour ProductListControl.xaml
    /// </summary>
    public partial class ProductListControl : UserControl
    {
       List<Produit> Produits;
        List<Marque> Marques;
        public ProductListControl()
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
                    ResponseObject<List<Produit>> produitData = await ProductService.GetAllProduits();
                    ResponseObject<List<Marque>> marqueData = await MarqueService.GetAllMarques();
                    Produits = produitData.Data;
                    Marques = marqueData.Data; 
                    listeProduits.ItemsSource = Produits;
                    Helpers.GlobaProduit = Produits;
                    marque_list.ItemsSource = Marques;
                   
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

        private void searchProduitAction(object sender, KeyEventArgs e)
        {
            try
            {
                var produits = Produits.Where(p => p.Designation.Contains(searchProduit.Text));
                listeProduits.ItemsSource = produits;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite.");
            }
        }

        private void open_dialog(object sender, RoutedEventArgs e)
        {
            create_form.IsOpen = true;
        }

        private void cancel_action_click(object sender, RoutedEventArgs e)
        {
            
            create_form.IsOpen=false;
            
        }
            
        private async void save_product_click(object sender, RoutedEventArgs e)
        {
            var des=desination.Text;
            CreateProductDTO produit=null;
            var marques = marque_list.SelectedItem;
            if (marques != null)
            {
                var m = (Marque)marques;
                var id = m.Id;
                produit = new CreateProductDTO
                {
                    Reference = "",
                    Designation = des,
                    MarqueId = id,
                };
            }
            ResponseObject<Produit> result = await ProductService.SaveProduit(produit);
            if (result.Status =="SUCCESSFUL")
            {
                create_form.IsOpen = false;

                snack_bar_message.Message.Content = result.Message;
                snack_bar_message.IsActive = true;
                snack_bar_message.Background = Brushes.Green;
               
            }


            else
            {
                create_form.IsOpen = false;
                snack_bar_message.Message.Content = result.Message;
                snack_bar_message.IsActive = true;
                snack_bar_message.Background = Brushes.Red;

            }

        }

        private void product_detail_click(object sender, RoutedEventArgs e)
        {
            Produit pro= (Produit)((Button)e.Source).DataContext;
            ProductDetailDialog  productDetailDialog = new ProductDetailDialog(pro);
            productDetailDialog.Show();
           
        }

        private void open_edit_product_dialog(object sender, RoutedEventArgs e)
        {
            Produit pro = (Produit)((Button)e.Source).DataContext;
            EditProduitDialog edit = new EditProduitDialog(pro);
            edit.Show();    
        }
    }
}
