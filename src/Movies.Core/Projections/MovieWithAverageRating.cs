namespace Movies.Core.Projections
{
    public class MovieWithAverageRating
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genre { get; set; }
        public double AverageRating { get; set; }
    }
}
