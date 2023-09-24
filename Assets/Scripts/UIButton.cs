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
        // 버튼이 클릭될 때마다 색상을 토글합니다.
        isSoundOn = !isSoundOn;

        // 토글된 색상을 설정합니다.
        if (isSoundOn)
        {
            SoundManager.Instance.SetEffectsVolume(1);
            SoundManager.Instance.SetMusicVolume(1);
            button.GetComponent<Image>().color = new Color(1, 1, 1, 1); // 원하는 색상으로 변경
        }
        else
        {
            SoundManager.Instance.SetEffectsVolume(0);
            SoundManager.Instance.SetMusicVolume(0);
            button.GetComponent<Image>().color = new Color((float)70 / 255, (float)80 / 255, (float)100 / 255, 1); // 원하는 색상으로 변경
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
