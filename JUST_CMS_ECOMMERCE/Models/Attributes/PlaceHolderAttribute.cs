namespace Models.Attributes
{
	public class PlaceHolderAttribute :
		System.Attribute, System.Web.Mvc.IMetadataAware
	{
		// نبايد نوشته شود Default Constructor :توجه
		//public PlaceHolderAttribute()
		//{
		//}

		public PlaceHolderAttribute(string placeHolder)
		{
			PlaceHolder = placeHolder;
		}

		public string PlaceHolder { get; set; }

		public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
		{
			if ((string.IsNullOrWhiteSpace(PlaceHolder) == false) &&
				(metadata.AdditionalValues.ContainsKey("placeholder") == false))
			{
				metadata.AdditionalValues.Add("placeholder", PlaceHolder);
			}
		}
	}
}
