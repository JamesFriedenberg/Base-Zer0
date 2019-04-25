using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour {
    public int upgradeVal;
    public GameObject shopManager;
	public WeaponInfo gunInfo;
    public Text upgradeText;
    public int scopeNum;
    public string[] ScopeNames;
	public int receiverNum;
	public string[] receiverNames;
	public GameObject statParent;
	// Use this for initialization
	void Start () {
		switch (upgradeVal) {
		case 1:
			if (!gunInfo.stockUpgraded) {
				upgradeText.text += "\n" + gunInfo.stockCost.ToString () + " Scrap";
			}

			break;
		case 2:
			if (scopeNum != 0 && !gunInfo.scopeUpgraded[scopeNum]) {
				GetComponentInChildren<Text> ().text = ScopeNames [scopeNum] + " : " + gunInfo.scopeCost [scopeNum].ToString () + " Scrap";
			}
			break;
		case 3:
			if (!gunInfo.barrelUpgraded) {
				upgradeText.text += "\n" + gunInfo.barrelCost.ToString () + " Scrap";
			}
			break;
		case 4:
			if (!gunInfo.magazineUpgraded) {
				upgradeText.text += "\n" + gunInfo.magazineCost.ToString () + " Scrap";
			}
			break;
		case 5:
			if (receiverNum != 0 && !gunInfo.receiverUpgraded[receiverNum]) {
				GetComponentInChildren<Text> ().text = receiverNames [receiverNum] + " : " + gunInfo.receiverCost [receiverNum].ToString () + " Scrap";
			}
			break;
		default:
			break;
		}

		 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void unHideElement(GameObject element)
    {
        if(element != null)
        {
            element.SetActive(true);
        }
        
    }
    public void hideElement(GameObject element)
    {
        if (element != null)
        {
            element.SetActive(false);
        }
    }
    public void hideAllChildren(GameObject parentOfChildren)
    {
        foreach(Transform child in parentOfChildren.transform)
        {
            child.gameObject.SetActive(false);
            hideAllChildren(child.gameObject);
        }
    }
    public void unhideAllChildren(GameObject parentOfChildren)
    {
        foreach (Transform child in parentOfChildren.transform)
        {
            child.gameObject.SetActive(true);
            hideAllChildren(child.gameObject);
        }
    }

    public void purchaseUpgrade(GameObject weapon)
    {
        WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();
        bool purchaseComplete = false;
        switch (upgradeVal)
        {
            case 1:
                if(shopManager.GetComponent<ShopManager>().scrap >= weaponRef.stockCost)
                {
                    shopManager.GetComponent<ShopManager>().scrap -= weaponRef.stockCost;
                    weaponRef.stockUpgraded = true;
                    weaponRef.stockUpgrade.SetActive(true);
                    if (weaponRef.stock != null)
                    {
                        weaponRef.stock.SetActive(false);
                    }
                    purchaseComplete = true;
                }
                break;
            /*case 2:
                if (weaponRef.scopeUpgrade != null && shopManager.GetComponent<ShopManager>().scrap >= weaponRef.scopeCost)
                {
                    shopManager.GetComponent<ShopManager>().scrap -= weaponRef.scopeCost;
                    //weaponRef.scopeUpgraded = true;
                    weaponRef.scopeUpgrade.SetActive(true);
                    if (weaponRef.scope != null)
                    {
                        weaponRef.scope.SetActive(false);
                    }
                    purchaseComplete = true;
                }
                break;*/
            case 3:
                if (shopManager.GetComponent<ShopManager>().scrap >= weaponRef.barrelCost)
                {
                    shopManager.GetComponent<ShopManager>().scrap -= weaponRef.barrelCost;
                    weaponRef.barrelUpgraded = true;
                    weaponRef.barrelUpgrade.SetActive(true);
                    if (weaponRef.barrel != null)
                    {
                        weaponRef.barrel.SetActive(false);
                    }
                    purchaseComplete = true;
                }
                break;
            case 4:
                if (shopManager.GetComponent<ShopManager>().scrap >= weaponRef.magazineCost)
                {
                    shopManager.GetComponent<ShopManager>().scrap -= weaponRef.magazineCost;
                    weaponRef.magazineUpgraded = true;
                    weaponRef.magazineUpgrade.SetActive(true);
                    if (weaponRef.magazine != null)
                    {
                        weaponRef.magazine.SetActive(false);
                    }
                    purchaseComplete = true;
                }
                break;
            default:
                break;
        }
        if(purchaseComplete)
        {
			foreach (BarStats b in statParent.GetComponentsInChildren<BarStats>()) {
				b.RecalculateStats ();
			}
            shopManager.GetComponent<ShopManager>().UpdateCashScrap();
			if (upgradeText != null) {
				Destroy (upgradeText);
			}
            Destroy(gameObject);
        }
       
    }

	public void PurchaseReceiver(GameObject weapon)
    {
        WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();
        if (!weaponRef.receiverUpgraded[receiverNum] &&  shopManager.GetComponent<ShopManager>().scrap >= weaponRef.receiverCost[receiverNum])
        {
			shopManager.GetComponent<ShopManager>().scrap -= weaponRef.receiverCost[receiverNum];
			shopManager.GetComponent<ShopManager>().UpdateCashScrap();
			weaponRef.receiverUpgraded[receiverNum] = true;
			GetComponentInChildren<Text>().text = receiverNames[receiverNum];
        }

		if (weaponRef.receiverUpgraded[receiverNum])
        {
			weaponRef.receiverSelected = receiverNum;
			foreach (BarStats b in statParent.GetComponentsInChildren<BarStats>()) {
				b.RecalculateStats ();
			}
            for (int i = 0; i < weaponRef.receiverUpgraded.Length; i++)
            {
                    if (i == receiverNum)
                    {
						GetComponentInChildren<Text>().text = receiverNames[receiverNum] + " :Selected";
						weaponRef.receiverSelected = i;
						weaponRef.activeReceiver = i;
						
                    }
                    else
                    {
						if(weaponRef.receiverButton[i].GetComponentInChildren<Text>().text.IndexOf(" :Selected") > -1)
                        {
							weaponRef.receiverButton[i].GetComponentInChildren<Text>().text = weaponRef.receiverButton[i].GetComponentInChildren<Text>().text.Substring(0, weaponRef.receiverButton[i].GetComponentInChildren<Text>().text.IndexOf(" :Selected"));
                        }
                    }

                
            }
        }
    }

	public void PurchaseScope(GameObject weapon)
	{
		WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();
		if (!weaponRef.scopeUpgraded[scopeNum] &&  shopManager.GetComponent<ShopManager>().scrap >= weaponRef.scopeCost[scopeNum])
		{
			shopManager.GetComponent<ShopManager>().scrap -= weaponRef.scopeCost[scopeNum];
			shopManager.GetComponent<ShopManager>().UpdateCashScrap();
			weaponRef.scopeUpgraded[scopeNum] = true;
			GetComponentInChildren<Text>().text = ScopeNames[scopeNum];
		}

		if (weaponRef.scopeUpgraded[scopeNum])
		{
			for (int i = 0; i < weaponRef.scope.Length; i++)
			{
				if (weaponRef.scope != null)
				{
					if (i == scopeNum)
					{
						if (weaponRef.scope [i] != null) {
							weaponRef.scope[i].SetActive(true);
						}
						GetComponentInChildren<Text>().text = ScopeNames[scopeNum] + " :Selected";
						weaponRef.scopeSelected = i;
						weaponRef.scopeActiveNum = i;

					}
					else
					{
						if (weaponRef.scope [i] != null) {
							weaponRef.scope[i].SetActive(false);
						}

						if(weaponRef.scopeButton[i].GetComponentInChildren<Text>().text.IndexOf(" :Selected") > -1)
						{
							weaponRef.scopeButton[i].GetComponentInChildren<Text>().text = weaponRef.scopeButton[i].GetComponentInChildren<Text>().text.Substring(0, weaponRef.scopeButton[i].GetComponentInChildren<Text>().text.IndexOf(" :Selected"));
						}
					}

				}
			}
		}
	}

	public void UpdateScope(GameObject weapon, int activeScopeNum)
	{
		WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();
		Debug.Log (weaponRef.scopeUpgraded.Length);
			for (int i = 0; i < weaponRef.scope.Length; i++)
			{
				if (weaponRef.scope != null)
				{
					if (weaponRef.scopeUpgraded [i]) {
						weaponRef.scopeButton[i].GetComponentInChildren<Text>().text = ScopeNames [i];
					}
					if (i == activeScopeNum) {
						if (weaponRef.scope [i] != null) {
							weaponRef.scope [i].SetActive (true);
						}
						GetComponentInChildren<Text> ().text = ScopeNames [activeScopeNum] + " :Selected";
						weaponRef.scopeSelected = i;

					} else {
						if (weaponRef.scope [i] != null) {
							weaponRef.scope[i].SetActive(false);
						}
					}
				}

		}
	}

	public void UpdateReceiver(GameObject weapon, int activeRecNum)
	{
		WeaponInfo weaponRef = weapon.GetComponent<WeaponInfo>();

			weaponRef.receiverSelected = activeRecNum;
			Debug.Log (activeRecNum);
			foreach (BarStats b in statParent.GetComponentsInChildren<BarStats>()) {
				b.RecalculateStats ();
			}
			for (int i = 0; i < weaponRef.receiverUpgraded.Length; i++)
			{
				if (weaponRef.receiverUpgraded [i]) {
					weaponRef.receiverButton[i].GetComponentInChildren<Text>().text = receiverNames [i];
				}
				if (i == activeRecNum)
				{
					GetComponentInChildren<Text>().text = receiverNames[activeRecNum] + " :Selected";
					weaponRef.receiverSelected = i;
				}

			}

	}

	public void ToggleWindow(GameObject window){
		if (window.activeSelf) {
			window.SetActive (false);
		}
	}
		
}
