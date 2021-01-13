using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBook.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cassandra;
using Cassandra.Mapping;

namespace MyApp.Namespace
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public int WhatToShow { get; set; }
        [BindProperty]
        public string SearchBook { get; set; }
        [BindProperty]
        public String email {get; set;}
        [BindProperty]
        public String password {get; set;}
        public String ErrorMessage {get; set;}
        public String ErrorMessage2 {get; set;}
        //[BindProperty]
        //public Korisnik newUser {get; set;}
        [BindProperty]
        public String newEmail {get; set;}
        [BindProperty]
        public String newPassword {get; set;}
        [BindProperty]
        public String newAddress {get; set;}
        [BindProperty]
        public String newCity {get; set;}
        [BindProperty]
        public String newPhoneNumber {get; set;}
        [BindProperty]
        public String newFirstName {get; set;}
        [BindProperty]
        public String newLastName {get; set;}
        public string Message {get; set;}
        public IActionResult OnGet()
        {
            WhatToShow = 1;
            string mejl= HttpContext.Session.GetString("email");
            if(mejl!=null)
                return RedirectToPage("/Index");
            else
                return Page();
        }

        public IActionResult OnPostLogin()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);

            Korisnik user = mapper.FirstOrDefault<Korisnik>("select * from \"korisnik\" where email = '" + email + "'");
            if(user == null)
            {
                ErrorMessage="Invalid email adress.";
                WhatToShow = 1;
                return Page();
            }
            else if(user.sifra != password)
            {
                ErrorMessage="Invalid password.";
                WhatToShow = 1;
                return Page();
            }

            HttpContext.Session.SetString("email", email);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostSignUp()
        {
            Cassandra.ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);

            Korisnik k = mapper.FirstOrDefault<Korisnik>("select * from korisnik where email = '" + newEmail + "'");
            if(k!=null)
            {
                ErrorMessage2="This email address is already used";
                WhatToShow = 0;
                return Page();
            }

            Korisnik newUser = new Korisnik(Cassandra.TimeUuid.NewId().ToString(), newEmail, newPassword, newFirstName, newLastName, newPhoneNumber, newCity, newAddress, 0);
            mapper.Insert<Korisnik>(newUser);
            

            HttpContext.Session.SetString("email", newEmail);
            return RedirectToPage("/Index");
        }
    }
}
