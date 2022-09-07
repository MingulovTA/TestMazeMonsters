using System;
using TestMazeMonsters.UI.Popups.Core;

namespace TestMazeMonsters.UI.Popups
{
    public interface IPopupService
    {
        void TryToOpenPopup(PopupId popupId);
        void TryToOpenPopup(PopupId popupId, Action<PopupCloseResult> popupCloseCallback);
        T TryToOpenPopup<T>(PopupId popupId, Action<PopupCloseResult> popupCloseCallback) where T : class;
    }
}
