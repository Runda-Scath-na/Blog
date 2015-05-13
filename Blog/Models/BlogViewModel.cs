using System.ComponentModel.DataAnnotations;

namespace Blog.Models {

    public class BlogViewModel{

        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostAuthor { get; set; }
        public string PostTease { get; set; }
        [Required]
        public string PostBody { get; set; }
    }
}