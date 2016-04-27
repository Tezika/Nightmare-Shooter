using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Enemy;
using Shooter.Enemy.Boss;
namespace Shooter.Game
{
public class ScoreManager : MonoBehaviour {
		public  float score;
		private Text _text;
		private EnemyManager _eManager;
		void Awake(){
			this._text = this.GetComponent<Text> (); 
			this._eManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		}
		void OnEnable(){
			this._eManager.onEnemyDead += HandleonEnemyDead;
		}

		void HandleonEnemyDead (GameObject obj){
			EnemyHpCtrl eCtrl = obj.GetComponent<EnemyHpCtrl> ();
			if (eCtrl != null) {
				this.score += eCtrl.scoreValue;
				this._text.text = "Score:" + score;
			}
		}
		// Use this for initialization
		void Start () {
			score = 0;
			this._text.text = "Score:" + score;
		}
	//	public static void changeScoreText(){
	//		this._text.text = "Score:" + score;
	//	}
		void Update(){

		}
	}
}
