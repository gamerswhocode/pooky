using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{


    [SerializeField]
    public AnimationClip openGate;

    [SerializeField]
    int NextSceneIndex;

    Animator anim;
    
    

    // Update is called once per frame
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.StopPlayback();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YesPlayer");
            var player = other.gameObject.GetComponent<Player>();
            if(player.playerHasKey)
            {
                TriggerLevelEnd();
            }
        }
    }

    void TriggerLevelEnd()
    {
        AudioSource audioS = GetComponent<AudioSource>();
        audioS.Play();
        
        anim.Play(openGate.name, -1, 0f);
        StartCoroutine("LevelTransition");


    }


    IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(NextSceneIndex);
    }
}
