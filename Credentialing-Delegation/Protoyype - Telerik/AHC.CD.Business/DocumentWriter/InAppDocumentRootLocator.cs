using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DocumentWriter
{
    /// <summary>
    /// Locates and returns the Running Application Root Folder
    /// </summary>
    public class InAppDocumentRootLocator : IDocumentRootLocator
    {
        public string GetDocumentRootFolder()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
