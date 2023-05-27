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
using static System.Net.Mime.MediaTypeNames;

namespace Sign_Up_Form.Pages.Diolog
{
    /// <summary>
    /// Logique d'interaction pour CreateUserDialog.xaml
    /// </summary>
    public partial class CreateUserDialog : Window
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;


        public CreateUserDialog()
        {
            InitializeComponent();
        }

       

        private async void Button_save_Click(object sender, RoutedEventArgs e)
        {
            if(user_first_name.Text==null || user_name.Text==null || login==null) {

                MessageBox.Show("Veuillez remplir tous les champs obligatoress");

            }
            else
            {
                User user = new User
                {
                    Nom = user_name.Text,
                    Prenom = user_first_name.Text,
                    Tel = telephone.Text,
                    Fonction = function.Text,
                    Login = login.Text,
                    Email = email.Text,
                    MotDePasse= "aci_lgcca",
                };
                ResponseObject<User> response = await UserService.SaveUser(user);

                if(response.Status.ToString()==ResponseStatus.SUCCESSFUL.ToString())
                {
                    this.Close();
                    DataChangedEventHandler handler = DataChanged;
                    if (handler != null)
                    {
                        handler(this, new EventArgs());
                    }
                    MessageBox.Show(response.Message);
                }
            }

            
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
