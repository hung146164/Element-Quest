using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSet : MonoBehaviour
{
    public static BackgroundSet instance;
    [SerializeField] Transform Player;
    [SerializeField] float yExtra;
    [SerializeField] Sprite[] SpritesBackground;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        instance = this;
    }
    private void FixedUpdate()
    {
        Vector2 newPos= new Vector2(Player.position.x, Player.position.y+yExtra);
        transform.position = newPos;
    }
    //ThayHinhNen
    public void SetSpriteBackGround(int level)
    {
        if(level>=0 && level<SpritesBackground.Length)
        {
            spriteRenderer.sprite = SpritesBackground[level];
        }
    }
    
}
