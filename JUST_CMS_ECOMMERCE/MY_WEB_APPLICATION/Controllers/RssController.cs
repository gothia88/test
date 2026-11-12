using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/01/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.Attributes.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceName: Resources.Strings.ControllersKeys.Rss)]
	public partial class RssController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public RssController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.Attributes.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			resourceName: Resources.Strings.ActionsKeys.Display)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			Models.GlobalApplicationSettings oGlobalApplicationSettings =
				Infrastructure.GlobalApplicationSettings.Instance;

			// **************************************************
			string strSiteUrl =
				oGlobalApplicationSettings.SiteUrl;

			if (string.IsNullOrWhiteSpace(strSiteUrl))
			{
				return (Content("[Site Url] is missing!"));
			}
			// **************************************************

			// **************************************************
			string strImageUrl =
				oGlobalApplicationSettings.RssImageUrl;

			if (string.IsNullOrWhiteSpace(strImageUrl))
			{
				return (Content("[Rss Image Url] is missing!"));
			}
			// **************************************************

			int intRssItemCount =
				oGlobalApplicationSettings.RssItemCount;

			string strRssRootCreatorUrl =
				string.Format("{0}/Rss", strSiteUrl);

			string strRssImageLink = strSiteUrl;
			string strRssChannelLink = strSiteUrl;

			string strRssImageTitle = "کاربران - Users";
			string strRssChannelTitle = "کاربران - Users";
			string strRssChannelDescription = "کاربران - Users";

			var varUsers =
				UnitOfWork.UserRepository
				.GetActiveForRss(take: intRssItemCount)
				.ToList()
				;

			// **************************************************
			Dtx.Rss.Channel oChannel =
				new Dtx.Rss.Channel
					(link: strRssChannelLink,
					title: strRssChannelTitle,
					description: strRssChannelDescription);

			oChannel.Language = "fa-IR";
			oChannel.PubDate = Models.Utility.Now;

			Dtx.Rss.Image oImage =
				new Dtx.Rss.Image
					(url: strImageUrl,
					link: strRssImageLink,
					title: strRssImageTitle);

			Dtx.Rss.Root oRoot =
				new Dtx.Rss.Root
					(image: oImage,
					channel: oChannel,
					creatorUrl: strRssRootCreatorUrl);
			// **************************************************

			Dtx.Rss.Item oItem = null;

			foreach (var varUser in varUsers)
			{
				string strUserTitle = varUser.Title;

				string strLink =
					string.Format("{0}/Account/DisplayProfile/{1}", strSiteUrl, varUser.Id);

				oItem = new Dtx.Rss.Item(strUserTitle, strLink);

				if ((varUser.UpdateDateTime == null) ||
					(varUser.UpdateDateTime.Value.HasValue == false))
				{
					oItem.PubDate = Models.Utility.Now;
				}
				else
				{
					oItem.PubDate = varUser.UpdateDateTime.Value;
				}

				oItem.Guid =
					new Dtx.Rss.Guid(true, strLink);

				if (string.IsNullOrWhiteSpace(varUser.Description) == false)
				{
					oItem.Description = varUser.Description;
				}

				if (string.IsNullOrWhiteSpace(varUser.CreatorUserEmailAddress) == false)
				{
					if (string.IsNullOrWhiteSpace(varUser.CreatorUserFullName))
					{
						oItem.Author = new Dtx.Rss.Email
							(varUser.CreatorUserEmailAddress, varUser.CreatorUserEmailAddress);
					}
					else
					{
						oItem.Author = new Dtx.Rss.Email
							(varUser.CreatorUserEmailAddress, varUser.CreatorUserFullName);
					}
				}

				oRoot.Items.Add(oItem);
			}

			System.IO.Stream oStream =
				Dtx.Rss.Utility.Publish(oRoot);

			return (File(fileStream: oStream, contentType: "text/xml"));
		}
	}
}
