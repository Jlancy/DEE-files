using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TextTyper : MonoBehaviour {


	[Range(0,0.1f)]
	public float TextSpeed =0.5f;
	bool stopText = false; 
	public bool SkipText = false;
	Image ChatBubble;
	public Text ChatLine;
	public int LineLength = 85;

	//Image Portrait   ;
	//bool PortraitActive = false;
	char CharSpace = ' ';
	char ColorCheck  = '1';
	char ColorEndCeck = '2';
	char CheckContinued = '3';
	public bool Typing =false;
	public bool TalkContinued = false;
	public bool EndOfSentence =false;

	//public Sprite tempTest;
	// adjust if the string is longer than set line ammount
	// Use this for initialization
	void Start () {
		ChatBubble = GameObject.FindGameObjectWithTag("ChatBubble").GetComponent<Image>();

		//Portrait = GameObject.FindGameObjectWithTag("Portrait").GetComponent<Image>();
		GameObject TextContainer = GameObject.FindGameObjectWithTag("ChatContainer");
		ChatLine = GameObject.FindGameObjectWithTag("ChatText").GetComponent<Text>();

		disableChatBubble();
	}
	
	// Update is called once per frame
	void Update () {
	//Debug.Log("stoptext ="+stopText);
	}



	public void StartText(string TalkText)
	{
		clearText();
		enableChatBubble();
		Typing = true;

		StartCoroutine(TypeText(TalkText));
	}
	public void EndText()
	{
		if(!TalkContinued){
		disableChatBubble();
		StopCoroutine(TypeText(""));
		}

		else
		{


			EndOfSentence = false;
		}
	}


	void enableChatBubble()
	{
		stopText = false;
		//			ChatText.text = "";
		ChatBubble.enabled = true;
		ChatLine.enabled = true;
		//	ChatText.enabled   = true;
	}
	
	 void disableChatBubble()
	{
		//stopText = true;

		ChatBubble.enabled = false;
		ChatLine.text ="";
		ChatLine.enabled = false;
		//Debug.Log("did a thing");
		stopText = true;
	}

	void clearText()
	{

			ChatLine.text ="";
		
	}



/*
	public void PortriatCase(Dialogue.Person person )
	{

		PortraitActive = true;
		
		switch(person)
		{
		case Dialogue.Person.Princess:
			Portrait.sprite = tempTest;
			
			break;
		case Dialogue.Person.Knight:
			Portrait.sprite = Resources.Load<Sprite>("Sprites/Icons");
			break;
		default:
			Debug.Log("welldam");
			break;
			
			
		}
		
		
	}

	void ClearPortrait()
	{
		Portrait = null;
	}
*/
	IEnumerator TypeText(string TextToDisplay)
	{

		bool CurrentLineReached = false;
		string tempHolder = "";
		string tempColorHolder= "";
		int CurrentLineLength = 0;
		bool colorText= false;
		string FrontColor ="<color=#E4A700FF>" ;
		string BackColor ="</color>";
		string StringNextChat = "";

		foreach ( char letter in TextToDisplay.ToCharArray())
		{
			if (letter == CheckContinued)
			{
				CurrentLineReached = true;
			}

			else if(letter != CharSpace)

				tempHolder += letter;
			
			else
			{
				
				tempHolder+= letter;
				CurrentLineLength += tempHolder.Length;
				
				if(CurrentLineLength >= LineLength)
				{

					CurrentLineLength = 0;
					ChatLine.text += "\n";
				}
				if(CurrentLineReached == true)
				{
					EndOfSentence = true;
					TalkContinued = true;
					StringNextChat += tempHolder;
					tempHolder = "";

				}
	
				else{
					foreach(char tempLetter in tempHolder)
					{
						if(stopText)
							break;
						
						if(tempLetter == ColorCheck)
							colorText = true;
						
						if(tempLetter == ColorEndCeck)
							colorText = false;
						
						if(tempLetter !=ColorCheck &&  tempLetter !=ColorEndCeck )
						{
							if(colorText )
							{
								
								tempColorHolder+= FrontColor;
								tempColorHolder+= tempLetter;
								tempColorHolder+= BackColor;
								
								ChatLine.text += tempColorHolder;
								tempColorHolder = "";
							}
							else{
								
								ChatLine.text += tempLetter;
							}
						}
						if(!SkipText)
						yield return new  WaitForSeconds(TextSpeed);
					
					}
					
					tempHolder="";
				}
				
			}

			if(stopText)
				break;
	
		}
		if(CurrentLineReached == true)
		{
			StringNextChat += tempHolder;
			Debug.Log(StringNextChat);
			Typing = false;

			while(EndOfSentence == true)
			{
				yield return null;

			}
			EndText();
			StartText( StringNextChat);

			tempHolder= "";

		}
		
		else if(tempHolder != null)
		{	

			foreach(char tempLetter in tempHolder)
			{
				if(stopText)
					break;

				ChatLine.text += tempLetter;

				if(!SkipText)
				yield return new  WaitForSeconds(TextSpeed);

			}
			tempHolder = null;
			Typing = false;

//				Debug.Log(Typing);
			
		}


	}
}

