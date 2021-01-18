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
    public class PrivacyModel : PageModel
    {
        public string Message {get; set;}
        [BindProperty]
        public string SearchBook { get; set; }
        public void OnGet()
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
        }
    }
}
