using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Cassandra;
using EBook.Model;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBook.Pages
{
    public class IndexModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }
        public IEnumerable<string> kategorije {get; set;}
        public List<Knjiga> najpopularnijeKnjige {get; set;}
        public int brojKnjiga {get; set;}
        public int brojKorisnika {get; set;}
        public int brojRezervacija {get; set;}
        public int brojRecenzija {get; set;}
        public Korisnik bibliotekar1 {get; set;}
        public Korisnik bibliotekar2 {get; set;}
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);

            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                Message = "Welcome " + korisnik.ime;
            }
            
            kategorije = mapper.Fetch<string>("SELECT DISTINCT zanr FROM \"Knjiga\"");

            //SELECT "knjigaID", AVG(ocena) FROM "Recenzija" GROUP BY "knjigaID" LIMIT 5;
            najpopularnijeKnjige = new List<Knjiga>();
            RowSet knjigaOcena = session.Execute("SELECT \"knjigaID\", AVG(ocena) as avgocena, COUNT(*) as brojocena, nazivknjige, zanrknjige, opisknjige FROM \"Recenzija\" GROUP BY \"knjigaID\"");

            foreach(var item in knjigaOcena)
            {
                Knjiga k = new Knjiga();
                k.knjigaID = item["knjigaID"] != null ? item["knjigaID"].ToString() : string.Empty;
                k.ocena = item["avgocena"] != null ? (float)item["avgocena"] : 0;
                k.brojocena = item["brojocena"] != null ? (long)item["brojocena"] : 0;
                k.naziv = item["nazivknjige"] != null ? item["nazivknjige"].ToString() : string.Empty;
                k.zanr = item["zanrknjige"] != null ? item["zanrknjige"].ToString() : string.Empty;
                k.opis = item["opisknjige"] != null ? item["opisknjige"].ToString() : string.Empty;
                najpopularnijeKnjige.Add(k);
            }
            najpopularnijeKnjige.Sort((a, b) => b.CompareTo(a));

            brojKnjiga = mapper.Single<int>("SELECT COUNT(*) FROM \"Knjiga\"");
            brojKorisnika = mapper.Single<int>("SELECT COUNT(*) FROM korisnik");
            brojRezervacija = mapper.Single<int>("SELECT COUNT(*) FROM \"Rezervacija\"");
            brojRecenzija = mapper.Single<int>("SELECT COUNT(*) FROM \"Recenzija\"");

            bibliotekar1 = mapper.FirstOrDefault<Korisnik>("SELECT * FROM korisnik where korisnikid = '7' and email = 'laura.savic@elfak.rs'");
            bibliotekar2 = mapper.FirstOrDefault<Korisnik>("SELECT * FROM korisnik where korisnikid = '8' and email = 'sara.dzesika@outlook.com'");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("email");
            Message = null;
            return RedirectToPage("/Index");
        }
    }
}
