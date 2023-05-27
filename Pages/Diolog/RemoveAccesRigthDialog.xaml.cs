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
    /// Logique d'interaction pour RemoveAccesRigthDialog.xaml
    /// </summary>
    public partial class RemoveAccesRigthDialog : Window
    {

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        static StringBuilder logString = new StringBuilder();
        User _user;
        public RemoveAccesRigthDialog(User user)

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

        private async void RmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    List<string> accessRights = Helpers.convertStringToList(_user.DroitAcces);
                    string droitAcces = ((DroitsAcces)AccessRightsCBX.SelectedItem).DesignationTechnique;
                    accessRights.Remove(droitAcces);
                    _user.DroitAcces = Helpers.convertListToString(accessRights);
                    ResponseObject<User> response = await UserService.UpdateUser(_user);
                    if (response.Status == ResponseStatus.SUCCESSFUL.ToString())
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
                MessageBox.Show("Echec d'enregistrement. " + ex.Message);
                //logString.Append("Echec du retrait d'un droit d'accès. " + ex.Message + ". La trace de l'exception est: " + ex.StackTrace);
                //Utils.WriteLog(logString.ToString());
                //logString.Clear();
            }


        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
