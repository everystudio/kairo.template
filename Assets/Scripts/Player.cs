using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private bool isToolAnimation = false;

    [SerializeField] private Transform toolTransform;
    [SerializeField] private DamageVolume toolDamageVolume;

    private void Start()
    {
        RemoveTool();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * speed * Time.deltaTime;

        /*
        if (isSwinging == false && Input.GetKeyDown(KeyCode.Space))
        {
            isSwinging = true;
            GetComponent<Animator>().SetTrigger("swing");
        }
        */

    }

    public void SetTool(ToolItem toolItem)
    {
        toolTransform.gameObject.SetActive(true);
        toolTransform.GetComponent<SpriteRenderer>().sprite = toolItem.GetIcon();
    }

    public void RemoveTool()
    {
        toolTransform.gameObject.SetActive(false);
    }
    public bool ToolAnimationStart(string triggerName)
    {
        if (isToolAnimation)
        {
            return false;
        }
        isToolAnimation = true;
        toolDamageVolume.Refresh();
        GetComponent<Animator>().SetTrigger(triggerName);
        return true;
    }

    private void OnAnimationSwingHit()
    {
        //Debug.Log("swing hit");
    }
    private void OnAnimationSwingEnd()
    {
        isToolAnimation = false;
    }
}
