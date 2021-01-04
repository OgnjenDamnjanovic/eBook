using System.Collections.Generic;
using Cassandra.Mapping;

namespace EBook.Model
{
    [TableName("Korisnik")]
    [PrimaryKey("korisnikID")]
    public class Korisnik
    {
        public string korisnikID;
        public string email;
        
        public string sifra;

        public string ime;
        public string prezime;
        public string brtelefona;
        public string adresa;
        public int tip;
        //public List<string> rezervacije;

    }
}