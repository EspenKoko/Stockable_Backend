using System.Reflection.Metadata;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class PDFHelpDoc
    {
        [Key]
        public int docId { get; set; }
        public string userType { get; set; }
        public byte[] pdfContent { get; set; }
    }
}

