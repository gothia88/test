namespace Dtx.Security
{
	/// <remarks>
	/// Version: 4.0 - Date: 2012/01/25
	/// </remarks>
	public static class HttpContext
	{
		public static string GetGlobalResourceString(string classKey, string resourceKey)
		{
			if (classKey == null)
			{
				return (string.Empty);
			}

			classKey = classKey.Trim();
			if (classKey == string.Empty)
			{
				return (string.Empty);
			}

			if (resourceKey == null)
			{
				return (string.Empty);
			}

			resourceKey = resourceKey.Trim();
			if (resourceKey == string.Empty)
			{
				return (string.Empty);
			}

			try
			{
				object obj =
					System.Web.HttpContext.GetGlobalResourceObject(classKey, resourceKey);

				if (obj == null)
				{
					return (string.Empty);
				}
				else
				{
					return (obj.ToString().Trim());
				}
			}
			catch
			{
				return (string.Empty);
			}
		}
	}
}