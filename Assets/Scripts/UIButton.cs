using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{

    public Button button;
    public bool isSoundOn = true;

    // Start is called before the first frame update
    void Start()
    {
        //onClick.AddListener(ToggleColorOnClick);
    }

    public void SoundOnOff()
    {
        // ��ư�� Ŭ���� ������ ������ ����մϴ�.
        isSoundOn = !isSoundOn;

        // ��۵� ������ �����մϴ�.
        if (isSoundOn)
        {
            SoundManager.Instance.SetEffectsVolume(1);
            SoundManager.Instance.SetMusicVolume(1);
            button.GetComponent<Image>().color = new Color(1, 1, 1, 1); // ���ϴ� �������� ����
        }
        else
        {
            SoundManager.Instance.SetEffectsVolume(0);
            SoundManager.Instance.SetMusicVolume(0);
            button.GetComponent<Image>().color = new Color((float)70 / 255, (float)80 / 255, (float)100 / 255, 1); // ���ϴ� �������� ����
        }
    }

    public void Resume()
    {
        GameManager.Instance.PressedPauseButton();
    }

    public void Quitgame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
