using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Shooter.UI
{
	public class UIItemCtrl : MonoBehaviour {
		protected float _fillSpeed;
		protected Image _img;
		protected bool _isFill = false;
		
		// Update is called once per frame
		void Update () {
			if (this._isFill) {
				this._img.fillAmount -= Time.deltaTime * this._fillSpeed;
				if(this._img.fillAmount == 0){
					this._isFill = false;
				}
			}
		}
	}
}
