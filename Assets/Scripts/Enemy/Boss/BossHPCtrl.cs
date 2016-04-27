using UnityEngine;
using System;
using System.Collections;
using Shooter.Enemy;
using Shooter.Enemy.Delegate;
namespace Shooter.Enemy.Boss
{
	public class BossHPCtrl : MonoBehaviour {
		public event BossHurt onBossHurt;
		public event Action onBossDead;
		public  float  HP = 300;
		private float _curHP;
		public float CurHp {
			get {
				return _curHP;
			}
		}
		private AudioSource _audio;
		private ParticleSystem _hitParticles;
		private CapsuleCollider _capsuleCollider;
		private EnemyMoveCtrl _moveCtrl;
		private EnemyAttackCtrl _attackCtrl;
		private bool _isDead = false;
		private bool _isSinking = false;
		[SerializeField]
		private float _sinkSpeed = 5.0f;
		void Awake(){
			this._audio = this.GetComponent<AudioSource> ();
			this._hitParticles = this.GetComponentInChildren<ParticleSystem> ();
			this._capsuleCollider = this.GetComponent<CapsuleCollider> ();
			this._moveCtrl = this.GetComponent<EnemyMoveCtrl> ();
			this._attackCtrl = this.GetComponent<EnemyAttackCtrl> ();
		}
		void Start(){
			this._curHP = HP;
		}
		public void takeDamage(float ap,Vector3 hitPoint){
			if(this._isDead) return;
			this._audio.Play ();
			this._curHP -= ap;
			if (this.onBossHurt != null) {
				this.onBossHurt(this._curHP,HP);
			}
			this._hitParticles.transform.position = hitPoint;
			this._hitParticles.Play();
			if (this._curHP <= 0) {
				this.dead ();
			}
		}
		void dead(){
			this._isDead = true;
			if (this.onBossDead != null) {
				this.onBossDead();
			}
			this._capsuleCollider.isTrigger = true;
			this._moveCtrl.enabled = false;
			this._attackCtrl.enabled = false;
			this.GetComponent<NavMeshAgent> ().enabled = false;
			this.GetComponent<Rigidbody> ().isKinematic = true;
			this._isSinking = true;
			Destroy (this.gameObject, 3.0f);
		}  
		// Update is called once per frame
		void Update () {
			if (this._isSinking) {
				this.transform.Translate(-Vector3.up * this._sinkSpeed * Time.deltaTime);
			}
		}
	}
}
