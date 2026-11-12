using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/12/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class TagRepository : Repository<Models.Common.Tag>, ITagRepository
	{
		public TagRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Common.Tag GetByCultureLcidAndName(int cultureLcid, string name)
		{
			Models.Common.Tag oTag =
				Get()

				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)

				.FirstOrDefault();

			return (oTag);
		}

		public Models.Common.Tag GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id)
		{
			Models.Common.Tag oTag =
				Get()

				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)

				.FirstOrDefault();

			return (oTag);
		}

		public IQueryable<ViewModels.Areas.Cms.Tag.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)

				.Select(current => new ViewModels.Areas.Cms.Tag.IndexViewModel
				{
					Id = current.Id,
					Name = current.Name,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					IsVerified = current.IsVerified,
					PageCount = current.Pages.Count(),
					PostCount = current.Posts.Count(),
					UpdateDateTime = current.UpdateDateTime
				})
				;

			return (varEntities);
		}
	}
}
