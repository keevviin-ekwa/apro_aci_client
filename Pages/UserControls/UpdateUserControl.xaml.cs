using Sign_Up_Form.Models;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign_Up_Form.Pages.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UpdateUserControl.xaml
    /// </summary>
    public partial class UpdateUserControl : UserControl
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        User user;
        public UpdateUserControl(User user)
        {
            this.user = user;
            InitializeComponent();
            user_name.Text = user.Nom;
            user_first_name.Text = user.Prenom;
            telephone.Text = user.Tel;
            function.Text=user.Fonction;
            email.Text=user.Email;
            login.Text=user.Login;
            

        }

      

        private async void Click_update_user(object sender, RoutedEventArgs e)
        {
            if (user_first_name.Text == null || user_name.Text == null || login == null)
            {

                MessageBox.Show("Veuillez remplir tous les champs obligatoress");

            }
            else
            {
                User user = new User
                {
                    Id=this.user.Id,
                    Nom = user_name.Text,
                    Prenom = user_first_name.Text,
                    Tel = telephone.Text,
                    Fonction = function.Text,
                    Login = login.Text,
                    Email = email.Text,
                    MotDePasse = "aci_lgcca",
                };

                ResponseObject<User> response = await UserService.UpdateUser(user);

                if (response.Status.ToString() == ResponseStatus.SUCCESSFUL.ToString())
                {


                    DataChangedEventHandler handler = DataChanged;
                    if (handler != null)
                    {
                        handler(this, new EventArgs());
                    }

                    MessageBox.Show(response.Message);
                }
            }
        }
    }
}
