using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace aulauwpsqlite
{
    class DatabaseHelper
    {
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = 
                    new SQLite.Net.SQLiteConnection
                    (new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), 
                    DB_PATH))
                {
                    conn.CreateTable<Pessoa>();
                }
            }
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = 
await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync
(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Insert(Pessoa objPessoa)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objPessoa);
                });
            }
        }

        public Pessoa ReadPessoa(int PessoaId)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingPessoa = conn.Query<Pessoa>("select * from Pessoa where Id =" + PessoaId).FirstOrDefault();
                return existingPessoa;
            }
        }

        public ObservableCollection<Pessoa> ReadAllPessoa()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Pessoa> myCollection = conn.Table<Pessoa>().ToList<Pessoa>();
                    ObservableCollection<Pessoa> PessoaList = new ObservableCollection<Pessoa>(myCollection);
                    return PessoaList;
                }
            }
            catch
            {
                return null;
            }

        }

        public void UpdateDetails(Pessoa ObjPessoa)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingPessoa = conn.Query<Pessoa>
                    ("select * from Pessoa where Id =" + ObjPessoa.Id).FirstOrDefault();
                if (existingPessoa != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjPessoa);
                    });
                }

            }
        }

        public void DeleteAllPessoa()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.DropTable<Pessoa>();
                conn.CreateTable<Pessoa>();
                conn.Dispose();
                conn.Close();

            }
        }

        public void DeletePessoa(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingPessoa = conn.Query<Pessoa>
                ("select * from Pessoa where Id =" + Id).FirstOrDefault();
                if (existingPessoa != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingPessoa);
                    });
                }
            }
        }
    }
}