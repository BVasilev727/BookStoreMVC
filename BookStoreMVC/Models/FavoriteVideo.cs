using BookStoreMVC.Areas.Identity.Data;

namespace BookStoreMVC.Models
{
    public class FavoriteVideo
    {
        public string UserId { get; set; }
        public string VideoId { get; set; }

        public BookStoreMVCUser User { get; set; } //nav property
        public Video Video { get; set; } // nav property

    }
}
