using BlogPlatform.DTO.Tag;

namespace BlogPlatform.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAllAsync();
        Task<TagResponse?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(TagRequest request);
        Task<bool> UpdateAsync(Guid id, TagRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
