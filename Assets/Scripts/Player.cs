using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private bool isSwinging = false;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * speed * Time.deltaTime;

        if (isSwinging == false && Input.GetKeyDown(KeyCode.Space))
        {
            isSwinging = true;
            GetComponent<Animator>().SetTrigger("swing");
        }

    }

    private void OnAnimationSwingHit()
    {
        //Debug.Log("swing hit");
    }
    private void OnAnimationSwingEnd()
    {
        //Debug.Log("swing end");
        isSwinging = false;
    }
}
