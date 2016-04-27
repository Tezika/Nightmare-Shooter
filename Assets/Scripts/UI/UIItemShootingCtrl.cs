using UnityEngine;
using UnityEngine.UI;

using Shooter.UI;
using Shooter.Player;
using Shooter.Player.Delegate;
using Shooter.Item;

using System.Collections;

namespace Shooter.UI
{
	public class UIItemShootingCtrl : UIItemCtrl{
		private PlayerShootingCtrl _pShootingCtrl;
		void Awake(){
			this._pShootingCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<PlayerShootingCtrl> ();
			this._img = this.GetComponent<Image> ();
		}
		void OnEnable(){
			this._pShootingCtrl.onGetShootingItem += HandleonGetShootingItem;;
		}

		void HandleonGetShootingItem (GameObject itemObj){
			ItemShootingCtrl ictrl = itemObj.GetComponent<ItemShootingCtrl> ();
			if (ictrl != null) {
				this._fillSpeed = 1.0f / ictrl.duration;
				this._img.fillAmount = 1;
				this._isFill = true;
			}
		}

		void OnDisable(){
			this._pShootingCtrl.onGetShootingItem -= HandleonGetShootingItem;;
		}

		// Use this for initialization`
		void Start () {
			this._img.fillAmount = 0;
		}

	}
	
}
