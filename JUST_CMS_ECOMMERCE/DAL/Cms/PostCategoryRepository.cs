using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/10/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PostCategoryRepository :
		Repository<Models.Cms.PostCategory>, IPostCategoryRepository
	{
		public PostCategoryRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IOrderedQueryable<Models.Cms.PostCategory>
			GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.PostCategory>
			GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.Cms.PostCategory.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid, System.Guid? parentId)
		{
			var varEntities =
				Get()

				.Where(current => current.ParentPostCategoryId == parentId)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.Select(current => new ViewModels.Areas.Cms.PostCategory.IndexViewModel
				{
					Id = current.Id,
					Name = current.Name,
					ImageUrl = current.ImageUrl,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					IsFeatured = current.IsFeatured,
					PostCount = current.Posts.Count(),
					ChildCount = current.Children.Count(),
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,
				})
				;

			return (varEntities);
		}

		public Models.Cms.PostCategory GetByCultureLcidAndParentIdAndName
			(int cultureLcid, System.Guid? parentId, string name)
		{
			var varEntity =
				Get()

				.Where(current => current.ParentPostCategoryId == parentId)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => string.Compare(current.Name, name, true) == 0)

				.FirstOrDefault();

			return (varEntity);
		}

		public Models.Cms.PostCategory GetByCultureLcidAndParentIdAndNameExceptId
			(int cultureLcid, System.Guid? parentId, string name, System.Guid id)
		{
			var varEntity =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.ParentPostCategoryId == parentId)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => string.Compare(current.Name, name, true) == 0)

				.FirstOrDefault();

			return (varEntity);
		}

		public IOrderedQueryable<Models.Cms.PostCategory>
			GetActiveLeavesByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.Children.Count == 0)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}
	}
}
