using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Equipment   {

	public string Name;
	public int Durablity;
	public int Cost;
	[Range(0,1)]
	public float Sale;
	// Use this for initialization

	public Equipment()
	{
		Name = "Default";
		Durablity = 2;
	}

	public void ReName ( string name)
	{
		Name = name;
	}

	public int CalculateSale()
	{
		float Total = 0;
		if(Sale !=0)
		{
			Total = Sale *(float) Cost ;
			Total -= Cost; 
			Total *= -1;
		}	
		else
			Total = Cost;

		return (int)Total  ;
	}


}
[System.Serializable]
public class Sword : Equipment
{
	public int Attack ;

	public Sword(int attack )
	{
		Attack = attack;

	}
	public Sword()
	{
		Name ="Sword";
		Attack = 1;
		Durablity = 2;
		Cost =10;

		
	}

}
[System.Serializable]
public class Armor : Equipment
{
	public int HP;


	public Armor(int hp)
	{
		HP = hp;
	}
	public Armor()
	{
		Name ="Armor";
		HP = 1;
		Durablity = 2;
		Cost =10;
	}

}
[System.Serializable]
public class Bow : Equipment
{
	public int Attack;


	public Bow(int attack)
	{
		Attack = attack ;

	}
	public Bow()
	{
		Name ="Bow";
		Attack = 1;
		Durablity = 2;
		Cost =10;
	}
}
[System.Serializable]
public class Quiver : Equipment 
{
	public int QuiverSize;

	public Quiver(int quiverSize)
	{
		QuiverSize = quiverSize;
	}
	public Quiver()
	{
		Name ="Quiver";
		QuiverSize = 1;
		Durablity = 2;
		Cost =10;
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
		Armor,
		Quiver
	}

	public GearType(Type gear)
	{
		Gear = gear;
	}
}




	
	
	


