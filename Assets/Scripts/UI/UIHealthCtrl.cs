using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Player;

namespace Shooter.UI
{
public class UIHealthCtrl : MonoBehaviour {
	public Color green = new Color();
	public Color yellow = new Color ();
	public Color red = new Color();
	private PlayerHPCtrl _pHpCtrl;
	private Image _sliderImg;

	void Awake(){
		this._pHpCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
		this._sliderImg = this.GetComponent<Image> ();
	}
	void OnEnable(){
		this._pHpCtrl.onPlayerHPChange += HandleonHpChange;
	}
	void OnDisable(){
		this._pHpCtrl.onPlayerHPChange -= HandleonHpChange;
	}
	private void HandleonHpChange (bool isAdd){
		if (this._pHpCtrl.CurHp <= 70 && this._pHpCtrl.CurHp > 40) {
			this._sliderImg.color = yellow;
		} else if (this._pHpCtrl.CurHp <= 40) {
			this._sliderImg.color = red;
		} else {
			this._sliderImg.color =  green;
		}
	}
}
}
