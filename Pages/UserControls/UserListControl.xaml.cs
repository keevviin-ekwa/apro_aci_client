using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Diolog;
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

namespace Sign_Up_Form.Pages.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserListControl.xaml
    /// Logique d'interaction pour UserListControl.xaml
    /// </summary>
    public partial class UserListControl : UserControl
    {


        public UserListControl()
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
                    ResponseObject<List<User>> userData = await UserService.GetAllUser();
                    listeUtilisateurs.ItemsSource = userData.Data;
                  

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

        private void user_detail_click(object sender, RoutedEventArgs e)
        {
            User user = (User)((Button)e.Source).DataContext;
            DetailUser detailUser = new DetailUser(user);
            detailUser.DataChanged += updateDialog_DataChanged;
            detailUser.Show();  
            
            
        }

        private void updateDialog_DataChanged(object sender, EventArgs e)
        {
            RefreshUser();
        }

        public void RefreshUser()
        {
            InitializeComponent();
            getData();
        }

        //private void open_change_password_form_click(object sender, RoutedEventArgs e)
        //{
        //    User user = (User)((Button)e.Source).DataContext;
        //    ChangePasswordDialog changePasswordDialog = new ChangePasswordDialog(user);
        //    changePasswordDialog.Show();
        //}

        private void open_create_user_form(object sender, RoutedEventArgs e)
        {
            CreateUserDialog createUserDialog = new CreateUserDialog();
            createUserDialog.DataChanged += updateDialog_DataChanged;
            createUserDialog.Show();
        }

        private async void Click_reset_password(object sender, RoutedEventArgs e)
        {
            User user = (User)((Button)e.Source).DataContext;
            user.MotDePasse = "aci_lgcca";
            user.IsFirstConnection = true;
            user.DateDerniereMaj = DateTime.Now;
            ResponseObject<User> response= await UserService.UpdateUser(user);
            if(response.Status == ResponseStatus.SUCCESSFUL.ToString())
            {
                MessageBox.Show("Mot de passe Reinitialisé");

            }
            else
            {
                MessageBox.Show("Une erreur s'est produite");
            }
        }

       
    }
}
