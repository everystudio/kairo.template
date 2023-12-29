using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("capacity", "inventorySerialID")]
	public class ES3UserType_ChestInventory : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ChestInventory() : base(typeof(ChestInventory)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ChestInventory)obj;
			
			writer.WriteProperty("capacity", instance.capacity, ES3Type_int.Instance);
			writer.WritePrivateField("inventorySerialID", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ChestInventory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "capacity":
						instance.capacity = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "inventorySerialID":
					instance = (ChestInventory)reader.SetPrivateField("inventorySerialID", reader.Read<System.String>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ChestInventoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ChestInventoryArray() : base(typeof(ChestInventory[]), ES3UserType_ChestInventory.Instance)
		{
			Instance = this;
		}
	}
}