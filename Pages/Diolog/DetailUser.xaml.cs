using Sign_Up_Form.Models;
using Sign_Up_Form.Pages.UserControls;
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
    /// Logique d'interaction pour DetailUser.xaml
    /// </summary>
    public partial class DetailUser : Window
    {
        User user;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;
        public DetailUser(User user)
        {
            this.user = user;
            InitializeComponent();
            UserControl usc = new UpdateUserControl(this.user);
            GridMain.Children.Add(usc);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
             
                case "ItemUpdateEmploye":
                    usc = new UpdateUserControl(this.user);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAccessRight":
                    usc = new UserwithAccessRigthControl(this.user);
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void Click_close_user_detail(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
