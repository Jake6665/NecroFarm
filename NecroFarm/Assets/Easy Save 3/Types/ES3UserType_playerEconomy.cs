using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("playerMoney", "numBones")]
	public class ES3UserType_playerEconomy : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_playerEconomy() : base(typeof(playerEconomy)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (playerEconomy)obj;
			
			writer.WritePrivateField("playerMoney", instance);
			writer.WritePrivateField("numBones", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (playerEconomy)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "playerMoney":
					reader.SetPrivateField("playerMoney", reader.Read<System.Int32>(), instance);
					break;
					case "numBones":
					reader.SetPrivateField("numBones", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_playerEconomyArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_playerEconomyArray() : base(typeof(playerEconomy[]), ES3UserType_playerEconomy.Instance)
		{
			Instance = this;
		}
	}
}