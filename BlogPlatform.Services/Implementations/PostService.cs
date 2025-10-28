using BlogPlatform.Domain.Entities;
using BlogPlatform.DTO.Post;
using BlogPlatform.Repositories.Interfaces;
using BlogPlatform.Services.Interfaces;

namespace BlogPlatform.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ITagRepository _tagRepo;

        public PostService(IPostRepository postRepo, ICategoryRepository categoryRepo, ITagRepository tagRepo)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
        }

        public async Task<bool> CreateAsync(CreatePostRequest request, Guid userId)
        {
            var category = await _categoryRepo.GetByIdAsync(request.CategoryId);
            if (category == null) return false;

            var tags = new List<PostTag>();
            foreach (var tagId in request.TagIds)
            {
                var tag = await _tagRepo.GetByIdAsync(tagId);
                if (tag != null)
                    tags.Add(new PostTag { TagId = tag.Id });
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                CategoryId = category.Id,
                AuthorId = userId,
                CreatedAt = DateTime.UtcNow,
                PostTags = tags,
            };

            await _postRepo.AddAsync(post);
            return await _postRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _postRepo.GetByIdAsync(id);
            if (post == null) return false;

            await _postRepo.DeleteAsync(post);
            return await _postRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostResponse>> GetAllAsync()
        {
            var posts = await _postRepo.GetAllAsync();
            return posts.Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AuthorUsername = p.Author.UserName,
                CategoryName = p.Category.Name,
                Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList()
            });
        }

        public async Task<PostResponse?> GetByIdAsync(Guid id)
        {
            var post = await _postRepo.GetByIdAsync(id);
            if (post == null) return null;

            return new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
                AuthorUsername = post.Author.UserName,
                CategoryName = post.Category.Name,
                Tags = post.PostTags.Select(pt => pt.Tag.Name).ToList()
            };
        }

        public async Task<IEnumerable<PostResponse>> SearchAsync(string keyword)
        {
            var posts = await _postRepo.SearchAsync(keyword);
            return posts.Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AuthorUsername = p.Author.UserName,
                CategoryName = p.Category.Name,
                Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList()
            });
        }

        public async Task<bool> UpdateAsync(Guid id, UpdatePostRequest request)
        {
            var post = await _postRepo.GetByIdAsync(id);
            if (post == null) return false;

            post.Title = request.Title;
            post.Content = request.Content;
            post.ImageUrl = request.ImageUrl;
            post.CategoryId = request.CategoryId;

            post.PostTags.Clear();
            foreach (var tagId in request.TagIds)
            {
                var tag = await _tagRepo.GetByIdAsync(tagId);
                if (tag != null)
                    post.PostTags.Add(new PostTag { TagId = tagId, PostId = post.Id });
            }

            await _postRepo.UpdateAsync(post);
            return await _postRepo.SaveChangesAsync();
        }
    }
}
