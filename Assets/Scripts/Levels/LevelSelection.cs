using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private Save _save;

        private int LevelCount => SceneManager.sceneCountInBuildSettings - 1;

        private void Start()
        {
            int next = (_save.GetLevel()-1) % LevelCount +1 ;
            SceneManager.LoadScene("Level"+ next.ToString());
        }
    }
}