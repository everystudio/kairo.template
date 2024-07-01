using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{
    private SpriteRenderer model;
    //private float growTime;

    [SerializeField] private Tilemap targetTilemap;
    private Vector3Int gridPosition;

    [System.Serializable]
    public class GrowSprites
    {
        public Sprite sprite;
        public int days;
        public float growTime;
    }
    [SerializeField] private GrowSprites[] growSprites;

    // 経過した日数
    public int growDays;
    public float growTime;

    private bool IsGrowup()
    {
        //Debug.Log(growSprites[growSprites.Length - 1].days);
        //Debug.Log(growDays);
        if (growSprites[growSprites.Length - 1].growTime <= growTime)
        {
            return true;
        }
        return false;
    }

    public bool Harvest()
    {
        //Debug.Log("Harvest");
        if (IsGrowup() == false)
        {
            return false;
        }
        GetComponent<Health>().Kill();
        return true;
    }



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
        if (animationFrame == (int)Plowland.PlowTileState.Wet)
        {
            return true;
        }

        return false;
    }

    public void GrowupTime(float deltaTime)
    {
        //Debug.Log("GrowupTime");


        if (IsWet() == false)
        {
            return;
        }

        growTime += deltaTime;
        for (int i = 0; i < growSprites.Length; i++)
        {
            if (growSprites[i].growTime <= growTime)
            {
                model.sprite = growSprites[i].sprite;
            }
        }
    }



    public void GrowupDay(int addDay)
    {
        /*
        濡れているかどうかは自分で判断させないようにする
        if (IsWet() == false)
        {
            return;
        }
        */

        growDays += addDay;
        for (int i = 0; i < growSprites.Length; i++)
        {
            if (growSprites[i].days <= growDays)
            {
                model.sprite = growSprites[i].sprite;
            }
        }
    }

    private void UpdateVisual()
    {

    }

    /*
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

            //growTime += Time.deltaTime;

            // どのスプライトを表示するかを決める
            for (int i = 0; i < growSprites.Length; i++)
            {
                if (growSprites[i].days < growDays)
                {
                    model.sprite = growSprites[i].sprite;
                }
            }

        }
    */

}
