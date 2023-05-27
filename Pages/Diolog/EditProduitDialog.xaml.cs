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
    /// Logique d'interaction pour EditProduitDialog.xaml
    /// </summary>
    public partial class EditProduitDialog : Window
    {
        Produit _produit;
        public EditProduitDialog(Produit produit)
        {
            InitializeComponent();
            _produit = produit;
            designation.Text = produit.Designation;
        }

        private async void edit_product_save(object sender, RoutedEventArgs e)
        {
            if (designation.Text != null)
            {
                _produit.Designation = designation.Text;

                try
                {
                    ResponseObject<Produit> response = await ProductService.updateProduit(_produit);
                    if(response.Status== ResponseStatus.SUCCESSFUL.ToString())
                    {
                        this.Close();
                        MessageBox.Show("Produit modifié avec succès");
                    }
                    else
                    {
                        MessageBox.Show(response.Message);
                    }
                }
                catch (Exception ex) { 
                
                    MessageBox.Show("Une erreur s'est produite");
                }
            }

        }
    }
}
