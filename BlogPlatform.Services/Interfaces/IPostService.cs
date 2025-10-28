using BlogPlatform.DTO.Post;

namespace BlogPlatform.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponse>> GetAllAsync();
        Task<PostResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<PostResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(CreatePostRequest request, Guid userId);
        Task<bool> UpdateAsync(Guid id, UpdatePostRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
