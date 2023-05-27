using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.Objectifs
{
    /// <summary>
    /// Logique d'interaction pour EditObjectifDialog.xaml
    /// </summary>
    public partial class EditObjectifDialog : Window
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        public EditObjectifDialog(Objectif objectif)
        {
            InitializeComponent();
            Mois.Text = objectif.Mois;
            objectif_grand_format.Text = objectif.ObjectifGF.ToString();
            objectif_petit_format.Text = objectif.ObjectifPF.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void update_objectf(object sender, RoutedEventArgs e)
        {
            var obj = new ObjectifDTO()
            {
                Mois=Mois.Text,
                ObjectifGF=double.Parse(objectif_grand_format.Text),
                ObjectifPF=double.Parse(objectif_petit_format.Text)
            };

            ResponseObject<Objectif> response = await ObjectifService.UpdateObjectif(obj);

            if (response.Status.ToString() == ResponseStatus.SUCCESSFUL.ToString())
            {
                this.Close();
                DataChangedEventHandler handler = DataChanged;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }
                MessageBox.Show(response.Message);

            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private void cancel_update(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
