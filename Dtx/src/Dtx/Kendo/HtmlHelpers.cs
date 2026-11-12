using System.Linq;
using System.Linq.Dynamic.Core;

namespace Dtx.Kendo
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/02/05
	/// Changed In Version: 1.9.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	/// <remarks>
	/// The .NET Framework original created a GridPost inside this method (which
	/// read System.Web.HttpContext). The .NET 10 port accepts a GridPost built
	/// from the request via GridPost.Parse(...).
	/// </remarks>
	public static class HtmlHelpers
	{
		static HtmlHelpers()
		{
		}

		public static GridResult<T> ParseGridData<T>
			(System.Linq.IQueryable<T> collection, GridPost gridPost)
		{
			if (gridPost == null)
			{
				gridPost = new GridPost();
			}

			foreach (GridSort oGridSort in gridPost.SortCollection)
			{
				collection =
					collection.OrderBy(oGridSort.Field + " " + oGridSort.Direction);
			}

			System.Collections.Generic.List<T> oGridData;

			try
			{
				oGridData =
					collection.Skip(gridPost.Skip).Take(gridPost.Take).ToList();
			}
			catch
			{
				oGridData =
					collection.OrderBy("ID ASC").Skip(gridPost.Skip).Take(gridPost.Take).ToList();
			}

			GridResult<T> oGridResult = new GridResult<T>();

			oGridResult.Data = oGridData;
			oGridResult.Total = collection.Count();

			return (oGridResult);
		}
	}
}
