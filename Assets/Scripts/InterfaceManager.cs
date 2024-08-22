using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]
    private Image _fullScreenPanel;
    [SerializeField]
    private TMP_Text _cableScoreTXT;
    public void LoadScene()
    {
        _fullScreenPanel.DOFade(1, 0f);
        _fullScreenPanel.DOFade(0, 3f);
    }

    public void UpdateCableText(int collected, int total)
    {
        _cableScoreTXT.text = "Cables: " + collected + "/" + total;
    }

    public Tween FadeScreenToBlack(float time = 3f)
    {
        return _fullScreenPanel.DOFade(1, time);
    }
}
