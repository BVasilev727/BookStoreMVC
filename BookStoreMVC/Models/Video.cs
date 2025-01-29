namespace BookStoreMVC.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string ThumbnailURL { get; set; }
        public string PostedByUser { get; set; }
        public int Likes { get; set; } = 0;

        public Video()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ICollection<FavoriteVideo> FavoriteVideos { get; set; }
    }
}
