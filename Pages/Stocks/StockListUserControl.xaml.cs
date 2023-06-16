using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Diolog;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Stocks
{
    /// <summary>
    /// Logique d'interaction pour StockListUserControl.xaml
    /// </summary>
    public partial class StockListUserControl : UserControl
    {
        List<Stock> Stocks;
        List<Stock> _stocks;
        List<Matiere> Matieres;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        public StockListUserControl()
        {
            InitializeComponent();
           // date_stock.SelectedDate = DateTime.Now;
            getData();
        }


        public void RefreshStock()
        {
            InitializeComponent();
            getData();
        }

        public async void getData()
        {
            try
            {
                var CurrentMonth= DateTime.Now.ToString("MM/yyyy");
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(CurrentMonth);
                    ResponseObject<List<Matiere>> matiereData = await MatiereService.GetAllMatieres();
                    Matieres = matiereData.Data;
                    Stocks = stockData.Data; //pour la listview
                    _stocks = stockData.Data;// pour la liste au niveau de l'ajout
                    listeStock.ItemsSource = Stocks;
                    matiere_list.ItemsSource = Matieres;


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

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
            {
                Regex regex = new Regex("[^0-9,-]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        private async void on_selected_date_change(object sender, SelectionChangedEventArgs e)
        {
            if(date.SelectedDate != null)
            {
                var Date =  (DateTime)date.SelectedDate;
                var Month = Date.ToString("MM/yyyy");

                try
                {

                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(Month);
                       
                        Stocks = stockData.Data;
                        
                        listeStock.ItemsSource = Stocks;
                       
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

        private void open_stock_form(object sender, RoutedEventArgs e)
        {
            var CurrentDate= new DateTime(DateTime.Now.Year,1,1);
            date_stock.SelectedDate = CurrentDate;
            date_stock.IsEnabled = false;
            create_stock_form.IsOpen = true;
        }

        private void close_stock_form(object sender, RoutedEventArgs e)
        {
            create_stock_form.IsOpen = false;
        }

   

        //private async void on_selected_stock_date_change(object sender, SelectionChangedEventArgs e)
        //{

        
        //    if (date_stock.SelectedDate != null)
        //    {
        //        var Date = (DateTime)date_stock.SelectedDate;
        //        var Month = Date.ToString("MM/yyyy");

        //        try
        //        {

        //            await Application.Current.Dispatcher.Invoke(async () =>
        //            {
        //                Mouse.OverrideCursor = Cursors.Wait;
        //                ResponseObject<List<Stock>> stockData = await StockService.GetAllStock(Month);
        //                _stocks = stockData.Data;
        //                List<Matiere> temp = new List<Matiere>();
        //                foreach (var matiere in Matieres)
        //                {
        //                  var mat= _stocks.Find(s=>s.MatiereId==matiere.Id);
        //                    if(mat == null)
        //                    {
        //                        temp.Add(matiere);
        //                    }
        //                }

        //                matiere_list.ItemsSource=temp;
        //            });
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                Mouse.OverrideCursor = null;
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            var pes = ex.Message;
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                Mouse.OverrideCursor = null;
        //            });
        //            MessageBox.Show("Echec de connexion au serveur. Impossible d'obtenir la liste des agences. Veuillez re-essayer plus tard.");
        //        }
        //    }
        //}

      

        private async void Save_stock(object sender, RoutedEventArgs e)
        {
          
           
            var matiere =(Matiere) matiere_list.SelectedItem;
            ResponseObject<List<Produit>> re = await ProductService.GetAllProduits();
            List<Produit> produit_ = re.Data;
           

            var StockDTO = new StockDTO
            {
                Mois = "",
                MatiereId=matiere.Id,
                StockDebut=double.Parse(stock_debut.Text),
                date = new DateTime(DateTime.Now.Year, 1, 1),

            };

            ResponseObject<Stock> response= await StockService.SaveStock(StockDTO);

            if (response.Status.ToString() == ResponseStatus.SUCCESSFUL.ToString())
            {
                create_stock_form.IsOpen = false;
                RefreshStock();
                stock_debut.Clear();
             
                MessageBox.Show(response.Message);

            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private void edit_stock_click(object sender, RoutedEventArgs e)
        {
            Stock stock= (Stock)((Button)e.Source).DataContext;
            EditStockDialog EditStock = new EditStockDialog(stock);
            EditStock.ShowDialog();
        }
    }
}
