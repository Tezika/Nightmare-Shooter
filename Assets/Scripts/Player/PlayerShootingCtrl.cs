using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shooter.Enemy;
using Shooter.Enemy.Boss;
using Shooter.Item;
using Shooter.Player.Delegate;

namespace Shooter.Player{
	
public class PlayerShootingCtrl : MonoBehaviour {

		public float shootingAP = 20.0f;
		public float timeBetweenShoot = 0.15f;
		public float shootDis = 100.0f;
		public float maxAP = 60.0f;
		public event GetItem onGetShootingItem;


		private int _layerMaskForShootable;
		private ParticleSystem  _shootParticle;
		private float _timer;
		private Light _gunLight;
		private LineRenderer _gunLineRender;
		private Ray _shootRay;
		private RaycastHit _shootRayCast;
		private AudioSource _audio;
		private float _effectTime = 0.1f;
	
		private bool _isShootingUp = false;   //only can hold one shooting buff
		private bool _isShootSpeedUp = false; //only can hold one shooting speed buff

		void Awake(){
			this._layerMaskForShootable  = LayerMask.GetMask("Shootable");
			this._audio = this.GetComponent<AudioSource> ();
			this._gunLight = this.GetComponent<Light> ();
			this._gunLineRender = this.GetComponent<LineRenderer> ();
			this._shootParticle = this.GetComponent<ParticleSystem> ();

		}
		// Use this for initialization
		void Start () {
			this._timer = 0.0f;
		}
		// Update is called once per frame
		void Update () {
			this._timer += Time.deltaTime;
			if (Input.GetButton ("Fire1") && this._timer >= timeBetweenShoot) {
				shoot();
			}
			if (this._timer >= this._effectTime * timeBetweenShoot) {
				disableEffect();
			}
		
		}
		void shoot(){
			this._timer = 0;
			this._audio.Stop ();
			this._audio.Play ();
			this._shootParticle.Stop ();
			this._shootParticle.Play ();

			_gunLight.enabled = true;
			_gunLineRender.enabled = true;

			this._gunLineRender.SetPosition (0, this.transform.position);

			this._shootRay.origin = this.transform.position;
			this._shootRay.direction = this.transform.forward;

			if (Physics.Raycast (this._shootRay, out this._shootRayCast, shootDis, this._layerMaskForShootable)) {
				GameObject shootRayCastObj = this._shootRayCast.collider.gameObject;
				if(shootRayCastObj.tag == "Enemy"){
					EnemyHpCtrl eHPCtrl = shootRayCastObj.GetComponent<EnemyHpCtrl>();
					if(eHPCtrl != null){
						eHPCtrl.takeDamage(shootingAP,this._shootRayCast.point);
					}
				}else if(shootRayCastObj.tag == "Boss"){
					BossHPCtrl  bHPCtrl = shootRayCastObj.GetComponent<BossHPCtrl>();
					if(bHPCtrl != null){
						bHPCtrl.takeDamage(shootingAP,this._shootRayCast.point);
					}
				}
				this._gunLineRender.SetPosition(1,this._shootRayCast.point);
			}else{
				this._gunLineRender.SetPosition(1,this.transform.position + this._shootRay.direction * shootDis);
			}

		}
		void OnTriggerEnter(Collider other){
//			Debug.Log("onTrigger");
			if (other.gameObject.tag == "Item") {
//				Debug.Log ("trigger");
				if(this.onGetShootingItem != null){
					this.onGetShootingItem(other.gameObject);
				}
			}
		}
		public void disableEffect(){
			this._gunLight.enabled = false;
			this._gunLineRender.enabled = false;
	//		this._timer = 0;
		}
		public void shootingUp(float deltaAPRate,float duration,Material buffMaterial){
			if (!this._isShootingUp) {
				this._isShootingUp = true;
				this.shootingAP *= deltaAPRate;
				if (shootingAP > maxAP) {
					shootingAP = maxAP;
				}
				Material tmpMaterial = this._gunLineRender.material;
				this._gunLineRender.material = buffMaterial;
				StartCoroutine(cancleShootingBuff(deltaAPRate,duration,tmpMaterial));
			}
		}
		IEnumerator cancleShootingBuff(float deltaAP,float duration,Material restoreMaterial){
			yield return new WaitForSeconds (duration);
			this.shootingAP /= deltaAP;
			this._gunLineRender.material = restoreMaterial;
			this._isShootingUp = false;
		}

		
		public void shootSpeedUp (float deltaRate, float duration)
		{
//			throw new System.NotImplementedException ();
			if (!this._isShootSpeedUp) {
				this._isShootSpeedUp = true;
				this.timeBetweenShoot /= deltaRate;
				StartCoroutine(cancleShootSpeedUp(deltaRate,duration));
			} 
		}

		IEnumerator cancleShootSpeedUp(float deltaRate,float duration){
			yield return new WaitForSeconds (duration);
			this.timeBetweenShoot *= deltaRate;
		}

	 }
}
