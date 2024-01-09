using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isInitialized")]
	public class ES3UserType_ObjectInitializer : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ObjectInitializer() : base(typeof(ObjectInitializer)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ObjectInitializer)obj;
			
			writer.WritePrivateField("isInitialized", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ObjectInitializer)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isInitialized":
					instance = (ObjectInitializer)reader.SetPrivateField("isInitialized", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ObjectInitializerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ObjectInitializerArray() : base(typeof(ObjectInitializer[]), ES3UserType_ObjectInitializer.Instance)
		{
			Instance = this;
		}
	}
}