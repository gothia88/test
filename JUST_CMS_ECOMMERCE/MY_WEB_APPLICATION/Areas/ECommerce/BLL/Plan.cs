using System.Linq;

namespace MY_WEB_APPLICATION.Areas.ECommerce.BLL
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/01/18
	/// Changed In Version: 1.8.8
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Plan
	{
		static Plan()
		{
		}

		public static Models.ECommerce.Plan GetFreePlan()
		{
			DAL.UnitOfWork oUnitOfWork = null;
			Models.ECommerce.Plan oPlan = null;

			try
			{
				oUnitOfWork =
					new DAL.UnitOfWork();

				oPlan =
					oUnitOfWork.ECommerceUnitOfWork.PlanRepository
					.Get()
					.Where(current => current.Price == 0)
					.FirstOrDefault()
					;

				if (oPlan != null)
				{
					oPlan.IsActive = true;
					oPlan.IsDeleted = false;
				}
				else
				{
					oPlan =
						new Models.ECommerce.Plan();

					oPlan.Ordering = 1;

					oUnitOfWork.ECommerceUnitOfWork.PlanRepository.Insert(oPlan);
				}

				oUnitOfWork.Save();
			}
			catch
			{
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}

			return (oPlan);
		}

		public static void SetPlanForUser(Models.User user, Models.ECommerce.Plan plan)
		{
			if (user == null)
			{
				return;
			}

			if (plan == null)
			{
				user.PlanId = null;
				user.StartPlanDateTime.Value = null;
				user.FinishPlanDateTime.Value = null;
			}
			else
			{
				user.PlanId = plan.Id;
				user.StartPlanDateTime.Value = Models.Utility.Now;
				user.FinishPlanDateTime.Value =
					Models.Utility.Now.AddMonths(plan.DurationInMonth);
			}
		}

		public static bool CheckPlanForUser(Models.User user)
		{
			if (user.Role.IsSiteResponsible)
			{
				if (user.PlanId.HasValue)
				{
					return (false);
				}

				if (user.StartPlanDateTime.Value.HasValue)
				{
					return (false);
				}

				if (user.FinishPlanDateTime.Value.HasValue)
				{
					return (false);
				}
			}
			else
			{
				if (user.PlanId.HasValue == false)
				{
					return (false);
				}

				if (user.StartPlanDateTime.Value.HasValue == false)
				{
					return (false);
				}

				if (user.FinishPlanDateTime.Value.HasValue == false)
				{
					return (false);
				}

				if (user.FinishPlanDateTime.Value < Models.Utility.Now)
				{
					return (false);
				}
			}

			return (true);
		}

		public static void CheckAndSetDefaultPlanForUser(Models.User user)
		{
			bool blnResult =
				CheckPlanForUser(user);

			if (blnResult == false)
			{
				if (user.Role.IsSiteResponsible)
				{
					SetPlanForUser(user, plan: null);
				}
				else
				{
					Models.ECommerce.Plan oFreePlan = GetFreePlan();

					SetPlanForUser(user, plan: oFreePlan);
				}
			}
		}
	}
}
