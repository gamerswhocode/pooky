using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastwall : MonoBehaviour
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
        if(collision.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.killPlayer(100);
        }
    }
}
