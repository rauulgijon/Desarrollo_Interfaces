// Models/PokemonModels.cs — C# with inline comments
// ------------------------------------------------------------

using System.Collections.Generic;

namespace ApiShowcase.Wpf.Models // Namespace groups related classes of this project
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int height { get; set; }
        public int weight { get; set; }
        public List<PokemonStat> stats { get; set; } = new();
    }

    public class PokemonStat
    {
        public int base_stat { get; set; }
        public StatRef stat { get; set; } = new StatRef();
    }

    public class StatRef
    {
        public string name { get; set; } = string.Empty;
    }
}