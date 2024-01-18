using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("itemID", "amount", "player")]
	public class ES3UserType_PickableItem : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PickableItem() : base(typeof(PickableItem)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PickableItem)obj;
			
			writer.WritePrivateField("itemID", instance);
			writer.WritePrivateField("amount", instance);
			writer.WritePrivateFieldByRef("player", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PickableItem)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "itemID":
					instance = (PickableItem)reader.SetPrivateField("itemID", reader.Read<System.String>(), instance);
					break;
					case "amount":
					instance = (PickableItem)reader.SetPrivateField("amount", reader.Read<System.Int32>(), instance);
					break;
					case "player":
					instance = (PickableItem)reader.SetPrivateField("player", reader.Read<anogame.ScriptableReference>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PickableItemArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PickableItemArray() : base(typeof(PickableItem[]), ES3UserType_PickableItem.Instance)
		{
			Instance = this;
		}
	}
}