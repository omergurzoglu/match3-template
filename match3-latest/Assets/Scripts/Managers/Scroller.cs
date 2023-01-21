
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage backGround;
    [SerializeField] private float xSpeed, ySpeed;

    private void Update() =>
        backGround.uvRect = new Rect(backGround.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime,
            backGround.uvRect.size);
}
