using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public string newPlayerName;
   public string playerName;
   public int newBestPoint;
   public int bestPoint;

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
   }
}
