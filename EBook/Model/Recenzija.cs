using Cassandra.Mapping;

namespace EBook.Model
{
    [TableName("Recenzija")]
    [PrimaryKey("recenzijaID")]
    public class Recenzija
    {
        public string recenzijaID;
        public string korisnikID;
        public string knjigaID;
        public string komentar;
        public int ocena;
    }
}