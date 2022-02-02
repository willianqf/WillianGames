using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterFacing2D : MonoBehaviour
{
    SpriteRenderer spriteRender;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void UpdateFacing(Vector2 movementInput){
        if(movementInput.x > 0){
            spriteRender.flipX = false;
        }else if(movementInput.x < 0){
            spriteRender.flipX = true;
        }
    }
    public bool IsFacingRight(){
        return spriteRender.flipX == false;
    }
}
