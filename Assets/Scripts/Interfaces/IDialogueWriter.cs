using TMPro;

namespace Assets.Scripts.Interfaces
{
    internal interface IDialogueWriter
    {
        public bool IsFinished { get; }
        void WriteDialogueText(TextMeshProUGUI tmpro, string text);
    }
}