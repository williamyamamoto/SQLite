using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace aulauwpsqlite
{
    public sealed partial class AddPessoa : Page
    {
        public AddPessoa()
        {
            this.InitializeComponent();
        }
        private async void AdicionarPessoaButton_Click(object sender, 
            RoutedEventArgs e)
        {
            DatabaseHelper Db_Helper = new DatabaseHelper();
            if (NomeTextBox.Text != "" & FoneTextBox.Text != "")
            {
                Db_Helper.Insert(new Pessoa(NomeTextBox.Text, 
                    FoneTextBox.Text));
                Frame.Navigate(typeof(MainPage));    
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog
                    ("Prencher os campos");    
                await messageDialog.ShowAsync();
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
