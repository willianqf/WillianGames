using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private struct PlayerInputConstants{
        public const string Horizontal = "Horizontal";
        public const string Jump = "Jump";
        public const string Vertical = "Vertical";
    }
    /// <summary>
    /// DETECTA MOVIMENTO DO JOGADOR
    /// </summary>
    public Vector2 GetMovementInput(){
        //input do teclado
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
        //Se o input do teclado for 0 tentamos ler o input do celular
        //Mathf.Approximately <- Lê aproximadamente (valor, "valor de comparação)
        if (Mathf.Approximately(horizontalInput, 0.0f)){
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }
        return new Vector2(horizontalInput, 0);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DETECTA O BOTÃO DE PULO DO JOGADOR E SE ESTÀ PRECIONADO
    /// </summary>
    public bool IsJumpButtonDown(){
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);
        return isKeyboardButtonDown || isMobileButtonDown; /// RETORNA VERDADEIRO OU FALSO CASO O BOTÃO SEJA APERTADO
    }
    public bool IsJumpButtonHeld(){
        bool isKeyboardButtonDown = Input.GetKey(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);
        return isKeyboardButtonDown || isMobileButtonDown;/// RETORNA VERDADEIRO ENQUANTO O BOTÃO DE PULO ESTEJA PRESSIONADO
    }

    /// ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// DETECTA O BOTÃO DE AGACHAR ESTÁ PRECIONADO TECLADO/MOBILE
    /// </summary>
    public bool IsCrochButtonDown(){
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.S);
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < 0;
        return isMobileButtonDown || isKeyboardButtonDown;
    }
    public bool IsCrouchButtonUp(){
        bool isKeyboardButtonDown = Input.GetKey(KeyCode.S) == false;
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) >= 0;
        return isKeyboardButtonDown && isMobileButtonDown;
    }
}
