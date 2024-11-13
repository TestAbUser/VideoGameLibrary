using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.Json;

namespace VideoGameLibrary.Models
{
    public class VideoGame
    {
        /// <summary>
        /// The video game Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// The video game name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The video game studio
        /// </summary>
        public required string Studio { get; set; }

        /// <summary>
        /// The video game genres
        /// </summary>
        public required string Genres { get; set; }
    }
}
