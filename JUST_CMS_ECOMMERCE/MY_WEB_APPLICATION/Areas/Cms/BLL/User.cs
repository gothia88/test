namespace MY_WEB_APPLICATION.Areas.Cms.BLL
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/12/13
	/// Changed In Version: 1.8.1
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class User
	{
		static User()
		{
		}

		public static bool IsAllowedForPublishingPost(Models.User user)
		{
			if (user == null)
			{
				throw (new System.ArgumentNullException
					("[MY_WEB_APPLICATION.Areas.Cms.BLL] -> [User] Class -> [IsAllowedForPublishingPost] Function -> [user] is null!"));
			}

			if (user.IsDeleted)
			{
				return (false);
			}

			if (user.IsInBlackList)
			{
				return (false);
			}

			if (user.IsActive == false)
			{
				return (false);
			}

			if (user.IsAccountTerminated)
			{
				return (false);
			}

			if (user.IsEmailAddressVerified == false)
			{
				return (false);
			}

			if ((user.IsAccountExpirable) &&
				(user.AccountExpireDate.Value.HasValue) &&
				(user.AccountExpireDate.Value > Models.Utility.Now))
			{
				return (false);
			}

			return (true);
		}
	}
}
