using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Player;
using Shooter.Enemy;

namespace Shooter.Game
{
	public class GameMananger : MonoBehaviour {
		public Canvas canvas;
		public float waitingTime = 2.0f;
		public Text gameOverText;
		private PlayerHPCtrl _phc;
		private PlayerMoveCtrl _pmc;
		private PlayerShootingCtrl _psc;
		private EnemyManager _eManager;


		public  AudioClip _vicClip;
		public  AudioClip _failClip;
		public  AudioClip _btnClip;
        private AudioSource _audio;

		void Awake(){
			this._phc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
			this._psc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInChildren<PlayerShootingCtrl> ();
			this._pmc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerMoveCtrl> ();
			this._eManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
			this._audio = GetComponent<AudioSource> ();

		}

		void OnEnable(){
			this._phc.onPlayerDead += HandleonPlayerDead;
			this._eManager.onBossDead += HandleonBossDead;
		}

		void HandleonBossDead (){
//			canvas.GetComponent<Animator>().SetTrigger
			gameOverText.text = "Congratulation :)";
			this._audio.loop = false;
			this.playEffectSound (this._vicClip);
			StartCoroutine (waitForOver (waitingTime,"GameSuccess"));
		}

		void HandleonPlayerDead (){
			gameOverText.text = "Game Over :(";
			this._audio.loop = false;
			this.playEffectSound (this._failClip);
			StartCoroutine (waitForOver (waitingTime,"GameOver"));
		}

		IEnumerator waitForOver(float waitingTime,string aniClipTriggerName){
			yield return new WaitForSeconds (waitingTime);
			canvas.GetComponent<Animator>().SetTrigger(aniClipTriggerName);
			this._eManager.enabled = false;
			if(this._psc.enabled) this._psc.enabled = false;
			//open canvas interactable
			canvas.GetComponent<CanvasGroup> ().interactable = true;
			canvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}

		void OnDisable(){
			this._phc.onPlayerDead -= HandleonPlayerDead;
			this._eManager.onBossDead -= HandleonBossDead;
		}

		void playEffectSound(AudioClip clip){
			this._audio.clip = clip;
			this._audio.Stop ();
			this._audio.Play ();
		}

		public void onClickRestart(){
			this.playEffectSound (this._btnClip);
			Application.LoadLevel (Application.loadedLevel);
		}
		public void onClickBack(){
			this.playEffectSound (this._btnClip);
			Application.LoadLevel (0);
		}

	}
}

