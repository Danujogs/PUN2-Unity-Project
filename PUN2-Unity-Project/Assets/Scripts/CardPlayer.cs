using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    public Transform atkPosRef;
    public Card choosenCard;
    [SerializeField] private TMP_Text nameText;

    public HealthBar healthBar;
    public TMP_Text healthText;
    public float Health;
    public PlayerStats stats = new PlayerStats
    {
        MaxHealth = 100,
        RestoreValue = 5,
        DamageValue = 10
    };
    public AudioSource damageClip;
    public AudioSource drawClip;
    private Tweener animationTweener;

    public TMP_Text NickName { get => nameText; }

    public bool IsReady = false;

    private void Start()
    {
        Health = stats.MaxHealth;
    }

    public void SetStats(PlayerStats newStats, bool restoreFullHealth = false)
    {
        this.stats = newStats;
        if (restoreFullHealth)
            Health = stats.MaxHealth;

        UpdateHealthBar();
    }

    public Attack? AttackValue
    {
        get => choosenCard == null ? null : choosenCard.AttackValue;
    }

    public void Reset()
    {
        if (choosenCard != null)
        {
            choosenCard.Reset();
            choosenCard = null;
        }
    }

    public void SetChoosenCard(Card newCard)
    {
        if (choosenCard != null)
        {
            choosenCard.transform.DOKill();
            choosenCard.Reset();
        }

        choosenCard = newCard;
        choosenCard.transform.DOScale(choosenCard.transform.localScale * 1.1f, 0.2f);
    }

    public void ChangeHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, stats.MaxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        //healthbar
        healthBar.UpdateBar(Health / stats.MaxHealth);
        //text
        healthText.text = Health + " / " + stats.MaxHealth;
    }


    public void AnimateAttack()
    {
        animationTweener = choosenCard.transform
        .DOMove(atkPosRef.position, 0.4f)
        .SetEase(Ease.InOutBack);
    }

    public void AnimateDamage()
    {
        damageClip.PlayOneShot(damageClip.clip);
        var Image = choosenCard.GetComponent<Image>();
        animationTweener = Image
            .DOColor(new Color(255, 0, 0), 0.1f)
            .SetLoops(5, LoopType.Yoyo)
            .SetDelay(0.2f);
    }

    public void AnimateDraw()
    {
        drawClip.PlayOneShot(drawClip.clip);
        animationTweener = choosenCard.transform
        .DOMove(choosenCard.OriginalPositon, 0.5f)
        .SetEase(Ease.InOutBack)
        .SetDelay(0.2f);
    }

    public bool isAnimating()
    {
        return animationTweener.IsActive();
    }

    public void isClickable(bool value)
    {
        Card[] cards = GetComponentsInChildren<Card>();
        foreach (var card in cards)
        {
            card.SetClickable(value);
        }
    }
}
