using AHC.CD.Business.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class GeneratePdf
    {
        public string FillForm(object dataObj, string pdfName, string templateName)
        {

            string pdfTemplate = HttpContext.Current.Server.MapPath("~/PdfTemplate/" + templateName);            
            string newFile = HttpContext.Current.Request.MapPath("~/TempGeneratedPdf/" + pdfName);            

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                        newFile, FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(dataObj))
            {
                if (prop.GetValue(dataObj) != null)
                {
                    Debug.WriteLine(prop.Name.ToString() + " is " + prop.GetValue(dataObj).ToString());
                    pdfFormFields.SetField(prop.Name.ToString(), prop.GetValue(dataObj).ToString());
                }
            }


            // report by reading values from completed PDF
            string sTmp = "PDF Completed";


            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();

            return pdfName;
        }

        public string CombinePdfs(List<string> pdflist, string outputFileName)
        {
            string generatedPdfPath = HttpContext.Current.Request.MapPath("~/GeneratedPdf/" + outputFileName);
            string tempFilePath = HttpContext.Current.Server.MapPath("~/TempGeneratedPdf");

            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(generatedPdfPath, FileMode.Create));
            if (writer == null)
            {
                return null;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in pdflist)
            {
                if (fileName != "")
                {
                    PdfReader reader = new PdfReader(tempFilePath + @"/" + fileName);
                    try
                    {
                        // we create a reader for a certain document

                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }
                    catch (Exception e)
                    { }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();

            return outputFileName;

        }

        public string FillApplicationForm(object dataObj, string pdfName, string templateName)
        {

            string pdfTemplate = HttpContext.Current.Server.MapPath("~/FormTemplate/" + templateName);
            string newFile = HttpContext.Current.Request.MapPath("~/TempGeneratedForm/" + pdfName);

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                        newFile, FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(dataObj))
            {
                if (prop.GetValue(dataObj) != null)
                {
                    Debug.WriteLine(prop.Name.ToString() + " is " + prop.GetValue(dataObj).ToString());
                    pdfFormFields.SetField(prop.Name.ToString(), prop.GetValue(dataObj).ToString());
                }
            }


            // report by reading values from completed PDF
            string sTmp = "PDF Completed";


            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();

            return pdfName;
        }

        public string CombineApplicationFormPdfs(List<string> pdflist, string outputFileName)
        {
            string generatedPdfPath = HttpContext.Current.Request.MapPath("~/GeneratedForm/" + outputFileName);
            string tempFilePath = HttpContext.Current.Server.MapPath("~/TempGeneratedForm");

            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(generatedPdfPath, FileMode.Create));
            if (writer == null)
            {
                return null;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in pdflist)
            {
                if (fileName != "")
                {
                    PdfReader reader = new PdfReader(tempFilePath + @"/" + fileName);
                    try
                    {
                        // we create a reader for a certain document

                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }
                    catch (Exception e)
                    { }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();

            return outputFileName;

        }
    }
}