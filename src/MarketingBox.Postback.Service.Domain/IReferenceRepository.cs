using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IReferenceRepository
    {
        Task DeleteReferenceAsync(long AffiliateId);
        Task<Reference> GetReferenceAsync(long AffiliateId);
        Task<Reference> CreateReferenceAsync(Reference request);
        Task<Reference> UpdateReferenceAsync(Reference request);
    }
}