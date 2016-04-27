using UnityEngine;
using System.Collections;
namespace Shooter.Item
{
public class ItemCtrl : MonoBehaviour {
		public float destoryTime = 2.0f;
		public float appearTime = 5.0f;
		public float sinkSpeed = 5.0f;
		protected AudioSource _audio;
		protected bool _isSinking = false;
		protected float _timer = 0.0f;

		protected Rigidbody _rig;
		protected BoxCollider _col; 
		private bool _isInLand = false;

		void OnCollisionEnter(Collision other){
			if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
				Destroy (this.gameObject);
			} else if (other.gameObject.tag == "Floor") {
				this._rig.useGravity = false;
				this._col.isTrigger = true;
				this._isInLand = true;
			}
		}
		void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
				this.startSinking();
			}
		}
		// Update is called once per frame
		void Update () {
			if (!this._isSinking && this._isInLand) {
				this._timer += Time.deltaTime;
				if(this._timer > appearTime){
					this._isSinking = true;
				}
			}
			if (this._isSinking) {
				this.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
			}
		}
		protected void commonInitInAwake(){
			this._audio = this.GetComponent<AudioSource> ();
			this._rig = this.GetComponent<Rigidbody> ();
			this._col = this.GetComponent<BoxCollider> ();
		}
		protected void playTheSound(){
			this._audio.Stop ();
			this._audio.Play ();
		}
		protected void startSinking(){
			this.GetComponent<Collider>().enabled = false;
			this._isSinking = true;
			StartCoroutine (waitForDestory (destoryTime));
		}
		IEnumerator waitForDestory(float time){
			yield return new  WaitForSeconds (time);
			Destroy (this.gameObject);
		}
	}
}
