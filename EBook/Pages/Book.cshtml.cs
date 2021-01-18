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

namespace EBook.Pages
{
    public class BookModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }
        [BindProperty]
        public int ocena { get; set; }
        [BindProperty]
        public string komentar { get; set; }
        [BindProperty]
        public string idKnjiga { get; set; }
        [BindProperty]
        public string zanr { get; set; }
        [BindProperty]
        public string opis { get; set; }
        [BindProperty]
        public string naziv { get; set; }
        [BindProperty]
        public string emailKorisnika { get; set; }
        [BindProperty]
        public Knjiga knjiga { get; set; }
        public IList<Knjiga> slicneKnjige { get; set; }
        public IList<Recenzija> recenzije { get; set; }
        public IList<string> prosek { get; set; }
        public IList<float> brOcenaPosebno { get; set; }
        public float brojOcena { get; set; }
        public float prosecnaOcena { get; set; }

        public void OnGet(string id, string zanr)
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                if(korisnik.tip==1)
                    Message="Admin";
                else
                    Message="User";    
                //Message = "Welcome " + korisnik.ime;
            }
            knjiga = mapper.First<Knjiga>("select * from \"Knjiga\" where \"knjigaID\"= ? and zanr= ?", id,zanr);
            recenzije = mapper.Fetch<Recenzija>("select * from \"Recenzija\" where \"knjigaID\"= ?", knjiga.knjigaID).ToList<Recenzija>();
            izracunajOcene();
            slicneKnjige = mapper.Fetch<Knjiga>("select * from \"Knjiga\" where zanr=?", zanr).ToList<Knjiga>();          
            for(int j=0;j<slicneKnjige.Count;j++)
            {
                if(slicneKnjige.ElementAt(j).knjigaID.Equals(knjiga.knjigaID))
                    slicneKnjige.RemoveAt(j);
            }
            if(slicneKnjige.Count>4)
            {
                for(int i=slicneKnjige.Count-1;i>3;i--)
                {
                    slicneKnjige.RemoveAt(i);
                }
            } 
        }

        public IActionResult OnPostRez()
        {
            int brojac=0;
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                Knjiga k = mapper.First<Knjiga>("select * from \"Knjiga\" where \"knjigaID\"= ? and zanr= ?", idKnjiga, zanr);
                //mogucnost da admin moze da rezervise neku knjigu za nekog korisnika ukoliko unese korisnikovu mail adresu
                if(korisnik.tip==1)
                {
                    Korisnik kor = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + emailKorisnika + "'");
                    if(kor!=null)
                    {
                        String queryUpdate = $"update \"EBook\".\"Knjiga\" set kolicina = {k.kolicina-1} where \"knjigaID\"='{idKnjiga}' and zanr='{zanr}'";
                        session.Execute(queryUpdate);
                        String query = $"insert into \"EBook\".\"Rezervacija\" (\"rezervacijaID\",\"korisnikID\", \"knjigaID\", datum, status, nazivknjige,zanrknjige) values ('"+Cassandra.TimeUuid.NewId()+ $"','{kor.korisnikID.ToString()}','{idKnjiga}','{(DateTime.Now).ToString("yyyy-MM-dd'T'HH:mm:ssZ")}','Aktivno', '{k.naziv}', '{k.zanr}')";
                        session.Execute(query);
                        return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=AdminTrue").ToString());
                    }
                    return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=AdminFalse").ToString());
                }
                //da izbroji korisnikove rezervacije
                IList<Rezervacija> rezervacije = new List<Rezervacija>();
                rezervacije = mapper.Fetch<Rezervacija>("select * from \"Rezervacija\" where \"korisnikID\"=?", korisnik.korisnikID).ToList<Rezervacija>();
                for(int i = 0;i<rezervacije.Count;i++)
                {
                    if(rezervacije.ElementAt(i).status.Equals("Na cekanju"))
                        brojac++;
                }
                //ako postoji bar jedna knjiga u bazi sa tim imenom i zanrom
                if(k.kolicina>0)
                {
                    //ako korisnik ima vise od 5 aktivnih rezervacija da mu onemoguci dok ne preuzme odredjene rezervacije
                    if(brojac>=5)
                    {
                        return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=false").ToString());
                    }
                    else
                    {
                        String queryUpdate = $"update \"EBook\".\"Knjiga\" set kolicina = {k.kolicina-1} where \"knjigaID\"='{idKnjiga}' and zanr='{zanr}'";
                        session.Execute(queryUpdate);
                        String query = $"insert into \"EBook\".\"Rezervacija\" (\"rezervacijaID\",\"korisnikID\", \"knjigaID\", datum, status, nazivknjige,zanrknjige) values ('"+Cassandra.TimeUuid.NewId()+ $"','{korisnik.korisnikID.ToString()}','{idKnjiga}','{(DateTime.Now).ToString("yyyy-MM-dd'T'HH:mm:ssZ")}','Na cekanju', '{k.naziv}', '{k.zanr}')";
                        session.Execute(query);
                        return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=true").ToString());
                    }
                }
                else
                {
                    return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=out").ToString());
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        public IActionResult OnPostRec()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                if(korisnik.tip==1)
                {
                    return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=admin").ToString());
                }
                Recenzija recenzija = mapper.FirstOrDefault<Recenzija>("select * from \"Recenzija\" where \"knjigaID\"= ? and \"korisnikID\"= ?", idKnjiga,korisnik.korisnikID.ToString());
                if(recenzija==null)
                {
                    String query = $"insert into \"EBook\".\"Recenzija\" (\"recenzijaID\",\"korisnikID\", \"knjigaID\", komentar, ocena,nazivknjige,zanrknjige,opisknjige) values ('"+Cassandra.TimeUuid.NewId()+ $"','{korisnik.korisnikID.ToString()}','{idKnjiga}','{komentar}',{ocena}, '{naziv}', '{zanr}', '{opis}')";
                    session.Execute(query);
                    return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr).ToString());
                }
                else{
                    return Redirect(("/Book?id="+idKnjiga+"&zanr="+zanr+"&success=already").ToString());
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("email");
            Message = null;
            return RedirectToPage("/Index");
        }

        public void izracunajOcene()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);
            float brojac1,brojac2,brojac3,brojac4,brojac5,ocene;
            brojac1=brojac2=brojac3=brojac4=brojac5=ocene=0;
            recenzije = mapper.Fetch<Recenzija>("select * from \"Recenzija\" where \"knjigaID\"= ?", knjiga.knjigaID).ToList<Recenzija>();
            foreach(Recenzija r in recenzije)
            {
                brojOcena++;
                ocene=ocene+r.ocena;
                if(r.ocena==1)
                    brojac1++;
                else if(r.ocena==2)
                    brojac2++;
                else if(r.ocena==3)
                    brojac3++;
                else if(r.ocena==4)
                    brojac4++;
                else
                    brojac5++;
            }
            prosecnaOcena=(float)Math.Round(ocene/brojOcena,2);
            prosek = new List<string>();
            brOcenaPosebno = new List<float>();
            prosek.Add(("width:"+(brojac1/brojOcena)*100+"%").ToString());
            prosek.Add(("width:"+(brojac2/brojOcena)*100+"%").ToString());
            prosek.Add(("width:"+(brojac3/brojOcena)*100+"%").ToString());
            prosek.Add(("width:"+(brojac4/brojOcena)*100+"%").ToString());
            prosek.Add(("width:"+(brojac5/brojOcena)*100+"%").ToString());
            brOcenaPosebno.Add(brojac1);
            brOcenaPosebno.Add(brojac2);
            brOcenaPosebno.Add(brojac3);
            brOcenaPosebno.Add(brojac4);
            brOcenaPosebno.Add(brojac5);
        }
    }
}
