var denialLetterData = [{
    NewReferenceNumber: "5657159201",
    NewSubscriberID: "UL*1234",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "SUZANNE NICHOLS",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {

    NewReferenceNumber: "5657159202",
    NewSubscriberID: "UL*1212",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "GRAY SARA",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {

    NewReferenceNumber: "5657159203",
    NewSubscriberID: "UL*2121",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Judith Palmer",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {

    NewReferenceNumber: "5657159204",
    NewSubscriberID: "UL*1080",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Alice Fowler",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {
    //---22-op-observation-pos
    NewReferenceNumber: "5657159205",
    NewSubscriberID: "UL*9090",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Jimmy Smith",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {
    //---21-pos
    NewReferenceNumber: "5657159206",
    NewSubscriberID: "UL*1111",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "George Romero",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed: "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}, {
    // 31-pos
    NewReferenceNumber: "5657159207",
    NewSubscriberID: "UL*1222",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Melissa Reed",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Expected_DOS: "09/23/2016",
    Rationale: "We denied this item listed above because: shower chair w/wo wheels any size (HCPCS code E2040) is non-covered by Medicare.herefore the requested shower chair is denied.",
    CriteriaUsed : "Carrier judgement Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.",
    AlternatePlan: "Member may purchase on his/her own.",
    ServiceSubjectToNotice: "DURABLE MEDICAL EQUIPMENT",
    ListOfMedicalServices: "Shower Chair"
}];


function DenialLetterPreview() {

    $("#preview_Denial").html("");
    for (var k = 0; k < denialLetterData.length; k++) {
        var denialcontent = $("#preview_Denial").html();
        var NewReferenceNumber=denialLetterData[k].NewReferenceNumber;
        var NewSubscriberID = denialLetterData[k].NewSubscriberID;
        var NewLastModifiedDate = denialLetterData[k].NewLastModifiedDate;
        var MemberName = denialLetterData[k].MemberName;
        var NewDate = denialLetterData[k].NewDate;
        var NewReceivedDate = denialLetterData[k].NewReceivedDate;
        var Rationale = denialLetterData[k].Rationale;
        var Expected_DOS = denialLetterData[k].Expected_DOS;
        var CriteriaUsed = denialLetterData[k].CriteriaUsed;
        var AlternatePlan = denialLetterData[k].AlternatePlan;
        var ServiceSubjectToNotice = denialLetterData[k].ServiceSubjectToNotice;
        var ListOfMedicalServices = denialLetterData[k].ListOfMedicalServices;

        var firstDiv = '<div id="htmlIdDenial' + k + '" style="color: black;font-weight: 400;font-size: 15px;font-family: times new roman;padding-top:10px"><br/><i class="fa fa-times-circle-o fa-2x pull-right pointer ReviewClickDenial" style="color:red" id="' + k + '"></i><i class="fa fa-check-circle-o fa-2x pull-right pointer ApproveClickDenial" style="color:green" id="' + k + '"></i><div class="col-lg-12" id="denialmemberletterdiv" style="padding-left:50px"><div><p><span class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;"><b>Important: </b>This notice explains your right to appeal our decision. Read this notice carefully. If you need help, you can call one of the numbers listed on the last page under "Get help & more information."</span></p></div><div style="text-align:center;padding-bottom:12px"><p class="TimesNewRoman-19FontSize" style="padding-top:20px;"><b style="font-family: Times New Roman;font-size: 22px;font-weight:700">Notice of Denial of Medical Coverage</b></p><p style="text-align:center;" class="TimesNewRoman-12FontSize">Denial of Medical Coverage</p></div><div id="borderStyle" class="TimesNewRoman-12FontSize" style="border-top:1px solid #020202;border-bottom:1px solid #020202;overflow: hidden;padding-bottom:5px;"><div style="width:90%;"><p style="float:left;margin-top:0px;margin-bottom:0px;" class="removeBottomMargin"><b>Date: </b><i>' + NewDate + '</i></p><p style="float:right;margin-top:0px;margin-bottom:0px;" class="removeBottomMargin"><b>Member Number: </b><i>' + NewSubscriberID + '</i></p></div><br /><div style="float:left;margin-top:0px;margin-bottom:-8px;"><p class="removeBottomMargin"><b >Name: </b><i class="text-uppercase">' + MemberName + '</i></p></div><br /><div style="float:left;width:90%;display:inline;"><p style="float:left;margin-top:0px;margin-bottom:0px;width:60%;" class="removeBottomMargin"><b>Service Subject to Notice: </b><i class=" wrap-words text-uppercase"> ' + ServiceSubjectToNotice + '<br /></i></p><p style="float:right;margin-top:0px;margin-bottom:0px;margin-right:2px;" class="removeBottomMargin"><b>Date of Service:</b><i> ' + Expected_DOS + '</i></p></div><br /><div style="width:90%;" class="TimesNewRoman-12FontSize"><p style="float:left;margin-top:0px;"><b>Authorization Number: </b><i>' + NewReferenceNumber + '</i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p style="float:right;margin-top:0px;"><b>Requested Date: </b><i>' + NewReceivedDate + '</i></p><br /></div></div>';
        var SecondDiv = '<div class="pSpacing"><br /><span class="TimesNewRoman-17FontSize" style="width: 50%;"><b style="font-size: 20px;font-family:Times New Roman">Your request was denied</b></span><br /><span class="TimesNewRoman-12FontSize">We have denied the medical services/items listed below requested by you or your doctor:</span><br /><br /><p style="margin-top:0px;" class="TimesNewRoman-12FontSize pSpacing"><b>List of Medical Services or Items:&nbsp;</b><br/><i class="text-uppercase wrap-words">' + ListOfMedicalServices + '</i><br/></p><div class="pSpacing" style="margin-top:18px"><p style="font-size: 20px;font-family:Times New Roman;padding-bottom:6px"><b style="padding-bottom:6px"">Why did we deny your request?</b></p><p class="TimesNewRoman-12FontSize" style="margin-top:10px">We denied the medical services/items listed above because:</p><p class="TimesNewRoman-12FontSize" style="margin-top:10px"><span><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + Rationale + '</i></span><br/><br/><span><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + CriteriaUsed + '</i><br /></span><i style="text-align: justify;text-justify: inter-word;">Physicians have the right to discuss UM denial decisions with the Physician. Members/Providers can obtain a copy of the actual benefit, guideline, protocol, or other similar criteria on which the denial decision was based, upon request.</i><br /><br /><span><b>Alternative Plan of Care:</b>   <br/><i class="wrap-words" style="text-align: justify;text-justify: inter-word;">' + AlternatePlan + '</i><br /><br /></span></p></div><div class="pSpacing" style="margin-top:15px"><p class="TimesNewRoman-17FontSize"><b style="font-size: 20px;font-weight: 700;font-family:Times New Roman">You have the right to appeal our decision</b></p><p class="TimesNewRoman-12FontSize" style="text-align: justify;text-justify: inter-word;margin-top:10px">You have the right to ask Ultimate Health Plans to review our decision by asking us for an appeal:</p><p class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;margin-top:13px"><b>Appeal:</b>&nbsp;Ask Ultimate Health Plans for an appeal within <b>60 days</b> of the date of this notice. We can give you more time if you have a good reason for missing the deadline.</p><p class="TimesNewRoman-15FontSize" style="margin-top:12px;font-size: 17px;font-family:Times New Roman"><b>If you want someone else to act for you</b></p><p class="TimesNewRoman-12FontSize pSpacing" style="text-align: justify;text-justify: inter-word;margin-top:15px">You can name a relative, friend, attorney, doctor, or someone else to act as your representative. If you want someone else to act for you, call us at: 1-888-657-4170 to learn how to name your representative. TTY users call 711. Both you and the person you want to act for you must sign and date a statement confirming this is what you want. You will need to mail or fax this statement to us.</p></div><br /></div><div class="page-break" style="page-break-before:always"></div><br />';
        var ThirdDiv = '<div style="text-align: justify;text-justify: inter-word;"><div class="col-lg-12"><div class="col-lg-2"></div><div class="col-lg-8"><div style="text-align:center;"><p style="text-align:center;" class="TahomaFontStyle-12FontSize"><b style="font-family:Times New Roman;font-weight: 700;">Important Information About Your Appeal Rights</b></p></div></div><div class="col-lg-2"></div></div><div style="margin-top:15px"><p><br/><b style="font-weight: 700;font-size: 20px;font-family:Times New Roman">There are 2 kinds of appeals</b></p><p class="TimesNewRoman-12FontSize pSpacing"><b>Standard Appeal-</b>We will give you a written decision on a standard appeal within <b>30 days</b> after we get your appeal. Our decision might take longer if you ask for an extension, or if we need more information about your case. We will tell you if we are taking extra time and will explain why more time will be needed. If your appeal is for payment of a service you have already received, we will give you a written decision within <b>60 days</b>.</p><p class="TimesNewRoman-12FontSize pSpacing"><br/><b>Fast Appeal:</b> We will give you a decision on a fast appeal within <b>72 hours</b> after we get your appeal. You can ask for a fast appeal if you or your doctor believe your health could be seriously harmed by waiting upto <b>30 days</b> for a decision.</p><p class="TimesNewRoman-12FontSize pSpacing"><br/><b>We will automatically give you a fast appeal if a doctor asks for one for you or supports your request.</b> If you ask for a fast appeal without support from a doctor, we will decide if your request requires a fast appeal. If we do not give you a fast appeal, we will give you a decision within <b>30 days</b>.';
        var FourthDiv = '</p></div><div class="TimesNewRoman-12FontSize pSpacing"><p class="TimesNewRoman-17FontSize"><br/><b style="font-size:20px">How to ask for an appeal with Ultimate Health Plans</b></p><br/><p><b  style="font-weight: 700;font-size:16px">Step 1:</b> You, your representative, or your doctor must ask us for an appeal. Your request must include:</p><br/><ul><li>Your name</li><li>Address</li><li>Member Number</li><li>Reasons for appealing</li><li>Any evidence you want us to review, such as medical records, doctors letters, or other information that explains why you need the item or service. Call your doctor if you need this information.</li></ul><p><i>You can ask us to see the medical records and other documents we used to make our decision before or during the appeal. At no cost to you, you can also ask for a copy of the guidelines we used to make our decision.</i></p><p style="margin-top:15px"><b style="font-weight: 700;font-size:16px">Step 2:</b> Mail, fax, or deliver your appeal or call us.</p><br/><p><b style="font-weight: 700;">For a Standard Appeal: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address: P.O. Box 6560, Spring Hill, FL 34611</p><div style="margin-left:160px;margin-top: 10px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Phone: 1-888-657-4170   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax: 1-885-895-4748</div><p style="padding-top: 2%;"><i>If you ask for a standard appeal by phone, we will send you a letter confirming what you told us.</i></p><p style="margin-top: 18px;"><b>For a Fast Appeal: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone: 1-888-657-4170  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fax: 1-885-895-4748';
        var FifthDiv = '</p></div><br/><div class="TimesNewRoman-12FontSize pSpacing"><p><b style="font-size: 20px;font-weight: 700;">What happens next?</b></p><br/><p>If you ask us for an appeal and we continue to deny your request for a service, we will send you a written decision and automatically send your case to an independent reviewer.<b> If the independent reviewer denies your request, the written decision will explain if you have additional appeal rights.</b></p><p style="margin-top:15px"><b style="font-weight: 700;font-size:20px">Get help and more information</b><ul><br/><li>Ultimate Health Plans Toll Free: 1-888-657-4170&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TTY users call: 711<br /> Monday through Sunday from 8 a.m. to 8 p.m.</li><li>1-800-MEDICARE(1-800-633-4227), 24 hours, 7 days a week. TTY users call: 1-877-486-2048</li><li>Medical Rights Center: 1-888-HMO-9050</li><li>Elder Care Locator: 1-800-677-1116</li></ul></p></div><br /></div><footer style="font-family:Times New Roman;font-size:13px"><p style="float:left;">Form CMS 10003-NDMCP(Iss. 06/2013)</p><p style="float:right;">OMB Approval 0938-0829</p></footer><br /><br/><br/><hr id="division_Denial' + k + '" style="border-top:1px solid #020202"/></div></div></div>';
        $("#preview_Denial").append(denialcontent + firstDiv + SecondDiv + ThirdDiv + FourthDiv + FifthDiv);
    }
}

$('.denial_body').off('click', '.denial_batch').on('click', '.denial_batch', function () {
    TabManager.openSideModal('~/Areas/UM/Views/LetterBatching/DenialLetter/_DenialLetterBatch.cshtml', 'BATCH LETTER', 'cancel', '', 'DenialLetterPreview', '');
    return;
});