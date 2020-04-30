using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    public Sprite Check;
    public Sprite Cross;

    private Image m_Image;
    private bool m_animationCompleted;
    private float m_FillAmount;
    // Start is called before the first frame update
    void Start()
    {
        m_FillAmount = 0;
        m_animationCompleted = false;
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_Image.fillAmount = m_FillAmount;
        CustomizeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AnimationCompleted()
    {
        return m_animationCompleted;
    }

    public void Correct()
    {
        CustomizeAnimation();
        m_Image.sprite = Check;

        StartCoroutine(fillingEffect());
    }

    public void Wrong()
    {
        CustomizeAnimation();
        m_Image.sprite = Cross;

        StartCoroutine(fillingEffect());
    }

    public void Clear()
    {
        m_FillAmount = 0;
        m_Image.fillAmount = m_FillAmount;
        m_animationCompleted = false;
    }

    IEnumerator fillingEffect()
    {
        while (m_FillAmount < 1)
        {
            m_FillAmount += 0.05f;
            m_Image.fillAmount = m_FillAmount;

            yield return null;
        }

        m_animationCompleted = true;
    }

    private void CustomizeAnimation()
    {
        int fillMethod = 1;
        int origin = (int)Random.Range(0, 3);

        m_Image.fillMethod = (Image.FillMethod)fillMethod;
        m_Image.fillOrigin = origin;
    }
}
