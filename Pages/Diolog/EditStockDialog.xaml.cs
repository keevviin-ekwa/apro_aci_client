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
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Diolog
{
    /// <summary>
    /// Logique d'interaction pour EditStockDialog.xaml
    /// </summary>
    public partial class EditStockDialog : Window
    {

        private Stock _stock;
        public EditStockDialog(Stock stock)
        {
            _stock = stock;
         
            InitializeComponent();
            matiere_premiere.Text = stock.Matiere.Designation;
            stock_debut.Text = stock.StockDebut.ToString();
            stock_fin.Text = stock.StockFin.ToString();
            date_stock.Text = stock.Mois;
        }

        private void cancel_edit_stock(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void save_edit_stock(object sender, RoutedEventArgs e)
        {
            var Stock= new UpdateStockDTO
            {
                Id=_stock.Id,
                StockDebut=double.Parse(stock_debut.Text),
                StockFin=double.Parse(stock_fin.Text),
                MatiereId=_stock.MatiereId,
                Mois=_stock.Mois,
                   
            };
            ResponseObject<Stock> response = await StockService.UpdateStock(Stock);

            if (response.Status == ResponseStatus.SUCCESSFUL.ToString())
            {
                MessageBox.Show("Les stocks ont été mise à jour avec succès");
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite");

            }
           
        }
    }
}
