using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class TimeLineBar : MonoBehaviour
{
    public Image timeLineImage;

    public void EmptyBar()
    {
        ChangeBarFillAmount(0, 4f);
    }

    public void ReloadBar()
    {
        ChangeBarFillAmount(1, 0.5f);
    }

    public void ChangeBarFillAmount(float fillAmount, float timeDuration)
    {
        timeLineImage.DOFillAmount(fillAmount, timeDuration).SetEase(Ease.Linear);
    }
}
