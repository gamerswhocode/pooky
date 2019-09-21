using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Image component;

    

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, sprites.Length);
        component.sprite = sprites[index];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Level1Scene");
        }
        
    }
}
