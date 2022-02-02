using Pada1.BBCore;
using UnityEngine;
using BBUnity.Conditions;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition
{
    [InParam("Target")]
    private GameObject target;
    [InParam("AIVision")]
    private AIVision aiVision;
    
    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;
    private float forgetTargetTime;
    public override bool Check()
    {
        //Memoria do BOT
        if (aiVision.IsVisible(target)){
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        return Time.time < forgetTargetTime;
        //return false;
        //return Vector2.Distance(gameObject.transform.position, target.transform.position) < 3;
    }
}
