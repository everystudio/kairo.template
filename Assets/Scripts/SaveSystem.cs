using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public int skill_id;
    public int skill_name;
}

[System.Serializable]
public class CardData
{
    public int card_id;
    public string card_name;
    public string attribute;

    public SkillData skill1;

}

public class SaveData
{
    public string user_name;
    public int coinNum;

    public List<CardData> cardList = new List<CardData>();

}



public class SaveSystem : MonoBehaviour
{
    void Start()
    {

        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Debug.Log("SaveData not found");
            SaveData data = new SaveData();
            data.user_name = "Player";
            data.coinNum = 100;

            CardData card = new CardData();
            card.card_id = 1;
            card.card_name = "スライム";
            data.cardList.Add(card);

            string json = JsonUtility.ToJson(data, true);
            Debug.Log(json);
            /*
          {
            "user_name": "Player",
            "coinNum": 100,
            "cardList": [
                        {
                         "card_id": 1,
                         "card_name": "Card1"
                        }
                        ]
            }
            */

            // {"user_name":"Player","coinNum":100,"cardList":[{"card_id":1,"card_name":"Card1"}]}

            PlayerPrefs.SetString("SaveData", json);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("SaveData 見つかった");
            string json = PlayerPrefs.GetString("SaveData");
            Debug.Log(json);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(data.user_name);
            Debug.Log(data.coinNum);

            foreach (CardData card in data.cardList)
            {
                Debug.Log(card.card_id);
                Debug.Log(card.card_name);
            }
        }

        //"{"user_name":"Player","coinNum":100}"

        /*
        SaveData data2 = JsonUtility.FromJson<SaveData>(json);
        Debug.Log(data2.user_name);
        Debug.Log(data2.coinNum);
        */

    }

}
