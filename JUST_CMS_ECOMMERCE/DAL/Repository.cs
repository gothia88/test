using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1392/05/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class Repository<T> :
		System.Object, IRepository<T> where T : Models.BaseEntity
	{
		#region Tag Services
		private static void GetTags(object obj)
		{
			Models.Common.ITag oTagEntity =
				obj as Models.Common.ITag;

			if (oTagEntity == null)
			{
				return;
			}

			oTagEntity.Tags = string.Empty;

			if (oTagEntity.TagList != null)
			{
				for (int intIndex = 0; intIndex <= oTagEntity.TagList.Count - 1; intIndex++)
				{
					oTagEntity.Tags +=
						oTagEntity.TagList[intIndex].Name.Trim();

					if (intIndex != oTagEntity.TagList.Count - 1)
					{
						oTagEntity.Tags += " | ";
					}
				}
			}
		}

		private static string FixTags(string tags)
		{
			if (tags == null)
			{
				return (null);
			}

			tags = tags.Trim();
			if (tags == string.Empty)
			{
				return (null);
			}

			while (tags.Contains("  "))
			{
				tags = tags.Replace("  ", " ");
			}

			string[] strSourceTagsList =
				tags.Split('|', '،', ',', '-');

			System.Collections.Generic.List<string> oTargetTagList =
				new System.Collections.Generic.List<string>();

			foreach (string strCurrentTag in strSourceTagsList)
			{
				string strTag = strCurrentTag.Trim();

				if (strTag != string.Empty)
				{
					if (oTargetTagList.Contains(strTag) == false)
					{
						oTargetTagList.Add(strTag);
					}
				}
			}

			string strResult = string.Empty;

			for (int intIndex = 0; intIndex <= oTargetTagList.Count - 1; intIndex++)
			{
				strResult += oTargetTagList[intIndex];

				if (intIndex != oTargetTagList.Count - 1)
				{
					strResult += " | ";
				}
			}

			if (strResult.EndsWith("| "))
			{
				strResult =
					strResult.Substring(0, strResult.Length - 2);
			}

			strResult = strResult.Trim();

			if (strResult == string.Empty)
			{
				return (null);
			}
			else
			{
				return (strResult);
			}
		}

		private static void BeforeInsertTag
			(Models.DatabaseContext databaseContext, object obj)
		{
			Models.Common.ITag oTagEntity =
				obj as Models.Common.ITag;

			if (oTagEntity == null)
			{
				return;
			}

			oTagEntity.Tags = FixTags(oTagEntity.Tags);

			if (string.IsNullOrEmpty(oTagEntity.Tags) == false)
			{
				string[] strTagsList =
					oTagEntity.Tags.Split('|');

				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				oTagEntity.TagList =
					new System.Collections.Generic.List<Models.Common.Tag>();

				for (int intIndex = 0; intIndex <= strTagsList.Length - 1; intIndex++)
				{
					string strTagName =
						strTagsList[intIndex].Trim();

					Models.Common.Tag oTag =
						databaseContext.Tags
						.Where(current => current.Name == strTagName)
						.Where(current => current.CultureLcid == intCultureLcid)
						.FirstOrDefault();

					if (oTag == null)
					{
						oTag = new Models.Common.Tag();

						oTag.Name = strTagName;
						oTag.CultureLcid = intCultureLcid;

						databaseContext.Tags.Add(oTag);
					}

					oTagEntity.TagList.Add(oTag);
				}
			}
		}

		private static void BeforeUpdateTag
			(Models.DatabaseContext databaseContext, object obj)
		{
			Models.Common.ITag oTagEntity =
				obj as Models.Common.ITag;

			if (oTagEntity == null)
			{
				return;
			}

			oTagEntity.Tags = FixTags(oTagEntity.Tags);

			if (oTagEntity.TagList != null)
			{
				oTagEntity.TagList.Clear();
			}

			if (string.IsNullOrEmpty(oTagEntity.Tags) == false)
			{
				string[] strTagsList =
					oTagEntity.Tags.Split('|');

				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				for (int intIndex = 0; intIndex <= strTagsList.Length - 1; intIndex++)
				{
					string strTagName =
						strTagsList[intIndex].Trim();

					Models.Common.Tag oTag =
						databaseContext.Tags
						.Where(current => current.Name == strTagName)
						.Where(current => current.CultureLcid == intCultureLcid)
						.FirstOrDefault();

					if (oTag == null)
					{
						oTag = new Models.Common.Tag();

						oTag.Name = strTagName;
						oTag.CultureLcid = intCultureLcid;

						databaseContext.Tags.Add(oTag);
					}

					oTagEntity.TagList.Add(oTag);
				}
			}
		}
		#endregion /Tag Services

		// نمی نويسيم Default Constructor ، Repository برای
		//public Repository()
		//{
		//}

		public Repository(Models.DatabaseContext databaseContext)
		{
			if (databaseContext == null)
			{
				throw (new System.ArgumentNullException("databaseContext"));
			}

			DatabaseContext = databaseContext;
			DbSet = DatabaseContext.Set<T>();
		}

		protected System.Data.Entity.DbSet<T> DbSet { get; set; }
		protected Models.DatabaseContext DatabaseContext { get; set; }

		public virtual void Insert(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}

			BeforeInsertTag(DatabaseContext, entity);

			DbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}

			BeforeUpdateTag(DatabaseContext, entity);

			//#if DEBUG
			//#endif

			// **************************************************
			// Just For Debug!
			// **************************************************
			System.Data.Entity.EntityState oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************

			if (oEntityState == System.Data.Entity.EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			// **************************************************
			// Just For Debug!
			// **************************************************
			oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************

			DatabaseContext.Entry(entity).State =
				System.Data.Entity.EntityState.Modified;

			// **************************************************
			// Just For Debug!
			// **************************************************
			oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}

			// **************************************************
			// Just For Debug!
			// **************************************************
			System.Data.Entity.EntityState oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************

			if (oEntityState == System.Data.Entity.EntityState.Detached)
			{
				DbSet.Attach(entity);
			}

			// **************************************************
			// Just For Debug!
			// **************************************************
			oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************

			DbSet.Remove(entity);

			// **************************************************
			// Just For Debug!
			// **************************************************
			oEntityState =
				DatabaseContext.Entry(entity).State;
			// **************************************************
			// /Just For Debug!
			// **************************************************
		}

		public virtual bool DeleteById(System.Guid id)
		{
			// Solution (1)
			//T oEntity =
			//	DbSet
			//	.Where(current => current.Id == id)
			//	.FirstOrDefault();
			// /Solution (1)

			// Solution (2)
			T oEntity = DbSet.Find(id);
			// /Solution (2)

			if (oEntity == null)
			{
				return (false);
			}
			else
			{
				Delete(oEntity);

				return (true);
			}
		}

		public virtual T GetById(System.Guid id)
		{
			T obj = DbSet.Find(id);

			GetTags(obj);

			return (obj);
		}

		public virtual System.Linq.IQueryable<T> Get()
		{
			return (DbSet);
		}

		public IQueryable<T> Get
			(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate)
		{
			return (DbSet.Where(predicate));
		}

		public virtual System.Collections.Generic.IEnumerable<T> GetWithRawSql
			(string query, params object[] parameters)
		{
			return (DbSet.SqlQuery(query, parameters).ToList());
		}
	}
}
