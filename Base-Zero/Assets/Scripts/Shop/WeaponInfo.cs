using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour {


    public GameObject stock;
    public GameObject[] scope;
	public int[] scopeNum;
	public int scopeActiveNum;
    public GameObject barrel;
    public GameObject magazine;
    public GameObject stockUpgrade;
    public GameObject barrelUpgrade;
    public GameObject magazineUpgrade;
    public GameObject stockButton;
    public GameObject[] scopeButton;
    public GameObject barrelButton;
    public GameObject magazineButton;
	public GameObject[] receiverButton;

    public GameObject WeaponAttachments;
    public GameObject purchaseButton;
    public bool stockUpgraded = false;
    public bool[] scopeUpgraded;
	public int scopeSelected;
    public bool barrelUpgraded = false;
    public bool magazineUpgraded = false;
    public int stockCost = 150;
    public int[] scopeCost;
    public int barrelCost = 150;
    public int magazineCost = 300;
    public int weaponCost = 1200;
    public bool purchased = false;
    public string name;
	public int damage;
	public int firerate;
	public int accuracy;
	public int recoil;
	public int magazinesize;
	public int magsizeUpgraded;
	public int accUp;
	public int recUp;
	public int receiverAccVal;
	public int receiverRecVal;
	public int receiverDamVal;
	public int receiverFireRateVal;
	public int receiverSelected;

	public int activeReceiver;
	public bool[] receiverUpgraded;
	public int[] receiverCost;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void hideAttachment(GameObject attachmentToHide)
    {
        attachmentToHide.SetActive(false);
    }
    public void unhideAttachment(GameObject attachmentToUnHide)
    {
        attachmentToUnHide.SetActive(true);
    }

    public void loadPurchase()
    {
        purchased = true;
        Destroy(purchaseButton);
    }

    



}
