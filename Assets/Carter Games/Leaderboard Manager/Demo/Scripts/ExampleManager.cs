using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Assets.LeaderboardManager.Demo
{
    public class ExampleManager : MonoBehaviour
    {
        [SerializeField] private InputField boardID;
        [SerializeField] private InputField playerName;
        [SerializeField] private InputField playerScore;
        
        
        public void AddToBoard()
        {
           
        }
        
        public void RemoveFromBoard()
        {
            
        }

        public void ClearBoard()
        {
            LeaderboardManager.ClearLeaderboard(boardID.text);
        }
    }
}