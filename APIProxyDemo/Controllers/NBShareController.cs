using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIProxyDemo.Controllers
{
	[Route("nbshare")]
	[ApiController]
	public class NBShareController : ControllerBase
	{
		[HttpGet]
		public async Task<string> get()
		{

			// IMPORTANT:
			// Notice I'm not bothering with making classes for the response. Ultimately all
			// http requests are really just strings anyway. So I'm just reading the JSON as
			// a string and passing it exactly as-is back out. The Angular app in turn will
			// correctly read it as JSON. (Interestingly, the ASP.NET core seems to notice
			// that the data is JSON and attaches the correct content type of application/json.)

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("https://dashboard.nbshare.io/api/v1/");
			HttpResponseMessage response = await client.GetAsync("apps/reddit");
			string json = await response.Content.ReadAsStringAsync();
			return json;
		}
	}
}
