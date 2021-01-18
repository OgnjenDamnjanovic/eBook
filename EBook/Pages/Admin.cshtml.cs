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
    public class AdminModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }
        public IList<Rezervacija> rezervacije { get; set; }
        public IList<Knjiga> knjige { get; set; }
        public IList<Recenzija> recenzije { get; set; }

        public void OnGet(string success)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                if(korisnik.tip==1)
                {
                    Message="Admin";
                    rezervacije = mapper.Fetch<Rezervacija>("select * from \"Rezervacija\"").OrderBy(x=>x.datum).ToList<Rezervacija>();
                    knjige = mapper.Fetch<Knjiga>("select * from \"Knjiga\"").OrderBy(x=>x.zanr).ToList<Knjiga>();
                    recenzije = mapper.Fetch<Recenzija>("select * from \"Recenzija\"").ToList<Recenzija>();
                }
                else
                    Message="User"; 
            }
        }

        public IActionResult OnPostObrisiRez(string id, string kId)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"delete from \"EBook\".\"Rezervacija\" where \"rezervacijaID\"= '{id}' and \"korisnikID\"='{kId}'";
            session.Execute(query);
            return Redirect("/Admin?success=dRez");
        }

        public IActionResult OnPostObrisiRec(string id, string kId)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"delete from \"EBook\".\"Recenzija\" where \"knjigaID\"= '{id}' and \"korisnikID\"='{kId}'";
            session.Execute(query);
            return Redirect("/Admin?success=dRec");
        }

        public IActionResult OnPostObrisiKnjigu(string id, string zanr)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"delete from \"EBook\".\"Knjiga\" where \"knjigaID\"= '{id}' and \"zanr\"='{zanr}'";
            session.Execute(query);
            return Redirect("/Admin?success=dBook");
        }

        public IActionResult OnPostPovecaj(string id, string zanr)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            Knjiga k = mapper.First<Knjiga>("select * from \"Knjiga\" where \"knjigaID\"= ? and zanr= ?", id, zanr);
            String query = $"update \"EBook\".\"Knjiga\" set kolicina = {k.kolicina+1} where \"knjigaID\"='{id}' and zanr='{zanr}'";
            session.Execute(query);
            return Redirect("/Admin?success=plus");
        }

        public IActionResult OnPostSmanji(string id, string zanr)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            Knjiga k = mapper.First<Knjiga>("select * from \"Knjiga\" where \"knjigaID\"= ? and zanr= ?", id, zanr);
            String query = $"update \"EBook\".\"Knjiga\" set kolicina = {k.kolicina-1} where \"knjigaID\"='{id}' and zanr='{zanr}'";
            session.Execute(query);
            return Redirect("/Admin?success=minus");
        }

        public IActionResult OnPostStatusAktivno(string id, string kId)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"update \"EBook\".\"Rezervacija\" set status='Aktivno' where \"rezervacijaID\"= '{id}' and \"korisnikID\"='{kId}'";
            session.Execute(query);
            return Redirect("/Admin?success=sAktivno");
        }

        public IActionResult OnPostStatusRealizovano(string id, string kId)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String query = $"update \"EBook\".\"Rezervacija\" set status='Realizovano' where \"rezervacijaID\"= '{id}' and \"korisnikID\"='{kId}'";
            session.Execute(query);
            return Redirect("/Admin?success=sRealizovano");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("email");
            Message = null;
            return RedirectToPage("/Index");
        }
    }
}
