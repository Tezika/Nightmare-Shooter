using UnityEngine;
using Shooter.Item;
using System.Collections;

namespace Shooter.Game
{
	public class ItemManager : MonoBehaviour {
		public float  startTime = 10.0f;
		public float  coolDownTime = 10.0f;
		public SpwanItemCtrl[] spwanItemCtrls;
			// Use this for initialization
		void Start () {
			InvokeRepeating ("spwanItems", startTime, coolDownTime);
		}

		void spwanItems(){
			SpwanItemCtrl spwanCtrl  = spwanItemCtrls[Random.Range(0,spwanItemCtrls.Length)];
			spwanCtrl.spwanAItem ();
		}
//		
//		// Update is called once per frame
//		void Update () {
//			
//		}
	}

}
