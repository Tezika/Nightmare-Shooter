using UnityEngine;
using System.Collections;
namespace Shooter.UI
{
	public class UIBtnCtrl : MonoBehaviour {
		private Animator _ani;
		private AudioSource _audio;
		void Awake(){
			this._ani = this.GetComponent<Animator> ();
			this._audio = this.GetComponent<AudioSource> ();
		}
		void playBtnSound(){
			this._audio.Stop ();
			this._audio.Play ();
		}
		public void onClickStart(){
			this.playBtnSound ();
			this._ani.SetTrigger("GameStart");
		}
		public void onClickQuit(){
			this.playBtnSound ();
			Application.Quit ();
		}
		public void onClickLetsGo(){
			this.playBtnSound ();
			Application.LoadLevel (1);
		}
	}

}
