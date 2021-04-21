using TMPro;
using UnityEngine;

public class Window_VideoURLInput : UIWindow
{
    [SerializeField]
    TMP_InputField urlField;

    System.Action<string> OnURLSubmitted;

    public void Show(System.Action<string> OnURLSubmitted)
    {
        base.Show();
        Reset();
        this.OnURLSubmitted = OnURLSubmitted;
    }

    public override void Close()
    {
        this.OnURLSubmitted?.Invoke(urlField.text);
        this.OnURLSubmitted = null;
        base.Close();
    }

    public void Reset()
    {
        this.OnURLSubmitted = null;
        urlField.SetTextWithoutNotify("");
    }
}
