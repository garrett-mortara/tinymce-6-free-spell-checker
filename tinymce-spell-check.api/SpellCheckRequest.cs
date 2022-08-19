namespace tinymce_spell_check.api;

public class SpellCheckRequest
{
  public SpellCheckRequest(string text, string lang, string method)
  {
    Text = text;
    Lang = lang;
    Method = method;
  }
  public string Text { get; set; }
  public string Lang { get; set; }
  public string Method { get; set; }
}
