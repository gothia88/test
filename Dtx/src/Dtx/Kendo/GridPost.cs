namespace Dtx.Kendo
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/07/26
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	/// <remarks>
	/// The .NET Framework original read the Kendo grid request directly from
	/// System.Web.HttpContext.Current.Request. The .NET 10 port is web-framework
	/// agnostic: call GridPost.Parse(...) with a request-parameter lookup.
	/// </remarks>
	public class GridPost : System.Object
	{
		public GridPost()
		{
		}

		/// <summary>
		/// Builds a GridPost from a Kendo UI grid request. Pass a lookup that
		/// reads a request parameter by name (form/query) and returns null when
		/// the parameter is absent.
		/// </summary>
		public static GridPost Parse(System.Func<string, string> getValue)
		{
			GridPost oGridPost = new GridPost();

			if (getValue == null)
			{
				return (oGridPost);
			}

			// **************************************************
			int intIndex = 0;
			string strSortField = null;

			do
			{
				string strSortDirKeyName =
					string.Format("sort[{0}][dir]", intIndex);

				string strSortFieldKeyName =
					string.Format("sort[{0}][field]", intIndex);

				string strSortDir =
					getValue(strSortDirKeyName);

				strSortField =
					getValue(strSortFieldKeyName);

				if (string.IsNullOrWhiteSpace(strSortField) == false)
				{
					if (string.IsNullOrWhiteSpace(strSortDir))
					{
						strSortDir = "asc";
					}

					oGridPost.SortCollection.Add(new GridSort(strSortField, strSortDir));
				}

				intIndex++;
			}
			while (string.IsNullOrWhiteSpace(strSortField) == false);
			// **************************************************

			// **************************************************
			oGridPost.Page = 1;
			try
			{
				oGridPost.Page =
					System.Convert.ToInt32(getValue("page"));
			}
			catch { }

			oGridPost.Skip = 0;
			try
			{
				oGridPost.Skip =
					System.Convert.ToInt32(getValue("skip"));
			}
			catch { }

			oGridPost.Take = 10;
			try
			{
				oGridPost.Take =
					System.Convert.ToInt32(getValue("take"));
			}
			catch { }

			oGridPost.PageSize = 10;
			try
			{
				oGridPost.PageSize =
					System.Convert.ToInt32(getValue("pageSize"));
			}
			catch { }
			// **************************************************

			return (oGridPost);
		}

		public int Page { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public int PageSize { get; set; }

		private System.Collections.Generic.List<GridSort> _sortCollection;
		public System.Collections.Generic.List<GridSort> SortCollection
		{
			get
			{
				if (_sortCollection == null)
				{
					_sortCollection =
						new System.Collections.Generic.List<GridSort>();
				}
				return (_sortCollection);
			}
		}
	}
}
