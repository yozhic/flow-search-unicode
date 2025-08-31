using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using Flow.Launcher.Plugin;

namespace Flow.Launcher.Plugin.SearchUnicode.Emoji
{
    public class EmojiPlugin : IPlugin, IContextMenu, ISettingProvider
    {
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        private (string stdout, string stderr) ExecuteUni(string action, IEnumerable<string> query)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "uni.exe"),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };
            startInfo.ArgumentList.Add("-as");
            startInfo.ArgumentList.Add("json");
            startInfo.ArgumentList.Add("-format");
            startInfo.ArgumentList.Add("all");
            startInfo.ArgumentList.Add(action);
            var settings = _context.API.LoadSettingJsonStorage<Settings>();
            startInfo.ArgumentList.Add("-gender");
            startInfo.ArgumentList.Add(settings.Gender);
            startInfo.ArgumentList.Add("-tone");
            startInfo.ArgumentList.Add(settings.Tone);
            startInfo.ArgumentList.Add("--");
            query.ToList().ForEach(q => startInfo.ArgumentList.Add(q));

            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                return (stdout, stderr);
            }
        }

        public List<Result> Query(Query query)
        {
            if (string.IsNullOrWhiteSpace(query.Search))
            {
                return new List<Result>(
                    new Result[] {
                        new Result {
                            Title = "Search emoji",
                            SubTitle = "Type a keyword to search for a Unicode emoji",
                            ActionKeywordAssigned = "u",
                            Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue721"), // Search
                        }
                    }
                );
            }

            var (stdout, stderr) = ExecuteUni("emoji", new List<string> { query.Search });
            if (stdout.Length == 0)
            {
                return new List<Result>(
                    new Result[] {
                        new Result {
                            Title = "No emoji found",
                            SubTitle = "Try searching for another keyword",
                            ActionKeywordAssigned = "u",
                            Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue71e"), // Warning
                        }
                    }
                );
            }

            try
            {
                var emojis = JsonSerializer.Deserialize<List<EmojiInfo>>(stdout);

                if (emojis.Count == 0)
                {
                    return new List<Result> {
                        new Result {
                            Title = "No matches found",
                            SubTitle = "Try searching for something else",
                            ActionKeywordAssigned = "u",
                            Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue721"), // Search
                        }
                    };
                }
                
                return emojis.Take(20).Select(c => new Result
                    {
                        Title = $"{c.Emoji} — {c.Name}",
                        SubTitle = $"{c.Codepoint}: {c.Group} ({c.Subgroup}). {c.Cldr}",
                        ActionKeywordAssigned = "e",
                        CopyText = c.Emoji,
                        IcoPath = c.ImageCdn,
                        ContextData = c,
                        Action = _ =>
                        {
                            System.Windows.Clipboard.SetText(c.Emoji);
                            return true;
                        }
                    }).ToList();
            }
            catch (JsonException e)
            {
                return new List<Result>(
                    new Result[] {
                        new Result {
                            Title = "Error parsing JSON",
                            SubTitle = e.Message,
                            ActionKeywordAssigned = "u",
                            Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue71e"), // Warning
                        }
                    }
                );
            }
        }

        public List<Result> LoadContextMenus(Result selectedResult)
        {
            if (selectedResult.ContextData is not EmojiInfo emoji)
            {
                return new List<Result>();
            }
            if (emoji == null)
            {
                return new List<Result>();
            }

            return new List<Result>
            {
                new Result
                {
                    Title = "CLDR main keywords",
                    SubTitle = emoji.Cldr,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("Segoe Fluent Icons", "\uf58b"), // pps 2 landscape
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Cldr);
                        return true;
                    }
                },
                new Result
                {
                    Title = "CLDR full keywords",
                    SubTitle = emoji.CldrFull,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("Segoe Fluent Icons", "\uf58d"), // pps 4 landscape
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.CldrFull);
                        return true;
                    }
                },
                new Result
                {
                    Title = "Unicode codepoint",
                    SubTitle = emoji.Codepoint,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("sans-serif", "U+"),
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Codepoint);
                        return true;
                    }
                },
                new Result
                {
                    Title = "Emoji",
                    SubTitle = emoji.Emoji,
                    ActionKeywordAssigned = "e",
                    IcoPath = emoji.ImageCdn,
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Emoji);
                        return true;
                    }
                },
                new Result
                {
                    Title = "Name",
                    SubTitle = emoji.Name,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("Segoe Fluent Icons", "\uec6c"), // favicon 2
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Name);
                        return true;
                    }
                },
                new Result
                {
                    Title = "Group",
                    SubTitle = emoji.Group,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue8a9"), // view all
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Group);
                        return true;
                    }
                },
                new Result
                {
                    Title = "Subgroup",
                    SubTitle = emoji.Subgroup,
                    ActionKeywordAssigned = "e",
                    Glyph = new GlyphInfo("Segoe Fluent Icons", "\ue8b3"), // select all
                    Action = _ =>
                    {
                        System.Windows.Clipboard.SetText(emoji.Subgroup);
                        return true;
                    }
                },
            };
        }
        
        public Control CreateSettingPanel()
        {
            return new SettingsControl(_context);
        }
    }
    public class EmojiInfo
        {
            [JsonPropertyName("cldr")]
            public string Cldr { get; set; }

            [JsonPropertyName("cldr_full")]
            public string CldrFull { get; set; }

            [JsonPropertyName("cpoint")]
            public string Codepoint { get; set; }

            [JsonPropertyName("emoji")]
            public string Emoji { get; set; }

            [JsonPropertyName("group")]
            public string Group { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("subgroup")]
            public string Subgroup { get; set; }

            public string ImageCdn
            {
                get
                {
                    var codepoint = Codepoint.Replace("U+", "").Replace(" ", "-").ToLower();
                    return $"assets\\{codepoint}.webp";
                }
            }
        }
    
    public class Settings
    {
        private string _gender = "people";

        public string Gender { 
            get {
                return _gender;
            } 
            set {
                _gender = value;
            }
        }

        private string _tone = "none";

        public string Tone { 
            get {
                return _tone;
            } 
            set {
                _tone = value;
            }
        }
    }
}