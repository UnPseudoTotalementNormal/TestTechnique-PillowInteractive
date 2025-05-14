using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sailor.UI
{
    public class SleepCompletionDisplay : MonoBehaviour
    {
        [SerializeField] private SailorController sailorController;
        [SerializeField] private Image sleepProgressBar;
    
        private bool isVisible;
    
        private void Awake()
        {
            Hide();
        }

        private void Update()
        {
            if (sailorController.currentState != SailorStates.Tired)
            {
                if (isVisible)
                {
                    Hide();
                }
                return;
            }
        
            if (!isVisible)
            {
                Show();
            }
        
            float _sleepProgress = 1f - (sailorController.tiredness / sailorController.tirednessThreshold);
            sleepProgressBar.fillAmount = _sleepProgress;
        }

        private void Show()
        {
            sleepProgressBar.transform.DOScale(Vector3.one, 0.5f);
            sleepProgressBar.DOFade(1, 0.5f);
            isVisible = true;
        }

        private void Hide()
        {
            sleepProgressBar.transform.DOScale(Vector3.zero, 0.5f);
            sleepProgressBar.DOFade(0, 0.5f);
            isVisible = false;
        }
    }
}