namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/08/21
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Image
	{
		static Image()
		{
		}

		public static System.Web.Mvc.FileContentResult NoPhoto
			(int? width = null, int? height = null)
		{
			string strFileName = "no-photo.jpg";

			string strRootRelativePathName =
				string.Format("~/App_Data/Photos/{0}", strFileName);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			System.IO.MemoryStream oMemoryStream =
				Infrastructure.Image.GetPhoto(strPathName, width, height);

			System.Web.Mvc.FileContentResult oFileContentResult =
				new System.Web.Mvc.FileContentResult
					(oMemoryStream.ToArray(), contentType: "image/png");

			oFileContentResult.FileDownloadName = "no-photo.png";

			return (oFileContentResult);
		}

		public static System.Web.Mvc.FileContentResult AccessDenied
			(int? width = null, int? height = null)
		{
			string strFileName = "access_denied.gif";

			string strRootRelativePathName =
				string.Format("~/App_Data/Photos/{0}", strFileName);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			System.IO.MemoryStream oMemoryStream =
				Infrastructure.Image.GetPhoto(strPathName, width, height);

			System.Web.Mvc.FileContentResult oFileContentResult =
				new System.Web.Mvc.FileContentResult
					(oMemoryStream.ToArray(), contentType: "image/png");

			oFileContentResult.FileDownloadName = "access_denied.png";

			return (oFileContentResult);
		}

		public static System.IO.MemoryStream GetPhoto
			(string pathName, int? width = null, int? height = null)
		{
			System.Drawing.Image oImage = null;

			try
			{
				System.IO.MemoryStream oMemoryStream =
					new System.IO.MemoryStream();

				oImage =
					System.Drawing.Image.FromFile(pathName);

				if ((width.HasValue == false) && (height.HasValue == false))
				{
					oImage.Save
						(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

					return (oMemoryStream);
				}

				System.Drawing.Image.GetThumbnailImageAbort oThumbnailCallback =
					new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

				if ((width.HasValue) && (height.HasValue))
				{
					System.Drawing.Image oThumbnailImage =
						oImage.GetThumbnailImage
							(width.Value, height.Value, oThumbnailCallback, System.IntPtr.Zero);

					oThumbnailImage.Save
						(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

					return (oMemoryStream);
				}

				if ((width.HasValue) && (height.HasValue == false))
				{
					if (oImage.Width <= width.Value)
					{
						oImage.Save
							(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

						return (oMemoryStream);
					}
					else
					{
						double dblCurrentPhotoWidth = (double)oImage.Width;
						double dblCurrentPhotoHeight = (double)oImage.Height;

						double dblFavoritePhotoWidth = (double)width.Value;

						double dblRatio =
							dblFavoritePhotoWidth / dblCurrentPhotoWidth;

						double dblFavoritePhotoHeight = dblCurrentPhotoHeight * dblRatio;

						height = (int)dblFavoritePhotoHeight;

						System.Drawing.Image oThumbnailImage =
							oImage.GetThumbnailImage
								(width.Value, height.Value, oThumbnailCallback, System.IntPtr.Zero);

						oThumbnailImage.Save
							(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

						return (oMemoryStream);
					}
				}

				if ((width.HasValue == false) && (height.HasValue))
				{
					if (oImage.Height <= height.Value)
					{
						oImage.Save
							(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

						return (oMemoryStream);
					}
					else
					{
						double dblCurrentPhotoWidth = (double)oImage.Width;
						double dblCurrentPhotoHeight = (double)oImage.Height;

						double dblFavoritePhotoHeight = (double)height.Value;

						double dblRatio =
							dblFavoritePhotoHeight / dblCurrentPhotoHeight;

						double dblFavoritePhotoWidth = dblCurrentPhotoWidth * dblRatio;

						width = (int)dblFavoritePhotoWidth;

						System.Drawing.Image oThumbnailImage =
							oImage.GetThumbnailImage
								(width.Value, height.Value, oThumbnailCallback, System.IntPtr.Zero);

						oThumbnailImage.Save
							(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

						return (oMemoryStream);
					}
				}
			}
			catch { }
			finally
			{
				if (oImage != null)
				{
					oImage.Dispose();
					oImage = null;
				}
			}

			return (null);
		}

		public static bool ThumbnailCallback()
		{
			return (false);
		}
	}
}
