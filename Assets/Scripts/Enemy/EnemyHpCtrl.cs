using UnityEngine;
using System;
using System.Collections;
using Shooter.Game;
namespace Shooter.Enemy
{
public class EnemyHpCtrl : MonoBehaviour {
//		public event Action onEnemyDamage;
	   
		public float  HP = 100;
		public float sinkSpeed = 2.5f;
		public int scoreValue = 10;
		public AudioClip deathClip;

		private float _curHP;

		public float CurHp {
			get {
				return _curHP;
			}
		}

		private Animator _anim;
		private AudioSource _audio;
		private ParticleSystem _hitParticles;
		private CapsuleCollider _capsuleCollider;
		private bool _isDead;
		private bool _isSinking;
		private EnemyMoveCtrl _moveCtrl;
		private EnemyAttackCtrl _attackCtrl;

		private EnemyManager _eManager;
		void Awake(){
			this._eManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
			this._anim = GetComponent <Animator> ();
			this._audio = GetComponent <AudioSource> ();
			this._hitParticles = GetComponentInChildren <ParticleSystem> ();
			this._capsuleCollider = GetComponent <CapsuleCollider> ();
			this._moveCtrl = GetComponent<EnemyMoveCtrl> ();
			this._attackCtrl = GetComponent<EnemyAttackCtrl> ();


		}
		void OnEnable(){
			this._eManager.onEnemiesDead += HandleonEnemiesDead;
		}

		void HandleonEnemiesDead ()
		{
			this.dead ();
		}
		void OnDisable(){
			this._eManager.onEnemiesDead -= HandleonEnemiesDead;
		}
		void Start () {
			this._isDead = false;
			this._isSinking = false;
			this._curHP = HP;
		}
		
		// Update is called once per frame
		void Update () {
			if (this._isSinking) {
				this.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
			}
		
		}
		public void takeDamage(float attack,Vector3 hitPoint){
			if(this._isDead) return;
			
			this._audio.Play ();
			
			this._curHP -= attack;
			
			this._hitParticles.transform.position = hitPoint;
			this._hitParticles.Play();

			if (this._curHP <= 0) {
				this._eManager.enemyDead(this.gameObject);
				this.dead ();
			}
		}

		void dead(){
			// The enemy is dead.
			this._isDead = true;
			// Turn the collider into a trigger so shots can pass through it.
			this._capsuleCollider.isTrigger = true;
			// Tell the animator that the enemy is dead.
			this._anim.SetTrigger ("Die");
			// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
			this._audio.clip = deathClip;
			this._audio.Play ();
			this._moveCtrl.enabled = false;
			this._attackCtrl.enabled = false;
		}

		//animation call func
		public void startSinking(){
			this.GetComponent<NavMeshAgent> ().enabled = false;
			this.GetComponent<Rigidbody> ().isKinematic = true;
			this._isSinking = true;
			StartCoroutine (this.waitFor (2.0f));
	//		Destroy (this.gameObject,2.0f);
		}

		 IEnumerator waitFor(float time){
			yield return  new WaitForSeconds(time);
			Destroy (this.gameObject);
		}

}
}
