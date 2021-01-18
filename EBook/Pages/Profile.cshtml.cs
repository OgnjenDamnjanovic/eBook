using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EBook.Model;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Http;

namespace EBook.Pages
{
    public class ProfileModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }
        public Korisnik korisnik { get; set; }
        public IList<Rezervacija> rezervacije { get; set; }
        public IList<Knjiga> knjige { get; set; }
        [BindProperty]
        public string password {get;set;}
        [BindProperty]
        public string newPassword {get;set;}

        public void OnGet(string success)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                if(korisnik.tip==1)
                    Message="Admin";
                else
                    Message="User";
                //Message = "Welcome " + korisnik.ime;
                rezervacije = mapper.Fetch<Rezervacija>("select * from \"EBook\".\"Rezervacija\" where \"korisnikID\"=?", korisnik.korisnikID).OrderBy(x=>x.datum).ToList<Rezervacija>();
            } 
        }

        public IActionResult OnPostSacuvaj()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            
            korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
            if(newPassword!=null && !newPassword.Equals(""))
            {
                String query = $"update \"EBook\".korisnik set sifra = '{newPassword}' where email= '" + email + $"' and korisnikid='{korisnik.korisnikID}'";
                session.Execute(query);
            }
            return Redirect("./Login");
        }

        public IActionResult OnPostObrisiRez(string id, string kId)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"delete from \"EBook\".\"Rezervacija\" where \"rezervacijaID\"= '{id}' and \"korisnikID\"='{kId}'";
            session.Execute(query);
            return RedirectToPage();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("email");
            Message = null;
            return RedirectToPage("/Index");
        }
    }
}
