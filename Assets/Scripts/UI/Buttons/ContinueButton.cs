using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image spriteImage;
    [SerializeField] private Sprite buttonDown;
    [SerializeField] private Sprite buttonUp;

    void Start()
    {
        spriteImage = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        spriteImage.sprite = buttonDown;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        spriteImage.sprite = buttonUp;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
