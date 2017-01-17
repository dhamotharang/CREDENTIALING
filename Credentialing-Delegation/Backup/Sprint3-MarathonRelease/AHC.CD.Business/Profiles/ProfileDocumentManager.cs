using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    class ProfileDocumentManager
    {
        IGenericRepository<ProfileDocument> profileDocumentRepository = null;
        IDocumentsManager documentManager = null;

        public ProfileDocumentManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.profileDocumentRepository = uow.GetGenericRepository<ProfileDocument>();
            this.documentManager = documentManager;
        }
        
        private string AddDocumentInPath(string docRootPath, DocumentDTO document, string oldFilePath, string docTitle)
        {
            string docPath = document.OldFilePath;

            //Save and add document
            if (document.InputStream != null)
            {
                docPath = documentManager.SaveDocument(document, document.DocTitle);
            }

            return docPath;
        }

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        private string AddUpdateDocument(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        {
            string newDocPath = oldFilePath;

            //Save and update Document
            if (document != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

                //Add or update other legal name document
                if (!String.IsNullOrEmpty(oldFilePath))
                    //Update the profile document information in repository
                    profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    profileRepository.AddDocument(profileId, profileDocument);
            }

            return newDocPath;
        }

        private string AddUpdateDocumentInformation(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        {
            string newDocPath = oldFilePath;

            //Save and update Document
            if (document != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

                //Add or update other legal name document
                if (!String.IsNullOrEmpty(oldFilePath))
                    //Update the profile document information in repository
                    profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    profileRepository.AddDocument(profileId, profileDocument);
            }
            else if (!String.IsNullOrEmpty(oldFilePath))
                //Update other information if document is present such as expiry date
                profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);

            return newDocPath;
        }

        private string AddDocument(int profileId, string docRootPath, DocumentDTO document, ProfileDocument profileDocument, string oldFilePath)
        {
            string newDocPath = oldFilePath;

            //Save and add document
            if (document != null)
            {
                //Save the document in the path
                newDocPath = profileDocument.DocPath = documentManager.SaveDocument(document, docRootPath);

                //Add other legal name document in database
                profileRepository.AddDocument(profileId, profileDocument);
            }

            return newDocPath;
        }
    }
}
