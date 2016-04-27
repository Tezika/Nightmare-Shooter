using UnityEngine;
using System.Collections;

namespace Shooter.Enemy.Boss.Skill{
	public class AOESkillCtrl : BaseSkillCtrl {
		public GameObject skillObj;
		public Transform[] skillPos;
		public float startDelayTime = 10.0f;
		public  float coolDownTime = 5.0f;
		public float skillDelayTimeForSound = 1.0f;
		private  AudioSource _audio;
		void Awake(){
			this._audio = this.GetComponent<AudioSource> ();
		}
		void Start(){
			InvokeRepeating ("skill", startDelayTime, coolDownTime);
		}
		void OnDisable(){
			CancelInvoke ();
		}
		protected override void skill(){
//			this._isCoolDown = !this._isCoolDown;
			this._audio.Stop ();
			this._audio.Play ();
			StartCoroutine (waitForSkill (skillDelayTimeForSound));
	
		}
		IEnumerator waitForSkill(float delay){
			yield return new WaitForSeconds (delay);
			foreach (Transform trans in skillPos) {
				//				Debug.Log(trans.localRotation);
				GameObject obj = Instantiate (skillObj, trans.position, Quaternion.identity) as GameObject;	
				obj.transform.rotation = trans.rotation;
				
			}
		}
	}
}

