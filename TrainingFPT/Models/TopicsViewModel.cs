using System.ComponentModel.DataAnnotations;
using TrainingFPT.Validations;

namespace TrainingFPT.Models
{
    public class TopicsViewModel
    {
        public List<TopicDetail> TopicDetailList { get; set; }
    }
    public class TopicDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose Course, please")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Enter name's course, please")]
        public string NameTopic { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Choose file video, please")]
        [AllowExtensionFile(new string[] { ".mp4", ".flv", ".mpeg ", ".mkv ", ".wmv", ".mov", ".avi" })]
        [AllowMaxSizeFile(5 * 1024 * 1024)]
        public IFormFile Video { get; set; }
        public string? ViewVideoTopic { get; set; }

        [Required(ErrorMessage = "Choose file audio, please")]
        [AllowExtensionFile(new string[] { ".mp3", ".wav", ".ogg ", ".flac ", ".aac", ".m4a", ".wma" })]
        [AllowMaxSizeFile(5 * 1024 * 1024)]
        public IFormFile Audio { get; set; }
        public string? ViewAudioTopic { get; set; }

        [Required(ErrorMessage = "Choose file DocumentTopic, please")]
        [AllowExtensionFile(new string[] { ".docx" })]
        [AllowMaxSizeFile(5 * 1024 * 1024)]
        public IFormFile DocumentTopic { get; set; }
        public string? ViewDocumentTopic { get; set; }

        public int? LikeTopic { get; set; }
        public int? StarTopic { get; set; }
        public string? NameCourse {  get; set; }

        [Required(ErrorMessage = "Choose Status, please")]
        public string Status { get; set; }
        public string? viewCourseName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
