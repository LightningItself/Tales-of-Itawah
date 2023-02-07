using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeaderboardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image spriteImage;
    [SerializeField] private Sprite buttonDown;
    [SerializeField] private Sprite buttonUp;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject mainMenu;

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
        leaderboard.SetActive(true);
        mainMenu.SetActive(false);

    }

}
