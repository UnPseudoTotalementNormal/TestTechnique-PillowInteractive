using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sailor.UI
{
    public class SleepCompletionDisplay : MonoBehaviour
    {
        [SerializeField] private SailorAI sailorAI;
        [SerializeField] private Image sleepProgressBar;
    
        private bool isVisible;
    
        private void Awake()
        {
            Hide();
        }

        private void Update()
        {
            if (sailorAI.currentState != SailorStates.Tired)
            {
                if (isVisible)
                {
                    Hide(); // Hide the UI if the sailor is not tired
                }
                return;
            }
        
            if (!isVisible)
            {
                Show(); // Show the UI if the sailor is tired
            }
        
            float _sleepProgress = 1f - (sailorAI.tiredness / sailorAI.tirednessThreshold);
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