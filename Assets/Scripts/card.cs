using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class card : MonoBehaviour
{
    public AudioClip flip;
    public AudioSource audioSource;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {
        audioSource.PlayOneShot(flip);

        anim.SetBool("IsOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if(gameManager.I.firstCard == null) // 첫번째 카드가 없을경우에 현재 뽑은 게임 오브젝트가 퍼스트 카드
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject; // 현재 뽑은 카드가 세컨드 카드
            gameManager.I.isMatched();
        }

        transform.Find("back").gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f); // 카드가 뒤집어지는 속도를 조절할 수 있음!
    }

    void closeCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }


}
