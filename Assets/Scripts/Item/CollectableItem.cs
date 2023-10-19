using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private float delay = 0f;
    [SerializeField] private AudioClip coinSound;

    [SerializeField] private EventCollectableItem OnCollectedItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DispatchAction());
        }
    }

    private IEnumerator DispatchAction()
    {
        OnCollectedItem.Invoke(new CollectableItemData()
        {
            item_id = 1,
            amount = this.amount
        });
        yield return new WaitForSeconds(delay);
        if (coinSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(coinSound);
        }
        Destroy(gameObject);
    }

}
