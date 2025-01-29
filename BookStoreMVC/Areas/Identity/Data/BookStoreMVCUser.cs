using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BookStoreMVCUser class
public class BookStoreMVCUser : IdentityUser
{
    public ICollection<FavoriteVideo> FavoriteVideos { get; set; }
}

