// Models/CountryModels.cs — C# with inline comments
// ------------------------------------------------------------

using System.Collections.Generic;

namespace ApiShowcase.Wpf.Models // Namespace groups related classes of this project
{
    public class CountryRaw
    {
        public Name name { get; set; } = new Name();
        public List<string> capital { get; set; } = new List<string>();
        public long population { get; set; }
        public string cca2 { get; set; } = string.Empty;
    }

    public class Name
    {
        public string common { get; set; } = string.Empty;
    }

    public class CountryItem
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public long Population { get; set; }
    }
}