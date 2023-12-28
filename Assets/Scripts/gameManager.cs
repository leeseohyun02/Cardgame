using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class gameManager : MonoBehaviour
{
    public Text timeText;
    public Text teamNameText;
    public Text matchedCountText;

    public GameObject endText;
    public GameObject card;

    float time = 0.0f;
    int count = 0;

    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;

    public AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;

    public void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i =0; i<16; i++)
        {
            
            //orderby�� �������ִµ� �������� ������ �ǵ���
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform; 
            float x = (i %4) * 1.4f - 2.1f; 
            float y = (i /4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }
    

    // Update is called once per frame
    void Update() 
    { 
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
        Invoke("timecheck", 0.1f);
        if (time > 30.0f)
        {
            Time.timeScale = 0f;
            endText.SetActive(true);
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(success);

            Invoke("teamName", 0.1f);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            int cardsLeft = GameObject.Find("cards").transform.childCount; // cards�ȿ� ��� �ִ���
            if(cardsLeft == 2) // 2�� ������ �� ���߰�
            {
                Invoke("GameEnd", 1f);
            }
        }
        else
        {
            audioSource.PlayOneShot(fail);
            teamNameText.text = "����";
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        count += 1;
        matchedCountText.text = count.ToString(); 

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        Time.timeScale = 0f;
        endText.SetActive(true);
    }

    void teamName()
    {
        string[] teamname = { "�ɼ���", "�ڹμ�", "�̼���", "�̿���", "�����" };
        teamname = teamname.OrderBy(item2 => Random.Range(-1.0f, 1.0f)).ToArray();
        
        for(int i=0; i<5; i++)
        {
            teamNameText.text = teamname[i].ToString();
        }
    }

    void timecheck()
    {
        if (time > 20.0f)
        {
            timeText.GetComponent<Text>().color = Color.red;
        }
    }
}
