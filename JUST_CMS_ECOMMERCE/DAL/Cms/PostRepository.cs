using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.2.2
	/// Update Date: 1394/03/09
	/// Changed In Version: 1.9.6
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PostRepository : Repository<Models.Cms.Post>, IPostRepository
	{
		public PostRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public System.Linq.IQueryable<ViewModels.Areas.Cms.Post.GetDataViewModel>
			GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.Cms.Post.GetDataViewModel
				{
					Id = current.Id,

					Title = current.Title,

					IsDraft = current.IsDraft,
					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,
					HasAttachment = current.HasAttachment,

					UpdateDateTime = current.UpdateDateTime,

					CommentCount = current.PostComments.Count(),

					PostCategoryId = current.PostCategoryId,
					PostCategoryName = current.PostCategory.Name,

					CreatorUserId = current.CreatorUserId,
					CreatorUserGender = current.CreatorUser.Gender.Name2,
					CreatorUserLastName = current.CreatorUser.LastName,
					CreatorUserFirstName = current.CreatorUser.FirstName,
				})
				;

			return (varEntities);
		}

		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		protected virtual System.Linq.IQueryable<Models.Cms.Post>
			XGetActive()
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDraft == false)
				.Where(current => current.IsDeleted == false)

				.Where(current => current.PostCategory.IsActive)
				.Where(current => current.PostCategory.IsDeleted == false)

				.Where(current => current.CreatorUser.IsActive)
				.Where(current => current.CreatorUser.IsDeleted == false)
				.Where(current => current.CreatorUser.IsAccountTerminated == false)

				.Where(current => current.StartPublishingDateTime.Value == null ||
					current.StartPublishingDateTime.Value <= Models.Utility.Now)

				.Where(current => current.FinishPublishingDateTime.Value == null ||
					current.FinishPublishingDateTime.Value >= Models.Utility.Now)
				;

			return (varEntities);
		}

		protected virtual System.Linq.IQueryable<Models.Cms.Post>
			XGetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				XGetActive()

				.Where(current => current.CultureLcid == cultureLcid)
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.Common.Rss.IndexViewModel>
			GetActiveForRss(int take)
		{
			var varEntities =
				XGetActive()

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Skip(0)
				.Take(take)

				.Select(current => new ViewModels.Areas.Common.Rss.IndexViewModel
				{
					Id = current.Id,

					Title = current.Title,
					Description = current.Description,

					UpdateDateTime = current.UpdateDateTime,

					CreatorUserLastName = current.CreatorUser.LastName,
					CreatorUserFirstName = current.CreatorUser.FirstName,
					CreatorUserGender = current.CreatorUser.Gender.Name2,
					CreatorUserEmailAddress = current.CreatorUser.EmailAddress,
				})
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.DisplayPostsViewModel>
			GetActiveByCultureLcid(int cultureLcid, int skip, int take)
		{
			var varEntities =
				XGetActiveByCultureLcid(cultureLcid)

				.OrderByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.UpdateDateTime)

				.Skip(skip)
				.Take(take)

				.Select(current => new ViewModels.Areas.Cms.Shared.DisplayPostsViewModel
				{
					Id = current.Id,

					Hits = current.Hits,

					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,

					Title = current.Title,
					ImageUrl = current.ImageUrl,
					Introduction = current.Introduction,

					PostCategoryId = current.PostCategoryId,
					PostCategoryName = current.PostCategory.Name,

					Author = current.Author,
					CreatorUserId = current.CreatorUser.Id,
					CreatorUserLastName = current.CreatorUser.LastName,
					CreatorUserFirstName = current.CreatorUser.FirstName,
					CreatorUserGender = current.CreatorUser.Gender.Name2,

					CommentCount = current.PostComments.Where
						(postComment => postComment.IsActive &&
						postComment.IsPrivate == false).Count(),
					IsCommentingEnabled = current.IsCommentingEnabled,

					UpdateDateTime = current.UpdateDateTime,
				})
				;

			return (varEntities);
		}

		public System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.DisplayPostsViewModel>
			GetActiveByCultureLcidAndPostCategoryId(int cultureLcid, System.Guid postCategoryId, int skip, int take)
		{
			var varEntities =
				XGetActiveByCultureLcid(cultureLcid)

				.Where(current => current.PostCategoryId == postCategoryId)

				.OrderByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.UpdateDateTime)

				.Skip(skip)
				.Take(take)

				.Select(current => new ViewModels.Areas.Cms.Shared.DisplayPostsViewModel
				{
					Id = current.Id,

					Hits = current.Hits,

					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,

					Title = current.Title,
					ImageUrl = current.ImageUrl,
					Introduction = current.Introduction,

					PostCategoryId = current.PostCategoryId,
					PostCategoryName = current.PostCategory.Name,

					Author = current.Author,
					CreatorUserId = current.CreatorUser.Id,
					CreatorUserLastName = current.CreatorUser.LastName,
					CreatorUserFirstName = current.CreatorUser.FirstName,
					CreatorUserGender = current.CreatorUser.Gender.Name2,

					CommentCount = current.PostComments.Where
						(postComment => postComment.IsActive &&
						postComment.IsPrivate == false).Count(),
					IsCommentingEnabled = current.IsCommentingEnabled,

					UpdateDateTime = current.UpdateDateTime,
				})
				;

			return (varEntities);
		}

		/// <summary>
		/// Version: 1.8.9
		/// </summary>
		public System.Linq.IQueryable<ViewModels.Areas.Cms.User.GetPostsViewModel>
			GetByCultureLcidAndCreatorUserId(int cultureLcid, System.Guid creatorUserId)
		{
			var varEntities =
				Get()

				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.CreatorUserId == creatorUserId)

				.OrderByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.Cms.User.GetPostsViewModel
				{
					Id = current.Id,

					Title = current.Title,

					IsDraft = current.IsDraft,
					IsActive = current.IsActive,
					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,
					HasAttachment = current.HasAttachment,

					UpdateDateTime = current.UpdateDateTime,

					CommentCount = current.PostComments.Count(),

					PostCategoryId = current.PostCategoryId,
					PostCategoryName = current.PostCategory.Name,
				})
				;

			return (varEntities);
		}



		public System.Linq.IQueryable<ViewModels.Areas.Cms.Shared.GetActiveUserPostsViewModel>
			GetActiveUserPosts(int cultureLcid, System.Guid userId)
		{
			var varEntities =
				XGetActiveByCultureLcid(cultureLcid)

				.Where(current => current.CreatorUserId == userId)

				.OrderByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.UpdateDateTime.Value)

				.Select(current => new ViewModels.Areas.Cms.Shared.GetActiveUserPostsViewModel
				{
					Id = current.Id,

					Title = current.Title,

					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,
					HasAttachment = current.HasAttachment,

					UpdateDateTime = current.UpdateDateTime,
				})
				;

			return (varEntities);
		}

		/// <summary>
		/// Version: 1.2.2
		/// Update Date: 1394/03/09
		/// Changed In Version: 1.9.6
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public System.Collections.Generic.IList<Models.Cms.Post> Search(int cultureLcid, string keywords, int take)
		{
			if (string.IsNullOrWhiteSpace(keywords))
			{
				return (null);
			}

			keywords = keywords.Trim();

			if (keywords == string.Empty)
			{
				return (null);
			}

			while (keywords.Contains("  "))
			{
				keywords =
					keywords.Replace("  ", " ");
			}

			var varEntities =
				XGetActiveByCultureLcid(cultureLcid);

			foreach (string strKeyword in keywords.Split(' '))
			{
				varEntities =
					varEntities.Where(current => current.Title.Contains(strKeyword));
			}

			var varResult =
				varEntities
				.Take(take)
				.ToList();

			return (varResult);
		}
	}
}
