namespace Movies.Core.Entities
{
    public class MovieRating
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
    }
}