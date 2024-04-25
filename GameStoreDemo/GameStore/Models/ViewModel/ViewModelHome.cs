namespace GameStore.Models.ViewModel
{
    public class ViewModelHome
    {
        public IEnumerable<MConsole> Consoles { get; set; }

        public IEnumerable<MCategory> Categories { get; set; }

        public IEnumerable<MProduct> Products { get; set; }
    }
}
