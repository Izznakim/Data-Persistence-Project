using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI highScoreText;

   private void Start()
   {
      int rank = 1;
      List<(string name, int score)> loadedPlayers = GameManager.Instance.LoadHighScore();
      foreach (var item in loadedPlayers)
      {
         highScoreText.text += $"{rank}. {item.name}: {item.score}\n";
         rank++;
      }
   }
}
