using System;
using Cassandra.Mapping;

namespace EBook.Model
{
    [TableName("Rezervacija")]
    [PrimaryKey("rezervacijaID")]
    public class Rezervacija
    {
        public string rezervacijaID;
        public string korisnikID;
        public string knjigaID;
        public DateTime datum;
        public string status;
        public string nazivknjige;
        public string zanrknjige;
    }
}