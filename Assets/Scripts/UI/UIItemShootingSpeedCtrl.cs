using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Player;
using Shooter.Item;
namespace Shooter.UI
{
	public class UIItemShootingSpeedCtrl : UIItemCtrl {
		private PlayerShootingCtrl _psc;
		void Awake(){
			this._psc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<PlayerShootingCtrl> ();
			this._img = this.GetComponent<Image> ();
		}
		void Start(){
			this._img.fillAmount = 0;
		}
		void OnEnable(){
			this._psc.onGetShootingItem += HandleonGetShootingItem;
		}

		void HandleonGetShootingItem (GameObject itemObj){
			ItemShootSpeedCtrl iCtrl = itemObj.GetComponent<ItemShootSpeedCtrl> ();
			if (iCtrl != null) {
				this._fillSpeed = 1.0f / iCtrl.duration;
				this._isFill = true;
				this._img.fillAmount = 1;
			}
			
		}
		void OnDisable(){
			this._psc.onGetShootingItem -= HandleonGetShootingItem;
		}
	}

}

