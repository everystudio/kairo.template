using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFirst : PageBase
{
    [SerializeField] private PageBase secondPagePrefab;

    private PageBase secondPage;

    public void OnClick()
    {
        Debug.Log("OnClick");

        if (secondPage == null)
        {
            secondPage = Instantiate(secondPagePrefab, transform);
            secondPage.Args = new PageSecond.ArgsSecond { message = "Hello World" };
            secondPage.Open();
        }


    }
}
