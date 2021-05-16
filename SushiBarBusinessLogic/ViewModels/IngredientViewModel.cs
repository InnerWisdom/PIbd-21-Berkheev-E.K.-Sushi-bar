using SushiBarBusinessLogic.Attributes;

namespace SushiBarBusinessLogic.ViewModels
{
    public class IngredientViewModel
    {
        [Column(title: "Number", width: 100)]
        public int Id { get; set; }

        [Column(title: "Ingredient name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
    }

}
