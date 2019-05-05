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
		/*switch (statNum) {
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
		}*/
		RecalculateStats ();
	}
	
	// Update is called once per frame
	public void RecalculateStats () {
		if (statNum == 5 && guninfo.magazineUpgraded) {
			//Debug.Log (guninfo.magsizeUpgraded.ToString());
			magsize.text = guninfo.magsizeUpgraded.ToString();
		} else if (statNum == 5)
        {
            magsize.text = guninfo.magazinesize.ToString();
        }
		if(statNum == 1){
			float damage = guninfo.damage;
			if (guninfo.receiverSelected == 2) {
				damage += guninfo.receiverDamVal;
			}

			statBar.fillAmount = (1.0f * (damage / 100.0f));
		}
		if(statNum == 2){
			float firerate = guninfo.firerate;
			if (guninfo.receiverSelected == 1) {
				firerate += guninfo.receiverFireRateVal;
			}

			statBar.fillAmount = (1.0f * (firerate / 100.0f));
		}
		if(statNum == 3){
			float accuracy = guninfo.accuracy;
			if (guninfo.barrelUpgraded) {
				accuracy += (guninfo.accUp);
			}
			if (guninfo.receiverSelected == 1) {
				accuracy -= guninfo.receiverAccVal;
			}

			statBar.fillAmount = (1.0f * (accuracy / 100.0f));
		}
		if(statNum == 4){
			float recoil = guninfo.recoil;
			if (guninfo.stockUpgraded) {
				recoil -= (guninfo.recUp);
			}
			if (guninfo.receiverSelected == 2) {
				recoil += guninfo.receiverRecVal;
			}

			statBar.fillAmount = (1.0f * (recoil / 100.0f));
		}
	}
}
