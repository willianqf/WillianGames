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
    public static PlayerController playercontroller;
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
    
    /// <summary>
    /// PISCAR PLAYER
    SpriteRenderer spriteplayer;
    float time = 0;
    float time2 = 0.05f;

    float inv1 = 0;
    float inv2 = 3f;

    bool InvEnd = false;

    void Awake()
    {
        playercontroller = this;
    }
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        playerInput = GetComponent<PlayerInput>();
        playerFacing = GetComponent<CharacterFacing2D>();
        damageable = GetComponent<IDamageable>();
        damageable.DeathEvent += OnDeath;
        spriteplayer = GetComponent<SpriteRenderer>();
        if (weaponObjet != null)
        {
            Weapon = weaponObjet.GetComponent<IWeapon>();
        }
        ReiniciarContagemInvencivel();
    }
    private void OnDestroy() {
        if(damageable != null){
            damageable.DeathEvent -= OnDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ContInvencivel();
        if(InvEnd)
        {
            PiscarPlayer();
            StopPlayerOn();
        }
        else
        {
            RunPlayerOn();
        }
    }

    void RunPlayerOn()
    {
        PlayerMovementRun(true);
        //PlayerMovementCheck();
        AttackButtonCheck();
        JumpButtonClick();
        playerMovement.UnCrouch();
    }

    void StopPlayerOn()
    {
        PlayerMovementRun(false);
        playerMovement.Crouch();

    }
    private void AttackButtonCheck()
    {
        /// <summary>
        /// ATACAR
        /// </summary>
        if(Weapon != null && playerInput.IsAttackButtonDown()){
            Weapon.Attack();
        }
    }
    private void CrochButtonCheck()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// AGACHAR 
        /// </summary>
        if(playerInput.IsCrochButtonDown()){
            playerMovement.Crouch();
        }else if (playerInput.IsCrouchButtonUp()){
            playerMovement.UnCrouch();
        }
    }
    private void PlayerMovementCheck()
    {
        /// <summary>
        /// MOVIMENTAÇÃO DO PERSONAGEM
        /// </summary>
        /// 
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput); /// TECLA PRESSIONADO
        playerFacing.UpdateFacing(movementInput);
    }
    private void PlayerMovementRun(bool check)
    {
        Vector2 movementInput;
        if (check)
        {
            movementInput = new Vector2(1, 0);
        }
        else
        {
            movementInput = new Vector2(0, 0);
        }
        playerMovement.ProcessMovementInput(movementInput); /// TECLA PRESSIONADO
        playerFacing.UpdateFacing(movementInput);
    }

    private void JumpButtonClick()
    {
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

    void PiscarPlayer()
    {
        time += Time.deltaTime;
        if (time >= time2)
        {
            spriteplayer.enabled = true;
            time = 0;
        } 
        else
        {
            spriteplayer.enabled = false;
        }     
    }

    void ContInvencivel()
    {
        inv1 += Time.deltaTime;
        if (inv1 >= inv2)
        {
            InvEnd = false;
        }
        if(!InvEnd)
        {
            spriteplayer.enabled = true;
        }
    }

    public void ReiniciarContagemInvencivel()
    {
        inv1 = 0;
        InvEnd = true;
    }
    private void OnDeath(){
        playerMovement.StopImmediately(); //StopImmediately <Chama a função que para a movimentação do jogador>
        enabled = false; //Para de tocar a função update
    }

    public void NextLevel()
    {

    }


}
