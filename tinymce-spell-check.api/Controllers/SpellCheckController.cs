using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WeCantSpell.Hunspell;

namespace tinymce_spell_check.api.Controllers;

[ApiController]
[Route("[controller]")]
public class SpellCheckController : ControllerBase
{
  IWebHostEnvironment _env;
  public SpellCheckController(IWebHostEnvironment env)
  {
    _env = env;
  }

  [HttpPost]
  public ActionResult<Dictionary<string, Dictionary<string, IEnumerable<string>>>> SpellCheck([FromForm] SpellCheckRequest request)
  {
    var resourcePath = Path.Combine(_env.ContentRootPath, "Dicts");
    WordList? wordList = null;

    // Throws an exception if no files match selected language
    var dictFile = new DirectoryInfo(resourcePath).GetFiles($"{request.Lang}*.dic").First().FullName;
    var affFile = new DirectoryInfo(resourcePath).GetFiles($"{request.Lang}*.aff").First().FullName;
    wordList = WordList.CreateFromFiles(dictFile, affFile);

    var words = Regex.Matches(request.Text, @"\w(?<!\d)[\w'-]*");
    var suggestions = new Dictionary<string, Dictionary<string, IEnumerable<string>>>();
    suggestions.Add("words", new Dictionary<string, IEnumerable<string>>());

    foreach (Match word in words)
    {
      if (!wordList.Check(word.Value))
      {
        if (!suggestions["words"].ContainsKey(word.Value))
          suggestions["words"]!.Add(word.Value, wordList.Suggest(word.Value));
      }
    };

    return Ok(suggestions);
  }
}
