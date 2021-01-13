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
        public string grad;
        public string adresa;
        public int tip;

        public Korisnik(string newId, string newEmail, string newPassword, string newFirstName, string newLastName, string newPhoneNumber, string newCity, string newAddress, int newTip)
        {
            this.korisnikID = newId;
            this.tip = newTip;
            this.email = newEmail;
            this.sifra = newPassword;
            this.grad = newCity;
            this.adresa = newAddress;
            this.brtelefona = newPhoneNumber;
            this.ime = newFirstName;
            this.prezime = newLastName;
        }

    }
}