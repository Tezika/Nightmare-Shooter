using UnityEngine;
using Shooter.Player;
using System.Collections;
namespace Shooter.Enemy
{
	public class EnemySpawnCtrl : MonoBehaviour {
	
		public GameObject spawnEnemy;
		public float startTime = 3.0f;
		public float coolDownTime = 6.0f;
		private EnemyManager _eManager;
		void Awake(){
			this._eManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		}
		void OnEnable(){
			this._eManager.onEnemiesDead += HandleonEnemiesDead;
			this._eManager.onPlayerDead  += HandleonPlayerDead;
		}

		void HandleonPlayerDead (){
			this.disenableScript ();
		}

		void HandleonEnemiesDead (){
			this.disenableScript ();
		}
		void disenableScript(){
			Debug.Log ("Disenable the script");
			CancelInvoke ();
			this.enabled = false;
		}
		void OnDisable(){
			this._eManager.onEnemiesDead -= HandleonEnemiesDead;
			this._eManager.onPlayerDead -= HandleonPlayerDead;
			
		}
		void Start(){
			InvokeRepeating ("spawnAEnemy", startTime, coolDownTime);
		}
		void spawnAEnemy(){
//			Debug.Log (this._eManager.IsOverNum);
			if (!this._eManager.IsOverNum) {
				Instantiate (spawnEnemy, this.transform.position, Quaternion.identity);
				this._eManager.spawnEnemy ();
			}
		}
		// Update is called once per frame
		void Update () {
		
			
		}
	}
}
