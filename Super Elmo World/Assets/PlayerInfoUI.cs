using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] Image playerImage;
    [SerializeField] TextMeshProUGUI coins, score, lives;

    private void Awake()
    {
        playerImage = this.GetComponentInChildren<Image>();
        coins = this.GetComponentInChildren<TextMeshProUGUI>();
        lives = this.GetComponentInChildren<TextMeshProUGUI>();
    }

}
