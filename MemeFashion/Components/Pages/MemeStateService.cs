public class MemeStateService
{
    public string? SelectedMeme { get; private set; }

    public void SetSelectedMeme(string memeName)
    {
        SelectedMeme = memeName;
    }
}