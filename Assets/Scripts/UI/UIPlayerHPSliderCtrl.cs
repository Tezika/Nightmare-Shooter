using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Player;

namespace Shooter.UI
{
public class UIPlayerHPSliderCtrl : MonoBehaviour {
		private PlayerHPCtrl _pHPCtrl;
		private Slider _slider;
		void Awake(){
			this._pHPCtrl = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHPCtrl> ();
			this._slider = this.GetComponent<Slider> ();
		}
		void OnEnable(){
	//		this._pHPCtrl.onHpChange += HandleonHpChange;
			this._pHPCtrl.onPlayerHPChange += HandleonPlayerHPChange;
		}

		void HandleonPlayerHPChange (bool isAdd){
			this._slider.value = this._pHPCtrl.CurHp;	
		}

		void OnDisable(){
			this._pHPCtrl.onPlayerHPChange += HandleonPlayerHPChange;
		}
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
