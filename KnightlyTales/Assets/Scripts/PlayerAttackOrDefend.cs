using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KnightlyTales
{
	public class PlayerAttackOrDefend : MonoBehaviour
	{
		public Text ammoText;
		private int ammo;
		private bool isShooting;
		private bool swordOrBow;					//true - sword; false - bow
		private Animator animator;					//Used to store a reference to the Player's animator component.
		private Image attackImage;
		private float waitTime = 3.0f;

		// Use this for initialization; 
		void Start ()
		{
			isShooting = false;
			swordOrBow = true;
			//Get the current ammo stored in GameManager.instance between levels.
			ammo = GameManager.instance.playerAmmo;

			//Set the ammoText to reflect the current player ammo total.
			ammoText.text = "" + ammo;

			//Get a component reference to the Player's animator component
			animator = GetComponent<Animator> ();

			attackImage = GameObject.FindGameObjectWithTag ("AttackBtn").GetComponent<Image> ();
		}

		void Update () 
		{
			if (isShooting == true)
			{
				//Reduce fill amount over 3 seconds
				attackImage.fillAmount += 1.0f/waitTime * Time.deltaTime;
			}
		}

		public int getAmmo ()
		{
			return ammo;
		}

		//true - attack; false - defend
		public void Attack ()
		{
			//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
			if (swordOrBow) {
				animator.SetTrigger ("playerSwing");		 	//uses sword
			} else {
				if (!animator.GetBool ("playerDefense") && !isShooting && ammo > 0) {
					LoseAmmo ();
					attackImage.fillAmount = 0;
					animator.SetTrigger ("playerShoot");		//uses bow
				}
			}

			//Set the playersTurn boolean of GameManager to false now that players turn is over.
			GameManager.instance.playersTurn = false;
		}

		public void Defend ()
		{

			animator.SetBool ("playerDefense", !animator.GetBool ("playerDefense"));	//uses shield

		}

		public void attackBow ()
		{
			swordOrBow = false;
		}

		public void attackSword ()
		{
			swordOrBow = true;
		}

		public void AddAmmo (int gain)
		{
			if (GameManager.instance.maxAmmo > ammo) {
				if (GameManager.instance.maxAmmo >= (ammo + gain)) {
					ammo += gain;
				} else {
					ammo = GameManager.instance.maxAmmo;
				}
				ammoText.text = "" + ammo;
			}
		}

		public void LoseAmmo ()
		{
			Shoot ();
			ammo --;
			ammoText.text = "" + ammo;
		}
	
		public void Shoot ()
		{
			isShooting = true;

			Invoke ("notShoot", 3);
		}
	
		void notShoot ()
		{
			isShooting = false;
		}
	}
}

