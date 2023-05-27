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
    /// Logique d'interaction pour AddAccesRigthDialog.xaml
    /// </summary>
    public partial class AddAccesRigthDialog : Window
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        static StringBuilder logString = new StringBuilder();
        User _user;
        public AddAccesRigthDialog(User user)
        {
            InitializeComponent();
            _user = user;
            getData();
        }


        public async void getData()
        {
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    ResponseObject<List<DroitsAcces>> produitData = await UserService.GetAllDroitsAcces();
                    AccessRightsCBX.ItemsSource = produitData.Data;

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

        //public void  AddAccesRightOnUserDialog(User user)
        //{
        //    InitializeComponent();
        //    _user = user;
        //    getData();
        //}


        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> accessRights = Helpers.convertStringToList(_user.DroitAcces);
                string droitAcces = ((DroitsAcces)AccessRightsCBX.SelectedItem).DesignationTechnique;


                if (accessRights.Contains(droitAcces))
                {
                    MessageBox.Show("Cet utilisateur a déjà ce droit");
                }
                else
                {
                    accessRights.Add(droitAcces);
                    _user.DroitAcces = Helpers.convertListToString(accessRights);
                    ResponseObject<User> response = await UserService.UpdateUser(_user);
                    if (response.Status == Utils.ResponseStatus.SUCCESSFUL.ToString())
                    {
                        MessageBox.Show("Succès de l'opération.");
                        DataChangedEventHandler handler = DataChanged;
                        if (handler != null)
                        {
                            handler(this, new EventArgs());
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Echec de l'opération.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
