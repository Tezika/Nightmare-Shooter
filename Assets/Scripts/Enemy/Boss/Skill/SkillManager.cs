using UnityEngine;
using System.Collections;
using Shooter.Player;
using Shooter.Enemy.Boss;
namespace Shooter.Enemy.Boss.Skill
{
	public class SkillManager : MonoBehaviour {
		public  BaseSkillCtrl[] bossSkills;
		private BossHPCtrl _bhc;
		private PlayerHPCtrl _phc;
		void Awake(){
			this._phc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
			this._bhc = this.GetComponent<BossHPCtrl> ();
		}
		void OnEnable(){
//		   this._phc.onPlayerDead += HandleonPlayerDead;
		   this._bhc.onBossDead  += HandleonBossDead;
		   this._phc.onPlayerDeadForDelay += HandleonPlayerDeadForDelay;
		}

		IEnumerator HandleonPlayerDeadForDelay (float delayTime){
			yield return  new WaitForSeconds (delayTime);
			this.cancleSkills ();
		}

		void HandleonBossDead ()
		{
			this.cancleSkills ();
		}
		void cancleSkills(){
			for (uint i = 0;i < bossSkills.Length; ++i) {
				Debug.Log("disable skill");
				bossSkills[i].enabled = false;
			}
			this.enabled = false;
		}
		void OnDisable(){
			this._phc.onPlayerDeadForDelay -= HandleonPlayerDeadForDelay;
			this._bhc.onBossDead -= HandleonBossDead;
		}
		// Use this for initialization
		void Start () {
			
		}

	}
}
