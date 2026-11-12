using System.Linq;
using System.Data.Entity;

namespace Infrastructure.Attributes
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectControllerPermissionAttribute : System.Attribute
	{
		public ProjectControllerPermissionAttribute
			(string resourceName,
			System.Type resourceType = null,
			bool isVisibleJustForProgrammer = true,
			bool forceToChangeNameAfterSyncing = false)
		{
			ResourceName = resourceName;
			ResourceType = resourceType;

			IsVisibleJustForProgrammer = isVisibleJustForProgrammer;
			ForceToChangeNameAfterSyncing = forceToChangeNameAfterSyncing;
		}

		public string ResourceName { get; set; }

		public System.Type ResourceType { get; set; }

		public bool IsVisibleJustForProgrammer { get; set; }

		public bool ForceToChangeNameAfterSyncing { get; set; }
	}
}
