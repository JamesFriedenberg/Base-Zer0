using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStats : MonoBehaviour {
	public Image statBar;
	public Text magsize;
	public int statNum;
	public WeaponInfo guninfo;
	private bool accUp = false;
	private bool recUp = false;
	private bool magUp = false;

	// Use this for initialization
	void Start () {
		switch (statNum) {
		case 1:
			statBar.fillAmount = (1.0f * (guninfo.damage / 100.0f));
			break;
		case 2:
			statBar.fillAmount = (1.0f * (guninfo.firerate / 100.0f));
			break;
		case 3:
			statBar.fillAmount = (1.0f * (guninfo.accuracy / 100.0f));
			break;
		case 4:
			statBar.fillAmount = (1.0f * (guninfo.recoil / 100.0f));
			break;
		case 5:
			magsize.text = guninfo.magazinesize.ToString();
			break;
		default:
			break;

		}

	}
	
	// Update is called once per frame
	void Update () {
		if (magsize != null && !magUp && guninfo.magazineUpgraded) {
			magsize.text = guninfo.magsizeUpgraded.ToString();
		}
		if(statNum == 3 && !accUp && guninfo.barrelUpgraded){
			statBar.fillAmount = (1.0f * ((guninfo.accuracy + guninfo.accUp) / 100.0f));
		}
		if(statNum == 4 && !recUp && guninfo.stockUpgraded){
			statBar.fillAmount = (1.0f * ((guninfo.recoil + guninfo.recUp) / 100.0f));
		}
	}
}
