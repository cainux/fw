using System.Collections.Generic;

namespace Movies.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IList<MovieRating> Ratings { get; set; }
    }
}