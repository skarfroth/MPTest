using UnityEngine;
using UnityEngine.UI;

public class CharacterCosmetics : MonoBehaviour
{
    public int currentColorIndex = 0;
    public Material[] playerColors;
    public Image currentColorImage;
    public Text currentColorText;

    public static CharacterCosmetics instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentColorImage.color = playerColors[currentColorIndex].color;
        currentColorText.text = playerColors[currentColorIndex].name;
    }

    public void NextColor()
    {
        if (currentColorIndex < playerColors.Length - 1)
        {
            currentColorIndex++;
            currentColorImage.color = playerColors[currentColorIndex].color;
            currentColorText.text = playerColors[currentColorIndex].name;
            LobbyController.Instance.LocalPlayerObject.GetComponent<PlayerObjectController>().CmdUpdatePlayerColor(currentColorIndex);
        }
    }

    public void PreviousColor()
    {
        if (currentColorIndex > 0)
        {
            currentColorIndex--;
            currentColorImage.color = playerColors[currentColorIndex].color;
            currentColorText.text = playerColors[currentColorIndex].name;
            LobbyController.Instance.LocalPlayerObject.GetComponent<PlayerObjectController>().CmdUpdatePlayerColor(currentColorIndex);
        }
    }
}
