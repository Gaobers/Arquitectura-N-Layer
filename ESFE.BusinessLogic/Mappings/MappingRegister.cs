using ESFE.BusinessLogic.DTOs;
using ESFE.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.Mappings
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponse>()
                .Map(pd => pd.BrandName, p => p.Brand.Name);
               
        }
    }
}
