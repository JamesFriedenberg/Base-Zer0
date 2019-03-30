using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneSwitcher : MonoBehaviour {

   public string goToSceneName;
   public Vector3 spawnPosition;

   private GameObject gm;

    //private GameObject arrow;
    //private arrowHandler arrowScr;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("gm");
	}
	
	// Update is called once per frame
	void Update () {
        //arrow = GameObject.FindGameObjectWithTag("Arrow");
        //arrowScr = arrow.GetComponent<arrowHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.GetComponent<GameManager>().startPosition = spawnPosition;
            SceneManager.LoadScene(goToSceneName);
        }
    }
}
