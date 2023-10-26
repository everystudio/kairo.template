using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private float delay = 0f;
    [SerializeField] private AudioClip coinSound;

    [SerializeField] private EventCollectableItem OnCollectedItem;

    [System.Serializable]
    public class References
    {
        public Rigidbody2D rigidBody2D;
        public SpriteRenderer spriteRenderer;
        public TextMeshPro amountText;
        public BoxCollider2D boxCollider2D;
    }

    [System.Serializable]
    public class Configuration
    {
        public int amount;
        public float moveSpeed = 2;
        public float activationWait = 1;
    }
    public float PickupDistance()
    {
        return references.boxCollider2D.size.magnitude * 0.5f;
    }
    public void Configure(int amount)
    {
        configuration.amount = amount;
        //isLooted = false;
        Refresh();
    }

    [SerializeField]
    private References references;

    [SerializeField]
    private Configuration configuration;
    //private bool isLooted;

    private void Refresh()
    {

    }

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
