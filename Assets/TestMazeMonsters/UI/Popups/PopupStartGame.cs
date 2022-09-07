using UnityEngine;
using UnityEngine.UI;

namespace TestMazeMonsters.UI.Popups
{
    public class PopupStartGame : PopupBase
    {
        [SerializeField] private Text _title;
        [SerializeField] private Text _message;

        public void Init(string title, string message)
        {
            _title.text = title;
            _message.text = message;
        }
    }
}
