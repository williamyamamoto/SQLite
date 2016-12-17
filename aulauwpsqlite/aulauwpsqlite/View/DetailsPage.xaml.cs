using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace aulauwpsqlite
{
    public sealed partial class DetailsPage : Page
    {
        DatabaseHelper Db_Helper = new DatabaseHelper();
        Pessoa currentPessoa = new Pessoa();
        public DetailsPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentPessoa = e.Parameter as Pessoa;
            NomeTextBox.Text = currentPessoa.Nome;  
            FoneTextBox.Text = currentPessoa.Fone;  
        }
        private void AlterarPessoaButton_Click(object sender, 
            RoutedEventArgs e)
        {
            currentPessoa.Nome = NomeTextBox.Text;
            currentPessoa.Fone = FoneTextBox.Text;
            Db_Helper.UpdateDetails(currentPessoa); 
            Frame.Navigate(typeof(MainPage));
        }
        private void ExcluirPessoaButton_Click(object sender, 
            RoutedEventArgs e)
        {
            Db_Helper.DeletePessoa(currentPessoa.Id);  
            Frame.Navigate(typeof(MainPage));
        }
        private void CancelarButton_Click(object sender, 
            RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
