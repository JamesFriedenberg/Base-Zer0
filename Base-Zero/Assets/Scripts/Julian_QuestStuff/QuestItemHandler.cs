using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class QuestItemHandler : MonoBehaviour {

    private GameObject associatedQuestObject;
    private GameObject mngrRef;
    private GameObject canvasObject;
    private Transform textTr;
    private Text pickupText;
    private QuestManager qm;

    private bool flag;
	// Use this for initialization
	void Start () {


        canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        textTr = canvasObject.transform.Find("pickupParent");
        pickupText = textTr.GetComponent<Text>();

        
        flag = true;
        mngrRef = GameObject.FindGameObjectWithTag("gm");
        qm = mngrRef.GetComponent<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (flag)
        {
            findAssociatedGameObject();
            flag = false;
        }
		
	}
    private void OnTriggerEnter(Collider other)
    {

        if (qm.currentQuests[qm.questIndex].GetComponent<Quest>() != null)
        {
            pickupText.text = "Press E To Pickup The Item";


        }
        else
        {
            

        }

        



    }
    private void OnTriggerStay(Collider other)
    {


        if (qm.currentQuests[qm.questIndex].GetComponent<Quest>() != null)
        {
            if (Input.GetKey(KeyCode.E))
            {

                if (other.gameObject.tag == "Player")
                {
                    qm.currentQuests[qm.questIndex].GetComponent<Quest>().changeQuestStatus("Completed");
                    pickupText.text = "";
                    Destroy(this.gameObject);
                    findAssociatedGameObject();
                }


            }

        }
        else
        {


        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.text = "";

    }
    public void findAssociatedGameObject()
    {
        associatedQuestObject = qm.returnQuestGO(this.gameObject.tag);
    }
    
}
