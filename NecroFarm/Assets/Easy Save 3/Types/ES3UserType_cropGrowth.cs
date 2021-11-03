using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("growthTime", "plantTime", "initialPlacement", "selfRef", "gridObj")]
	public class ES3UserType_cropGrowth : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_cropGrowth() : base(typeof(cropGrowth)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (cropGrowth)obj;
			
			writer.WritePrivateField("growthTime", instance);
			writer.WritePrivateField("plantTime", instance);
			writer.WritePrivateField("initialPlacement", instance);
			writer.WritePropertyByRef("selfRef", instance.selfRef);
			writer.WritePrivateFieldByRef("gridObj", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (cropGrowth)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "growthTime":
					reader.SetPrivateField("growthTime", reader.Read<System.Double>(), instance);
					break;
					case "plantTime":
					reader.SetPrivateField("plantTime", reader.Read<System.DateTime>(), instance);
					break;
					case "initialPlacement":
					reader.SetPrivateField("initialPlacement", reader.Read<System.Boolean>(), instance);
					break;
					case "selfRef":
						instance.selfRef = reader.Read<ScriptableObjects>();
						break;
					case "gridObj":
					reader.SetPrivateField("gridObj", reader.Read<UnityEngine.GameObject>(), instance);
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