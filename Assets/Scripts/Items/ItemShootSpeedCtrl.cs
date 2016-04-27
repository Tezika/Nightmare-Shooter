using UnityEngine;
using System.Collections;
using Shooter.Player;
namespace Shooter.Item
{
	public class ItemShootSpeedCtrl : ItemCtrl {	
		// Use this for initialization
		public float deltaRate = 2.0f;
		public float duration = 10.0f;
		private PlayerShootingCtrl _psc;
		void Awake(){
			this.commonInitInAwake ();
			this._psc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<PlayerShootingCtrl> (); 
		}
	 	void OnEnable(){
			this._psc.onGetShootingItem += HandleonGetShootingItem;
		}

	 	void HandleonGetShootingItem (GameObject itemObj){
			if (itemObj == this.gameObject) {
				this.playTheSound();
				this.startSinking();
				this._psc.shootSpeedUp(deltaRate,duration);
			}
	 	}
		void OnDisable(){
			this._psc.onGetShootingItem -= HandleonGetShootingItem;
		}
		void Start () {
	    }
    }

}