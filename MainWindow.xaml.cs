using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.Password;
using Sign_Up_Form.Services;
using Sign_Up_Form.Utils;
using System;
using System.Windows;
using System.Windows.Input;

namespace Sign_Up_Form
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        LoginUserDTO userToConnect = new LoginUserDTO();
                        userToConnect.UserName = LoginTXB.Text;
                        userToConnect.Password = PasswordPWBX.Password;

                        if (await UserService.connectUser(userToConnect) != null)
                        {
                            if (Helpers.connectedUserModel.userConnected.IsFirstConnection == true)
                            {
                                ChangePasswordFirstConnection win2 = new ChangePasswordFirstConnection();
                                win2.Show();
                                this.Close();
                            }
                            else
                            {
                                HomePage win2 = new HomePage();
                                win2.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Login ou mot de passe incorrect.");
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
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Echec de connexion au serveur.");
                    //logString.Append("Echec de connexion au serveur." + ex.Message + ". La trace de l'exception est: " + ex.StackTrace);
                    //Utils.WriteLog(logString.ToString());
                    //logString.Clear();
                }
            }
        }

        private async void ButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    LoginUserDTO userToConnect = new LoginUserDTO();
                    userToConnect.UserName = LoginTXB.Text;
                    userToConnect.Password = PasswordPWBX.Password;

                    if (await UserService.connectUser(userToConnect) != null)
                    {
                        if (Helpers.connectedUserModel.userConnected.IsFirstConnection == true)
                        {
                            ChangePasswordFirstConnection win2 = new ChangePasswordFirstConnection();
                            win2.Show();
                            this.Close();
                        }
                        else
                        {
                            HomePage win2 = new HomePage();
                            win2.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login ou mot de passe incorrect");
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
                MessageBox.Show("Echec de connexion au serveur.");
            }
        }
    }
}