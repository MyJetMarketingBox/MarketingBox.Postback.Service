using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Grpc.Models;
using System.Threading.Tasks;

namespace MarketingBox.Postback.Service.Repositories
{
    public interface IReferenceRepository
    {
        Task DeleteReferenceAsync(long AffiliateId);
        Task<ReferenceResponse> GetReferenceAsync(long AffiliateId);
        Task<ReferenceResponse> SaveReferenceAsync(FullReferenceRequest request);
        Task<ReferenceResponse> UpdateReferenceAsync(FullReferenceRequest request);
    }
}