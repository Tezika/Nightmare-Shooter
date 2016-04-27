using UnityEngine;
using System.Collections;
using Shooter.Player;

namespace Shooter.Enemy
{
public class EnemyAttackCtrl : MonoBehaviour {
		public float timeBetweenAttack;
		public float AP;

		private GameObject _player;
		private PlayerHPCtrl _playerHealthCtrl;
		private Animator _anim;
		private bool _isPlayerInRange;
		private float _timer;
		void Awake(){
			this._player = GameObject.FindGameObjectWithTag("Player");
			if (this._player != null) {
				this._playerHealthCtrl = this._player.GetComponent<PlayerHPCtrl> ();
			}
			this._anim = this.GetComponent<Animator> ();
	  	}
		void OnEnable(){
			this._playerHealthCtrl.onPlayerDead += HandleonPlayerDead;
		}

		void HandleonPlayerDead ()
		{
//			Debug.Log("call the playerDead");
			if (this.gameObject.tag == "Enemy") {
				this._anim.SetTrigger("PlayerDie");
			}
		}
		void OnDisable(){
			this._playerHealthCtrl.onPlayerDead -= HandleonPlayerDead;
		}
		// Use this for initialization
		void Start () {
			this._timer = 0;
			this._isPlayerInRange = false;
		}
		void OnTriggerEnter(Collider other){
			if (other.tag == "Player") {
				this._isPlayerInRange  =true;
			}
		}
		void OnTriggerExit(Collider other){
			if (other.tag == "Player") {
				this._isPlayerInRange = false;
			}

		}
		// Update is called once per frame
		void Update () {
			this._timer += Time.deltaTime;
			if (_timer > timeBetweenAttack && this._isPlayerInRange) {
				 attack();
			}
		}
		void attack(){
			this._timer = 0;
			if (this._playerHealthCtrl.CurHp > 0) {
				this._playerHealthCtrl.takeDamage (AP);
			}

		}

	}
}
