using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class PageBase : MonoBehaviour
{
    private CanvasGroup MyCanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            return canvasGroup;
        }
    }
    private CanvasGroup canvasGroup;

    public object Args { get; set; }

    public virtual async UniTask PreOpen() { }

    public async void Open()
    {
        await PreOpen();

        Debug.Log("Open - start");

        MyCanvasGroup.interactable = false;
        // CanvasGroupのアルファ値をUniTaskを使って0から1にする
        float duration = 0.25f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            MyCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            await UniTask.Yield();
        }

        MyCanvasGroup.alpha = 1f;

        MyCanvasGroup.interactable = true;
        Debug.Log("Open - end");
    }


    public async void Close()
    {
        Debug.Log("Close - start");

        MyCanvasGroup.interactable = false;
        // CanvasGroupのアルファ値をUniTaskを使って1から0にする
        float duration = 0.25f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            MyCanvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / duration);
            await UniTask.Yield();
        }

        MyCanvasGroup.alpha = 0f;

        MyCanvasGroup.interactable = false;

        Destroy(gameObject);
        Debug.Log("Close - end");
    }
}
