using UnityEngine;
using UnityEngine.UI;

namespace ShmupBoss
{
    [AddComponentMenu("Shmup Boss/Main Menu/UI/How To Play Menu")]
    public class HowToPlayMenu : MainMenuSubMenu
    {
        [Tooltip("The button which will disable this 'How to Play' canvas and enable the main menu canvas.")]
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
