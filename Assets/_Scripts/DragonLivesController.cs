using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragonLivesController : MonoBehaviour
{
    public int maxLives = 3;
    public float invincibilityDuration = 2f;
    public Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    public Color normalColor = Color.white;
    public Color invincibleColor = new Color(1f, 1f, 1f, 0.45f);
    public float blinkInterval = 0.15f;

    private int currentLives;
    private bool isInvincible;
    public Renderer[] renderers;
    private Coroutine invincibilityCoroutine;

    public int CurrentLives
    {
        get { return currentLives; }
    }

    public bool IsInvincible
    {
        get { return isInvincible; }
    }

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        ResetLives();
    }

    private void OnEnable()
    {
        UpdateHearts();
    }

    private void Start()
    {
        UpdateHearts();
    }

    public bool TryTakeHit()
    {
        if (isInvincible)
        {
            return true;
        }

        currentLives--;
        UpdateHearts();

        if (currentLives <= 0)
        {
            return false;
        }

        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }

        invincibilityCoroutine = StartCoroutine(InvincibilityRoutine());
        return true;
    }

    public void ResetLives()
    {
        currentLives = Mathf.Max(1, maxLives);
        isInvincible = false;
        SetRenderersVisible(true);
        SetDragonColor(normalColor);
        UpdateHearts();
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        float timer = 0f;
        bool flash = false;
        while (timer < invincibilityDuration)
        {
            flash = !flash;
            SetRenderersVisible(!flash);
            SetDragonColor(flash ? invincibleColor : normalColor);
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        SetRenderersVisible(true);
        SetDragonColor(normalColor);
        isInvincible = false;
        invincibilityCoroutine = null;
    }

    private void SetRenderersVisible(bool isVisible)
    {
        if (renderers == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].enabled = isVisible;
            }
        }
    }

    private void SetDragonColor(Color color)
    {
        if (renderers == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null)
            {
                continue;
            }

            Material material = renderers[i].material;
            if (material.HasProperty("_Color"))
            {
                material.color = color;
            }

            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }

            if (material.HasProperty("_EmissionColor"))
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
        }
    }

    private void UpdateHearts()
    {
        if (heartImages != null && heartImages.Length > 0)
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (heartImages[i] == null)
                {
                    continue;
                }

                bool isFull = i < currentLives;
                heartImages[i].enabled = true;
                if (fullHeartSprite != null && emptyHeartSprite != null)
                {
                    heartImages[i].sprite = isFull ? fullHeartSprite : emptyHeartSprite;
                }
                else
                {
                    heartImages[i].color = isFull ? Color.red : new Color(1f, 1f, 1f, 0.25f);
                }
            }
        }

        if (UIController.instance != null && UIController.instance.healthBar != null)
        {
            UIController.instance.healthBar.maxValue = Mathf.Max(1, maxLives);
            UIController.instance.healthBar.value = Mathf.Max(0, currentLives);
        }
    }
}
