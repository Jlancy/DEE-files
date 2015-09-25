
using UnityEngine;
using System.Collections;
using UnityEngine.UI;//Allows us to use UI.
using UnityStandardAssets.CrossPlatformInput;


	//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
	public class Player : MovingObject
	{
		public Joystick joystick;           // Reference to joystick prefab
		public GameObject joystickBounds;
		public bool useAxisInput = true;   // Use Input Axis or Joystick
		public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.
		public int wallDamage = 1;					//How much damage a player does to a wall when chopping it.
		public Text healthText;						//UI Text to display current player health total.
		public Slider healthSlider;
		public Inventory inventory;					//Takes care of inventory.
		public AudioClip moveSound1;				//1 of 2 Audio clips to play when player moves.
		public AudioClip moveSound2;				//2 of 2 Audio clips to play when player moves.
		public AudioClip eatSound1;					//1 of 2 Audio clips to play when player collects a health object.
		public AudioClip eatSound2;					//2 of 2 Audio clips to play when player collects a health object.
		public AudioClip drinkSound1;				//1 of 2 Audio clips to play when player collects a soda object.
		public AudioClip drinkSound2;				//2 of 2 Audio clips to play when player collects a soda object.
		public AudioClip gameOverSound;				//Audio clip to play when player dies.

		private PlayerAttackOrDefend attackOrDefend;//Used to store the boolean for defense or attack(sword or bow).
		private Animator animator;					//Used to store a reference to the Player's animator component.
		private int health;							//Used to store player health points total during level.
		private int restOfHealth;
		private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
		private float h, v;         // Horizontal and Vertical values
		private float originalH, originalV;
		private SlotManger _slotManger;
		
		//Start overrides the Start function of MovingObject
		protected override void Start ()
		{
			_slotManger = FindObjectOfType<SlotManger>();
			inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

			//Get a component reference to the Player's animator component
			animator = GetComponent<Animator> ();

			attackOrDefend = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttackOrDefend> ();

			//Get the current health point total stored in GameManager.instance between levels.
			health = GameManager.instance.playerHealthPoints;

			//Set the healthText to reflect the current player health total.
			healthText.text = health + "%";
			healthSlider.value = health;
			originalH = joystick.transform.position.x;
			originalV = joystick.transform.position.y;
			//disable joystick at the start
			joystick.GetComponent<Image>().enabled = false;
			//not attached in my scene
			//joystickBounds.GetComponent<Image>().enabled = false;
			//joystick.enabled = false;
			//Call the Start function of the MovingObject base class.
			base.Start ();
		}
		
		
		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
			//When Player object is disabled, store the current local health, inventory, and ammo
			//total in the GameManager so it can be re-loaded in next level.
			GameManager.instance.playerHealthPoints = health;
			GameManager.instance.saveInventory ();
			GameManager.instance.playerAmmo = attackOrDefend.getAmmo();
			
		}
		
		private void Update ()
		{
			//Debug.Log("y");
			//If it's not the player's turn, exit the function.
			if (!GameManager.instance.playersTurn)
				return;
			
			int horizontal = 0;  	//Used to store the horizontal move direction.
			int vertical = 0;		//Used to store the vertical move direction.
			
			// Get horizontal and vertical input values from either axis or the joystick.
			if (!useAxisInput) {
				h = joystick.transform.position.x - originalH;
				v = joystick.transform.position.y - originalV;
			}
			else {
				h = Input.GetAxis("Horizontal");
				v = Input.GetAxis("Vertical");
			}
			
			if (Mathf.Abs (h) > Mathf.Abs (v)) {
				if(h>0) {
					// Apply horizontal velocity
					horizontal = 1;
				} else {
					// Apply horizontal velocity
					horizontal = -1;
				}
			} else {
				if(Mathf.Abs (h) < Mathf.Abs (v))
				{
					if(v>0) {
						// Apply vertical velocity
						vertical = 1;
					} else {
						// Apply vertical velocity
						vertical = -1;
					}
				} 
			}

			if (horizontal != 0 || vertical != 0) {
				//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
				//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
				AttemptMove<Wall> (horizontal, vertical);
			}
		}
		
		//AttemptMove overrides the AttemptMove function in the base class MovingObject
		//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
		protected override void AttemptMove <T> (int xDir, int yDir)
		{

			//Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
			base.AttemptMove <T> (xDir, yDir);
			
			//Hit allows us to reference the result of the Linecast done in Move.
			RaycastHit2D hit;
			
			//If Move returns true, meaning Player was able to move into an empty space.
			if (Move (xDir, yDir, out hit)) {
				//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
				SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
			}
			
			//Since the player has moved and lost health points, check if the game has ended.
			CheckIfGameOver ();
			
			//Set the playersTurn boolean of GameManager to false now that players turn is over.
			GameManager.instance.playersTurn = false;
		}
		
		
		//OnCantMove overrides the abstract function OnCantMove in MovingObject.
		//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
		protected override void OnCantMove <T> (T component)
		{
			//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
			animator.SetTrigger ("playerSwing");

			//Set hitWall to equal the component passed in as a parameter.
			Wall hitWall = component as Wall;
			
			//Call the DamageWall function of the Wall we are hitting.
			hitWall.DamageWall (wallDamage);

		}

		
		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
			//Check if the tag of the trigger collided with is Exit.
			if (other.tag == "Exit") {
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", restartLevelDelay);
				
				//Disable the player object since level is over.
				enabled = false;
			}
			
			//Check if the tag of the trigger collided with is food.
			else {
				switch (other.tag) {
				case "Food":
					inventory.AddItem (1);
					_slotManger.updateCheck = true;
					break;
				case "Soda":
					inventory.AddItem (2);
					_slotManger.updateCheck = true;
					attackOrDefend.AddAmmo (9);
					break;
				case "Ammo":
					attackOrDefend.AddAmmo (1);
					break;
				}
				//Disable the object the player collided with.
				other.gameObject.SetActive (false);
			}
		}

		
		//Restart reloads the scene when called.
		private void Restart ()
		{
			//GameManager.instance.inventory.saveInventory (GameManager.instance.items);

			//Load the last scene loaded, in this case Main, the only scene in the game.
			Application.LoadLevel (Application.loadedLevel);
		}
		
		
		//Losehealth is called when an enemy attacks the player.
		//It takes a parameter loss which specifies how many points to lose.
		public void LoseHealth (int loss)
		{
		//Set the trigger for the player animator to transition to the playerHit animation.
		animator.SetTrigger ("playerHit");
			
		//Subtract lost health points from the players total.
		health -= loss;
			
		//Update the health display with the new total.
		healthText.text = "-" + loss + " " + health + "%";
		healthSlider.value = health;
		//Check to see if game has ended.
		CheckIfGameOver ();
	}

		public void GainHealth (int gain)
		{	
			if (health < 100) {
				if (health <= 100 - gain) {
					//Add points to players health points total
					health += gain;
					
					//Update healthText to represent current total and notify player that they gained points
					healthText.text = "+" + gain + " " + health + "%";
				} else {
					restOfHealth = 100 - health;
					health = 100;
					healthText.text = "+" + restOfHealth + " " + health + "%";
				}
				healthSlider.value = health;
				switch (Random.Range(1,3)) {
				case 1:
					//Call the RandomizeSfx function of SoundManager and pass in two eating sounds to choose between to play the eating sound effect.
					SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
					break;
				case 2:
					//Call the RandomizeSfx function of SoundManager and pass in two drinking sounds to choose between to play the drinking sound effect.
					SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound2);
					break;
				default:
					break;
				}
			}
		}

		
		
		//CheckIfGameOver checks if the player is out of health points and if so, ends the game.
		private void CheckIfGameOver ()
		{
			//Check if health point total is less than or equal to zero.
			if (health <= 0) {
				//Call the PlaySingle function of SoundManager and pass it the gameOverSound as the audio clip to play.
				SoundManager.instance.PlaySingle (gameOverSound);
				
				//Stop the background music.
				SoundManager.instance.musicSource.Stop ();
				
				//Call the GameOver function of GameManager.
				GameManager.instance.GameOver ();
			}
		}

		public void EnableJoystick()
		{
			//Enabel joystick at the start
			joystick.GetComponent<Image>().enabled = true;
			//not attached in my scene
			//joystickBounds.GetComponent<Image>().enabled = true;
			//joystick.enabled = true;
		}
	}

