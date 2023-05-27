using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Objectifs
{
    /// <summary>
    /// Logique d'interaction pour ObjectifListControl.xaml
    /// </summary>
    public partial class ObjectifListControl : UserControl
    {
        List<Objectif> Objectifs;



        public ObjectifListControl()
        {
            InitializeComponent();
            produit_list.ItemsSource = Helpers.GlobaProduit;
            getData();
        }

        public async void getData()
        {
            try
            {

                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<Objectif>> objectifData = await ObjectifService.GetAllOBjectif();

                    Objectifs = objectifData.Data;
                    liste_objectif.ItemsSource = Objectifs;

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

        private void open_create_goal_form(object sender, RoutedEventArgs e)
        {
            create_goal_form.IsOpen = true;
        }

        private async void save_goal(object sender, RoutedEventArgs e)
        {


            if (objectif_grand_format.Text == null || objectif_petit_format.Text == null || produit_list.SelectedItem == null)
            {
                MessageBox.Show("Remplissez tous les champs");
            }


            Produit produit = (Produit)produit_list.SelectedItem;

            if (reprendre_objectif.IsChecked == true)
            {
                var LastdayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
                var numberOfMonht = ((LastdayOfYear.Year - date_goal.SelectedDate.Value.Year) * 12) + LastdayOfYear.Month - date_goal.SelectedDate.Value.Month;
                var currentMonthNumber = int.Parse(date_goal.SelectedDate.Value.ToString("MM"));
                var currentMonth = date_goal.SelectedDate.Value;
                var i = currentMonthNumber;
                do
                {

                    var _Objectif = new ObjectifDTO()
                    {

                        ObjectifGF = double.Parse(objectif_grand_format.Text),
                        ObjectifPF = double.Parse(objectif_petit_format.Text),
                        ProduitId = produit.id,
                        Mois = currentMonth.ToString("MM/yyyy"),
                    };

                    currentMonth = currentMonth.AddMonths(1);
                   


                    ResponseObject<Objectif> _response = await ObjectifService.SaveObjectif(_Objectif);

                    if (currentMonthNumber==12 && _response.Status == ResponseStatus.SUCCESSFUL.ToString())
                    {
                        objectif_grand_format.Clear();
                        objectif_petit_format.Clear();
                        create_goal_form.IsOpen = false;
                        InitializeComponent();
                        getData();
                        MessageBox.Show(_response.Message);
                    }

                    if(currentMonthNumber==12 && _response.Status == ResponseStatus.FAILED.ToString())
                    {
                        MessageBox.Show(_response.Message);
                    }

                    currentMonthNumber++;
                } while (currentMonthNumber <= 12);

            }
            else
            {


                var Objectif = new ObjectifDTO()
                {

                    ObjectifGF = double.Parse(objectif_grand_format.Text),
                    ObjectifPF = double.Parse(objectif_petit_format.Text),
                    ProduitId = produit.id,
                    Mois = date_goal.SelectedDate.Value.ToString("MM/yyyy"),
                };

                ResponseObject<Objectif> response = await ObjectifService.SaveObjectif(Objectif);
                if (response.Status == ResponseStatus.SUCCESSFUL.ToString())
                {
                    objectif_grand_format.Clear();
                    objectif_petit_format.Clear();
                    create_goal_form.IsOpen = false;
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

        public void RefreshObjectif()
        {
            InitializeComponent();
            getData();
        }

        private void updateDialog_DataChanged(object sender, EventArgs e)
        {
            RefreshObjectif();
        }

        private void cancel_save_goal(object sender, RoutedEventArgs e)
        {

            objectif_grand_format.Clear();
            objectif_petit_format.Clear();
            create_goal_form.IsOpen = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void edit_objectif(object sender, RoutedEventArgs e)
        {
            Objectif objectif = (Objectif)((Button)e.Source).DataContext;
            EditObjectifDialog editObjectif = new EditObjectifDialog(objectif);
            editObjectif.DataChanged += updateDialog_DataChanged;

            editObjectif.Show();
        }


    }
}
