using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DefendQuest : MonoBehaviour {


    private string questStatus;

    public string questName;
    public string location;
    public string questText;
    public Text UITextRef;

    public bool getToSite;
    public string getToSiteText;

    public GameObject defenseTargetObj;
    public Transform blah;
    public Vector3 defenseTargetLocation;
    public bool defendTarget;
    public string defendTargetText;

    public bool completed;

    // Use this for initialization
    void Start()
    {
       // Instantiate(defenseTargetObj, defenseTargetLocation.);
        questStatus = "InProgress";
        //Instantiate()
        this.transform.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        UITextRef = GameObject.FindWithTag("ObjectiveText").GetComponentInChildren<Text>();
        if (questStatus == "InProgress")
        {
            UITextRef.text = questText;

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
}
