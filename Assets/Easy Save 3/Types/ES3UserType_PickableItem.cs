using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("itemID", "amount", "player")]
	public class ES3UserType_PickableItem : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PickableItem() : base(typeof(anogame.inventory.PickableItem)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (anogame.inventory.PickableItem)obj;
			
			writer.WritePrivateField("itemID", instance);
			writer.WritePrivateField("amount", instance);
			writer.WritePrivateFieldByRef("player", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (anogame.inventory.PickableItem)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "itemID":
					instance = (anogame.inventory.PickableItem)reader.SetPrivateField("itemID", reader.Read<System.String>(), instance);
					break;
					case "amount":
					instance = (anogame.inventory.PickableItem)reader.SetPrivateField("amount", reader.Read<System.Int32>(), instance);
					break;
					case "player":
					instance = (anogame.inventory.PickableItem)reader.SetPrivateField("player", reader.Read<anogame.ScriptableReference>(), instance);
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

		public ES3UserType_PickableItemArray() : base(typeof(anogame.inventory.PickableItem[]), ES3UserType_PickableItem.Instance)
		{
			Instance = this;
		}
	}
}