using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using EBook.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class NewBookModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }

        [BindProperty]
        public Knjiga novaKnjiga { get; set; }
  
        private IWebHostEnvironment  _environment;
        public NewBookModel(IWebHostEnvironment environment)
        {
            _environment=environment;
        }
         public ActionResult OnGet()
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
                    {Message="User";return RedirectToPage("/Index");}
                //Message = "Welcome " + korisnik.ime;
            }
            else
            {
                return RedirectToPage("/Login");
            }
            novaKnjiga = new Knjiga();
            return Page();
        }
        public IActionResult OnPost()
        {
             Cassandra.ISession session = SessionManager.GetSession();

            if (session == null)
                return RedirectToPage("/Error");


                string fileName=saveBase64AsImage(novaKnjiga.slika);
                novaKnjiga.slika="images/"+"Library"+"/"+fileName;
             



                String noviId = Cassandra.TimeUuid.NewId().ToString();
                String query=$"insert into \"Knjiga\" (zanr, \"knjigaID\", autor, brstrana, godina, kolicina, naziv, opis, pismo, slika, jezik)  values ('{novaKnjiga.zanr}', '" + noviId +$"', '{novaKnjiga.autor}', '{novaKnjiga.brstrana}', '{novaKnjiga.godina}', {novaKnjiga.kolicina}, '{novaKnjiga.naziv}', '{novaKnjiga.opis}', '{novaKnjiga.pismo}','{novaKnjiga.slika}','{novaKnjiga.jezik}')";
               
           
           session.Execute(query);
            return RedirectToPage("/Book", new {id=noviId, zanr=novaKnjiga.zanr});

        }
        public string saveBase64AsImage(string img)
        {
            img=img.Substring(img.IndexOf(',') + 1);
            var imgConverted=Convert.FromBase64String(img);
            
            string imgName=System.Guid.NewGuid().ToString();

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images/"+"Library"+"/"+imgName+".jpg");
        
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                fileStream.Write(imgConverted, 0, imgConverted.Length);
                    fileStream.Flush();
                
            }
            return imgName+".jpg";
        }
    }
}
