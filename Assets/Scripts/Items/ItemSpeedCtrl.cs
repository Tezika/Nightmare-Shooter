using UnityEngine;
using System.Collections;
using Shooter.Player;
namespace Shooter.Item
{
	public class ItemSpeedCtrl : ItemCtrl {
			public float duration = 3.0f;
			public float deltaSpeed = 1.0f;
		//	public event GetItemSpeed onGetItemSpeed;

			private PlayerMoveCtrl _pMoveCtrl;
			void Awake(){
			    this.commonInitInAwake ();
				this._pMoveCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerMoveCtrl> ();
			}
			void OnEnable(){
			  this._pMoveCtrl.onPlayerGetSpeedItem += HandleonPlayerGetSpeedItem;
			}
			void OnDisable(){
				this._pMoveCtrl.onPlayerGetSpeedItem -= HandleonPlayerGetSpeedItem;
			}
			void HandleonPlayerGetSpeedItem (GameObject itemObj){
				if (itemObj == this.gameObject) {
				this.playTheSound();
					this.startSinking();
					this._pMoveCtrl.speedUp(deltaSpeed,duration);
				}
			}
			// Use this for initialization
			void Start () {
			
			}

		}
}
