using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace aulauwpsqlite
{
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Pessoa> DB_PessoaList = new ObservableCollection<Pessoa>();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += ReadPessoaList_Loaded;
        }
        private void ReadPessoaList_Loaded(object sender, RoutedEventArgs e)
        {
            ReadAllPessoaList dbPessoas = new ReadAllPessoaList();
            DB_PessoaList = dbPessoas.GetAllPessoa();//Get all DB Pessoas  
            if (DB_PessoaList.Count > 0)
            {
                ExcluirPessoasButton.IsEnabled = true;
            }
            listBoxobj.ItemsSource = DB_PessoaList.OrderByDescending(i => i.Id).ToList();//Binding DB data to LISTBOX and Latest Pessoa ID can Display first.  
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                Pessoa listitem = listBoxobj.SelectedItem as Pessoa;//Get slected listbox item Pessoa ID
                Frame.Navigate(typeof(DetailsPage), listitem);
            }
        }

        private void AdicionarPessoaButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPessoa));
        }

        private void ExcluirPessoasButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper delete = new DatabaseHelper();
            delete.DeleteAllPessoa();//delete all DB Pessoas
            DB_PessoaList.Clear();//Clear collections
            ExcluirPessoasButton.IsEnabled = false;
            listBoxobj.ItemsSource = DB_PessoaList;
        }
    }
}
