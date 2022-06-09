using System.Collections.Generic;
using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IReferenceRepository
    {
        Task<Reference> DeleteAsync(long affiliateId);
        Task<Reference> GetAsync(long affiliateId);
        Task<(IReadOnlyCollection<Reference>,int)> SearchAsync(SearchReferenceRequest request);
        Task<Reference> CreateAsync(CreateOrUpdateReferenceRequest request);
        Task<Reference> UpdateAsync(CreateOrUpdateReferenceRequest request);
    }
}