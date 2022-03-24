using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;

namespace tests.Handlers;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext db)
    {
        var application1 = new Application() { Id = 1, Name = "Computer Science", Subdomain = "cs" };
        var application2 = new Application() {Id = 2, Name = "taken", Subdomain = "taken"};
        
        var category1 = new Category() { Id = 1, ApplicationId = 1, Label = "Algorithms" };
        var category2 = new Category() { Id = 2, ApplicationId = 2, Label = "Operating Systems" };
        
        application1.Categories = new List<Category>() {category1, category2};

        var tag1 = new Tag() { Label = "discord" };
        var tag2 = new Tag() { Label = "python" };
        var tag3 = new Tag() { Label = "daily" };

        category1.CategoryHasSuggestedTags = new List<CategoryHasSuggestedTag>()
        {
            new () { Category = category1, CategoryId = 1, Tag = tag1, TagLabel = tag1.Label },
            new () { Category = category1, CategoryId = 1, Tag = tag2, TagLabel = tag2.Label }
        };

        category2.CategoryHasSuggestedTags = new List<CategoryHasSuggestedTag>()
        {
            new () { Category = category2, CategoryId = 2, Tag = tag1, TagLabel = tag1.Label },
            new () { Category = category2, CategoryId = 2, Tag = tag3, TagLabel = tag3.Label }
        };

        var user1 = new User() { Username = "nick", Email = "hello@world.com" };

        var post1 = new Post()
        {
            Id = 1, CategoryId = 1, Preview = "Bla bla bla", Title = "My group", IsActive = true,
            UserId = user1.Username, User = user1, Category = category1
        };

        post1.PostHasTags = new List<PostHasTag>()
        {
            new() {PostId = 1, Post = post1, Tag = tag1, TagLabel = tag1.Label}
        };

        user1.Posts = new List<Post>() { post1 };
        
        db.Applications.AddRange(application1, application2);
        db.Users.Add(user1);

        db.SaveChanges();
    }
}
