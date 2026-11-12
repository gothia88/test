namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/02/16
	/// Changed In Version: 1.9.2
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class BaseControllerWithUnitOfWork : BaseController
	{
		public BaseControllerWithUnitOfWork()
		{
		}

		private DAL.UnitOfWork _unitOfWork;

		protected virtual DAL.UnitOfWork UnitOfWork
		{
			get
			{
				if (_unitOfWork == null)
				{
					_unitOfWork = new DAL.UnitOfWork();
				}
				return (_unitOfWork);
			}
		}

		/// <summary>
		/// Version: 1.0.2
		/// Update Date: 1394/02/16
		/// Changed In Version: 1.9.2
		/// Developer: Mr. Dariush Tasdighi
		/// </summary>
		protected virtual void DisposeCurrentUnitOfWorkAndCreateANewOne()
		{
			if (_unitOfWork != null)
			{
				_unitOfWork.Dispose();
				_unitOfWork = null;
			}

			_unitOfWork =
				new DAL.UnitOfWork();
		}

		protected override void Dispose(bool disposing)
		{
			if (_unitOfWork != null)
			{
				_unitOfWork.Dispose();
				_unitOfWork = null;
			}

			base.Dispose(disposing);
		}
	}
}
