using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public string newPlayerName;
   public string playerName;
   public int newBestPoint;
   public int bestPoint;
   public List<(string, int)> highScore;

   private void Awake()
   {
      if (Instance != null)
      {
         Destroy(Instance);
         return;
      }
      Instance = this;
      DontDestroyOnLoad(gameObject);
   }

   [System.Serializable]
   class Player
   {
      public string playerName;
      public int bestPoint;

      public Player(string playerName = "", int bestPoint = 0)
      {
         this.playerName = playerName;
         this.bestPoint = bestPoint;
      }
   }

   [System.Serializable]
   class HighScore
   {
      public List<Player> highScore = new List<Player>();
   }

   public void SaveBestScore()
   {
      Player data = new Player();
      data.playerName = newPlayerName;
      data.bestPoint = newBestPoint;

      string json = JsonUtility.ToJson(data);

      File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
   }

   public void LoadBestScore()
   {
      string path = Application.persistentDataPath + "/savefile.json";
      if (File.Exists(path))
      {
         string json = File.ReadAllText(path);
         Player data = JsonUtility.FromJson<Player>(json);

         playerName = data.playerName;
         bestPoint = data.bestPoint;
      }
   }

   public void ResetBestScore()
   {
      File.Delete(Application.persistentDataPath + "/savefile.json");
      File.Delete(Application.persistentDataPath + "/highscore.json");
   }

   public void SaveHighScore(string playerName, int point)
   {
      List<Player> players = LoadRawHighScore();
      players.Add(new Player(playerName, point));

      players = players.OrderByDescending(p => p.bestPoint).ToList();

      if (players.Count > 10)
      {
         players.RemoveAt(players.Count - 1);
      }

      HighScore data = new HighScore { highScore = players };
      string json = JsonUtility.ToJson(data, true);
      File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
   }

   private List<Player> LoadRawHighScore()
   {
      string path = Application.persistentDataPath + "/highscore.json";
      if (File.Exists(path))
      {
         string json = File.ReadAllText(path);
         HighScore data = JsonUtility.FromJson<HighScore>(json);
         return data.highScore;
      }
      return new List<Player>();
   }

   public List<(string playerName, int point)> LoadHighScore()
   {
      List<Player> players = LoadRawHighScore();
      return players.Select(player => (player.playerName, player.bestPoint)).ToList();
   }
}
