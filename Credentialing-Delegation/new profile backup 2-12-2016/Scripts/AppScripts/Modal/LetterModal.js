/// <reference path="../../../Views/Home/Letter/_ApprovalLetter.cshtml" />
var showLetter = function (id) {
    if (id == '1') {
        ApprovalLetterPreview();
    } else if (id == '2') {
        DenialLetterPreview();
    } else if (id == '3') {
        NomncLetterPreview();
    } else if (id == '4') {
        DencLetterPreview();
    }
}

//------------Approval Letter-----
var ApprovalLetterPreview = function () {
    ShowModal('~/Views/Home/Letter/_ApprovalLetter.cshtml', 'Approval Letter');
    var MemberName = '';
    var NewDate = moment(new Date).format('LL');
    var NewReceivedDate = new Date();
    
    var NewLastModifiedDate = new Date();
    var NewSubscriberID = '';
    var NewServiceRequested = "";
    var ServiceRequestedContent = "INPATIENT ADMISSION";
    var Service_Attending = "ATTENDING PROVIDER";

//-------requested by
   
        var PCP_Provider = "DR. ";
    //$scope.AuthorizationsList[index].MemberProviders[1].Name
        var NewRequestedBy = '';
    //$scope.AuthorizationsList[index].MemberProviders[2].Name
        var NewServiceProvider = '';
    
    //----facility name
            var final = "DR. " + NewServiceProvider + '</span><br/>&nbsp;&nbsp;<span>' ;
            var address1 = '9037 BYRON STREET';
            var City = 'WEEKI WACHEE, ';
            var State = 'FL ';
            var ZipCode = '34607';

    var NewReferenceNumber = '';
    var Content0 = '<div id="htmlId">';
    var Content1 = '<div  class="col-lg-12" style="width: 100.5%"><div class="col-lg-12" width="100%"><img src="/Areas/UM/Content/Images/Ultimate_Logo.png" class="pull-left imageofult"><div class="CalibriFontStyle-12FontSize pull-right"><p class="removeBottomMargin "><span class=" CalibriFontStyle-12FontSize spacing" style="padding-left: 12px; font-family: Calibri">1244 Mariner Boulevard </span></p><p class="removeBottomMargin"><span class=" CalibriFontStyle-12FontSize spacing" style="padding-left: 8px; font-family: Calibri">Spring Hill, Florida 34609</span></p><p class="removeBottomMargin"><span class=" CalibriFontStyle-12FontSize spacing" style="font-family: Calibri">www.chooseultimate.com</span></p></div><div class="clearfix"></div><div ><br/><p class="removeBottomMargin" style="font-family: "Times New Roman;font-size: 15px"><span style="text-transform: uppercase;font-family: Times New Roman">' + MemberName + '</span></p></div><div style="text-transform: uppercase"><p class="removeBottomMargin"><span style="font-family:Times New Roman;font-size: 15px;text-transform: uppercase">' + address1 + '</span></p><p class="removeBottomMargin"><span style="font-family:Times New Roman;font-size: 15px;text-transform: uppercase">' + City + State + ZipCode + '</span></p></div><p style="text-align:right" class="removeBottomMargin" style="font-family: "Times New Roman;font-size: 15px">' + NewDate + '</p><br /><p style="float:left" class="removeBottomMargin">Dear<span style="text-transform: uppercase;font-family: Times New Roman;"> ' + MemberName + ': </span></p><br /><br /><span style="font-family:Times New Roman;font-size: 15px;">Ultimate Health Plans received a request for the following service on<b> ' + NewReceivedDate + ':<br /><br /></b></span></div>';
    var Content2 = '<div id="content2" class="col-lg-12"><table class="col-lg-12 wrap-words" id="tableData" style="border:1px solid black; text-transform: uppercase;font-family: Times New Roman"><tr style="border:1px solid black;"><td width="40%" style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;MEMBER ID</td><td>&nbsp;&nbsp;' + NewSubscriberID + '</td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;SERVICE REQUESTED</td><td style="padding-left: 10px;"><span>' + ServiceRequestedContent + '</span></td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;REQUESTED BY</td><td>&nbsp;&nbsp;' + PCP_Provider + NewRequestedBy + '</td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;' + Service_Attending + '</td><td style="text-transform: uppercase;">&nbsp;&nbsp;' + final + '</td></tr></table></div>';
    var Content3 = '<div id="content3" class="col-lg-12" width="100%"><p class="removeBottomMargin"><span class=" pSpacing" style="font-family: "Times New Roman;font-size: 15px"><br />Ultimate Health Plans has approved this request on <b> ' + NewLastModifiedDate + '</b> and has assigned authorization number <b>' + NewReferenceNumber + '</b> for this service. This authorization is valid for 90 days from the date of approval.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin"><span class="TimesNewRoman-12FontSize pSpacing" style="font-family: "Times New Roman;font-size: 15px">If you have any questions, please contact our Member Services Department at 1-888-657-4170. We are available 7 days per week from 8:00am to 8:00pm EST. TTY/TDD users should call 711.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin"><span class="TimesNewRoman-12FontSize pSpacing" style="font-family: "Times New Roman;font-size: 15px">Please be advised that from February 15 to September 30, Ultimate Health Plans may use alternative technologies to answer your call on weekends and federal holidays.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin" style="margin-top:10px;"><span style="font-family: "Times New Roman;font-size: 15px">Thank You,<br />Ultimate Health Plans</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin">&nbsp;</p><div class="text-center TimesNewRoman-12FontSize" style="font-family: "Times New Roman;font-size: 15px"><strong><i>Ultimate Health Plans is an HMO plan with a Medicare contract.</i></strong></div><div class="text-center TimesNewRoman-12FontSize" style="font-family: "Times New Roman;font-size: 15px"><strong><i>Enrollment in Ultimate Health Plans depends on contract renewal.</i></strong></div><p class="removeBottomMargin">&nbsp;</p><footer><span style="float:right;" style="font-family: "Times New Roman;font-size: 15px;text-color:#D3D3D3"> H2962_Part C Approval Letter v216 Accepted</span></footer></div></div><div class="clearfix"></div><br /></div>';

    setTimeout(function () { $("#ApprovalPreviewModal").append(Content0 + Content1 + Content2 + Content3); }, 3000);

    
    
   // ShowModal('ApprovalPreviewModal');
}

//------------Denial Letter-------
var DenialLetterPreview = function () {
    function getFormattedDate(date) { var year = date.getFullYear(); var month = (1 + date.getMonth()).toString(); month = month.length > 1 ? month : '0' + month; var day = date.getDate().toString(); day = day.length > 1 ? day : '0' + day; return month + '/' + day + '/' + year; }
    function getDateFormat(date) { var year = date.split("-")[0]; var month = date.split("-")[1]; var day = date.split("-")[2].split("T")[0]; return month + "/" + day + "/" + year; }
    function MemberIDFormat(ID) { var OldId = ID.substring(5, 9); return "UL*" + OldId; }

    var MemberName = '';
    var NewDate = moment(new Date).format('LL');
    var NewReceivedDate = moment(new Date).format('LL');
    var NewLastModifiedDate = moment(new Date).format('LL');
    var NewSubscriberID = '';
    var NewReferenceNumber = '-';
    var Expected_DOS = "";
    var Rationale = "";
    var CriteriaUsed = "";
    var AlternatePlan = "";
    var ServiceSubjectToNotice = "";
    var MedicalNecessariesList = "";
    var ListOfMedicalServices = "";

    var firstDiv = '<div id="htmlIdDenial"><br/><div class="col-lg-12" id="denialmemberletterdiv" style="padding-left:50px"><div><p><span class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;"><b>Important: </b>This notice explains your right to appeal our decision. Read this notice carefully. If you need help, you can call one of the numbers listed on the last page under "Get help & more information."</span></p></div><div style="text-align:center;padding-bottom:12px"><p class="TimesNewRoman-19FontSize" style="padding-top:20px;"><b style="font-family: Times New Roman;font-size: 22px;font-weight:700">Notice of Denial of Medical Coverage</b></p><p style="text-align:center;" class="TimesNewRoman-12FontSize">Denial of Medical Coverage</p></div><div id="borderStyle" class="TimesNewRoman-12FontSize" style="border-top:1px solid #020202;border-bottom:1px solid #020202;overflow: hidden;"><div style="width:90%;"><p style="float:left;margin-top:0px;margin-bottom:0px;" class="removeBottomMargin"><b>Date: </b><i>' + NewDate + '</i></p><p style="float:right;margin-top:0px;margin-bottom:0px;" class="removeBottomMargin"><b>Member Number: </b><i>' + NewSubscriberID + '</i></p></div><br /><div style="float:left;margin-top:0px;margin-bottom:0px;"><p class="removeBottomMargin"><b>Name: </b><i class="text-uppercase">' + MemberName + '</i></p></div><br /><div style="width:90%;display:inline;"><p style="float:left;margin-top:0px;margin-bottom:0px;width:60%;" class="removeBottomMargin"><b>Service Subject to Notice: </b><i class=" wrap-words text-uppercase">' + ServiceSubjectToNotice + '<br /></i></p><p style="float:right;margin-top:0px;margin-bottom:0px;margin-right:80px;" class="removeBottomMargin"><b>Date of Service:</b><i> ' + Expected_DOS + '</i></p></div><br /><div style="width:90%;" class="TimesNewRoman-12FontSize"><p style="float:left;margin-top:0px;"><b>Authorization Number: </b><i>' + NewReferenceNumber + '</i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p style="float:right;margin-top:0px;"><b>Requested Date:</b><i> ' + NewReceivedDate + '</i></p><br /></div></div>';
    var SecondDiv = '<div class="pSpacing"><br /><span class="TimesNewRoman-17FontSize" style="width: 50%;"><b style="font-size: 20px;font-family:Times New Roman">Your request was denied</b></span><br /><span class="TimesNewRoman-12FontSize">We have denied the medical services/items listed below requested by you or your doctor:</span><br /><br /><p style="margin-top:0px;" class="TimesNewRoman-12FontSize pSpacing"><b>List of Medical Services or Items:&nbsp;</b><br/><span><i class="text-uppercase wrap-words">' + ListOfMedicalServices + '</i><br/></span></p><div class="pSpacing" style="margin-top:18px"><p style="font-size: 20px;font-family:Times New Roman;padding-bottom:6px"><b style="padding-bottom:6px"">Why did we deny your request?</b></p><p class="TimesNewRoman-12FontSize" style="margin-top:10px">We denied the medical services/items listed above because:</p><p class="TimesNewRoman-12FontSize" style="margin-top:10px"><span><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + Rationale + '</i><br /><br /></span><span><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + CriteriaUsed + '</i><br /><br /></span><i style="text-align: justify;text-justify: inter-word;">Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.</i><br /><br /><span><b>Alternative Plan of Care:</b>   <br/><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + AlternatePlan + '</i><br /><br /></span></p></div><div class="pSpacing" style="margin-top:15px"><p class="TimesNewRoman-17FontSize"><b style="font-size: 20px;font-weight: 700;font-family:Times New Roman">You have the right to appeal our decision</b></p><p class="TimesNewRoman-12FontSize" style="text-align: justify;text-justify: inter-word;margin-top:10px">You have the right to ask Ultimate Health Plans to review our decision by asking us for an appeal:</p><p class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;margin-top:13px"><b>Appeal:</b>&nbsp;Ask Ultimate Health Plans for an appeal within <b>60 days</b> of the date of this notice. We can give you more time if you have a good reason for missing the deadline.</p><p class="TimesNewRoman-15FontSize" style="margin-top:12px;font-size: 17px;font-family:Times New Roman"><b>If you want someone else to act for you</b></p><p class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;margin-top:15px">You can name a relative, friend, attorney, doctor, or someone else to act as your representative. If you want someone else to act for you, call us at: 1-888-657-4170 to learn how to name your representative. TTY users call 711. Both you and the person you want to act for you must sign and date a statement confirming this is what you want. You will need to mail or fax this statement to us.</p></div><br /></div><div class="page-break" style="page-break-before:always"></div><br />';
    var ThirdDiv = '<div style="text-align: justify;text-justify: inter-word;"><div class="col-lg-12"><div class="col-lg-2"></div><div class="col-lg-8"><div style="text-align:center;"><p style="text-align:center;" class="TahomaFontStyle-12FontSize"><b style="font-family:Times New Roman;font-weight: 700;">Important Information About Your Appeal Rights</b></p></div></div><div class="col-lg-2"></div></div><div style="margin-top:15px"><p><br/><b style="font-weight: 700;font-size: 20px;font-family:Times New Roman">There are 2 kinds of appeals</b></p><p class="TimesNewRoman-12FontSize pSpacing"><b>Standard Appeal-</b>We will give you a written decision on a standard appeal within <b>30 days</b> after we get your appeal. Our decision might take longer if you ask for an extension, or if we need more information about your case. We will tell you if we are taking extra time and will explain why more time will be needed. If your appeal is for payment of a service you have already received, we will give you a written decision within <b>60 days</b>.</p><p class="TimesNewRoman-12FontSize pSpacing"><br/><b>Fast Appeal:</b> We will give you a decision on a fast appeal within <b>72 hours</b> after we get your appeal. You can ask for a fast appeal if you or your doctor believe your health could be seriously harmed by waiting upto <b>30 days</b> for a decision.</p><p class="TimesNewRoman-12FontSize pSpacing"><br/><b>We will automatically give you a fast appeal if a doctor asks for one for you or supports your request.</b> If you ask for a fast appeal without support from a doctor, we will decide if your request requires a fast appeal. If we do not give you a fast appeal, we will give you a decision within <b>30 days</b>.';
    var FourthDiv = '</p></div><div class="TimesNewRoman-12FontSize pSpacing"><p class="TimesNewRoman-17FontSize"><br/><b style="font-size:20px">How to ask for an appeal with Ultimate Health Plans</b></p><br/><p><b  style="font-weight: 700;font-size:16px">Step 1:</b> You, your representative, or your doctor must ask us for an appeal. Your request must include:</p><br/><ul><li>Your name</li><li>Address</li><li>Member Number</li><li>Reasons for appealing</li><li>Any evidence you want us to review, such as medical records, doctors letters, or other information that explains why you need the item or service. Call your doctor if you need this information.</li></ul><p><i>You can ask us to see the medical records and other documents we used to make our decision before or during the appeal. At no cost to you, you can also ask for a copy of the guidelines we used to make our decision.</i></p><p style="margin-top:15px"><b style="font-weight: 700;font-size:16px">Step 2:</b> Mail, fax, or deliver your appeal or call us.</p><br/><p><b style="font-weight: 700;">For a Standard Appeal: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address: P.O. Box 6560, Spring Hill, FL 34611</p><div style="margin-left:160px;margin-top: 10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone: 1-888-657-4170   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax: 1-885-895-4748</div><p style="padding-top: 2%;"><i>If you ask for a standard appeal by phone, we will send you a letter confirming what you told us.</i></p><p style="margin-top: 18px;"><b>For a Fast Appeal: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone: 1-888-657-4170  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fax: 1-885-895-4748';
    var FifthDiv = '</p></div><br/><div class="TimesNewRoman-12FontSize pSpacing"><p><b style="font-size: 20px;font-weight: 700;">What happens next?</b></p><br/><p>If you ask us for an appeal and we continue to deny your request for a service, we will send you a written decision and automatically send your case to an independent reviewer.<b> If the independent reviewer denies your request, the written decision will explain if you have additional appeal rights.</b></p><p style="margin-top:15px"><b style="font-weight: 700;font-size:20px">Get help and more information</b><ul><br/><li>Ultimate Health Plans Toll Free: 1-888-657-4170&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TTY users call: 711<br /> Monday through Sunday from 8 a.m. to 8 p.m.</li><li>1-800-MEDICARE(1-800-633-4227), 24 hours, 7 days a week. TTY users call: 1-877-486-2048</li><li>Medical Rights Center: 1-888-HMO-9050</li><li>Elder Care Locator: 1-800-677-1116</li></ul></p></div><br /></div><br /><br/><br/><footer style="font-family:Times New Roman;font-size:13px"><p style="float:left;">Form CMS 10003-NDMCP(Iss. 06/2013)</p><p style="float:right;">OMB Approval 0938-0829</p></footer></div></div></div>';
    $("#DenialTemplate").append(firstDiv + SecondDiv + ThirdDiv + FourthDiv + FifthDiv);

}

var NomncLetterPreview = function () {
   
    ShowModal('~/Views/Home/Letter/_NomncLetter.cshtml', 'Nomnc Letter');
}

var DencLetterPreview = function () {
    showModal('DENCPreviewModal');
}