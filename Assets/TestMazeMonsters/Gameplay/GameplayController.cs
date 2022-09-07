using System.Collections;
using TestMazeMonsters.Gameplay.Player;
using TestMazeMonsters.UI.Popups;
using TestMazeMonsters.UI.Popups.Core;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private IPopupService _popupService;
    
    [Inject]
    private void Construct(IPopupService popupService)
    {
        _popupService = popupService;
    }

    private IEnumerator Start()
    {
        //xD yeah...
        yield return null;
        yield return null;
        yield return null;
        _popupService.TryToOpenPopup<PopupStartGame>(PopupId.StartGame,PopupCloseHandler)
            .Init("StartGame", "Welcome text!");
    }

    private void PopupCloseHandler(PopupCloseResult result)
    {
        Debug.Log("Попап был закрыт с результом "+result);
        _playerController.gameObject.SetActive(true);
    }
    
}
