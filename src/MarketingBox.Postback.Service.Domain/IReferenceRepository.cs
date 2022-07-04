using System.Collections.Generic;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IReferenceRepository
    {
        Task<Reference> DeleteAsync(long postbackId);
        Task<Reference> GetAsync(long postbackId);
        Task<(IReadOnlyCollection<Reference>,int)> SearchAsync(SearchReferenceRequest request);
        Task<Reference> CreateAsync(CreateReferenceRequest request);
        Task<Reference> UpdateAsync(UpdateReferenceRequest request);
    }
}