using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag + " from trigger");
        if (collision.CompareTag("Attack"))
        {
            var element = collision.GetComponentInParent<Player>().getElement();
            if (element == Element.Fire)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
