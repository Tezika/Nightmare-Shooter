using UnityEngine;
using System.Collections;
using Shooter.Enemy;
using Shooter.Enemy.Delegate;
namespace Shooter.Enemy.Boss{
	public class SpawnBossCtrl : MonoBehaviour {
		public Transform bossSpawnPos;
		public event BossAppear onBossAppear;
		public GameObject bossObj;
		private EnemyManager _eManager;
		void Awake(){
			this._eManager = this.GetComponentInParent<EnemyManager> ();
		}
		void OnEnable(){
			this._eManager.onEnemiesDead += HandleonEnemiesDead;
		}
		void HandleonEnemiesDead ()
		{
			GameObject go = Instantiate (bossObj, bossSpawnPos.position, Quaternion.identity)as GameObject;
			if (this.onBossAppear != null) {
				this.onBossAppear(go);
			}
		}
		void OnDisable(){
			this._eManager.onEnemiesDead -= HandleonEnemiesDead;
		}
	}
}

