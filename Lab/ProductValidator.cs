using Lab.Entities;

namespace Lab
{
    public class ProductValidator : IValidator<TSource>
    {
        public bool Validate(TSource model)
        {
            return model.Price - model.Cost >= 0;
        }
    }
}