using BlogPlatform.Domain.Entities;
using BlogPlatform.DTO.Tag;
using BlogPlatform.Repositories.Interfaces;
using BlogPlatform.Services.Interfaces;

namespace BlogPlatform.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepo;

        public TagService(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public async Task<bool> CreateAsync(TagRequest request)
        {
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };

            await _tagRepo.AddAsync(tag);
            return await _tagRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null) return false;

            await _tagRepo.DeleteAsync(tag);
            return await _tagRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            var tags = await _tagRepo.GetAllAsync();
            return tags.Select(t => new TagResponse
            {
                Id = t.Id,
                Name = t.Name,
            });
        }

        public async Task<TagResponse?> GetByIdAsync(Guid id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null) return null;

            return new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }

        public async Task<bool> UpdateAsync(Guid id, TagRequest request)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null) return false;

            tag.Name = request.Name;

            await _tagRepo.UpdateAsync(tag);
            return await _tagRepo.SaveChangesAsync();
        }
    }
}
