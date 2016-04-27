using UnityEngine;
using System.Collections;
using Shooter.Player;
namespace Shooter.Enemy.Boss.Skill
{
	public class SkillSphereCtrl : MonoBehaviour {
		public float speed = 5.0f;
		public float AP = 30.0f;
		public float appearTime = 5.0f;
//		private float _timer = 0;
		void Awake(){

		}
		void Start(){

		}
		// Use this for initialization
		void OnCollisionEnter(Collision other){
			if (other.gameObject.tag == "Player") {
				PlayerHPCtrl phc = other.gameObject.GetComponent<PlayerHPCtrl> ();
				phc.takeDamage (AP);
			} else if (other.gameObject.tag == "Env") {
				Destroy(this.gameObject);
			}
		}
		// Update is called once per frame
		void Update () {
			//this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 1);
			this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
	}

}

