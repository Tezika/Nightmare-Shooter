using UnityEngine;
using System.Collections;
using Shooter.Player;
namespace Shooter.Item
{
public class ItemHpCtrl : ItemCtrl {
		public float addHp = 50; 
		private PlayerHPCtrl _pHPCtrl;
		void Awake(){
			this.commonInitInAwake ();
     		this._pHPCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
		}
		void OnEnable(){
			this._pHPCtrl.onPlayerGetHPItem += HandleonPlayerGetHPItem;
		}
		void OnDisable(){
			this._pHPCtrl.onPlayerGetHPItem -= HandleonPlayerGetHPItem;
		}
		void HandleonPlayerGetHPItem (GameObject itemObj){
			if (itemObj == this.gameObject) {
				this.playTheSound();
				this._pHPCtrl.addHP(addHp);
				this.startSinking();
			}
		}
	}
}
