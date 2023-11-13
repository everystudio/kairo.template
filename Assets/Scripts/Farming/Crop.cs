using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{
    private SpriteRenderer model;
    private float growTime;

    [SerializeField] private Tilemap targetTilemap;
    private Vector3Int gridPosition;

    [System.Serializable]
    public class GrowSprites
    {
        public Sprite sprite;
        public int seconds;
    }
    [SerializeField] private GrowSprites[] growSprites;

    private void Awake()
    {
        // 子供にあるSpriteRendererを取得
        model = GetComponentInChildren<SpriteRenderer>();
    }

    public void Destroy()
    {
        Debug.Log("destroy self");
        Destroy(this.gameObject);
    }

    public void Initialize(Tilemap targetTilemap, Vector3Int gridPosition)
    {
        this.targetTilemap = targetTilemap;
        this.gridPosition = gridPosition;
        transform.position = targetTilemap.GetCellCenterWorld(gridPosition);
    }

    private bool IsWet()
    {
        if (targetTilemap == null)
        {
            return false;
        }

        int animationFrame = targetTilemap.GetAnimationFrame(gridPosition);
        if (animationFrame == 1)
        {
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (targetTilemap == null)
        {
            return;
        }

        if (IsWet() == false)
        {
            return;
        }

        growTime += Time.deltaTime;

        // どのスプライトを表示するかを決める
        for (int i = 0; i < growSprites.Length; i++)
        {
            if (growSprites[i].seconds < growTime)
            {
                model.sprite = growSprites[i].sprite;
            }
        }

    }


}
