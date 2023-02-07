using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image spriteImage;
    [SerializeField] private Sprite buttonDown;
    [SerializeField] private Sprite buttonUp;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text inputText;
    [SerializeField] private GameObject pauseButton;

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
        Time.timeScale = 1;

        gameManager.Name = inputText.text;
        GameObject.Find("MainMenu").SetActive(false);
        pauseButton.SetActive(true);
    }
    

}
