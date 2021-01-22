using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cassandra.Mapping;
using EBook.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class EditBookModel : PageModel
    
{       [BindProperty]
        public string nazivSlike{ get; set; }
          public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }

        [BindProperty]
        public Knjiga zaEdit { get; set; }
         private IWebHostEnvironment  _environment;
        public EditBookModel(IWebHostEnvironment environment)
        {
            _environment=environment;
        }
        public ActionResult OnGet(string zanr, string id)
        {
              Cassandra.ISession session = SessionManager.GetSession();
              IMapper mapper=new Mapper(session);

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
            string slikaPrep="data:image/jpeg;base64,";
                
              zaEdit=mapper.FirstOrDefault<Knjiga>($"select * from \"EBook\".\"Knjiga\" where \"knjigaID\"='{id}' and zanr='{zanr}';");
              slikaPrep+=Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_environment.ContentRootPath, "wwwroot/"+zaEdit.slika)));
              nazivSlike=zaEdit.slika;
              zaEdit.slika=slikaPrep;
              return Page();
        }
        public IActionResult OnPost(string staraSlika)
         {
             Cassandra.ISession session = SessionManager.GetSession();

            if (session == null)
                return RedirectToPage("/Error");


                string fileName=saveBase64AsImage(zaEdit.slika);
                zaEdit.slika="images/"+"Library"+"/"+fileName;
             
                 System.IO.FileInfo oldPic = new FileInfo( Path.Combine(_environment.ContentRootPath, "wwwroot/"+nazivSlike));
                    oldPic.Delete(); 
          
        
                
         
              
               String query=$"update \"Knjiga\" set autor='{zaEdit.autor}', brstrana='{zaEdit.brstrana}', godina='{zaEdit.godina}', kolicina={zaEdit.kolicina}, naziv='{zaEdit.naziv}', opis='{zaEdit.opis}', pismo='{zaEdit.pismo}', slika='{zaEdit.slika}', jezik= '{zaEdit.jezik}'where  \"knjigaID\"='{zaEdit.knjigaID}' and zanr='{zaEdit.zanr}'";
            
           session.Execute(query);
         
            return RedirectToPage("/Book", new {id=zaEdit.knjigaID,zanr=zaEdit.zanr});

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
    public IActionResult OnPostObrisi(string id,string zanr,string slika)
    { Cassandra.ISession session = SessionManager.GetSession();

            if (session == null)
                return RedirectToPage("/Error");
                  String query=$"delete from \"Knjiga\" where  \"knjigaID\"='{id}' and zanr='{zanr}'";
            
           session.Execute(query);
            System.IO.FileInfo oldPic = new FileInfo( Path.Combine(_environment.ContentRootPath, "wwwroot/"+slika));
                    oldPic.Delete(); 

        return RedirectToPage("/Library");
    }
               
                   

        
    }
}
