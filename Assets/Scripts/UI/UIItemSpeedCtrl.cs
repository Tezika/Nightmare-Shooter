using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Player;
using Shooter.Player.Delegate;
using Shooter.Item;

namespace Shooter.UI
{
	public class UIItemSpeedCtrl : UIItemCtrl {
		private PlayerMoveCtrl _pMoveCtrl;
		void Awake(){
			this._pMoveCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerMoveCtrl> ();
			this._img = this.GetComponent<Image> ();
		}
		void OnEnable(){
			this._pMoveCtrl.onPlayerGetSpeedItem += HandleonPlayerGetSpeedItem;
		}

		void HandleonPlayerGetSpeedItem (GameObject itemObj){
			ItemSpeedCtrl ictrl = itemObj.GetComponent<ItemSpeedCtrl> ();
			if (ictrl != null) {
				this._fillSpeed = 1.0f / ictrl.duration;
				this._img.fillAmount = 1;
				this._isFill = true;
			}

		}
		void OnDisable(){
			this._pMoveCtrl.onPlayerGetSpeedItem -= HandleonPlayerGetSpeedItem;
		}
		// Use this for initialization`
		void Start () {
			this._img.fillAmount = 0;
		}

	}
}
