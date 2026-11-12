using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/10/26
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AccessToEntityResult<T> where T : Models.BaseEntity
	{
		public AccessToEntityResult()
		{

		}

		public T Entity { get; set; }

		public System.Web.Mvc.ActionResult ActionResult { get; set; }
	}
}
