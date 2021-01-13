using System;
using System.Collections.Generic;
using Cassandra;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace EBook.Model
{
    [TableName("Knjiga")]
    [PrimaryKey("knjigaID")]
    public class Knjiga : IComparable<Knjiga>
    {
        public string knjigaID {get; set;}
        public string naziv {get; set;}
        public string autor {get; set;}
        public string opis {get; set;}
        public string zanr {get; set;}
        public string brstrana {get; set;}
        public string godina {get; set;}
        public int kolicina {get; set;}
        public string pismo {get; set;}
        public string slika {get; set;}
        public string jezik {get; set;}
        
        [IgnoreAttribute]
        public float ocena {get; set;}
        [IgnoreAttribute]
        public long brojocena {get; set;}

        public int CompareTo(Knjiga compareKnjiga)
        {
            // A null value means that this object is greater.
            if (compareKnjiga == null)
                return 1;

            else
                return this.ocena.CompareTo(compareKnjiga.ocena);
        }

        
        public bool Equals(Knjiga other)
        {
            if (other == null) return false;
            return (this.ocena.Equals(other.ocena));
        }
    }
}