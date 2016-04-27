using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Shooter.Player;
using Shooter.Enemy.Delegate;
using Shooter.Enemy.Boss;

namespace Shooter.Enemy.Delegate
{
	public delegate void EnemyDead(GameObject obj);
	public delegate void BossHurt(float curHP,float amountAP);
	public delegate void BossAppear(GameObject obj);
}
namespace Shooter.Enemy
{
public class EnemyManager : MonoBehaviour {
		public event EnemyDead onEnemyDead;   //enemy dead by player
		public event Action onEnemySpawn;
		public event Action onEnemiesDead;
		public event Action onPlayerDead;
		public event BossHurt onBossHurt;
		public event Action onBossDead;
        
		public uint passLevelEnemyNums = 20;
		public uint maxEnemyNums = 10;
		public EnemySpawnCtrl[] spawnEnemiesCtrl;

		private SpawnBossCtrl _sbc;
		private BossHPCtrl _bhc;

		private PlayerHPCtrl _pHPCtrl;
		private uint _curEnemyNum = 0;
		private bool _isOverNum = false;

		public bool IsOverNum {
			get {
				return _isOverNum;
			}
		}

		// Use this for initialization
		void Awake(){
			this._pHPCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
			this._sbc = this.GetComponentInChildren<SpawnBossCtrl> ();
		}

		void OnEnable(){
//			Debug.Log("onEnable");
			this._pHPCtrl.onPlayerDead += HandleonPlayerDead;
			this._sbc.onBossAppear += HandleonBossAppear;

		}

		void HandleonBossAppear (GameObject obj)
		{
			if (null != obj) {
				this._bhc = obj.GetComponent<BossHPCtrl>();
				if(null != this._bhc){
					this._bhc.onBossHurt += HandleonBossHurt;
					this._bhc.onBossDead += HandleonBossDead;
				}
			}
		}

		void HandleonBossDead (){
			if (this.onBossDead != null) {
				this.onBossDead();
			}
		}

		void HandleonBossHurt (float curHP, float amountAP){
			//resend event for boss
			if (this.onBossHurt != null) {
				this.onBossHurt (curHP, amountAP);
			}
		}

		void HandleonPlayerDead (){
//			Debug.Log("player dead in manager for enemies");
			if (this.onPlayerDead != null) {
				this.onPlayerDead();
			}
			this.enabled = false;
		}
		void OnDisable(){
			this._pHPCtrl.onPlayerDead -= HandleonPlayerDead;
			this._sbc.onBossAppear -= HandleonBossAppear;
			if (null != this._bhc) {
				this._bhc.onBossHurt -= HandleonBossHurt;
				this._bhc.onBossDead -= HandleonBossDead;
			}
		}		
		// Update is called once per frame
		void Update () {

		}
		public void spawnEnemy(){
//			Debug.Log("call the spawn EnemyFunc" + this._curEnemyNum);
			++this._curEnemyNum;
			if (this.onEnemySpawn != null) {
				this.onEnemySpawn ();
			}
			if (_curEnemyNum >= maxEnemyNums) {
				this._isOverNum = !this._isOverNum;
			}
		}
		public void enemyDead(GameObject obj){
			--this._curEnemyNum;
			--this.passLevelEnemyNums;
			if (this.onEnemyDead != null) {
				this.onEnemyDead (obj);
			}
			if (this._isOverNum) {
				this._isOverNum = !this._isOverNum;
			}
			if (passLevelEnemyNums == 0) {
				if(this.onEnemiesDead != null){
					this.onEnemiesDead();
				}
			}
		}
	}
}
