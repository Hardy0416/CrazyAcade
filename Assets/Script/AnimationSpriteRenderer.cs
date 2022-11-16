using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpriteRenderer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite[] animationSprites;

    public float animationTime = 0.25f;
    public int animationFrame;

    public bool loop = true;
    public bool idle = true;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    void NextFrame()
    {
        animationFrame++;
        if(loop && animationFrame >= animationSprites.Length) {

        }
        if ( idle) {
            spriteRenderer.sprite = idleSprite;
        }
        else if(animationFrame >= 0 && animationFrame < animationSprites.Length){
            spriteRenderer.sprite = animationSprites[animationFrame];
        }

    }


}
