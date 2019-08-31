using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            position.x -= speed * Time.deltaTime;  
            Debug.Log("moving left");
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            position.x += speed * Time.deltaTime;
            Debug.Log("moving right");
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector2.up * jumpForce);
        }
        playerRigidbody.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
