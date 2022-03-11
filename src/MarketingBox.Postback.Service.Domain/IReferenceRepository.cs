using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IReferenceRepository
    {
        Task<long> DeleteAsync(long affiliateId);
        Task<Reference> GetAsync(long affiliateId);
        Task<Reference> CreateAsync(CreateOrUpdateReferenceRequest request);
        Task<Reference> UpdateAsync(CreateOrUpdateReferenceRequest request);
    }
}