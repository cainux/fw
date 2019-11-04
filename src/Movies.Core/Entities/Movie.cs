using System.Collections.Generic;

namespace Movies.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genre { get; set; }
        public IList<MovieRating> Ratings { get; set; }
    }
}