using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Shooter.Enemy;
namespace Shooter.UI
{
   public class UIEnemySliderCtrl : MonoBehaviour {
		public Color _bossHPColor = new Color();
		public Image _sliderImg;
		private EnemyManager _eManager;
		private float filldelta = 0.0f;
		private Slider _slider;
		private Text _txtOfPrompt;
		void Awake(){
			this._slider = this.GetComponentInChildren<Slider> ();
			this._txtOfPrompt = this.GetComponentInChildren<Text> ();
			this._eManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		}
		void OnEnable(){
			this._eManager.onEnemyDead += HandleonEnemyDead;
			this._eManager.onEnemiesDead += HandleonEnemiesDead;
			this._eManager.onBossHurt += HandleonBossHurt;
		}

		void HandleonBossHurt (float curHP,float HP){
			float sliderVal = curHP / HP * this._slider.maxValue;
			this._slider.value = sliderVal;
		}

		void HandleonEnemiesDead (){
//		    this._sliderImg.color  = _bossHPColor;
			this._sliderImg.color = Color.Lerp (this._sliderImg.color, _bossHPColor, 2.0f);
			this._txtOfPrompt.text = "BossHP:";
		}

		void HandleonEnemyDead (GameObject obj){
			if (filldelta == 0) {
				filldelta = this._slider.maxValue / this._eManager.passLevelEnemyNums;
			}
			this._slider.value += filldelta;
		   
		}
		void OnDisable(){
			this._eManager.onEnemyDead -= HandleonEnemyDead;
			this._eManager.onEnemiesDead -= HandleonEnemiesDead;
			this._eManager.onBossHurt -= HandleonBossHurt;
		}
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}

