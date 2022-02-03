using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))] /// INFORMA A UNITY QUE É NECESSÁRIO TER ESTE COMPONENTE
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    CharacterFacing2D playerFacing;
    PlayerInput playerInput;

    public IWeapon Weapon {get; private set;}
    [SerializeField] 
    GameObject weaponObjet;
    IDamageable damageable;
    // Start is called before the first frame update
    
    [Header("Camera")]
    [SerializeField]
    private Transform cameraTarget;
    [Range(0.0f, 5.0f)]
    [SerializeField]
    private float cameraTargetOffsetX = 2.0f;
    [Range(0.5f, 50.0f)]
    [SerializeField]
    private float cameraTargetFlipSpeed = 2.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField]
    private float characterSpeedInfluence = 2.0f;
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        playerInput = GetComponent<PlayerInput>();
        playerFacing = GetComponent<CharacterFacing2D>();
        damageable = GetComponent<IDamageable>();
        damageable.DeathEvent += OnDeath;
        if (weaponObjet != null)
        {
            Weapon = weaponObjet.GetComponent<IWeapon>();
        }
    }
    private void OnDestroy() {
        if(damageable != null){
            damageable.DeathEvent -= OnDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// <summary>
        /// MOVIMENTAÇÃO DO PERSONAGEM
        /// </summary>
        /// 
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput); /// TECLA PRESSIONADO
        playerFacing.UpdateFacing(movementInput);
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// PULO DO JOGADOR
        /// </summary>
        if (playerInput.IsJumpButtonDown()){ ///Faz o jogador pular enquanto o botão de pulo for precionado
            playerMovement.Jump();
        }
        if(playerInput.IsJumpButtonHeld() == false){ /// Da impulso enquanto o botão de pulo ficar precionado
            playerMovement.UpdateJumpAbort();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// AGACHAR 
        /// </summary>
        if(playerInput.IsCrochButtonDown()){
            playerMovement.Crouch();
        }else if (playerInput.IsCrouchButtonUp()){
            playerMovement.UnCrouch();
        }

        /// <summary>
        /// ATACAR
        /// </summary>
        if(Weapon != null && playerInput.IsAttackButtonDown()){
            Weapon.Attack();
        }
    }

    private void FixedUpdate() {
        // CONTROLE DA CAMERA (TARGET DA DIREÇÃO E DO GAMEOBJECT)
        bool isFacingRight = playerFacing.IsFacingRight();
        float targetOffsetX = isFacingRight ? cameraTargetOffsetX : -cameraTargetOffsetX;
        float currentOffsetX = Mathf.Lerp(cameraTarget.localPosition.x, targetOffsetX, Time.fixedDeltaTime * cameraTargetFlipSpeed);
        currentOffsetX += playerMovement.CurrentVelocity.x * Time.fixedDeltaTime * characterSpeedInfluence;
        cameraTarget.localPosition = new Vector3(currentOffsetX, cameraTarget.localPosition.y, -cameraTarget.localPosition.z);
        //
    }

    private void OnDeath(){

        playerMovement.StopImmediately(); //StopImmediately <Chama a função que para a movimentação do jogador>
        enabled = false; //Para de tocar a função update

    }

}
