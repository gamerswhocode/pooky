using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField]
    Sprite iceBlock;

    bool isFrozen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("freeeze");
        if (!isFrozen)
        {
            if (collision.CompareTag("Attack"))
            {
                var element = collision.GetComponentInParent<Player>().getElement();
                if (element == Element.Ice)
                {
                    convertToIce();
                }
            }
        }
    }

    void convertToIce()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D box = GetComponent<BoxCollider2D>();

            sr.sprite = iceBlock;
            box.isTrigger = false;
    }
}
