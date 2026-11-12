using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.BLL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/12/16
	/// Changed In Version: 1.8.2
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Post
	{
		static Post()
		{
		}

		public static bool IsAllowedForDisplaying(Models.Cms.Post post)
		{
			if (post == null)
			{
				throw (new System.ArgumentNullException
					("[MY_WEB_APPLICATION.Areas.Cms.BLL] -> [Post] Class -> [IsAllowedForDisplaying] Function -> [post] is null!"));
			}

			if (post.IsDeleted)
			{
				return (false);
			}

			if (post.IsActive == false)
			{
				return (false);
			}

			if (post.PostCategory.IsDeleted)
			{
				return (false);
			}

			if (post.PostCategory.IsActive == false)
			{
				return (false);
			}

			if (post.CreatorUser == null)
			{
				return (false);
			}

			if (User.IsAllowedForPublishingPost(post.CreatorUser) == false)
			{
				return (false);
			}

			return (true);
		}
	}
}
