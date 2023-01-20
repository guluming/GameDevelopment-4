using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endText;
    public Text timeText;
    public AudioClip match;
    public AudioSource audioSource;
    float time;
    int cardKinds = 8;

    private void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        List<int> rtans = new List<int>();
        for (int i=0; i<cardKinds*2; i++) {
            rtans.Add(i/2);
        }

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToList();

        for (int i=0; i<16; i++) {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = i % 4 * 1.4f - 2.1f;
            float y = i / 4 * 1.4f - 3.0f;
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

        if (time > 30.0f) {
            GameEnd();
        }
    }

    public void isMatched() 
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().destroyCrad();
            secondCard.GetComponent<card>().destroyCrad();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2) {
                Invoke("GameEnd", 0.1f);
            }
        }
        else {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        Time.timeScale = 0f;
        endText.SetActive(true);
    }

    public void retryGame() 
    {
        SceneManager.LoadScene("MainScene");
    }
}
