namespace kouBlog.Models.DTO
{
    public class PostDto
    {

        public int PostID { get; set; }
        
        public int id { get; set; }

        public string PostName { get; set; }
        public string PostContent { get; set; }

        public int PostWriternumber { get; set; }
        public string PostWriterName { get; set; }
        public int PostNumberofLike { get; set; }



    }
}