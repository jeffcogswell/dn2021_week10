using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FavoritesDemo.Models
{
	public class ArtworksResponse
	{
		public List<ArtworkResponse> data { get; set; }
		public ConfigResponse config { get; set; }
	}

	public class ArtworkDetail
	{
		public ArtworkResponse data { get; set; }
		public ConfigResponse config { get; set; }
	}

	public class ArtworkResponse
	{
		public int id { get; set; }
		public string api_link { get; set; }
		public string title { get; set; }
		public string medium_display { get; set; }
		public string artist_title { get; set; }
		public string date_display { get; set; }
		public string dimensions { get; set; }
		public string place_of_origin { get; set; }
		public string inscriptions { get; set; }
		public string credit_line { get; set; }
		public string department_title { get; set; }
		public string provenance_text { get; set; }
		public string image_id { get; set; }
	}

	public class ConfigResponse
	{
		public string iiif_url { get; set; }
	}

	public class ChicagoArtAPI
	{
		private static HttpClient _realClient = null;
		public static HttpClient MyHttp
		{
			get
			{
				if (_realClient == null)
				{
					_realClient = new HttpClient();
					_realClient.BaseAddress = new Uri("https://api.artic.edu/api/v1/"); // ADD YOUR OWN BASE ADDRESS HERE
				}
				return _realClient;
			}
		}

		public static async Task<ArtworksResponse> GetArts(int count)
		{
			var connection = await MyHttp.GetAsync($"artworks?limit={count}");
			ArtworksResponse arts = await connection.Content.ReadAsAsync<ArtworksResponse>();
			return arts;
		}

		public static async Task<ArtworkDetail> GetArt(int id)
		{
			var connection = await MyHttp.GetAsync($"artworks/{id}");
			ArtworkDetail arts = await connection.Content.ReadAsAsync<ArtworkDetail>();
			return arts;
		}
	}
}
