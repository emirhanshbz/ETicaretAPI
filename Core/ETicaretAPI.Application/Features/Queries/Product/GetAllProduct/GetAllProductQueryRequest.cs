﻿using ETicaretAPI.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse> //request olduğunu ve hangi sınıfın response olarak döndüreleceğini tanımlayan hazır bir interface
    {
        //public Pagination Pagination { get; set; }

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
