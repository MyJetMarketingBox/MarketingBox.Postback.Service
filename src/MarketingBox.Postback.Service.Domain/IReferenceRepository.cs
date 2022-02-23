using System.Threading.Tasks;
using MarketingBox.Postback.Service.Domain.Models;

namespace MarketingBox.Postback.Service.Domain
{
    public interface IReferenceRepository
    {
        Task<long> DeleteAsync(long affiliateId);
        Task<Reference> GetAsync(long affiliateId);
        Task<Reference> CreateAsync(Reference request);
        Task<Reference> UpdateAsync(Reference request);
    }
}