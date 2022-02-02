using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

public static class CharacterMovementAnimationKeys{
    public const string IsCrouching = "IsCrouching";
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";

}
public static class EnemyAnimationKeys{
    public const string IsChasing = "IsChasing";
}
public class CharacterAnimationController : MonoBehaviour
{
    protected Animator animator;
    protected CharacterMovement2D characterMovement;
    //protected EnemyAIController aiController;

    protected virtual void Awake(){  //Chama a função assim que o objeto é invocado
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement2D>();
        //aiController = GetComponent<EnemyAIController>();
    }

    protected virtual void Update(){
        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, characterMovement.CurrentVelocity.x / characterMovement.MaxGroundSpeed);
    }



}
