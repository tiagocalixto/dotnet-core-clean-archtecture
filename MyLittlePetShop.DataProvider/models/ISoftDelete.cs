using System;
namespace MyLittlePetShop.DataProvider.models
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
