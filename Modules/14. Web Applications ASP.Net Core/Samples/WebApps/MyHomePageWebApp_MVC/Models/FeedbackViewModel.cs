using System.ComponentModel;

namespace MyHomePageWebApp.Models
{
    public class FeedbackViewModel
    {
        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Почта")]
        public string Email { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }
    }
}
