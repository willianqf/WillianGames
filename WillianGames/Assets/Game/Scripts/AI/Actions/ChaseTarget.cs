using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Framework;
using  Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Platformer2D.Character;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
    [InParam("Target")]
    private GameObject target;
    [InParam("AIController")]
    private EnemyAIController aiController;

    [InParam("ChaseSpeed")]
    private float chaseSpeed;

    [InParam("CharacterMovement")]
    private CharacterMovement2D charMovement;

    public override void OnStart(){
       base.OnStart();
       aiController.IsChasing = true;
       charMovement.MaxGroundSpeed = chaseSpeed;

    }
    public override void OnAbort(){
        aiController.IsChasing = false;
    }
   public override TaskStatus OnUpdate(){
       if (target == null){
           return TaskStatus.ABORTED;
       }
       Vector2 toTarget = target.transform.position - aiController.transform.position;
       aiController.MovementInput = new Vector2(Mathf.Sign(toTarget.x), 0);
       return TaskStatus.RUNNING;
   }
}
