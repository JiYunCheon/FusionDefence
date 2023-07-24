using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnAttackEvent();
public delegate void OnAnimEndEvent();
public delegate void OnSkillAttackEvent();


public class AnimationEventReceiver : MonoBehaviour
{
    public OnAttackEvent callbackAttackEvent=null;
    public OnAnimEndEvent callbackEndEvent = null;
    public OnSkillAttackEvent callbackSkillAttackEvent = null;
	

	public void AttackEvent()
	{
		//Debug.Log("## Attack Event");
		if (callbackAttackEvent != null)
			callbackAttackEvent();

	}

	//
	// 애니메이션이 종료될 때 호출되는 이벤트
	public void EndEvent()
	{
		if (callbackEndEvent != null)
			callbackEndEvent();
	}

	public void SkillAttackEvent()
	{
		//Debug.Log("## Queen_Skill Attack Event");
		if (callbackSkillAttackEvent != null)
			callbackSkillAttackEvent();

	}
}
