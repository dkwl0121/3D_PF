using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_Ani : MonoBehaviour {

	public const string IDLE	= "Pumpkin_Idle";
	public const string RUN		= "Pumpkin_Run";
	public const string ATTACK	= "Pumpkin_Attack";
	public const string SKILL	= "Pumpkin_Skill";
	public const string DAMAGE	= "Pumpkin_Damage";
	public const string STUN	= "Pumpkin_Stun";
	public const string DEATH	= "Pumpkin_Death";

	Animation anim;

	void Start () {
		anim = GetComponent<Animation>();
	}

	public void IdleAni (){
		anim.CrossFade (IDLE);
	}

	public void RunAni (){
		anim.CrossFade (RUN);
	}

	public void AttackAni (){
		anim.CrossFade (ATTACK);
	}

	public void SkillAni (){
		anim.CrossFade (SKILL);
	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void StunAni (){
		anim.CrossFade (STUN);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}

}
