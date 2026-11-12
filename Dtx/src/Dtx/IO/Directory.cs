namespace Dtx.IO
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/11/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Directory
	{
		/// <summary>
		/// Add the tag (&lt;identity impersonate="true"/&gt;) in your web.config file.
		/// </summary>
		/// <remarks>
		/// The .NET Framework original granted "Everyone" full control using the
		/// Windows-only System.IO.Directory.GetAccessControl/SetAccessControl APIs
		/// (System.IO.FileSystem.AccessControl assembly) — an IIS/Windows hosting
		/// concern with no portable equivalent. The .NET 10 port keeps the method
		/// as a no-op; on Windows, grant the ACL with the
		/// System.IO.FileSystem.AccessControl package if that behavior is still required.
		/// </remarks>
		public static void SetFullControlForEveryOne(string path)
		{
			// No-op on the portable .NET 10 target.
		}

		public static void Copy(string sourcePathName, string destinationPathName, bool copySubDirectories)
		{
			// Get the subdirectories for the specified directory.
			System.IO.DirectoryInfo oDirectory =
				new System.IO.DirectoryInfo(sourcePathName);

			System.IO.DirectoryInfo[] oDirectories = oDirectory.GetDirectories();

			if (oDirectory.Exists == false)
			{
				throw (new System.IO.DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourcePathName));
			}

			// If the destination directory doesn't exist, create it. 
			if (System.IO.Directory.Exists(destinationPathName) == false)
			{
				System.IO.Directory.CreateDirectory(destinationPathName);
			}

			// Get the files in the directory and copy them to the new location.
			System.IO.FileInfo[] oFiles = oDirectory.GetFiles();

			foreach (System.IO.FileInfo oFile in oFiles)
			{
				string strTempPath =
					System.IO.Path.Combine(destinationPathName, oFile.Name);

				oFile.CopyTo(strTempPath, false);
			}

			// If copying subdirectories, copy them and their contents to new location. 
			if (copySubDirectories)
			{
				foreach (System.IO.DirectoryInfo oSubDirectory in oDirectories)
				{
					string strTempPath =
						System.IO.Path.Combine(destinationPathName, oSubDirectory.Name);

					Copy(oSubDirectory.FullName, strTempPath, copySubDirectories);
				}
			}
		}
	}
}
