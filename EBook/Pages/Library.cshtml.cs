using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using EBook.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Http;

namespace MyApp.Namespace
{
    public class LibraryModel : PageModel
    {
        [BindProperty]
        public string SearchBook { get; set; }
        public string Message {get; set;}
        public string zanrFilter { get; set; }
        public List<string> zanrovi { get; set; }
        public IList<Knjiga> sveKnjige { get; set; }
        private IWebHostEnvironment  _environment;
        public LibraryModel(IWebHostEnvironment env)
        {
            sveKnjige=new List<Knjiga>();
            zanrovi=new List<string>();
            _environment=env;
        }

        public  void OnGet(string zanr)
        {   
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);

            String email = HttpContext.Session.GetString("email");
            if(!String.IsNullOrEmpty(email))
            {
                Korisnik korisnik = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + email + "'");
                Message = "Welcome " + korisnik.ime;
            }
            
            
            zanrFilter=zanr;
            HttpContext.Session.Remove("pagingState");
            
            foreach (var zanrRaw in session.Execute("SELECT DISTINCT zanr FROM \"Knjiga\";"))
            {
                zanrovi.Add(zanrRaw["zanr"].ToString());
         
            }

            RowSet knjigeRaw;
            if(String.IsNullOrEmpty(zanr)|!zanrovi.Contains(zanr))
            {//all
             var ps = session.Prepare("SELECT * from \"Knjiga\" ;");
             var statement=ps.Bind().SetAutoPage(false).SetPageSize(10);
              knjigeRaw=session.Execute(statement);
             var pagingState=knjigeRaw.PagingState;
                if(pagingState!=null)
                HttpContext.Session.Set("pagingState",pagingState);
            }
            else
            {
             var ps = session.Prepare("SELECT * from \"Knjiga\" where zanr='"+zanr+"';");
             var statement=ps.Bind().SetAutoPage(false).SetPageSize(5);
             knjigeRaw=session.Execute(statement);
             var pagingState=knjigeRaw.PagingState;
              if(pagingState!=null)
             HttpContext.Session.Set("pagingState",pagingState);
            }

               
               
           
   


            foreach(var knjigaRaw in knjigeRaw)
            {
                Knjiga knjiga = new Knjiga();
                knjiga.autor = knjigaRaw["autor"] != null ? knjigaRaw["autor"].ToString() : string.Empty;
                knjiga.brstrana = knjigaRaw["brstrana"] != null ? knjigaRaw["brstrana"].ToString() : string.Empty;
                knjiga.godina = knjigaRaw["godina"] != null ? knjigaRaw["godina"].ToString() : string.Empty;
                knjiga.knjigaID = knjigaRaw["knjigaID"].ToString();
                knjiga.kolicina = knjigaRaw["kolicina"] != null ? Int32.Parse( knjigaRaw["kolicina"].ToString()) : 0;
                knjiga.naziv = knjigaRaw["naziv"] != null ? knjigaRaw["naziv"].ToString() : string.Empty;
                knjiga.opis = knjigaRaw["opis"] != null ? knjigaRaw["opis"].ToString() : string.Empty;
                knjiga.pismo = knjigaRaw["pismo"] != null ? knjigaRaw["pismo"].ToString() : string.Empty;
                knjiga.slika = knjigaRaw["slika"] != null ? knjigaRaw["slika"].ToString() : string.Empty;
                knjiga.zanr = knjigaRaw["zanr"] != null ? knjigaRaw["zanr"].ToString() : string.Empty;
                knjiga.jezik = knjigaRaw["jezik"] != null ? knjigaRaw["jezik"].ToString() : string.Empty;
             
                sveKnjige.Add(knjiga);
             
        }
        
         }
         public JsonResult OnGetLoadMore(string selektovaniZanr)
         {
             byte[] pagingState;
             bool gotPagingState=HttpContext.Session.TryGetValue("pagingState",out pagingState);
                if(!gotPagingState)
                {
                    return new JsonResult("nemaVise");
                }
            
             Cassandra.ISession session = SessionManager.GetSession();
             RowSet knjigeRaw;
             if(selektovaniZanr.CompareTo("All")==0)
             {
             var ps = session.Prepare("SELECT * from \"Knjiga\" ;");
             var statement=ps.Bind().SetAutoPage(false).SetPageSize(7).SetPagingState(pagingState);
              knjigeRaw=session.Execute(statement);
              pagingState=knjigeRaw.PagingState;
                if(pagingState!=null)
                HttpContext.Session.Set("pagingState",pagingState);
                else
                HttpContext.Session.Remove("pagingState");
             }
             else
             {
                  var ps = session.Prepare("SELECT * from \"Knjiga\" where zanr='"+selektovaniZanr+"';");
                  var statement=ps.Bind().SetAutoPage(false).SetPageSize(5).SetPagingState(pagingState);
                  knjigeRaw=session.Execute(statement);
                  pagingState=knjigeRaw.PagingState;
                  if(pagingState!=null)
                     HttpContext.Session.Set("pagingState",pagingState); 
                     else
                      HttpContext.Session.Remove("pagingState");
             }
                List<Knjiga> josKnjiga=new List<Knjiga>();
                       foreach(var knjigaRaw in knjigeRaw)
                            {       
                                Knjiga knjiga = new Knjiga();
                                knjiga.autor = knjigaRaw["autor"] != null ? knjigaRaw["autor"].ToString() : string.Empty;
                                knjiga.brstrana = knjigaRaw["brstrana"] != null ? knjigaRaw["brstrana"].ToString() : string.Empty;
                                knjiga.godina = knjigaRaw["godina"] != null ? knjigaRaw["godina"].ToString() : string.Empty;
                                knjiga.knjigaID = knjigaRaw["knjigaID"].ToString();
                                knjiga.kolicina = knjigaRaw["kolicina"] != null ? Int32.Parse( knjigaRaw["kolicina"].ToString()) : 0;
                                knjiga.naziv = knjigaRaw["naziv"] != null ? knjigaRaw["naziv"].ToString() : string.Empty;
                                knjiga.opis = knjigaRaw["opis"] != null ? knjigaRaw["opis"].ToString() : string.Empty;
                                knjiga.pismo = knjigaRaw["pismo"] != null ? knjigaRaw["pismo"].ToString() : string.Empty;
                                knjiga.slika = knjigaRaw["slika"] != null ? LoadImage(knjigaRaw["slika"].ToString()) : string.Empty;
                                knjiga.zanr = knjigaRaw["zanr"] != null ? knjigaRaw["zanr"].ToString() : string.Empty;
                                knjiga.jezik = knjigaRaw["jezik"] != null ? knjigaRaw["jezik"].ToString() : string.Empty;
                                
                                josKnjiga.Add(knjiga);
                            
                        }


             return new JsonResult(josKnjiga);
         }


           public string LoadImage(string imgURL)
        {
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/"+imgURL);

            byte[] imgRaw=System.IO.File.ReadAllBytes(file);
           
            string imgString=Convert.ToBase64String(imgRaw);
            imgString="data:image/png;base64,"+imgString;
            return imgString;


            
        }
}
}
   