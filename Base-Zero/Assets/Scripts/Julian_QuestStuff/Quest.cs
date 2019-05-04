using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    //InProgress or Completed
    private string questStatus;

    public string questName;
    public string location;
    public string questText;
    public Text UITextRef;

    public GameObject questItemPrefab;
    public Vector3 locationOfObject;

   
    // Use this for initialization
    void Start()
    {
        UITextRef = GameObject.FindWithTag("ObjectiveText").GetComponentInChildren<Text>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        questStatus = "InProgress";
       
        this.transform.gameObject.SetActive(true);

        if(SceneManager.GetActiveScene().name == location)
        {
            Instantiate(questItemPrefab, (locationOfObject), Quaternion.identity);
        }

        StartCoroutine(FadeTextToFullAlpha(1.0f, UITextRef));


    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == location)
        {
            Instantiate(questItemPrefab, (locationOfObject), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("ObjectiveText") == null) return;
        UITextRef = GameObject.FindWithTag("ObjectiveText").GetComponentInChildren<Text>();
        
        if (questStatus == "InProgress")
        {
            UITextRef.text = questText;

        }
        else if(questStatus == "Completed")
        {
            UITextRef.text = "Quest Completed, Return to HQ";

        }

    }
    public string getQuestStatus()
    {
        return questStatus;
    }
    public void changeQuestStatus(string newQuestStatus)
    {
        questStatus = newQuestStatus;
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {

        for(int l = 0; l < 3; l++)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);

            while (i.color.a > 0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }

        }
        i.color = new Color(i.color.r, i.color.g, i.color.b,1);

    }
}
