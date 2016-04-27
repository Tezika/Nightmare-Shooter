using UnityEngine;
using System.Collections;
using Shooter.Player;

namespace Shooter.Enemy.Boss.Skill
{
	public class LineSkillCtrl : BaseSkillCtrl {
		// Use this for initialization
		public float AP = 25;
		public float maxAttackDis = 15;
		public float minAttackDis = 3;
		public float coolDownTime = 3.0f;
		public float attackDegreeRange = 45.0f;     // 0 ~ 90;
		public float reactionTime = 0.5f;
		private Transform _pTrans;
		private AudioSource _audio;
//		private PlayerHPCtrl _phc;
		private float _timer = 0;
		private Ray _lineRay = new Ray ();
		private Light _light;
		private LineRenderer _lineRender;
		private float _effectRateForTime = 0.1f;
		private int _layerMaskForHitble;
		void Awake(){
			this._pTrans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
//			this._phc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHPCtrl> ();
			this._light = this.GetComponent<Light> ();
			this._lineRender = this.GetComponent<LineRenderer> ();
			this._layerMaskForHitble = LayerMask.GetMask ("Hitable");
			this._audio = this.GetComponent<AudioSource> ();
		}
		void Start () {
			
		}
		void OnDisable(){
			this.disEffect ();
		}
		// Update is called once per frame
		void Update () {
			this._timer += Time.deltaTime;
			if (this._timer > coolDownTime) {
				if(this.addjustCanUseSkill()){
					this.skill();
				}
				    
			}
			if (this._timer > this._effectRateForTime * coolDownTime) {
				 disEffect();
			}
		}
		void disEffect(){
			this._light.enabled = false;
			this._lineRender.enabled = false;
		}

		protected override void skill(){
			//set head rotation
			this._audio.Stop ();
			this._audio.Play ();
			this._timer = 0;
			this._light.enabled = true;
			this._lineRender.enabled = true;

			Vector3 deltaVec = _pTrans.position - transform.position;
			Quaternion q = Quaternion.LookRotation (deltaVec);
			this.transform.rotation = q;
			// add a  feedback time
//			StartCoroutine (waitForReactionTime (reactionTime));
			//prepare the line
			this._lineRay.origin = this.transform.position;
			this._lineRay.direction = this.transform.forward;
			RaycastHit lineCast;
			this._lineRender.SetPosition (0, this.transform.position);
			if (Physics.Raycast (this._lineRay, out lineCast, maxAttackDis, this._layerMaskForHitble)) {
				PlayerHPCtrl phc = lineCast.collider.gameObject.GetComponent<PlayerHPCtrl> ();
				if (null != phc) {
					Debug.Log("hit");
					phc.takeDamage (AP);
					this._lineRender.SetPosition(1,lineCast.point);
				}else{
					this._lineRender.SetPosition (1, this.transform.position + maxAttackDis * this.transform.forward);
				}
				
			} else {
				this._lineRender.SetPosition (1, this.transform.position + maxAttackDis * this.transform.forward);
			}  
		}
		private bool addjustCanUseSkill(){
			float dis = Vector3.Distance (this._pTrans.position, this.transform.position);
			if (dis <= minAttackDis || dis > maxAttackDis) {
				return false;
			}
			//60 degree attack range
			Vector3 deltaVec = this._pTrans.transform.position - this.transform.position;
			float dot = Vector3.Dot (deltaVec, this.transform.forward);
			if (dot < Mathf.Cos(attackDegreeRange)) {
				return false;
			}

			return true;
		}
		IEnumerator waitForReactionTime(float time){
			yield return new WaitForSeconds (time);
		}
		 
	}

}

