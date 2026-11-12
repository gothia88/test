namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/31
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CustomRequiredDateTimeModelBinder : System.Web.Mvc.IModelBinder
	{
		public CustomRequiredDateTimeModelBinder()
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

			Models.ComplexTypes.CustomRequiredDateTime oCustomRequiredDateTime =
				new Models.ComplexTypes.CustomRequiredDateTime();

			if (string.IsNullOrWhiteSpace(strDay) == false)
			{
				intDay = System.Convert.ToInt16(strDay);
				oCustomRequiredDateTime.Day = System.Convert.ToInt16(strDay);
			}

			if (string.IsNullOrWhiteSpace(strYear) == false)
			{
				intYear = System.Convert.ToInt16(strYear);
				oCustomRequiredDateTime.Year = System.Convert.ToInt16(strYear);
			}

			if (string.IsNullOrWhiteSpace(strMonth) == false)
			{
				intMonth = System.Convert.ToInt16(strMonth);
				oCustomRequiredDateTime.Month = System.Convert.ToInt16(strMonth);
			}

			if ((oCustomRequiredDateTime.Day.HasValue == false) || (oCustomRequiredDateTime.Day == 0) ||
				(oCustomRequiredDateTime.Year.HasValue == false) || (oCustomRequiredDateTime.Year == 0) ||
				(oCustomRequiredDateTime.Month.HasValue == false) || (oCustomRequiredDateTime.Month == 0))
			{
				oCustomRequiredDateTime.Value = null;

				if (intDay != 0)
				{
					oCustomRequiredDateTime.Day = intDay;
				}

				if (intYear != 0)
				{
					oCustomRequiredDateTime.Year = intYear;
				}

				if (intMonth != 0)
				{
					oCustomRequiredDateTime.Month = intMonth;
				}

				if ((oCustomRequiredDateTime.Day.HasValue == false) ||
					(oCustomRequiredDateTime.Day == 0))
				{
					string strErrorMessage =
						string.Format(Resources.Messages.Required, Resources.Models.General.Day);

					bindingContext.ModelState.AddModelError
						(strPropertyName + ".Day", strErrorMessage);
				}

				if ((oCustomRequiredDateTime.Year.HasValue == false) ||
					(oCustomRequiredDateTime.Year == 0))
				{
					string strErrorMessage =
						string.Format(Resources.Messages.Required, Resources.Models.General.Year);

					bindingContext.ModelState.AddModelError
						(strPropertyName + ".Year", strErrorMessage);
				}

				if ((oCustomRequiredDateTime.Month.HasValue == false) ||
					(oCustomRequiredDateTime.Month == 0))
				{
					string strErrorMessage =
						string.Format(Resources.Messages.Required, Resources.Models.General.Month);

					bindingContext.ModelState.AddModelError
						(strPropertyName + ".Month", strErrorMessage);
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
						oCustomRequiredDateTime.Value =
							Dtx.Calendar.Convert.PersionToCivil
								(oCustomRequiredDateTime.Year.Value,
								oCustomRequiredDateTime.Month.Value,
								oCustomRequiredDateTime.Day.Value);

						break;
					}

					default:
					{
						oCustomRequiredDateTime.Value =
							new System.DateTime
								(oCustomRequiredDateTime.Year.Value,
								oCustomRequiredDateTime.Month.Value,
								oCustomRequiredDateTime.Day.Value);

						break;
					}
				}
			}

			return (oCustomRequiredDateTime);
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
