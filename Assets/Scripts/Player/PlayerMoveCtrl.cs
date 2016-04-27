using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shooter.Item;
using Shooter.Player.Delegate;

namespace Shooter.Player{
public class PlayerMoveCtrl : MonoBehaviour {
	
		public float moveSpeed = 6.0f;
		public float maxSpeed = 10.0f;
		public float minSpeed = 6.0f;
		public event GetItem onPlayerGetSpeedItem;
		

		private int _floorMask;
		private Rigidbody _playerRig;
		private Vector3 _movementVec;
		private Animator _anim;
		private float _rayLenth = 100.0f; 
		void Awake(){
			this._playerRig = this.GetComponent<Rigidbody> ();
			this._floorMask = LayerMask.GetMask("Floor");
			this._anim = this.GetComponent<Animator> ();
		}
		void Start () {
		  
		}

		void FixedUpdate(){
			move ();
			turn ();
		}
		// Update is called once per frame
		void Update () {
		
		}
		void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "Item") {
				if(this.onPlayerGetSpeedItem!= null){
					this.onPlayerGetSpeedItem(other.gameObject);
				}
			}
		}
		void move(){
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
			if (h == 0 && v == 0) {
				this._anim.SetBool("IsWalking",false);
				return;
			}
			this._movementVec.Set (h, 0, v);
			this._playerRig.MovePosition (this.transform.position + this._movementVec.normalized * moveSpeed * Time.fixedDeltaTime);
			this._anim.SetBool ("IsWalking", true);
		}
		void turn(){
			Ray rayFromScreen = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit floorHit;
			if (Physics.Raycast (rayFromScreen, out floorHit, this._rayLenth, this._floorMask)) {
				Vector3 playerToMouse = floorHit.point - this.transform.position;
				playerToMouse.y = 0;
				Quaternion q = Quaternion.LookRotation(playerToMouse);
				this._playerRig.MoveRotation(q);

			}

		}

		public void speedUp(float deltaSpeed,float duration){
			moveSpeed += deltaSpeed;
			if (moveSpeed > maxSpeed) {
				moveSpeed = maxSpeed;
			}
			StartCoroutine (cancleSpeedUp(deltaSpeed,duration));
		}
		IEnumerator cancleSpeedUp(float deltaSpeed,float duration){
			yield return new WaitForSeconds (duration);
			moveSpeed -= deltaSpeed;
			if (moveSpeed < minSpeed) {
				moveSpeed  = minSpeed;
			}
		}
	}
}		
