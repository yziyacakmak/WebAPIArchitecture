using App.Repositories.Categories;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository):ICategoryService
    {

        //crud
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await categoryRepository.AddAsync(category);
            return category;
        }


    }
}
