using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.Models.ViewModel
{
    public class ViewModelProduct
    {
        public MProduct VMProduct { get; set; }

        public IEnumerable<SelectListItem> categorySelectList { get; set; }
        
        public IEnumerable<SelectListItem> consoleSelectList { get; set; }

    }
}
