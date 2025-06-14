using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthUI : MonoBehaviour
{

    [SerializeField] Image[] healthBarImages;
    [SerializeField] Sprite fullLifeSprite;
    [SerializeField] Sprite emptyLifeSprite;
    [SerializeField] PlayerHealth healthScript;
    [SerializeField] CombinedPlayerHealth combinedPlayerHealthScript;

    [SerializeField] bool isUsingCombinedHealth = true;
    private void Start()
    {
        
    }
    void Update()
    {
        if (isUsingCombinedHealth)
        {
            for(int i = 0; i < healthBarImages.Length; i++)
            {
                if(i >= combinedPlayerHealthScript.maximumLivesCount)
                {
                    healthBarImages[i].enabled = false;
                }
                else if(i < combinedPlayerHealthScript.livesCount)
                {
                    healthBarImages[i].enabled = true;
                    healthBarImages[i].sprite = fullLifeSprite;
                }
                else
                {
                    healthBarImages[i].enabled = true;
                    healthBarImages[i].sprite = emptyLifeSprite;
                }
            }
        }

        else

        {
            for(int i = 0; i < healthBarImages.Length; i++)
            {
                if(i < healthScript.livesCount)
                {
                    healthBarImages[i].sprite = fullLifeSprite;
                }
                else
                {
                    healthBarImages[i].sprite = emptyLifeSprite;
                }
            }
        }
       
        Canvas.ForceUpdateCanvases();
    }
}
