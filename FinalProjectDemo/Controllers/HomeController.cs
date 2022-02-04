using FavoritesDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesDemo.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Welcome(string username)
		{
			if (MyUser.NoUser())
			{
				if (username == null || username == "")
				{
					return Redirect("/");
				}
				else
				{
					MyUser.Username = username;
				}
			}

			List<JoinResults> favs = ArtDB.GetFavorites(MyUser.Username); 
			return View(favs);
		}

		public async Task<IActionResult> ViewArt()
		{
			if (MyUser.NoUser())
			{
				return Redirect("/");
			}
			return View(await ChicagoArtAPI.GetArts(10));
		}

		public async Task<IActionResult> AddFav(int id)
		{
			if (MyUser.NoUser())
			{
				return Redirect("/");
			}
			// Get the detail again... Technically we probably just did, but this is quick and easy for now
			ArtworkDetail detail = await ChicagoArtAPI.GetArt(id);
			// Save to database...
			ArtDB.AddFavorite(MyUser.Username, detail);
			return Redirect("/home/welcome");

		}

		public async Task<IActionResult> Test()
		{
			ArtworkDetail detail = await ChicagoArtAPI.GetArt(249831);
			return Json(detail);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
