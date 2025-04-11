using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;
using UnityEngine;
using Document = iTextSharp.text.Document;

public class PDFExporter : MonoBehaviour
{
    private const string FILE_EXTENTION = ".pdf";

    [Tooltip("The PDF document name that you want to export")]
    public string fileName = "example";

    [Tooltip("The directory where you want to export the pdf document.")]
    public string filePath;

    [Tooltip("The content that you want to save in the PDF document")]
    public string content = "Hello from Unity!";

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
        CreatePDF(path, content);
        Debug.Log("PDF created at: " + path);
   
    }

    void CreatePDF(string filePath, string content)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();
        document.Add(new Paragraph(content));
        document.Close();
    }
}
