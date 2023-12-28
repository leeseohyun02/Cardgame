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

        if(gameManager.I.firstCard == null) // ù��° ī�尡 ������쿡 ���� ���� ���� ������Ʈ�� �۽�Ʈ ī��
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject; // ���� ���� ī�尡 ������ ī��
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
        Invoke("closeCardInvoke", 1.0f); // ī�尡 ���������� �ӵ��� ������ �� ����!
    }

    void closeCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }


}
