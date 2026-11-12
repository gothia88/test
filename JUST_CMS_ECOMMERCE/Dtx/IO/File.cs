namespace Dtx.IO
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/05/16
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class File
	{
		public static string Read(string pathName)
		{
			if (pathName == null)
			{
				return (string.Empty);
			}

			pathName = pathName.Trim();
			if (pathName == string.Empty)
			{
				return (string.Empty);
			}

			if (System.IO.File.Exists(pathName) == false)
			{
				return (string.Empty);
			}

			string strResult = string.Empty;

			System.IO.StreamReader oStream = null;
			try
			{
				oStream =
					new System.IO.StreamReader
						(pathName, System.Text.Encoding.UTF8);

				strResult = oStream.ReadToEnd();
			}
			catch
			{
			}
			finally
			{
				if (oStream != null)
				{
					//oStream.Close();
					oStream.Dispose();
					oStream = null;
				}
			}

			return (strResult);
		}

		public static bool Write(string pathName, string text, bool append)
		{
			if (pathName == null)
			{
				return (false);
			}

			pathName = pathName.Trim();
			if (pathName == string.Empty)
			{
				return (false);
			}

			if (text == null)
			{
				text = string.Empty;
			}

			System.IO.StreamWriter oStream = null;
			try
			{
				oStream =
					new System.IO.StreamWriter
						(pathName, append, System.Text.Encoding.UTF8);

				oStream.Write(text);

				return (true);
			}
			catch
			{
				return (false);
			}
			finally
			{
				if (oStream != null)
				{
					//oStream.Close();
					oStream.Dispose();
					oStream = null;
				}
			}
		}

		public static bool Append(string pathName, string text)
		{
			return (Write(pathName, text, true));
		}

		public static bool Overwrite(string pathName, string text)
		{
			return (Write(pathName, text, false));
		}

		private static string GetCurrentCultureName()
		{
			return (System.Threading.Thread.CurrentThread.CurrentCulture.Name);
		}

		public static string GetFileNameByCulture
			(string name, string cultureName, string extension)
		{
			string strFileName = name;

			//if (string.Compare(cultureName, "en-US", ignoreCase: true) != 0)
			//{
			//	strFileName =
			//		string.Format("{0}.{1}", strFileName, cultureName);
			//}

			//strFileName =
			//	string.Format("{0}.{1}", strFileName, extension);

			strFileName =
				string.Format("{0}.{1}.{2}",
				strFileName, cultureName, extension);

			return (strFileName);
		}

		public static string GetFileNameByCurrentCulture
			(string fileName, string extension)
		{
			return (GetFileNameByCulture(fileName, GetCurrentCultureName(), extension));
		}

		public static string GetPathNameByCulture
			(string rootRelativePath, string name, string cultureName, string extension)
		{
			string strFileName =
				GetFileNameByCulture(name, cultureName, extension);

			string strRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, strFileName);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			return (strPathName);
		}

		public static string GetPathNameByCurrentCulture
			(string rootRelativePath, string name, string extension)
		{
			return (GetPathNameByCulture
				(rootRelativePath, name, GetCurrentCultureName(), extension));
		}
	}
}
