using UnityEngine;
using UnityEngine.UI;
using Shooter.Player;

using System.Collections;
namespace Shooter.UI
{

	public class UIFlashImgCtrl : MonoBehaviour {
		private PlayerHPCtrl _pHPCtrl;
		private Image _flashImage;
		private float _flashSpeed = 5f;                               
		private Color _flashColourForDamage = new Color(1f, 0f, 0f, 0.1f); 
		private Color _flashColourForAddHP =  new Color(0f,1f,0f,0.1f);
//		private Color _flashColor = null;
		private bool _isAddHP = false;
		private bool _isDamaged = false;
		void Awake(){
			this._pHPCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
			this._flashImage = this.GetComponent<Image> ();
		}
		void OnEnable(){
			this._pHPCtrl.onPlayerHPChange += HandleonPlayerHPChange;
		}

		void HandleonPlayerHPChange (bool isAdd){
			if (isAdd) {
				this._isAddHP = true;
			} else {
				this._isDamaged = true;
			}
		}

		void OnDisable(){
			this._pHPCtrl.onPlayerHPChange += HandleonPlayerHPChange;
		}
		// Update is called once per frame
		void Update () {
			if (this._isDamaged && !this._isAddHP) {
				this._flashImage.color = this._flashColourForDamage;
				this._isDamaged = false;
			} else if (this._isAddHP && !this._isDamaged) {
				this._flashImage.color = this._flashColourForAddHP;
				this._isAddHP = false;
			} else if (this._isAddHP && this._isDamaged) {
				this._flashImage.color = this._flashColourForAddHP;
				this._isAddHP = false;
				this._isDamaged = false;
			}
			else {
				this._flashImage.color = Color.Lerp(this._flashImage.color,Color.clear,this._flashSpeed*Time.deltaTime);
			} 
		}
	}	
}
