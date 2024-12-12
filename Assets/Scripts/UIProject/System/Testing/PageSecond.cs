using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class PageSecond : PageBase
{
    [SerializeField] private PageBase secondPagePrefab;

    private PageBase secondPage;



    public class ArgsSecond
    {
        public string message;
    }

    [SerializeField] private TextMeshProUGUI messageText;

    public override async UniTask PreOpen()
    {
        ArgsSecond args = Args as ArgsSecond;
        messageText.text = args.message;
    }

    public void OnClickLeft()
    {
        Debug.Log("OnClickLeft");

        secondPage = Instantiate(secondPagePrefab, transform);
        secondPage.Args = new ArgsSecond { message = "Left" };
        secondPage.Open();

    }

    public void OnClickRight()
    {
        Debug.Log("OnClickRight");

        secondPage = Instantiate(secondPagePrefab, transform);
        secondPage.Args = new ArgsSecond { message = "Right" };
        secondPage.Open();

    }

    public void OnClickClose()
    {
        Debug.Log("OnClickClose");

        Close();
    }





}
