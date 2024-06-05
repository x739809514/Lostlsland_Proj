using System;
using UnityEngine;

public class MailBox : Interaction
{
    public SpriteRenderer mailOpenBoxSprite;
    private SpriteRenderer mailBox;
    private BoxCollider2D collider2D;

    private void OnEnable()
    {
        EventSystem.afterLoadScene += OnAfterLoadScene;
    }

    private void OnDisable()
    {
        EventSystem.afterLoadScene -= OnAfterLoadScene;
    }

    private void OnAfterLoadScene()
    {
        if (isDone==false)
        {
            mailBox.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            mailBox.sprite = mailOpenBoxSprite.sprite;
            collider2D.enabled = false;
        }
    }

    private void Awake()
    {
        mailBox = transform.GetComponent<SpriteRenderer>();
        collider2D = transform.GetComponent<BoxCollider2D>();
    }

    protected override void OnItemClick()
    {
        base.OnItemClick();
        mailBox.transform.GetChild(0).gameObject.SetActive(true);
        mailBox.sprite = mailOpenBoxSprite.sprite;
        collider2D.enabled = false;
    }
}
