using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(IDamageable))]
public class EnemyAIController : MonoBehaviour
{
    CharacterMovement2D enemyMovement;
    CharacterFacing2D enemyFacing;
    IDamageable damageable;

    [SerializeField]
    private TriggerDamage damager;

    private Vector2 movementInput;
    public Vector2 MovementInput{
        get
        {
            return movementInput;
        }
        set
        {
            movementInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1));
        }
    }

    private bool isChasing;
    public bool IsChasing{
        get => isChasing;
        set => isChasing = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<CharacterMovement2D>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        damageable = GetComponent<IDamageable>();
        damageable.DeathEvent += OnDeath;
    }

    private void OnDestroy() {
        if (damageable != null){
            damageable.DeathEvent -= OnDeath;
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);
    }
    /*
    public void SetMovementInputX(float x){
        movementInput.x = Mathf.Clamp(x, -1, 1); //Recebe um valor e mantem o valor entre o limite <valor, menor, maior>
    }
    */
    private void OnDeath()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        enabled = false;
        enemyMovement.StopImmediately();
        damager.gameObject.SetActive(false);
        //Destroy(gameObject);
    }

}
