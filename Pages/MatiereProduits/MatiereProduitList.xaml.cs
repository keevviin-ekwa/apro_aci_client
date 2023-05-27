using Sign_Up_Form.DTO;
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

namespace Sign_Up_Form.Pages.MatiereProduits
{
    /// <summary>
    /// Logique d'interaction pour MatiereProduitList.xaml
    /// </summary>
    public partial class MatiereProduitList : UserControl
    {
        List<Produit> Produits;
        List<MatiereProduit> matiereProduits;
       
        public MatiereProduitList()
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
                    
                    Produits = produitData.Data;
                   
                    products_list.ItemsSource = Produits;
                    // products_list.SelectedIndex = 0;
                    ResponseObject<List<Matiere>> matiere = await MatiereService.GetAllMatieres();
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(Produits[0].id);
                    matiereProduits = matiereProduit.Data;
                    listeMatieres.ItemsSource = matiereProduits;
                    product_list.ItemsSource = Produits;
                    matiere_list.ItemsSource = matiere.Data;

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

        private async void selected_item_change(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                var selected_product=(Produit) products_list.SelectedItem;
               
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                   // InitializeComponent();
                    ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(selected_product.id);
                    matiereProduits = matiereProduit.Data;
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

        private void open_create_form(object sender, RoutedEventArgs e)
        {
           
            create_form.IsOpen = true;

        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            create_form.IsOpen = false;
            
        }

        private async void save_click(object sender, RoutedEventArgs e)
        {
           var product= (Produit) product_list.SelectedItem;
           var matiere=(Matiere)matiere_list.SelectedItem;
            var matiereProduit = new MatiereProduitDTO
            {
                ProduitId = product.id,
                MatiereId = matiere.Id,
                contributionMatierePF = double.Parse(contribution_pf.Text, System.Globalization.CultureInfo.InvariantCulture),
                ContributionMatiereGF = double.Parse(contribution_gf.Text, System.Globalization.CultureInfo.InvariantCulture),

            };
            ResponseObject<MatiereProduit> response = await MatiereProduitService.SaveMatiereProduit(matiereProduit);
            if (response.Status.ToString() == ResponseStatus.SUCCESSFUL.ToString())
            {
                create_form.IsOpen = false;
                MessageBox.Show("Enregustrement réussie");
                contribution_gf.Clear();
                contribution_pf.Clear();
            }
            else
            {
                MessageBox.Show("Une érreur s'est produite");
            }
        }

        private async void on_select_product_change(object sender, SelectionChangedEventArgs e)
        {

            if(product_list.SelectedItem != null)
            {
                try
                {
                    var selected_product = (Produit)product_list.SelectedItem;

                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        // InitializeComponent();
                        ResponseObject<List<MatiereProduit>> matiereProduit = await MatiereProduitService.GetAllMatieresByProduct(selected_product.id);
                        ResponseObject<List<Matiere>> matiere = await MatiereService.GetAllMatieres();
                        List<MatiereProduit> list = matiereProduit.Data;
                        List<Matiere> matieres = matiere.Data;
                        List<Matiere> temp = new List<Matiere>();

                        //on parcours la liste de matiere, si une matiere a deja une valeur dans la liste des matieres produit a le saute sinon a l'ajoute a l
                        //liste temporaire
                        foreach (var item in matieres)
                        {
                            var mat = list.Find(m => m.MatiereId == item.Id);
                            if(mat == null)
                            {
                                temp.Add(item); 
                            }
                        }
                        matiere_list.ItemsSource = temp;



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
}
