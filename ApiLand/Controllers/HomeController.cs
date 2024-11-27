using System.Diagnostics;
using System.Net.Http;
using ApiLand.Interfaces;
using ApiLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLand.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;

        public HomeController( ILogger<HomeController> logger, IApiService apiService )
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WebAPI( )
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData( string apiType )
        {
            try
            {
                // Use the service directly to fetch data
                var data = await _apiService.FetchDataAsync( apiType );
                return Json( data ); // Return the fetched data as JSON
            }
            catch ( ArgumentException ex )
            {
                return BadRequest( ex.Message ); // Handle invalid input
            }
            catch ( HttpRequestException ex )
            {
                return StatusCode( 500, ex.Message ); // Handle API call errors
            }
        }

        public IActionResult GraphQL()
        {
            return View();
        }

        public IActionResult MongoDB()
        {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
