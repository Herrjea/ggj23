using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] float frameDuration;

    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer imageRenderer;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        imageRenderer = GetComponent<SpriteRenderer>();
        print(name + " " + sprites.Length);
    }

    private void Update()
    {
        if ((timer += Time.deltaTime) >= frameDuration)
        {
            timer = 0;
            imageRenderer.sprite = sprites[index];
            index = ++index % sprites.Length;
        }
    }
}
