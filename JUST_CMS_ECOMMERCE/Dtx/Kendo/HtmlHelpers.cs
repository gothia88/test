using System.Linq;
using System.Linq.Dynamic;

namespace Dtx.Kendo
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1394/02/05
	/// Changed In Version: 1.9.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class HtmlHelpers
	{
		static HtmlHelpers()
		{
		}

		/// <summary>
		/// Version: 1.0.3
		/// Update Date: 1394/02/05
		/// Changed In Version: 1.9.0
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		public static GridResult<T> ParseGridData<T>(System.Linq.IQueryable<T> collection)
		{
			GridPost oGridPost = new GridPost();

			foreach (GridSort oGridSort in oGridPost.SortCollection)
			{
				collection =
					collection.OrderBy(oGridSort.Field + " " + oGridSort.Direction);
			}

			System.Collections.Generic.List<T> oGridData;

			try
			{
				oGridData =
					collection.Skip(oGridPost.Skip).Take(oGridPost.Take).ToList();
			}
			catch
			{
				oGridData =
					collection.OrderBy("ID ASC").Skip(oGridPost.Skip).Take(oGridPost.Take).ToList();
			}

			GridResult<T> oGridResult = new GridResult<T>();

			oGridResult.Data = oGridData;
			oGridResult.Total = collection.Count();

			return (oGridResult);
		}
	}
}
