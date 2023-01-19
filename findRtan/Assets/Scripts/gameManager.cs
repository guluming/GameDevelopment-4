using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text timeText;
    public GameObject card;
    float time;
    int cardKinds = 8;
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
