using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Diolog;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sign_Up_Form.Pages.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserwithAccessRigthControl.xaml
    /// </summary>
    public partial class UserwithAccessRigthControl : UserControl
    {


        User _user;
        List<User> userList = new List<User>();
        public UserwithAccessRigthControl(User user)
        {
            InitializeComponent();
            userList.Add(user);
            _user = user;
            listUtilisateurs.ItemsSource= userList;
        }



        public async void refreshUser(User user)
        {
           
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<User> utilisateur = await UserService.GetUserById(user.Id);
                    List<User> listUtilisateur = new List<User>();
                    listUtilisateur.Add(utilisateur.Data);
                    listUtilisateurs.ItemsSource = listUtilisateur;
                });
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
                MessageBox.Show("Impossible d'obtenir les détails sur l'employé. " + ex.Message);
            }

        }


        private void accessRightDialog_DataChanged(object sender, EventArgs e)
        {
            InitializeComponent();
            refreshUser(_user);
        }

        private void remove_acces_rigth_click(object sender, RoutedEventArgs e)
        {
            RemoveAccesRigthDialog rem= new RemoveAccesRigthDialog(_user);
            rem.DataChanged += accessRightDialog_DataChanged;
            rem.ShowDialog();
        }

        private void add_acces_rigth_click(object sender, RoutedEventArgs e)
        {
            AddAccesRigthDialog add= new AddAccesRigthDialog(_user);
            add.DataChanged += accessRightDialog_DataChanged;
            add.ShowDialog();
        }
    }
}
