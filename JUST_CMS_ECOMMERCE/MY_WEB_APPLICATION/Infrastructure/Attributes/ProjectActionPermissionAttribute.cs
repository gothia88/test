using System.Linq;
using System.Data.Entity;

namespace Infrastructure.Attributes
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1394/01/04
	/// Changed In Version: 1.8.4
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectActionPermissionAttribute : ProjectControllerPermissionAttribute
	{
		public ProjectActionPermissionAttribute
			(string resourceName,
			System.Type resourceType = null,
			bool isVisibleJustForProgrammer = true,
			bool forceToChangeNameAfterSyncing = false,
			bool forceToChangeAccessTypeAfterSyncing = false,
			Models.Enums.AccessTypes accessType = Models.Enums.AccessTypes.Special)
			: base(resourceName: resourceName,
				resourceType: resourceType,
				isVisibleJustForProgrammer: isVisibleJustForProgrammer,
				forceToChangeNameAfterSyncing: forceToChangeNameAfterSyncing)
		{
			AccessType = accessType;
			ForceToChangeAccessTypeAfterSyncing = forceToChangeAccessTypeAfterSyncing;
		}

		public Models.Enums.AccessTypes AccessType { get; set; }

		public bool ForceToChangeAccessTypeAfterSyncing { get; set; }
	}
}
