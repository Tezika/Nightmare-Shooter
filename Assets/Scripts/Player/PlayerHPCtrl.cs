using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using Shooter.Item;
using Shooter.Player.Delegate;

namespace Shooter.Player.Delegate{
	//fix the lineskill dsiplay bug
	public delegate IEnumerator PlayerDeadCoroutine(float delayTime);

}

namespace Shooter.Player{
public class PlayerHPCtrl : MonoBehaviour {

		public event Action onPlayerDead;
		public event Action onPlayerDamaged;
		public event PlayerHpChange onPlayerHPChange;
		public event GetItem onPlayerGetHPItem;
		public event PlayerDeadCoroutine onPlayerDeadForDelay;
		

    
		public float startHp = 100.0f;                                                         
		public AudioClip deathClip;
		[SerializeField]private float _deadDelayTime = 2.0f;
		private float _curHp;
		public float CurHp {
			get {
				return _curHp;
			}
		}
		private AudioSource _audio;
		private Animator _anim;
		private PlayerMoveCtrl _moveCtrl;
		private PlayerShootingCtrl _pShootingCtrl;
		private bool _isDead = false;


		void Awake(){
			this._audio = this.GetComponent<AudioSource> ();
			this._anim = this.GetComponent<Animator> ();
			this._moveCtrl = this.GetComponent<PlayerMoveCtrl> ();
			this._pShootingCtrl = this.GetComponentInChildren<PlayerShootingCtrl> ();
		}
		// Use this for initialization
		void Start () {
			this._curHp = startHp;
		}
		public void addHP(float hp){
			this._curHp += hp;
			if (this._curHp > 100) {
				this._curHp = 100;
			}
			this.onPlayerHPChange (true);
		}
		public void takeDamage(float attack){
			this._audio.Play ();
			this._curHp -= attack;
			if (_curHp <= 0 && !this._isDead) {
				this._curHp = 0;
				this.dead ();
			} else {
				if(this.onPlayerDamaged != null){
				    this.onPlayerDamaged();
				}
//				if(this.onHpChange != null){
//				   this.onHpChange();
//				}
			}
			if(this.onPlayerHPChange != null){
				this.onPlayerHPChange(false);
			}
		}
		void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "Item") {
				if(this.onPlayerGetHPItem != null){
					this.onPlayerGetHPItem(other.gameObject);
				}
			}
		}
		void dead(){
			Debug.Log("Player Dead!");
			if (this.onPlayerDead != null) {
//				Debug.Log("call the event for player dead");
				this.onPlayerDead ();
			}
			if (this.onPlayerDeadForDelay != null) {
				StartCoroutine(this.onPlayerDeadForDelay(this._deadDelayTime));
			}
			this._isDead = true;
			// Tell the animator that the player is dead.
			this._anim.SetTrigger ("Die");
			// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
			this._audio.clip = deathClip;
			this._audio.Play ();
			// Turn off the movement and shooting scripts.
			this._moveCtrl.enabled = false;
			this.enabled = false;
			//Turn off shooting
			this._pShootingCtrl.disableEffect ();
			this._pShootingCtrl.enabled = false;
		}
		
	}
}
