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
        bool isAvailable = IsAvailable();
        //Memoria do BOT
        if (aiVision.IsVisible(target) && isAvailable){
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        // RETORNA VERDADEIRO OU FALSO SE O BOT ESTIVER FORA DE VISÃO E VIVO/MORTO
        return Time.time < forgetTargetTime && isAvailable;
        //return false;
        //return Vector2.Distance(gameObject.transform.position, target.transform.position) < 3;
    }
    bool IsAvailable(){
        if (target == null)
        {
            return false;
        }
        // TROCAR ALTERNATIVA PARA GET COMPONENT
        IDamageable damageable = target.GetComponent<IDamageable>();
        if(damageable != null)
        {
            return !damageable.IsDead;
        }
        return true;
    }
}
