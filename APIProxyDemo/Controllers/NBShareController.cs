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
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("https://dashboard.nbshare.io/api/v1/");
			HttpResponseMessage response = await client.GetAsync("apps/reddit");
			string json = await response.Content.ReadAsStringAsync();
			return json;
		}
	}
}
