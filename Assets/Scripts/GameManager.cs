using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public string playerName;
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
}
