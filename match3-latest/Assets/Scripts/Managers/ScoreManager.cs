
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int _score=0;
        private void OnEnable() => GridManager.GemDestroyed += UpdateScore;
        private void OnDisable() => GridManager.GemDestroyed -= UpdateScore;

        private void UpdateScore()
        {
            scoreText.transform.DORewind();
            scoreText.transform.DOPunchScale (new Vector3 (1, 1, 1), .25f);
            _score++;
            scoreText.text = _score.ToString();
        }
    }
}