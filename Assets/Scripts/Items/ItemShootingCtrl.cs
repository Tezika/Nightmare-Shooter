using UnityEngine;
using System.Collections;
using Shooter.Player;

namespace Shooter.Item
{
public class ItemShootingCtrl : ItemCtrl {
	    public float deltaAPRate = 2.0f;
	    public float duration = 5.0f;
		public Material buffMaterial;
//	    public event GetItemShooting onGetItemShooting;
	    private PlayerShootingCtrl _pShootingCtrl;

	  void Awake(){
			this.commonInitInAwake ();
		this._pShootingCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<PlayerShootingCtrl> (); 
	   }
		void OnEnable(){
		   this._pShootingCtrl.onGetShootingItem += HandleonGetShootingItem;
		}

		void HandleonGetShootingItem (GameObject itemObj){
			if (itemObj == this.gameObject) {
				this.playTheSound();
				this.startSinking();
				this._pShootingCtrl.shootingUp(deltaAPRate,duration,buffMaterial);
			}
			
		}
		void OnDisable(){
			this._pShootingCtrl.onGetShootingItem -= HandleonGetShootingItem;
		}
	 	// Use this for initialization
		void Start () {

		}

  }
}
