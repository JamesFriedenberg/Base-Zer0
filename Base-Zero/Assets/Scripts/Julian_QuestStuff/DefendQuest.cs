using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DefendQuest : MonoBehaviour {

    public GameObject player;
    private string questStatus;

    public string questName;
    public string location;
    public string questText;
    public Text UITextRef;

    public bool getToSite;
    public string getToSiteText;

    public GameObject defenseTargetObj;
    public Vector3 defenseTargetLocation;
    public bool defendTarget;
    public string defendTargetText;

    public bool destroyDefendTargetonFinish;
    public int timer;

    private bool flag = true;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(defenseTargetObj, (defenseTargetLocation), Quaternion.identity);
        defenseTargetObj = GameObject.Find(defenseTargetObj.name + "(Clone)");
        questStatus = "InProgress";
        //Instantiate()
        this.transform.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(defenseTargetObj.transform.position, player.transform.position);
        //Debug.Log(distance);
        UITextRef = GameObject.FindWithTag("ObjectiveText").GetComponentInChildren<Text>();
        if (questStatus == "InProgress")
        {
            UITextRef.text = questText;
            if(distance < 20f)
            {
                questStatus = "defendTarget";
            }

        }
        else if(questStatus == "defendTarget")
        {
            if (flag)
            {
                StartCoroutine(Timer());
                flag = false;
            }
            if(distance > 25f)
            {
                questStatus = "InProgress";
                flag = true;
            }
            if(timer <= 0)
            {
                questStatus = "Completed";
                if (destroyDefendTargetonFinish)
                {
                    DestroyImmediate(defenseTargetObj, true);
                }
            }
            UITextRef.text = getToSiteText + ":" + timer;

        }
        else if (questStatus == "Completed")
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
    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
    }
    public GameObject sendCurQuestObject()
    {
        return defenseTargetObj;
    }
}
