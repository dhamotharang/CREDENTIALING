using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Resources.DatabaseQueries
{
    public class AdoQueries
    {
        #region EmailService
        public static readonly string SENTMAILS_QUERY = @"select distinct ei.EmailInfoID,ei.Subject,ei.SendingDate,erd.Recipient,erd.RecipientType from [dbo].[EmailInfoes] as ei
                                                         left join [dbo].[EmailRecipientDetails] as erd on erd.EmailInfo_EmailInfoID=ei.EmailInfoID where RecipientType='To'";







//        public static readonly string SENTMAILS_QUERY = @"select et.[EmailTrackerID],et.[EmailTrackerDate],et.[EmailStatusType],et.[Status],erd.[EmailRecipientDetailID],erd.[ProfileID],erd.[Recipient],erd.[RecipientType]
//                                                        ,erd.[Status],ei.[EmailInfoID],ei.[From],ei.[Subject],ei.[Body],ei.[SendingDate],ei.[IsRecurrenceEnabled],ei.[EmailNotificationType],ei.[Status],ei.[LastModifiedDate]
//                                                        ,ea.[EmailAttachmentID],ea.[AttachmentRelativePath],ea.[AttachmentServerPath],ea.[Status]
//                                                        from [dbo].[EmailTrackers] as et left join [dbo].[EmailRecipientDetails] as erd on erd.EmailRecipientDetailID=et.EmailRecipientDetail_EmailRecipientDetailID
//                                                        join [dbo].[EmailInfoes] as ei on ei.EmailInfoID=erd.EmailInfo_EmailInfoID
//                                                        join [dbo].[EmailAttachments] as ea on ei.EmailInfoID=ea.EmailInfo_EmailInfoID order by ei.[SendingDate] desc";



        public static readonly string FOLLOWUPMAILS_QUERY = @"select distinct ei.[EmailInfoID],ei.[From],ei.[Subject],ei.[Body],ei.[SendingDate],ei.[IsRecurrenceEnabled],ei.[EmailNotificationType]
                                                           ,ei.[Status],ei.[LastModifiedDate],erd.[EmailRecurrenceDetailID],erd.[NextMailingDate],erd.[FromDate],erd.[ToDate],erd.[IntervalFactor]
                                                            ,erd.[RecurrenceIntervalType],erd.[IsRecurrenceScheduled],erd.[Status],ercd.[EmailRecipientDetailID],ercd.[ProfileID],ercd.[Recipient]
                                                            ,ercd.[RecipientType],ercd.[Status],ercd.[LastModifiedDate],ea.[EmailAttachmentID],ea.[AttachmentRelativePath],ea.[AttachmentServerPath]
                                                            ,ea.[Status]
                                                            from [dbo].[EmailInfoes] as ei left join [dbo].[EmailRecurrenceDetails] as erd on erd.[EmailRecurrenceDetailID]=ei.[EmailRecurrenceDetail_EmailRecurrenceDetailID]
                                                            left join
                                                            [dbo].[EmailRecipientDetails] as ercd
                                                            on ercd.[EmailInfo_EmailInfoID]=ei.[EmailInfoID]      
                                                            left join
                                                            [dbo].[EmailAttachments] as ea
                                                            on ea.EmailInfo_EmailInfoID=ei.EmailInfoID      
                                                            where [IsRecurrenceScheduled]='YES' order by ei.[SendingDate]";

        public static readonly string INDIVIDUALSENTMAIL_QUERY = @"select [EmailInfoID]
                                                                  ,[From]
                                                                  ,[Subject]
                                                                  ,[Body]
                                                                  ,[SendingDate]
                                                                  ,[IsRecurrenceEnabled]
                                                                  ,[EmailNotificationType]
                                                                  ,[EmailRecurrenceDetail_EmailRecurrenceDetailID]
	                                                              
                                                                  ,[ProfileID]

    
                                                                  ,[EmailAttachmentID]
                                                                  ,[AttachmentRelativePath]
                                                                  ,[AttachmentServerPath],
	                                                              STUFF((select distinct '; '+[Recipient]
	                                                              from [dbo].[EmailInfoes] as ei

                                                            left join [dbo].[EmailRecipientDetails] as erd
                                                            on erd.EmailInfo_EmailInfoID=ei.EmailInfoID

                                                            left join [dbo].[EmailAttachments] as ea
                                                            on ea.[EmailInfo_EmailInfoID]=ei.[EmailInfoID]
	                                                              where [RecipientType]='To'
	                                                              and (ei.[EmailInfoID]=new.[EmailInfoID])
	                                                              for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)') 
                                                                    ,1,2,'')RecipientTo,

		                                                            STUFF((select distinct '; '+[Recipient]
	                                                              from [dbo].[EmailInfoes] as ei

                                                            left join [dbo].[EmailRecipientDetails] as erd
                                                            on erd.EmailInfo_EmailInfoID=ei.EmailInfoID

                                                            left join [dbo].[EmailAttachments] as ea
                                                            on ea.[EmailInfo_EmailInfoID]=ei.[EmailInfoID]
	                                                              where [RecipientType]='CC'
	                                                              and (ei.[EmailInfoID]=new.[EmailInfoID])
	                                                              for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)') 
                                                                    ,1,2,'')RecipientCC,


		                                                            STUFF((select distinct '; '+[Recipient]
	                                                              from [dbo].[EmailInfoes] as ei

                                                            left join [dbo].[EmailRecipientDetails] as erd
                                                            on erd.EmailInfo_EmailInfoID=ei.EmailInfoID

                                                            left join [dbo].[EmailAttachments] as ea
                                                            on ea.[EmailInfo_EmailInfoID]=ei.[EmailInfoID]
	                                                              where [RecipientType]='BCC'
	                                                              and (ei.[EmailInfoID]=new.[EmailInfoID])
	                                                              for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)') 
                                                                    ,1,2,'')RecipientBCC



                                                            from
                                                            (
                                                            select [EmailInfoID]
                                                                  ,[From]
                                                                  ,[Subject]
                                                                  ,[Body]
                                                                  ,[SendingDate]
                                                                  ,[IsRecurrenceEnabled]
                                                                  ,[EmailNotificationType]
                                                                  ,[EmailRecurrenceDetail_EmailRecurrenceDetailID]
	                                                              
                                                                  ,[ProfileID]
                                                                  ,[Recipient]
 
   
	                                                              ,[EmailAttachmentID]
                                                                  ,[AttachmentRelativePath]
                                                                  ,[AttachmentServerPath]
     

	                                                               from [dbo].[EmailInfoes] as ei

                                                            left join [dbo].[EmailRecipientDetails] as erd
                                                            on erd.EmailInfo_EmailInfoID=ei.EmailInfoID

                                                            left join [dbo].[EmailAttachments] as ea
                                                            on ea.[EmailInfo_EmailInfoID]=ei.[EmailInfoID]
                                                            where ei.[EmailInfoID] = @EmailInfoId
                                                            ) as new";
                                                                 











































        #endregion

        #region Plan
        public static readonly string PLANS_QUERY = @"select distinct [PlanID],[PlanName],[PlanLogoPath],ContactPersonName,PlanStatus,(Appartment+' '+Street+' '+County+' '+City+' '+
                                                      State+' '+Country+' '+ZipCode) as PlanAddress,STUFF((select distinct ' , '+ed.[EmailAddress] from [dbo].[Plans] as pl left join
                                                      [dbo].[PlanContactDetails] as pcd on pcd.[Plan_PlanID]=pl.PlanID left join [dbo].[PlanAddresses] as pa on pa.[Plan_PlanID]=pl.PlanID
                                                      left join [dbo].[EmailDetails] as ed on ed.ContactDetail_ContactDetailID=pcd.ContactDetail_ContactDetailID where pl.[PlanID]=new.[PlanID]
                                                      for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')EmailAddress from(select distinct pl.[PlanID],pl.[PlanName],pl.[PlanLogoPath],
                                                      pl.Status as PlanStatus,pcd.ContactPersonName,pa.Appartment,pa.Street,pa.County,pa.City,pa.State,pa.Country,pa.ZipCode,ed.[EmailAddress]
                                                      from [dbo].[Plans] as pl left join [dbo].[PlanContactDetails] as pcd on pcd.[Plan_PlanID]=pl.PlanID left join [dbo].[PlanAddresses] as pa
                                                      on pa.[Plan_PlanID]=pl.PlanID left join [dbo].[EmailDetails] as ed on ed.ContactDetail_ContactDetailID=pcd.ContactDetail_ContactDetailID) as new";

        public static readonly string PLANS_QUERY_BYID = @"select distinct [PlanID],[PlanName],[PlanLogoPath],ContactPersonName,PlanStatus,(Appartment+' '+Street+' '+County+' '+City+' '+
                                                      State+' '+Country+' '+ZipCode) as PlanAddress,STUFF((select distinct ' , '+ed.[EmailAddress] from [dbo].[Plans] as pl left join
                                                      [dbo].[PlanContactDetails] as pcd on pcd.[Plan_PlanID]=pl.PlanID left join [dbo].[PlanAddresses] as pa on pa.[Plan_PlanID]=pl.PlanID
                                                      left join [dbo].[EmailDetails] as ed on ed.ContactDetail_ContactDetailID=pcd.ContactDetail_ContactDetailID where pl.[PlanID]=new.[PlanID]
                                                      for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'')EmailAddress from(select distinct pl.[PlanID],pl.[PlanName],pl.[PlanLogoPath],
                                                      pl.Status as PlanStatus,pcd.ContactPersonName,pa.Appartment,pa.Street,pa.County,pa.City,pa.State,pa.Country,pa.ZipCode,ed.[EmailAddress]
                                                      from [dbo].[Plans] as pl left join [dbo].[PlanContactDetails] as pcd on pcd.[Plan_PlanID]=pl.PlanID left join [dbo].[PlanAddresses] as pa
                                                      on pa.[Plan_PlanID]=pl.PlanID left join [dbo].[EmailDetails] as ed on ed.ContactDetail_ContactDetailID=pcd.ContactDetail_ContactDetailID) as new where [PlanID]=@PlanID";
        #endregion

        #region Provider Service
            public static readonly string PROVIDERSERVICE_QUERY = @"select * from ProviderService";    
        #endregion

            #region Paln And Provider Service(View)
            //public static readonly string PlanAndProvidersService_QUERY = @"select * from PlanAndProvidersService";
            #endregion


    #region Plan And Provider Service(ADO Query)

            public static readonly string PlanAndProvidersService_QUERY = @"
                                                                    select distinct [ProfileID],[NPINumber],[FirstName],[MiddleName],[LastName],

                                                                    Stuff(
	                                                                    (select distinct ' ,'+pl.[PlanName]
	                                                                    from [dbo].[CredentialingActivityLogs] as cal

                                                                    inner join 
                                                                    [dbo].[CredentialingLogs] as cl
                                                                    on cal.[CredentialingLog_CredentialingLogID]=cl.[CredentialingLogID]

                                                                    inner join
                                                                    [dbo].[CredentialingInfoes] as ci
                                                                    on cl.[CredentialingInfo_CredentialingInfoID]=ci.[CredentialingInfoID]

                                                                    inner join
                                                                    [dbo].[Profiles] as p
                                                                    on ci.[ProfileID]=p.[ProfileID]

                                                                    inner join
                                                                    [dbo].[Plans] as pl
                                                                    on pl.[PlanID]=ci.[PlanID]

                                                                    inner join
                                                                    [dbo].[PersonalDetails] as pd
                                                                    on pd.[PersonalDetailID]=p.[PersonalDetail_PersonalDetailID]

                                                                    inner join
                                                                    [dbo].[OtherIdentificationNumbers] as oi
                                                                    on p.[OtherIdentificationNumber_OtherIdentificationNumberID]=oi.[OtherIdentificationNumberID]
                                                                    where cal.[Activity]='Closure' and [ActivityStatus]='Completed' and p.[ProfileID]=new.[ProfileID]
                                                                    for XML PATH(''),TYPE).value('.', 'NVARCHAR(MAX)') 
                                                                            ,1,2,'') PlanName

                                                                    from(

                                                                    select distinct p.[ProfileID],oi.[NPINumber],pd.[FirstName],pd.[MiddleName],pd.[LastName],pl.[PlanName]
                                                                    from [dbo].[CredentialingActivityLogs] as cal

                                                                    inner join 
                                                                    [dbo].[CredentialingLogs] as cl
                                                                    on cal.[CredentialingLog_CredentialingLogID]=cl.[CredentialingLogID]

                                                                    inner join
                                                                    [dbo].[CredentialingInfoes] as ci
                                                                    on cl.[CredentialingInfo_CredentialingInfoID]=ci.[CredentialingInfoID]

                                                                    inner join
                                                                    [dbo].[Profiles] as p
                                                                    on ci.[ProfileID]=p.[ProfileID]

                                                                    inner join
                                                                    [dbo].[Plans] as pl
                                                                    on pl.[PlanID]=ci.[PlanID]

                                                                    inner join
                                                                    [dbo].[PersonalDetails] as pd
                                                                    on pd.[PersonalDetailID]=p.[PersonalDetail_PersonalDetailID]

                                                                    inner join
                                                                    [dbo].[OtherIdentificationNumbers] as oi
                                                                    on p.[OtherIdentificationNumber_OtherIdentificationNumberID]=oi.[OtherIdentificationNumberID]
                                                                    where cal.[Activity]='Closure' and [ActivityStatus]='Completed') as new";

    #endregion
    }
}
