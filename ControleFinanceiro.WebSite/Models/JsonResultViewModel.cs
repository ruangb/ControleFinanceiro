namespace ControleFinanceiro.WebSite.Models
{
    public class JsonResultViewModel
    {
        public bool Success { get; set; }
        public object Content { get; set; }
        public string Message { get; set; }

        public JsonResultViewModel(bool success, object content)
        {
            Success = success;
            Content = content;
        }

        public JsonResultViewModel(bool success, string message)
        {
            Success = success;

            if (Success)
            {
                Content = message;
            }
            else
            {
                Message = message;
            }
        }
    }
}
