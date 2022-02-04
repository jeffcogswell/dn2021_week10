using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesDemo.Models
{

	public class MyUser
	{
		public static string Username = "";
		public static bool NoUser()
		{
			if (Username == "" || Username == null)
			{
				return true;
			}
			return false;
		}
	}

	/*
	 * First, ONLY store what you need to display in the favorites list. When they click the favorite
	 * to see the full details, then you'll do an API call to get the rest of the details.
	 * Also, instead of putting the reduced detail in the favorites table, move it to a separate table.
	 * So don't do this:
	public class Favorite
	{
		public int id { get; set; }
		public string username { get; set; }
		public int artwork_id { get; set; }
		public string title { get; set; }
		public string thumbnail_url { get; set; }

		public string artist { get; set; }
		public string mynotes { get; set; }
	}*/

	// Instead do this:
	
	public class Favorite
	{
		public int id { get; set; }
		public string username { get; set; }
		public int artwork_id { get; set; } // This is the foreign key
		public string mynotes { get; set; }
	}

	public class ArtMiniDetails
	{
		// This table ONLY includes the "mini details", i.e. WHAT TO SHOW in the favorites list, not the entire detail
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int id { get; set; }
		public string title { get; set; }
		public string thumbnail_url { get; set; }
		public string artist { get; set; }
	}

	public class JoinResults
	{
		public int id { get; set; }
		public string username { get; set; }
		public int artwork_id { get; set; }
		public string title { get; set; }
		public string thumbnail_url { get; set; }
		public string artist { get; set; }
		public string mynotes { get; set; }
	}


	public class ArtContext : DbContext
	{
		public DbSet<Favorite> UserFavorites { get; set; }
		public DbSet<ArtMiniDetails> ArtMiniDetails{ get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESSGC;Database=artdbfavs;Integrated Security=SSPI;");
			// Or For username/password, use the following:
			// optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=efconsole1;User Id=sa;Password=abc123;");
		}
	}

	public class ArtDB
	{
		public static List<JoinResults> GetFavorites(string username)
		{
			// Notice how we do a join here!

			List<JoinResults> result = null;
			using (ArtContext ctx = new ArtContext())
			{
				var query = from fav in ctx.UserFavorites
							join detail in ctx.ArtMiniDetails on fav.artwork_id equals detail.id // gotta use "equals" in join on clause for some reason
							where fav.username == username
							select new JoinResults() { 
								id = fav.id, username = fav.username, artwork_id = detail.id,
								title = detail.title, thumbnail_url = detail.thumbnail_url,
								artist = detail.artist, mynotes = fav.mynotes };

				result = query.ToList();
			}
			return result;
		}

		public static bool AddFavorite(string username, ArtworkDetail detail)
		{
			// Remember! ArtworkDetail is from the API! We're going to build the two DB instances here: Favorite and ArtMiniDetail.
			using (ArtContext ctx = new ArtContext())
			{
				// Make sure the user didn't already favorite this. If they did, just return

				List<Favorite> favs = ctx.UserFavorites.Where(s => s.artwork_id == detail.data.id && s.username == username).ToList();
				if (favs.Count > 0)
				{
					return false; // False will mean the favorite already exists
				}

				Favorite newfav = new Favorite();
				newfav.username = MyUser.Username;
				newfav.artwork_id = detail.data.id;

				ArtMiniDetails minidetail = new ArtMiniDetails();
				minidetail.id = detail.data.id;
				minidetail.title = detail.data.title;
				minidetail.artist = detail.data.artist_title;
				minidetail.thumbnail_url = $"{detail.config.iiif_url}/{detail.data.image_id }/full/100,/0/default.jpg";

				// Now we need to see if the detail already exists in the table, and if not, add it first.

				List<ArtMiniDetails> result = ctx.ArtMiniDetails.Where(s => s.id == minidetail.id).ToList();
				if (result.Count == 0)
				{
					// Didn't find it, so add it!
					ctx.ArtMiniDetails.Add(minidetail);
				}

				// And finally add the user detail

				ctx.UserFavorites.Add(newfav);
				ctx.SaveChanges();
			}
			return true;
		}
	}
}
