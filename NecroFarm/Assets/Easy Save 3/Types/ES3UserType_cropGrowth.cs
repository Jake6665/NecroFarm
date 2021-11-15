using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("plantTime", "finshedGrowing", "initialPlacement", "gridObj", "hasRestored")]
	public class ES3UserType_cropGrowth : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_cropGrowth() : base(typeof(cropGrowth)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (cropGrowth)obj;
			
			writer.WritePrivateField("plantTime", instance);
			writer.WritePrivateField("finshedGrowing", instance);
			writer.WritePrivateField("initialPlacement", instance);
			writer.WritePrivateFieldByRef("gridObj", instance);
			writer.WritePrivateField("hasRestored", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (cropGrowth)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "plantTime":
					reader.SetPrivateField("plantTime", reader.Read<System.DateTime>(), instance);
					break;
					case "finshedGrowing":
					reader.SetPrivateField("finshedGrowing", reader.Read<System.Boolean>(), instance);
					break;
					case "initialPlacement":
					reader.SetPrivateField("initialPlacement", reader.Read<System.Boolean>(), instance);
					break;
					case "gridObj":
					reader.SetPrivateField("gridObj", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "hasRestored":
					reader.SetPrivateField("hasRestored", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_cropGrowthArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_cropGrowthArray() : base(typeof(cropGrowth[]), ES3UserType_cropGrowth.Instance)
		{
			Instance = this;
		}
	}
}