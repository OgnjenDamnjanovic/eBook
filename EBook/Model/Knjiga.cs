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
        public String knjigaID;
        public String naziv;
        public String autor;
        public String opis;
        public String zanr;
        public String brstrana;
        public String godina;
        public int kolicina; 
        
    }
}