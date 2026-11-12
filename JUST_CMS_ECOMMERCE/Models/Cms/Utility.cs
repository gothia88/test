using System.Linq;
using System.Data.Entity;

namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/10/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Utility
	{
		static Utility()
		{
		}

		public static bool GenerateRandomData()
		{
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext =
					new DatabaseContext();

				Models.Cms.CmsSettings oSettings =
					oDatabaseContext.CmsSettings
					.FirstOrDefault();

				if ((oSettings != null) &&
					(oSettings.IsRandomDataGenerated))
				{
					return (false);
				}

				int intEnglishCultureLcid = 1033;
				int intPersianCultureLcid = 1065;

				Models.User oPersianProgrammerUser =
					oDatabaseContext.Users
					.Where(current => current.Role.Code == (int)Models.Enums.Roles.Programmer)
					.FirstOrDefault();

				// **************************************************
				CreateRandomPostCategories
					(oDatabaseContext, intPersianCultureLcid, oPersianProgrammerUser.Id, 2);

				CreateRandomPostCategories
					(oDatabaseContext, intEnglishCultureLcid, oPersianProgrammerUser.Id, 2);
				// **************************************************

				// **************************************************
				CreateRandomPosts
					(oDatabaseContext, intPersianCultureLcid, oPersianProgrammerUser.Id, 200);

				CreateRandomPosts
					(oDatabaseContext, intEnglishCultureLcid, oPersianProgrammerUser.Id, 100);
				// **************************************************

				if (oSettings == null)
				{
					oSettings =
						new CmsSettings();

					oSettings.IsRandomDataGenerated = true;

					oDatabaseContext.CmsSettings.Add(oSettings);
				}
				else
				{
					oSettings.IsRandomDataGenerated = true;
				}

				oDatabaseContext.SaveChanges();

				return (true);
			}
			catch (System.Exception ex)
			{
				Dtx.LogHandler.Report(typeof(Utility), null, ex);

				return (false);
			}
			finally
			{
				if (oDatabaseContext != null)
				{
					oDatabaseContext.Dispose();
					oDatabaseContext = null;
				}
			}
		}

		private static void CreateRandomPostCategories
			(Models.DatabaseContext databaseContext,
			int cultureLcid,
			System.Guid creatorUserId,
			int postCategoryCountInEachLevel)
		{
			Cms.PostCategory oPostCategory1 = null;
			Cms.PostCategory oPostCategory2 = null;
			Cms.PostCategory oPostCategory3 = null;

			for (int intLevel1 = 1; intLevel1 <= postCategoryCountInEachLevel; intLevel1++)
			{
				oPostCategory1 =
					new Cms.PostCategory();

				switch (cultureLcid)
				{
					case 1065:
					{
						oPostCategory1.Name =
							string.Format("طبقه‌بندی مطلب ({0})", intLevel1);

						break;
					}

					case 1033:
					{
						oPostCategory1.Name =
							string.Format("Post Category ({0})", intLevel1);

						break;
					}
				}

				oPostCategory1.CultureLcid = cultureLcid;
				oPostCategory1.SetInsertDateTime(creatorUserId);
				oPostCategory1.SetIsActive(true, creatorUserId);

				oPostCategory1.Children =
					new System.Collections.Generic.List<Cms.PostCategory>();

				for (int intLevel2 = 1; intLevel2 <= postCategoryCountInEachLevel; intLevel2++)
				{
					oPostCategory2 =
						new Cms.PostCategory();

					oPostCategory2.Name =
						string.Format("{0} ({1})", oPostCategory1.Name, intLevel2);

					oPostCategory2.CultureLcid = cultureLcid;
					oPostCategory2.SetInsertDateTime(creatorUserId);
					oPostCategory2.SetIsActive(true, creatorUserId);

					oPostCategory2.Children =
						new System.Collections.Generic.List<Cms.PostCategory>();

					for (int intLevel3 = 1; intLevel3 <= postCategoryCountInEachLevel; intLevel3++)
					{
						oPostCategory3 =
							new Cms.PostCategory();

						oPostCategory3.Name =
							string.Format("{0} ({1})", oPostCategory2.Name, intLevel3);

						oPostCategory3.CultureLcid = cultureLcid;
						oPostCategory3.SetInsertDateTime(creatorUserId);
						oPostCategory3.SetIsActive(true, creatorUserId);

						oPostCategory2.Children.Add(oPostCategory3);
					}

					oPostCategory1.Children.Add(oPostCategory2);
				}

				databaseContext.PostCategories.Add(oPostCategory1);
			}

			// برای تست
			databaseContext.SaveChanges();
		}

		private static void CreateRandomPosts
			(Models.DatabaseContext databaseContext,
			int cultureLcid,
			System.Guid creatorUserId,
			int count)
		{
			// **************************************************
			string strPostImages = string.Empty;
			for (int intIndex = 1; intIndex <= 30; intIndex++)
			{
				strPostImages +=
					string.Format("/Content/Demo/Products/{0}.png", intIndex);

				if (intIndex != 30)
				{
					strPostImages += ";";
				}
			}
			string[] oProductImages = strPostImages.Split(';');
			// **************************************************

			Models.AccessType oAccessType =
				databaseContext.AccessTypes
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.Code == (int)Models.Enums.AccessTypes.Public)
				.FirstOrDefault();

			Cms.Post oPost = null;

			int intTemp = 0;

			for (int intIndex = 1; intIndex <= count; intIndex++)
			{
				oPost =
					new Cms.Post();

				switch (cultureLcid)
				{
					case 1065:
					{
						oPost.Title =
							string.Format("مطلب ({0})", intIndex);

						oPost.Body =
							string.Format("متن اصلی ({0})", intIndex);

						oPost.Introduction =
							string.Format("مقدمه متن ({0})", intIndex);

						break;
					}

					case 1033:
					{
						oPost.Title =
							string.Format("Post ({0})", intIndex);

						oPost.Body =
							string.Format("Body ({0})", intIndex);

						oPost.Introduction =
							string.Format("Introduction ({0})", intIndex);

						break;
					}
				}

				oPost.CultureLcid = cultureLcid;
				oPost.SetInsertDateTime(creatorUserId);

				if (intIndex % 2 == 0)
				{
					oPost.SetIsActive(true, creatorUserId);
				}

				if (intIndex % 3 == 0)
				{
					oPost.IsFeatured = true;
					oPost.IsCommentingEnabled = true;
				}

				if (intIndex % 4 == 0)
				{
					oPost.SetIsVerified(true, creatorUserId);
				}

				if ((oPost.IsActive) && (oPost.IsFeatured))
				{
					intTemp++;
				}

				if (intTemp % 3 == 0)
				{
					oPost.ImageUrl = string.Empty;
				}
				else
				{
					oPost.ImageUrl =
						oProductImages.OrderBy(current => System.Guid.NewGuid()).FirstOrDefault();
				}

				oPost.AccessTypeId = oAccessType.Id;

				oPost.PostCategoryId =
					databaseContext.PostCategories
					.Where(current => current.Children.Count == 0)
					.Where(current => current.CultureLcid == cultureLcid)
					.OrderBy(current => System.Guid.NewGuid())
					.FirstOrDefault().Id;

				databaseContext.Posts.Add(oPost);

				if (intIndex % 50 == 0)
				{
					// برای تست
					databaseContext.SaveChanges();
				}
			}

			// برای تست
			databaseContext.SaveChanges();
		}
	}
}
