using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileDocumentManager
    {
        public IProfileRepository ProfileRepository { get; private set; }
        public IDocumentsManager DocumentManager { get; private set; }

        public ProfileDocumentManager(IProfileRepository profileRepository, IDocumentsManager documentManager)
        {
            this.ProfileRepository = profileRepository;
            this.DocumentManager = documentManager;
        }
        
        public string AddDocumentInPath(DocumentDTO document)
        {
            string docPath = document.OldFilePath;

            //Save and add document
            if (document.InputStream != null)
            {
                docPath = DocumentManager.SaveDocument(document, document.DocRootPath);
            }

            return docPath;
        }

        public string AddUpdateDocument(int profileId, DocumentDTO document, ProfileDocument profileDocument)
        {
            string newDocPath = document.OldFilePath;

            //Save and update Document
            if (document.InputStream != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = DocumentManager.SaveDocument(document, document.DocRootPath);

                
                if (!String.IsNullOrEmpty(document.OldFilePath))
                    //Update the profile document information in repository
                    ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    ProfileRepository.AddDocument(profileId, profileDocument);
            }
            else if(document.IsRemoved)
            {
                profileDocument.DocPath = null;
                ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);
                return null;
            }

            return newDocPath;
        }

        public string AddUpdatePSVDocument(int profileId, DocumentDTO document, ProfileDocument profileDocument)
        {
            string newDocPath = document.OldFilePath;

            //Save and update Document
            if (document.InputStream != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = DocumentManager.SaveDocument(document, document.DocRootPath);
                
            }
            else if (document.IsRemoved)
            {
                profileDocument.DocPath = null;
                //ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);
                return null;
            }

            return newDocPath;
        }

        public string AddUpdateDocumentInformation(int profileId, DocumentDTO document, ProfileDocument profileDocument)
        {
            string newDocPath = document.OldFilePath;

            //Save and update Document
            if (document.InputStream != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = DocumentManager.SaveDocument(document, document.DocRootPath);

                if (!String.IsNullOrEmpty(document.OldFilePath))
                    //Update the profile document information in repository
                    ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    ProfileRepository.AddDocument(profileId, profileDocument);
            }
            else if (document.IsRemoved)
            {
                profileDocument.DocPath = null;
                ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);
                return null;
            }
            else if (!String.IsNullOrEmpty(document.OldFilePath))
                //Update other information if document is present such as expiry date
                ProfileRepository.UpdateDocument(profileId, document.OldFilePath, profileDocument);

            return newDocPath;
        }

        public string AddDocument(int profileId, DocumentDTO document, ProfileDocument profileDocument)
        {
            string newDocPath = document.OldFilePath;

            //Save and add document
            if (document.InputStream != null)
            {
                //Save the document in the path
                newDocPath = profileDocument.DocPath = DocumentManager.SaveDocument(document, document.DocRootPath);

                //Add other legal name document in database
                ProfileRepository.AddDocument(profileId, profileDocument);
            }

            return newDocPath;
        }

        public string AddPSVDocument(int profileId, DocumentDTO document, ProfileDocument profileDocument)
        {
            string newDocPath = document.OldFilePath;

            //Save and add document
            if (document.InputStream != null)
            {
                //Save the document in the path
                newDocPath = profileDocument.DocPath = DocumentManager.SaveDocument(document, document.DocRootPath);
                
            }

            return newDocPath;
        }
    }
}
