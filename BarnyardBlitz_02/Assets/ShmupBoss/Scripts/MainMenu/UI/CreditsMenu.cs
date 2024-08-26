using UnityEngine;
using UnityEngine.UI;

namespace ShmupBoss
{
    [AddComponentMenu("Shmup Boss/Main Menu/UI/Credits Menu")]
    public class CreditsMenu : MainMenuSubMenu
    {
        [Tooltip("The button which will disable this credits canvas and enable the main menu canvas.")]
        [SerializeField]
        private Button backButton;

        private void Start()
        {
            if (backButton != null)
            {
                backButton.onClick.AddListener(OpenMainMenu);
            }
        }
    }
}
