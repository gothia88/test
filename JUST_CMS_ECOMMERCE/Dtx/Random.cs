namespace Dtx
{
	public static class Random
	{
		public static string GetRandomString(int length)
		{
			return (GetRandomString(length, Enums.TextCases.Normal));
		}

		public static string GetRandomString(int length, Enums.TextCases textCase)
		{
			string strResult = string.Empty;
			string strGuid = Guid.NewGuidWithoutDash;

			int intLength = strGuid.Length;

			System.Text.StringBuilder oText =
				new System.Text.StringBuilder(length);

			System.Random oRandom =
				new System.Random(System.DateTime.Now.Millisecond);

			int intCount = 1;
			while (intCount <= length)
			{
				int intIndex = oRandom.Next(0, intLength); // Note: Do not write [intLength - 1]!
				oText.Append(strGuid[intIndex]);

				intCount++;
			}

			switch (textCase)
			{
				case Enums.TextCases.Normal:
				{
					strResult = oText.ToString();
					break;
				}

				case Enums.TextCases.LowerCase:
				{
					strResult = oText.ToString().ToLower();
					break;
				}

				case Enums.TextCases.UpperCase:
				{
					strResult = oText.ToString().ToUpper();
					break;
				}
			}

			return (strResult);
		}
	}
}
