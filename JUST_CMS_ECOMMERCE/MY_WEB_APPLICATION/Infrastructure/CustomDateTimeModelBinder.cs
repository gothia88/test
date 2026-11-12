namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/31
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CustomDateTimeModelBinder : System.Web.Mvc.IModelBinder
	{
		public CustomDateTimeModelBinder()
		{
		}

		public object BindModel
			(System.Web.Mvc.ControllerContext controllerContext,
			System.Web.Mvc.ModelBindingContext bindingContext)
		{
			int intDay = 0;
			int intYear = 0;
			int intMonth = 0;

			string strPropertyName = bindingContext.ModelName;

			System.Web.HttpRequestBase oRequest =
				controllerContext.HttpContext.Request;

			//string strDay = oRequest.Params[strPropertyName + ".Day"];
			//string strYear = oRequest.Params[strPropertyName + ".Year"];
			//string strMonth = oRequest.Params[strPropertyName + ".Month"];

			string strDay = oRequest.Unvalidated[strPropertyName + ".Day"];
			string strYear = oRequest.Unvalidated[strPropertyName + ".Year"];
			string strMonth = oRequest.Unvalidated[strPropertyName + ".Month"];

			Models.ComplexTypes.CustomDateTime oCustomDateTime =
				new Models.ComplexTypes.CustomDateTime();

			if (string.IsNullOrWhiteSpace(strDay) == false)
			{
				intDay = System.Convert.ToInt16(strDay);
				oCustomDateTime.Day = System.Convert.ToInt16(strDay);
			}

			if (string.IsNullOrWhiteSpace(strYear) == false)
			{
				intYear = System.Convert.ToInt16(strYear);
				oCustomDateTime.Year = System.Convert.ToInt16(strYear);
			}

			if (string.IsNullOrWhiteSpace(strMonth) == false)
			{
				intMonth = System.Convert.ToInt16(strMonth);
				oCustomDateTime.Month = System.Convert.ToInt16(strMonth);
			}

			if ((oCustomDateTime.Day.HasValue == false) || (oCustomDateTime.Day == 0) ||
				(oCustomDateTime.Year.HasValue == false) || (oCustomDateTime.Year == 0) ||
				(oCustomDateTime.Month.HasValue == false) || (oCustomDateTime.Month == 0))
			{
				oCustomDateTime.Value = null;

				if (intDay != 0)
				{
					oCustomDateTime.Day = intDay;
				}

				if (intYear != 0)
				{
					oCustomDateTime.Year = intYear;
				}

				if (intMonth != 0)
				{
					oCustomDateTime.Month = intMonth;
				}
			}
			else
			{
				int intCulture =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				switch (intCulture)
				{
					case 1065:
					{
						oCustomDateTime.Value =
							Dtx.Calendar.Convert.PersionToCivil
								(oCustomDateTime.Year.Value,
								oCustomDateTime.Month.Value,
								oCustomDateTime.Day.Value);

						break;
					}

					default:
					{
						oCustomDateTime.Value =
							new System.DateTime
								(oCustomDateTime.Year.Value,
								oCustomDateTime.Month.Value,
								oCustomDateTime.Day.Value);

						break;
					}
				}
			}

			return (oCustomDateTime);
		}
	}

	//public class XCustomDateTimeModelBinder : System.Web.Mvc.DefaultModelBinder
	//{
	//	public XCustomDateTimeModelBinder()
	//	{
	//	}

	//	public override object BindModel
	//		(System.Web.Mvc.ControllerContext controllerContext,
	//		System.Web.Mvc.ModelBindingContext bindingContext)
	//	{
	//		return (base.BindModel(controllerContext, bindingContext));
	//	}
	//}
}
