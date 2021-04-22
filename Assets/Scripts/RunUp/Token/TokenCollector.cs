using UnityEngine;

namespace RunUp.Token {
    public class TokenCollector : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Token")) return;
            
            var token = other.gameObject.GetComponent<Token>();
            token.Collect();
        }
    }
   
}