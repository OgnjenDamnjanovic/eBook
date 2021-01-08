using System;
using System.Collections.Generic;
using Cassandra;
using Cassandra.Mapping;

namespace EBook.Model
{
    [TableName("Knjiga")]
    [PrimaryKey("knjigaID")]
    public class Knjiga
    {
        public string knjigaID;
        public string naziv;
        public string autor;
        public string opis;
        public string zanr;
        public string brstrana;
        public string godina;
        public int kolicina; 
        public string pismo;
    }
}