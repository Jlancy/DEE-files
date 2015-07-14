using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace KnightlyTales{
	public class NPC : MonoBehaviour {
		public string DefaultPharse;
		string TestString;
		[Range(0,0.5f)]
		public float TextSpeed =0.5f;
		bool stopText = false;  // break foreach loop to type text stopCoroutine will not stop it
		//probaly gonna add these to a Quest script;
	

		Image ChatBubble;
		Text ChatText;
	
		// Use this for initialization
		void Start () {
			//find The chatBubble
			// need to create one if it doesnt exit
			ChatBubble = GameObject.FindGameObjectWithTag("ChatBubble").GetComponent<Image>();
			ChatText   = GameObject.FindGameObjectWithTag("ChatText").GetComponent<Text>(); 
			disableChatBubble();
			TestString = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

	       


		}
		
		// Update is called once per frame

		void Update()
		{
		//	Debug.Log(ChatText.text.Length);


			
		}
		void OnTriggerEnter2D(Collider2D other)
		{
			stopText =false;
			enableChatBubble();
			StartCoroutine(TypeText(TestString));
		}
		void OnTriggerExit2D(Collider2D other)
		{
			//StopCoroutine(TypeText(DefaultPharse));
			stopText = true;
			disableChatBubble();


		}


		void enableChatBubble()
		{
			ChatText.text = "";
			ChatBubble.enabled = true;
			ChatText.enabled   = true;
		}

		void disableChatBubble()
		{
			ChatBubble.enabled = false;
			ChatText.enabled   = false;
		}

		IEnumerator TypeText(string TextToDisplay)
		{

			foreach ( char letter in TextToDisplay.ToCharArray())
			{
				ChatText.text += letter;
				// add sound 
				if(stopText)
					break;

				else
					yield return new  WaitForSeconds(TextSpeed);

			}

		}

	}
}
