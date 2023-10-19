using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }


}
