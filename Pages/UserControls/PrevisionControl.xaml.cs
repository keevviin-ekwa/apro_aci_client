using Sign_Up_Form.Models;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace Sign_Up_Form.Pages.UserControls
{
    /// <summary>
    /// Logique d'interaction pour PrevisionControl.xaml
    /// </summary>
    public partial class PrevisionControl : UserControl
    {
        List<Stock> Stocks;
        List<Produit> Produits;
        List<Matiere> Matieres;
        Objectif objectif;
        public PrevisionControl()
        {
            InitializeComponent();
            month.SelectedDate= DateTime.Today;
            getData();
           
           
        }



       

        public async void getData()
        {
            try
            {
                var CurrentMonth = DateTime.Now.ToString("MM/yyyy");
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(CurrentMonth);
                    ResponseObject<List<Matiere>> matiereData = await MatiereService.GetAllMatieres();
                    ResponseObject<List<Produit>> produitData = await ProductService.GetAllProduits();
                   
                    product_list.ItemsSource = produitData.Data;
                    product_list.SelectedIndex = 0;
                    var selectedProduct=(Produit) product_list.SelectedItem;
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(26);
                    ResponseObject<Objectif> objectifData = await ObjectifService.GetOBjectifByMonth(CurrentMonth);
                    Produits = produitData.Data;
                    objectif= objectifData.Data;
                    Stocks=stockData.Data;

                    var ListeDeStock = new List<Stock>();
                    foreach (var stock in Stocks)
                    {

                        ResponseObject<Matiere> matiere = await MatiereService.GetMatiereById(stock.MatiereId);
                        var mat = matiere.Data;
                        var Commande = mat.Commandes.FirstOrDefault(c => c.DateCommande.ToString("MM/yyyy") == DateTime.Now.ToString("MM/yyyy"));
                        var Livraison = mat.Commandes.FirstOrDefault(c => c.DateLivraison.ToString("MM/yyyy") == DateTime.Now.ToString("MM/yyyy"));
                        //stock.Livraison = Livraison != null ? Livraison.Quantite : 0;
                        stock.Commande = Commande != null ? Commande.SemaineCommande : 0;
                        ListeDeStock.Add(stock);
                    }
                    list_produit.ItemsSource = ListeDeStock;
                    
                 
                    petit_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifPF.ToString()+" Tonnes";
                    grand_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifGF.ToString()+" Tonnes";
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


        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var selectedProduct = (Produit)product_list.SelectedItem;
            var mois = month.SelectedDate.Value;
            var nextMonth = mois.AddMonths(1).ToString("MM/yyyy");
            month.SelectedDate = mois.AddMonths(1);
            day.Text = mois.AddMonths(1).ToString("Y", new CultureInfo("fr-FR"));
            try
            {
                var selectedMonth = mois.ToString("MM/yyyy");
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(nextMonth);
                    ResponseObject<Objectif> objectifData = await ObjectifService.GetOBjectifByMonth(nextMonth);
                    ResponseObject<List<Matiere>> matiereData = await MatiereService.GetAllMatieres();
                    ResponseObject<List<Produit>> produitData = await ProductService.GetAllProduits();
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(selectedProduct.id);
                    List<Stock> stockList = new List<Stock>();
                    Produits = produitData.Data;
                    objectif = objectifData.Data;
                    Stocks = stockData.Data;

                    var ListeDeStock = new List<Stock>();
                    foreach (var stock in Stocks)
                    {
                        ResponseObject<Matiere> matiere = await MatiereService.GetMatiereById(stock.MatiereId);
                        var mat = matiere.Data;
                        var Commande = mat.Commandes.FirstOrDefault(c => c.DateCommande.ToString("MM/yyyy") == mois.AddMonths(1).ToString("MM/yyyy"));
                        var Livraison = mat.Commandes.FirstOrDefault(c => c.DateLivraison.ToString("MM/yyyy") == mois.AddMonths(1).ToString("MM/yyyy"));
                        stock.Livraison = Livraison != null ? Livraison.Quantite : 0;
                       // stock.Commande = Commande != null ? Commande.SemaineCommande : 0;
                        ListeDeStock.Add(stock);
                    }

                    list_produit.ItemsSource =ListeDeStock;
                    objectif = objectifData.Data;
                    petit_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifPF.ToString() + " Tonnes";
                    grand_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifGF.ToString() + " Tonnes";

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

        private async void previous_btn_click(object sender, RoutedEventArgs e)
        {

            var selectedProduct = (Produit)product_list.SelectedItem;
            var mois = month.SelectedDate.Value;
            var nextMonth = mois.AddMonths(-1).ToString("MM/yyyy");
            month.SelectedDate = mois.AddMonths(-1);
            day.Text = mois.AddMonths(-1).ToString("Y", new CultureInfo("fr-FR"));


            try
            {
                var selectedMonth = mois.ToString("MM/yyyy");
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(nextMonth);
                    ResponseObject<Objectif> objectifData = await ObjectifService.GetOBjectifByMonth(nextMonth);
                    ResponseObject<List<Matiere>> matiereData = await MatiereService.GetAllMatieres();
                    ResponseObject<List<Produit>> produitData = await ProductService.GetAllProduits();
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(selectedProduct.id);

                    Produits = produitData.Data;
                    objectif = objectifData.Data;
                    Stocks = stockData.Data;
                   

                    var ListeDeStock = new List<Stock>();
                    foreach(var stock in Stocks) {

                        ResponseObject<Matiere> matiere = await MatiereService.GetMatiereById(stock.MatiereId);
                        var mat = matiere.Data;
                        var Commande = mat.Commandes.FirstOrDefault(c => c.DateCommande.ToString("MM/yyyy") == mois.AddMonths(-1).ToString("MM/yyyy"));
                        var Livraison = mat.Commandes.FirstOrDefault(c => c.DateLivraison.ToString("MM/yyyy") == mois.AddMonths(-1).ToString("MM/yyyy"));
                       // stock.Livraison = Livraison != null ? Livraison.Quantite : 0;
                        stock.Commande = Commande != null ? Commande.SemaineCommande : 0;
                        ListeDeStock.Add(stock);
                    }


                    objectif = objectifData.Data;
                    petit_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifPF.ToString() + " Tonnes";
                    grand_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifGF.ToString() + " Tonnes";
                    list_produit.ItemsSource = ListeDeStock;

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

        private async void filter_click(object sender, RoutedEventArgs e)
        {
            var mois = month.SelectedDate.Value;
            var Produit = (Produit)product_list.SelectedItem;

            try
            {
                var selectedMonth = mois.ToString("MM/yyyy");
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(selectedMonth);
                    ResponseObject<Objectif> objectifData = await ObjectifService.GetOBjectifByMonth(selectedMonth);
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(Produit.id);
                    objectif = objectifData.Data;
                    Stocks = stockData.Data;
                    Stocks.ForEach(s =>
                    {
                        

                    });
                    list_produit.ItemsSource = stockData.Data;
                    objectif = objectifData.Data;
                    petit_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifPF.ToString() + " Tonnes";
                    grand_format.Text = objectif == null ? 0.ToString() : objectif.ObjectifGF.ToString() + " Tonnes";

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
