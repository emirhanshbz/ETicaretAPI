using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile
{
    public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
