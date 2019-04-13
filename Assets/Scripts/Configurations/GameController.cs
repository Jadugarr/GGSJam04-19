using UnityEngine;

namespace Configurations
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private SpriteConfiguration _spriteConfiguration;
        
        private void Awake()
        {
            Configurations.SpriteConfiguration = _spriteConfiguration;
        }
    }
}