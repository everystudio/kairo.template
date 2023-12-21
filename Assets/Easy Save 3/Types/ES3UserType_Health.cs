using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("currentHP", "isDead")]
	public class ES3UserType_Health : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Health() : base(typeof(Health)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Health)obj;
			
			writer.WritePrivateField("currentHP", instance);
			writer.WritePrivateField("isDead", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Health)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "currentHP":
					instance = (Health)reader.SetPrivateField("currentHP", reader.Read<System.Single>(), instance);
					break;
					case "isDead":
					instance = (Health)reader.SetPrivateField("isDead", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_HealthArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HealthArray() : base(typeof(Health[]), ES3UserType_Health.Instance)
		{
			Instance = this;
		}
	}
}