using System.IO;
using UnityEngine;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using Document = iTextSharp.text.Document;

public class PDFExporter : MonoBehaviour
{
    private const string FILE_EXTENTION = ".pdf";

    [Tooltip("The PDF document name that you want to export")]
    public string fileName = "example";

    [Tooltip("The directory where you want to export the pdf document.")]
    public string filePath;

    [Tooltip("The header that you want to save in the PDF document")]
    public string header = "Header!";

    [Tooltip("The modelName that you want to save in the PDF document")]
    public string modelName = "Model Name!";

    [Tooltip("Assign Image from Inspector")]
    public Texture2D modelImage;

    void Start()
    {
        string docName = fileName;
        string directory = filePath;
        
        if (fileName == string.Empty)
        {
            docName = "Example";
            Debug.LogWarning("No file name is provided, System automatically set a default name as: " + docName);
        }

        if (filePath == string.Empty)
        {
            directory = Application.persistentDataPath;
            Debug.LogWarning("No file directory is provided, System automatically set a default directory as: " + directory);
        }

        string path = directory + docName + FILE_EXTENTION;
        CreateFormattedPDF(path);
        Debug.Log("PDF created at: " + path);
   
    }


    void CreateFormattedPDF(string path)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
        document.Open();

        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        Font headerFont = new Font(baseFont, 25, Font.BOLD);
        Font italicFont = new Font(baseFont, 14, Font.ITALIC);


        Paragraph header = new Paragraph(this.header, headerFont);
        document.Add(header);

        document.Add(new Paragraph(" "));
        document.Add(new Paragraph(" "));

        Paragraph someName = new Paragraph(modelName, italicFont);
        document.Add(someName);

        document.Add(new Paragraph(" "));

        Paragraph imageLabel = new Paragraph("Model Image:", new Font(baseFont, 14));
        document.Add(imageLabel);

        if (modelImage != null)
        {
            Texture2D readableTex = new Texture2D(modelImage.width, modelImage.height, TextureFormat.RGBA32, false);
            readableTex.SetPixels(modelImage.GetPixels());
            readableTex.Apply();

            byte[] imgBytes = readableTex.EncodeToPNG();
            iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(imgBytes);

            pdfImg.ScaleToFit(400f, 400f);
            pdfImg.SpacingBefore = 10f;
            pdfImg.Alignment = Element.ALIGN_LEFT;
            document.Add(pdfImg);
        }
        else
        {
            Debug.LogWarning("Model Image not assigned. Please assign a Texture2D in the Inspector.");
        }

        document.Close();
    }
}
