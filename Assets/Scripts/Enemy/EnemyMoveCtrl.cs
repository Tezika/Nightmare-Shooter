using UnityEngine;
using System.Collections;
using Shooter.Player;

namespace Shooter.Enemy
{
public class EnemyMoveCtrl : MonoBehaviour {
		private Transform _playerTransform;
		private UnityEngine.AI.NavMeshAgent _nav;
//		private EnemyHpCtrl _eHpCtrl;
		private PlayerHPCtrl _pHpCtrl;
		private bool _isPlayerDead  = false;
		// Use this for initialization
		void Awake(){
			this._playerTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
			this._pHpCtrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHPCtrl> ();
			this._nav = this.GetComponent<UnityEngine.AI.NavMeshAgent> ();
//			this._eHpCtrl = this.GetComponent<EnemyHpCtrl> ();
		}
		void OnEnable(){
			this._pHpCtrl.onPlayerDead += HandleonPlayerDead;
		}

		void HandleonPlayerDead (){
			this._isPlayerDead = true;
		}
		void OnDisable(){
			this._pHpCtrl.onPlayerDead -= HandleonPlayerDead;
		}
		void Start () {
			this._nav.SetDestination (this._playerTransform.position);  
		}
		
		// Update is called once per frame
		void Update () {
			//find a path
			if (!this._isPlayerDead) {
				if(this._nav.isOnNavMesh){
					this._nav.SetDestination (this._playerTransform.position);
				}else{
					Debug.Log("Destory an not on NavMesh enemy");
					Destroy(this.gameObject);
				}
			} else {
				this._nav.enabled = false;
			}
		}
	}
}
