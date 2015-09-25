using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	[System.Serializable]
	public class Equipment   {

		public string Name;


		// Use this for initialization

		public Equipment()
		{
			Name = "Default";

		}

		public void ReName ( string name)
		{
			Name = name;
		}



	}
	[System.Serializable]
	public class Sword : Equipment
 	{
		public float Attack ;
		public float AttackSpeed ;


		public Sword(float attack, float attackSpeed )
		{
			Attack = attack;
			AttackSpeed = attackSpeed;
		}
		public  void NewStats(float attack , float attackSpeed)
		{
			Attack = attack; 
			AttackSpeed = attackSpeed;
		}
	}
	[System.Serializable]
	public class Shield : Equipment
	{
		public float Defense;


		public Shield(float defense)
		{
			Defense = defense;
		}

		public void NewStats(float defense )
		{
			Defense = defense;
		
		}
	}
	[System.Serializable]
	public class Bow : Equipment
	{
		public float Attack;
		public float AttackSpeed;

		public Bow(float attack, float attackSpeed)
		{
			Attack = attack ;
			AttackSpeed = attackSpeed;
		}
		public void NewStats(float attack , float attackSpeed)
		{

			Attack = attack; 
			AttackSpeed = attackSpeed;
		}
	}
	[System.Serializable]
	public class Quiver : Equipment 
	{
		public float QuiverSize;

		public Quiver(float quiverSize)
		{
			QuiverSize = quiverSize;
		}

		public void NewStats(float quiverSize)
		{
			QuiverSize = quiverSize;
			
		}

	}
	[System.Serializable]
	public class GearType
	{
		public Type Gear;
		public enum Type
		{
			Sword,
			Bow,
			Shield,
			Quiver
		}

		public GearType(Type gear)
		{
			Gear = gear;
		}
	}




	
	
	


