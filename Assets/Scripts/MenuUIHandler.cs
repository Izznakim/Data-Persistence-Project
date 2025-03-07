using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
   public TMP_InputField newPlayerName;
   public TextMeshProUGUI bestScoreText;

   private void Start()
   {
      GameManager.Instance.LoadBestScore();
      bestScoreText.text = $"Best Score : {GameManager.Instance.playerName} : {GameManager.Instance.bestPoint}";
   }

   public void StartGame()
   {
      GameManager.Instance.newPlayerName = newPlayerName.text;
      SceneManager.LoadScene(1);
   }

   public void HighScoreScene()
   {
      SceneManager.LoadScene(2);
   }

   public void QuitGame()
   {
#if UNITY_EDITOR
      EditorApplication.ExitPlaymode();
#else
      Application.Quit();
#endif
   }
}
