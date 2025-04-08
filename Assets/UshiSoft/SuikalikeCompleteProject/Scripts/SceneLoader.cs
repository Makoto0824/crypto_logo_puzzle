using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Button startButton;
    public AudioSource audioSource;  // インスペクタで割り当て
    public AudioClip buttonSound;    // インスペクタで割り当て

    void Start()
    {
        // ボタンがクリックされたときのイベント設定
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        // 少し遅延してからボタン音を再生
        StartCoroutine(PlaySoundAndLoadScene());
    }

    System.Collections.IEnumerator PlaySoundAndLoadScene()
    {
        // ボタン音を再生
        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);

            // 音の再生時間だけ待機
            yield return new WaitForSeconds(buttonSound.length);
        }
        else
        {
            yield return null;
        }

        // Gameシーンを読み込む
        SceneManager.LoadScene("Game");
    }
}