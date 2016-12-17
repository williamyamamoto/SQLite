using System.Collections.ObjectModel;

namespace aulauwpsqlite
{
    public class ReadAllPessoaList
    {
        DatabaseHelper Db_Helper = new DatabaseHelper();
        public ObservableCollection<Pessoa> GetAllPessoa()
        {
            return Db_Helper.ReadAllPessoa();
        }
    }
}
