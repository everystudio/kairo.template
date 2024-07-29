using UnityEngine;
using TMPro;

public class ResizeImageToFitText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private RectTransform rectTransform;

    private Transform componentA;




    void Start()
    {
        GameObject a = GameObject.FindWithTag("tag");
        if (a != null)
        {
            componentA = a.GetComponent<Transform>();
        }










        rectTransform = GetComponent<RectTransform>();
        AdjustImageSize();
    }

    void Update()
    {
        AdjustImageSize();
    }

    void AdjustImageSize()
    {
        if (textComponent != null)
        {
            // TextMeshProのBoundsを取得
            var textBounds = textComponent.textBounds;

            // TextMeshProのサイズに基づいてImageのサイズを調整
            rectTransform.sizeDelta = new Vector2(textBounds.size.x, textBounds.size.y);
        }
    }
}
