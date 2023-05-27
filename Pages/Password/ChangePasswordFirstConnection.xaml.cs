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

namespace Sign_Up_Form.Pages.Password
{
    /// <summary>
    /// Logique d'interaction pour ChangePasswordFirstConnection.xaml
    /// </summary>
    public partial class ChangePasswordFirstConnection : Window
    {
        public ChangePasswordFirstConnection()
        {
            InitializeComponent();
        }

        private void BtnCancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnUpdatePassword_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string encryptedPassword = Cipher.Encrypt(NewPasswordPBX.Password, Helpers.PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION);
                if (NewPasswordPBX.Password != ConfirmPasswordPBX.Password)
                {
                    MessageBox.Show("Le nouveau mot de passe et sa confirmation sont différents.");
                }
                else if (NewPasswordPBX.Password == Helpers.DEFAULT_USER_PASSWORD)
                {
                    MessageBox.Show("Le mot de passe " + Helpers.DEFAULT_USER_PASSWORD + " n'est pas autorisé.");
                }
                else
                {
                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        ChangePasswordDTO pwdChangeModel = new ChangePasswordDTO();
                        pwdChangeModel.UserId = Helpers.connectedUserModel.userConnected.Id;
                        pwdChangeModel.NewPassword = NewPasswordPBX.Password;
                        ResponseObject<User> user = await UserService.UpdateUserPassword(pwdChangeModel);
                        if (user.Data.MotDePasse == encryptedPassword)
                        {
                            Helpers.connectedUserModel.userConnected.MotDePasse = encryptedPassword;
                            MessageBox.Show("Succès de modification.");
                           
                            HomePage win2 = new HomePage();
                            win2.Show();
                            this.Close();
                        }
                    });
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Mouse.OverrideCursor = null;
                    });
                }
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
                MessageBox.Show("Echec d'enregistrement. " + ex.Message);
                //logString.Append("Echec de l'enregistrement du nouveau mot de passe." + ex.Message + ". La trace de l'exception est: " + ex.StackTrace);
                //Utils.WriteLog(logString.ToString());
                //logString.Clear();
            }
        }
    }
}
