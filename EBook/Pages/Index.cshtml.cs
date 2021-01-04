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

namespace EBook.Pages
{
    public class IndexModel : PageModel
    {
        public String hotelID {get; set;}
        public String name {get; set;}
        public String address {get; set;}
        public String city {get; set;}
        public Knjiga knjiga {get; set;}
        public Korisnik korisnik {get; set;}
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ISession session = SessionManager.GetSession();
            IMapper mapper = new Mapper(session);

            /*Row hotelData = session.Execute("select * from \"Hotel\" where \"hotelID\"='1'").FirstOrDefault();

            hotelID = hotelData["hotelID"] != null ? hotelData["hotelID"].ToString() : string.Empty;
            name = hotelData["name"] != null ? hotelData["name"].ToString() : string.Empty;
            address = hotelData["address"] != null ? hotelData["address"].ToString() : string.Empty;
            city = hotelData["city"] != null ? hotelData["city"].ToString() : string.Empty;*/

            //knjiga = mapper.First<Knjiga>("select * from \"Knjiga\"");
            korisnik = mapper.First<Korisnik>("select * from \"Korisnik\"");
            
        }
    }
}
