using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool isLit = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Attack"))
        {
            var element = other.GetComponentInParent<Player>().getElement();
            if (element == Element.Fire)
            {
                isLit = true;
            }
            else if (element == Element.Water)
            {
                isLit = false;
            }
        }
    }
}
