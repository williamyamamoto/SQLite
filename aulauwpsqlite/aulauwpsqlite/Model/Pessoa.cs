using System;

namespace aulauwpsqlite
{
    public class Pessoa
    {
        [SQLite.Net.Attributes.PrimaryKey, 
            SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string DataCriacao { get; set; }

        public Pessoa()
        {
            //construtor  
        }
        public Pessoa(string nome, string fone)
        {
            Nome = nome;
            Fone = fone;
            DataCriacao = DateTime.Now.ToString();
        }
    }
}
