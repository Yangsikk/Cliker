using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContestManager : MonoBehaviour
{
    public GameObject[] BurgerSprite;
    public GameObject[] Delete;
    public Sprite[] Decision;
    public GameObject O;
    public Text TextTimer;
    public Text TextResult;
    public Text TextReward;
    public GameObject ResultPanel;

    public Vector3 Question;
    public Vector3 QuestionPosition;
    public Vector3 Answer;
    public Vector3 AnswerPosition;
    public Vector3 DecisionPosition;
    public List<GameObject> QuestionArr;
    public List<GameObject> AnswerArr;
    int i =1 ;

    public float AnswerY;
    public float Timer;
    public int Correct;


    // Start is called before the first frame update
    void Start()
    {
        SetQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        TextTimer.text = Mathf.Round(Timer).ToString();
        if(Timer < 0)
        {
            TimeUp();
        }
    }
    public void ReturnScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SetQuestion()
    {
        float QuestionY = 1.1f + 0.4f * i;
       
        Instantiate(BurgerSprite[0], Question, Quaternion.identity);
        Instantiate(BurgerSprite[0], Answer, Quaternion.identity);
        for (i= 0;i<4;i++)
        {
            QuestionY = 1.1f + 0.3f * i;
            QuestionPosition = new Vector3(-4.2f, QuestionY, 1.0f);
            QuestionArr.Add(BurgerSprite[Random.Range(2, 4)]);
            QuestionArr[i].GetComponent<SpriteRenderer>().sortingOrder = i + 1;
            Instantiate(QuestionArr[i], QuestionPosition, Quaternion.identity);          
        }
        QuestionY += 0.4f;
        QuestionPosition = new Vector3(-4.2f, QuestionY, 1.0f);
        Instantiate(BurgerSprite[1], QuestionPosition, Quaternion.identity);       
    }
    
    public void MakeTomato()
    {
        Debug.Log(AnswerArr.Count);
        AnswerArr.Add(BurgerSprite[4]);
        int layer = AnswerArr.Count-1;
        AnswerPosition = SetAnswerPosition(AnswerArr.Count);
        AnswerArr[layer].GetComponent<SpriteRenderer>().sortingOrder = AnswerArr.Count;
        Instantiate(BurgerSprite[4], AnswerPosition, Quaternion.identity);       
        Debug.Log(AnswerArr[layer].GetComponent<SpriteRenderer>().sortingOrder);
    }
    public void MakeCheese()
    {        
        AnswerArr.Add(BurgerSprite[2]);
        int layer = AnswerArr.Count-1;
        AnswerPosition = SetAnswerPosition(AnswerArr.Count);
        AnswerArr[layer].GetComponent<SpriteRenderer>().sortingOrder = AnswerArr.Count;
        Instantiate(BurgerSprite[2], AnswerPosition, Quaternion.identity);     
    }
    public void MakePattie()
    {
        AnswerArr.Add(BurgerSprite[3]);
        int layer = AnswerArr.Count-1;
        AnswerPosition = SetAnswerPosition(AnswerArr.Count);
        AnswerArr[layer].GetComponent<SpriteRenderer>().sortingOrder = AnswerArr.Count;
        Instantiate(BurgerSprite[3], AnswerPosition, Quaternion.identity);
    }

    public void Submit()
    {
        int index = 0;

        AnswerPosition = SetAnswerPosition(AnswerArr.Count+1);
        Instantiate(BurgerSprite[1], AnswerPosition, Quaternion.identity);
        for(int k = 0; k<4;k++)
        {
            Debug.Log(QuestionArr[k] + "," + AnswerArr[k]);
        }
        for(int j = 0; j<QuestionArr.Count;j++)
        {
            if (QuestionArr[j] == AnswerArr[j])
            {
                index++;            
                Debug.Log(index);
            }
        }
        Debug.Log(index + "," + QuestionArr.Count);
        if (index == QuestionArr.Count)
        {
            Correct++;
            O.transform.GetComponent<SpriteRenderer>().sprite = Decision[0];
            Instantiate(O, DecisionPosition, Quaternion.identity);
            Debug.Log(Correct);
        }
        else
        {
            O.transform.GetComponent<SpriteRenderer>().sprite = Decision[1];
            Instantiate(O, DecisionPosition, Quaternion.identity);
        }
        QuestionArr.Clear();
        AnswerArr.Clear();
        Delete = GameObject.FindGameObjectsWithTag("Burger");
        for (int d = 0; d < Delete.Length; d++)
        {
            Destroy(Delete[d],0.5f);
        }
        SetQuestion();
    }

    public void TimeUp()
    {
        Time.timeScale = 0;
        ResultPanel.SetActive(true);
        if(Correct > 5)
        {
            TextResult.text = ("성   공 !");
            TextReward.text = ("X 500");
            GameManager.money += 500;
        }
        else
        {
            TextResult.text = ("실   패 !");
            TextReward.text = ("X");
        }
    }
    public Vector3 SetAnswerPosition(int index)
    {
        float y = 1.1f + 0.3f * index;
        Vector3 position = new Vector3(4.2f, y, 1.0f);
        return position;
    }
}
