

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

        public void SaveFolder()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }

        public void CaptureScreenshot()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
                //capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //Saving the Image File (I am here Saving it in My E drive).
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

        public void CreateDoc()
        {
            document = new Spire.Doc.Document();
            section = document.AddSection();
            DocFile = @"C:\Capture_Folder\Screenshot_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".docx";
            document.SaveToFile(DocFile, Spire.Doc.FileFormat.Docx);
            Counter = 1;
        }

        public void AddImageToDoc(string path)
        {
            //Spire.Doc.Section section = document.AddSection();
            Spire.Doc.Documents.Paragraph paragraph = section.AddParagraph();
            paragraph.AppendText($"Screenshot: {Counter}");
            paragraph.ApplyStyle(BuiltinStyle.Heading2);
            Counter++;
            Spire.Doc.Fields.DocPicture image = paragraph.AppendPicture(System.Drawing.Image.FromFile(path));
            image.VerticalAlignment = Spire.Doc.ShapeVerticalAlignment.Center;
            image.HorizontalAlignment = Spire.Doc.ShapeHorizontalAlignment.Center; image.Width = 500;
            image.Height = 250;
        }

        public void CloseDoc()
        {
            document.SaveToFile(DocFile, Spire.Doc.FileFormat.Docx);
            document.Close();
            document.Dispose();
        }

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