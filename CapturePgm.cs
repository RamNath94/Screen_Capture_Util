

namespace CapturePress
{
    using Spire.Doc.Documents;
    using System.Drawing.Imaging;
    public class CapturePgm
    {
        private Spire.Doc.Document document;
        private Spire.Doc.Section section;
        private int Counter;

        private static String DocFile;
        private string FilePath = @"C:\Capture_Folder\temp\";
        
        
      
        /// <summary>
        /// Create default save folder for saving
        /// </summary>
        public void SaveFolder()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }

        /// <summary>
        /// Caputure the screenshot and save it as jpg 
        /// </summary>
        public void CaptureScreenshot()
        {
            try
            {
              
                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                string filenName = "Capture_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".jpg";
                string FinalPathString = FilePath + filenName;
                captureBitmap.Save(FinalPathString, ImageFormat.Jpeg);
                AddImageToDoc(FinalPathString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Create new word document
        /// </summary>
        public void CreateDoc()
        {
            document = new Spire.Doc.Document();
            section = document.AddSection();
            DocFile = @"C:\Capture_Folder\Screenshot_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".docx";
            document.SaveToFile(DocFile, Spire.Doc.FileFormat.Docx);
            Counter = 1;
        }

        /// <summary>
        /// Take screenshot and add to the word doc 
        /// </summary>
        /// <param name="path"></param>
        public void AddImageToDoc(string path)
        {
           
            Spire.Doc.Documents.Paragraph paragraph = section.AddParagraph();
            paragraph.AppendText($"Screenshot: {Counter}");
            paragraph.ApplyStyle(BuiltinStyle.Heading2);
            Counter++;
            Spire.Doc.Fields.DocPicture image = paragraph.AppendPicture(System.Drawing.Image.FromFile(path));
            image.VerticalAlignment = Spire.Doc.ShapeVerticalAlignment.Center;
            image.HorizontalAlignment = Spire.Doc.ShapeHorizontalAlignment.Center; image.Width = 500;
            image.Height = 250;
        }

        /// <summary>
        /// Save and Close the word doc 
        /// </summary>
        public void CloseDoc()
        {
            document.SaveToFile(DocFile, Spire.Doc.FileFormat.Docx);
            document.Close();
            document.Dispose();
        }

        /// <summary>
        /// Delete the temproryly saved screenshot
        /// </summary>
        public void CleanUp()
        {
            string root = @"C:\Capture_Folder\temp";
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
        }
    }
}