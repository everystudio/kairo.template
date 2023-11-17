using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using anogame.inventory;

public class FarmTree : MonoBehaviour
{
    [SerializeField] private bool isStump = false;
    [SerializeField] private float stumpHealth = 2f;

    [SerializeField] private Transform bodyTransform;

    [SerializeField] private ItemDropper bodyItemDropper;
    [SerializeField] private ItemDropper stumpItemDropper;

    private void Start()
    {
        var bodyRenderer = bodyTransform.GetComponent<SpriteRenderer>();
        // bodyRendererの画像の中心座標を取得
        var bodyCenter = bodyRenderer.sprite.bounds.center;

        Debug.Log(transform.position);
        Debug.Log(bodyCenter);

    }

    public void Shake()
    {
        transform.DOShakePosition(0.5f, 0.1f, 10, 90f, false, true);
    }

    public void Cut()
    {
        if (isStump == false)
        {
            isStump = true;
            bodyTransform.DOLocalRotate(new Vector3(0, 0, -90), 2.5f).SetEase(Ease.OutBounce).onComplete += () =>
            {
                // 画像の中心座標をグローバル座標系でオフセットを取得する
                Vector2 offset;
                var bodyRenderer = bodyTransform.GetComponent<SpriteRenderer>();
                offset = bodyRenderer.sprite.bounds.center;

                //bodyRendererは回転しているためそのままオフセットを利用できないので変換する
                offset = bodyTransform.TransformPoint(offset);

                bodyItemDropper.SetOffset(offset);
                bodyItemDropper.DropItem();
                Destroy(bodyTransform.gameObject);
            };
            GetComponent<Health>().Revive(stumpHealth);
        }
        else
        {
            stumpItemDropper.DropItem();
            Destroy(gameObject);
        }
    }



}
