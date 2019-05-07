using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public GameObject[] questDatabase;

    public GameObject[] currentQuests = new GameObject[5];

    private int[] questIntArray = new int[6];
    private QuestPopulator qp;
    private bool questsWereAdded = true;

    public int questIndex = 0;

    public GameObject navArrow;
    private arrowHandler arrowHandler;

    public static QuestManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(this.gameObject);
        if(arrowHandler != null){
            arrowHandler = navArrow.GetComponent<arrowHandler>();
        }
        qp = this.GetComponent<QuestPopulator>();
        //for(int i = 0; i < 3; i++)
        //{

        //    int index = Random.Range(0, HQQuests.Length);
        //    currentQuests[i] = HQQuests[index];
        //    Destroy(HQQuests[index]);
        //}

       // currentQuests[0].SetActive(true);
		
	}

    // Update is called once per frame
    void Update() {

        navArrow = GameObject.FindGameObjectWithTag("Arrow");
        if (questsWereAdded)
        {
			if (qp == null) {
				return;
			}
            generateQuests();
            questsWereAdded = false;
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {

            string[] s = qp.getQuestNames();
            for (int i = 0; i < s.Length; i++)
                Debug.Log(s[i]);
        }

        currentQuests[questIndex].SetActive(true);
        string curQuestStatus;
        if (currentQuests[questIndex].tag == "q_defense")
        {
            curQuestStatus = currentQuests[questIndex].GetComponent<DefendQuest>().getQuestStatus();
        }
        else
        {
            curQuestStatus = currentQuests[questIndex].GetComponent<Quest>().getQuestStatus();
        }
        if (curQuestStatus == "Completed")
        {
            currentQuests[questIndex].SetActive(false);
            questIndex++;
            arrowHandler.onObjectiveChange();
        }
        if(questIndex >= 6)
        {
            SceneManager.LoadScene(4);
        }


    }
    public GameObject sendQuestItem()
    {
    
        if(currentQuests[questIndex].GetComponent<Quest>() != null)
        {
            return currentQuests[questIndex].GetComponent<Quest>().questItemPrefab;

        }
        else
        {
            return currentQuests[questIndex].GetComponent<DefendQuest>().defenseTargetObj;

        }
    }
    public string sendCurQuestName()
    {
        return currentQuests[questIndex].name;
    }
    public string sendCurQuestLocation()
    {
        if(currentQuests[questIndex] == null) return "Quest location not found";
        if (currentQuests[questIndex].tag == "q_defense")
        {
            return currentQuests[questIndex].GetComponent<DefendQuest>().location;
        }
        else
        {
            return currentQuests[questIndex].GetComponent<Quest>().location;
        }
    }
    public GameObject returnQuestGO(string tag)
    {
        switch (tag)
        {
            case "Heli1":
                return questDatabase[0];
            case "Heli2":
                return questDatabase[1];
            case "Heli3":
                return questDatabase[2];
            case "Heli4":
                return questDatabase[3];
            case "Heli5":
                return questDatabase[4];
            case "Nuke1":
                return questDatabase[5];
            case "Nuke2":
                return questDatabase[6];
            case "Nuke3":
                return questDatabase[7];
            case "Nuke4":
                return questDatabase[8];
            case "Nuke5":
                return questDatabase[9];
            case "Misc1":
                return questDatabase[10];
            case "Misc2":
                return questDatabase[11];
            case "Misc3":
                return questDatabase[12];
            case "Misc4":
                return questDatabase[13];
            case "Vehicle2":
                return questDatabase[15];
         
        }
        return null;
    }
    private void generateQuests()
    {






        int endingChooser = Random.Range(0, 2);

        if(endingChooser == 1)
        {
            currentQuests[5] = questDatabase[14];
            int playChooser = Random.Range(0, 4);

            if(playChooser == 0)
            {
                currentQuests[0] = questDatabase[1];
                currentQuests[1] = questDatabase[11];
                currentQuests[2] = questDatabase[3];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[4];

                questIntArray[0] = 1;
                questIntArray[1] = 11;
                questIntArray[2] = 3;
                questIntArray[3] = 13;
                questIntArray[4] = 4;
                questIntArray[5] = 14;
            }
            else if(playChooser == 1)
            {
                currentQuests[0] = questDatabase[2];
                currentQuests[1] = questDatabase[10];
                currentQuests[2] = questDatabase[4];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[0];
                questIntArray[0] = 2;
                questIntArray[1] = 10;
                questIntArray[2] = 4;
                questIntArray[3] = 13;
                questIntArray[4] = 0;
                questIntArray[5] = 14;
            }
            else if(playChooser == 2)
            {
                currentQuests[0] = questDatabase[0];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[2];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[3];
                questIntArray[0] = 0;
                questIntArray[1] = 13;
                questIntArray[2] = 2;
                questIntArray[3] = 12;
                questIntArray[4] = 3;
                questIntArray[5] = 14;
            }
            else if (playChooser == 3)
            {
                currentQuests[0] = questDatabase[3];
                currentQuests[1] = questDatabase[10];
                currentQuests[2] = questDatabase[1];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[2];
                questIntArray[0] = 3;
                questIntArray[1] = 10;
                questIntArray[2] = 1;
                questIntArray[3] = 13;
                questIntArray[4] = 2;
                questIntArray[5] = 14;
            }
            else if(playChooser == 4)
            {
                currentQuests[0] = questDatabase[4];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[1];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[3];
                questIntArray[0] = 4;
                questIntArray[1] = 13;
                questIntArray[2] = 1;
                questIntArray[3] = 12;
                questIntArray[4] = 3;
                questIntArray[5] = 14;
            }
            else
            {
                currentQuests[0] = questDatabase[4];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[1];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[3];
                questIntArray[0] = 4;
                questIntArray[1] = 13;
                questIntArray[2] = 1;
                questIntArray[3] = 12;
                questIntArray[4] = 3;
                questIntArray[5] = 14;
            }

        }
        else if(endingChooser == 0)
        {
            currentQuests[5] = questDatabase[15];
            int playChooser = Random.Range(0, 4);

            if (playChooser == 0)
            {
                currentQuests[0] = questDatabase[5];
                currentQuests[1] = questDatabase[11];
                currentQuests[2] = questDatabase[8];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[9];
                questIntArray[0] = 5;
                questIntArray[1] = 11;
                questIntArray[2] = 8;
                questIntArray[3] = 13;
                questIntArray[4] = 9;
                questIntArray[5] = 15;
            }
            else if (playChooser == 1)
            {
                currentQuests[0] = questDatabase[7];
                currentQuests[1] = questDatabase[10];
                currentQuests[2] = questDatabase[9];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[5];
                questIntArray[0] = 7;
                questIntArray[1] = 10;
                questIntArray[2] = 9;
                questIntArray[3] = 13;
                questIntArray[4] = 5;
                questIntArray[5] = 15;
            }
            else if (playChooser == 2)
            {
                currentQuests[0] = questDatabase[5];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[7];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[9];
                questIntArray[0] = 5;
                questIntArray[1] = 13;
                questIntArray[2] = 7;
                questIntArray[3] = 12;
                questIntArray[4] = 9;
                questIntArray[5] = 15;
            }
            else if (playChooser == 3)
            {
                currentQuests[0] = questDatabase[8];
                currentQuests[1] = questDatabase[10];
                currentQuests[2] = questDatabase[6];
                currentQuests[3] = questDatabase[13];
                currentQuests[4] = questDatabase[7];
                questIntArray[0] = 8;
                questIntArray[1] = 10;
                questIntArray[2] = 6;
                questIntArray[3] = 13;
                questIntArray[4] = 7;
                questIntArray[5] = 15;
            }
            else if (playChooser == 4)
            {
                currentQuests[0] = questDatabase[9];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[6];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[8];
                questIntArray[0] = 9;
                questIntArray[1] = 13;
                questIntArray[2] = 6;
                questIntArray[3] = 12;
                questIntArray[4] = 8;
                questIntArray[5] = 15;
            }
            else
            {
                currentQuests[0] = questDatabase[9];
                currentQuests[1] = questDatabase[13];
                currentQuests[2] = questDatabase[6];
                currentQuests[3] = questDatabase[12];
                currentQuests[4] = questDatabase[8];
                questIntArray[0] = 9;
                questIntArray[1] = 13;
                questIntArray[2] = 6;
                questIntArray[3] = 12;
                questIntArray[4] = 8;
                questIntArray[5] = 15;
            }
        }

        //List<int> takeout = new List<int>();
        ////rentQuests[currentQuests.Length] = questDatabase[questDatabase.Length];

        //currentQuests[currentQuests.Length - 1] = questDatabase[questDatabase.Length - 1];
        //for(int i = 0; i < currentQuests.Length - 1; i++)
        //{

        //    int val = Random.Range(0, questDatabase.Length-1);
        //    while (takeout.Contains(val))
        //    {
        //        val = Random.Range(0, questDatabase.Length-1);

        //    }

        //    takeout.Add(val);
        //    currentQuests[i] = questDatabase[val];
        //}




        //currentQuests[5] = questDatabase[13];
        //for(int i = 0; i < questPopulatedArr.Length; i++)
        //{
        //    string cur = questPopulatedArr[i];



        //    switch (cur)
        //    {
        //        case "heli1":
        //            currentQuests[i] = questDatabase[0];
        //            break;
        //        case "heli2":
        //            currentQuests[i] = questDatabase[1];
        //            break;
        //        case "heli3":
        //            currentQuests[i] = questDatabase[2];
        //            break;
        //        case "heli4":
        //            currentQuests[i] = questDatabase[3];
        //            break;
        //        case "heli5":
        //            currentQuests[i] = questDatabase[4];
        //            break;
        //        case "nuke1":
        //            currentQuests[i] = questDatabase[5];
        //            break;
        //        case "nuke2":
        //            currentQuests[i] = questDatabase[6];
        //            break;
        //        case "nuke3":
        //            currentQuests[i] = questDatabase[7];
        //            break;
        //        case "nuke4":
        //            currentQuests[i] = questDatabase[8];
        //            break;
        //        case "nuke5":
        //            currentQuests[i] = questDatabase[9];
        //            break;
        //        case "misc1":
        //            currentQuests[i] = questDatabase[10];
        //            break;
        //        case "misc2":
        //            currentQuests[i] = questDatabase[11];
        //            break;
        //        case "misc3":
        //            currentQuests[i] = questDatabase[12];
        //            break;
        //        case "misc4":
        //            currentQuests[i] = questDatabase[13];
        //            break;




        //        default:
        //            break;

        //    }
        //}
    }
    public int[] savedQuestArray()
    {
        return questIntArray;
    }
}
