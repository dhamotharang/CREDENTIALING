var ApprovalData = [{
    NewReferenceNumber: "5657159201",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*1234",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "SUZANNE NICHOLS",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "SERVICE PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {

    NewReferenceNumber: "5657159202",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*1212",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "GRAY SARA",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "SERVICE PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {

    NewReferenceNumber: "5657159203",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*2121",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Judith Palmer",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "SERVICE PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {

    NewReferenceNumber: "5657159204",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*1080",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Alice Fowler",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "SERVICE PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {
    //---22-op-observation-pos
    NewReferenceNumber: "5657159205",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*9090",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Jimmy Smith",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "ATTENDING PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {
    //---21-pos
    NewReferenceNumber: "5657159206",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*1111",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "George Romero",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "ATTENDING PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}, {
    // 31-pos
    NewReferenceNumber: "5657159207",
    NewRequestedBy: "DALTON BENSON",
    NewSubscriberID: "UL*1222",
    NewLastModifiedDate: "09/23/2016",
    MemberName: "Melissa Reed",
    NewDate: "September 15, 2016",
    NewReceivedDate: "09/23/2016",
    Service_Attending: "ATTENDING PROVIDER",
    ServiceRequestedContent: "SKILLED NURSING ADMISSION ",
    PCP_Provider: "DR. DALTON BENSON",
    NewFacilityName: "BAYWOOD NURSING CENTER",
    final: "DR. DALTON BENSON</span><br/>&nbsp;&nbsp;<span>'BAYWOOD NURSING CENTER",
    address1: '9037 BYRON STREET',
    City: 'SPRING HILL, ',
    State: 'FL, ',
    ZipCode: '34606'

}];
function ApprovalBatchLetter() {
    $("#preview_Approval").html("");
    for (var i = 0; i < ApprovalData.length; i++) {
        var datacontent = $("#preview_Approval").html();
        var NewReferenceNumber = ApprovalData[i].NewReferenceNumber;
        var NewRequestedBy = ApprovalData[i].NewRequestedBy;
        var NewSubscriberID = ApprovalData[i].NewSubscriberID;
        var NewLastModifiedDate = ApprovalData[i].NewLastModifiedDate;
        var MemberName = ApprovalData[i].MemberName;
        var NewDate = ApprovalData[i].NewDate;
        var NewReceivedDate = ApprovalData[i].NewReceivedDate;
        var Service_Attending = ApprovalData[i].Service_Attending;
        var ServiceRequestedContent = ApprovalData[i].ServiceRequestedContent;
        var final = ApprovalData[i].final;
        var address1 = ApprovalData[i].address1;
        var City = ApprovalData[i].City;
        var State = ApprovalData[i].State;
        var ZipCode = ApprovalData[i].ZipCode;
        
        var Content0 = '<div style="color:black;font-weight: 400;font-size:15px;"><div class="pull-right"><i class="fa fa-times-circle-o fa-2x pull-right pointer ReviewClick" style="color:red" id="' + i + '"></i><i class="fa fa-check-circle-o fa-2x pull-right pointer ApproveClick" style="color:green" id="' + i + '"></i></div>';
        var Content1 = '<div  class="col-lg-12" style="width: 100.5%"><div id="content1" class="col-lg-12" width="100%"><img src="/Resources/Images/Common/Ultimate_Logo.png" class="pull-left LogoHeight"><div class="CalibriFontStyle-12FontSize pull-right"><p class="removeBottomMargin "><span class=" CalibriFontStyle-12FontSize spacing" style="padding-left: 12px; font-family: Calibri">1244 Mariner Boulevard </span></p><p class="removeBottomMargin"><span class=" CalibriFontStyle-12FontSize spacing" style="padding-left: 8px; font-family: Calibri">Spring Hill, Florida 34609</span></p><p class="removeBottomMargin"><span class=" CalibriFontStyle-12FontSize spacing" style="font-family: Calibri">www.chooseultimate.com</span></p></div><div class="clearfix"></div><div ><br/><p class="removeBottomMargin" style="font-family: "Times New Roman;font-size: 15px"><span style="text-transform: uppercase;font-family: Times New Roman">' + MemberName + '</span></p></div><div style="text-transform: uppercase"><p class="removeBottomMargin"><span style="font-family:Times New Roman;font-size: 15px;text-transform: uppercase">' + address1 + '</span></p><p class="removeBottomMargin"><span style="font-family:Times New Roman;font-size: 15px;text-transform: uppercase">' + City + State + ZipCode + '</span></p></div><p style="text-align:right" class="removeBottomMargin" style="font-family: "Times New Roman;font-size: 15px">' + NewDate + '</p><br /><p style="float:left" class="removeBottomMargin">Dear<span style="text-transform: uppercase;font-family: Times New Roman;"> ' + MemberName + ': </span></p><br /><br /><span style="font-family:Times New Roman;font-size: 15px;">Ultimate Health Plans received a request for the following service on<b> ' + NewReceivedDate + ':<br /><br /></b></span></div>';
        var Content2 = '<div id="content2" class="col-lg-12"><table class="col-lg-12 wrap-words" id="tableData" style="border:1px solid black; text-transform: uppercase;font-family: Times New Roman"><tr style="border:1px solid black;"><td width="40%" style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;MEMBER ID</td><td style="font-weight: 700;">&nbsp;&nbsp;' + NewSubscriberID + '</td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;SERVICE REQUESTED</td><td style="padding-left: 10px;font-weight: 700;"><span>' + ServiceRequestedContent + '</span></td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;REQUESTED BY</td><td style="font-weight: 700;">&nbsp;&nbsp;' + PCP_Provider + NewRequestedBy + '</td></tr><tr style="border:1px solid black;"><td style="border:1px solid black;font-weight: normal;">&nbsp;&nbsp;' + Service_Attending + '</td><td style="text-transform: uppercase;font-weight: 700;">&nbsp;&nbsp;' + final + '</td></tr></table></div>';
        var Content3 = '<div id="content3" class="col-lg-12" width="100%"><p class="removeBottomMargin"><span class=" pSpacing" style="font-family: "Times New Roman;font-size: 15px"><br />Ultimate Health Plans has approved this request on <b> ' + NewLastModifiedDate + '</b> and has assigned authorization number <b>' + NewReferenceNumber + '</b> for this service. This authorization is valid for 90 days from the date of approval.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin"><span class="TimesNewRoman-12FontSize pSpacing" style="font-family: "Times New Roman;font-size: 15px">If you have any questions, please contact our Member Services Department at 1-888-657-4170. We are available 7 days per week from 8:00am to 8:00pm EST. TTY/TDD users should call 711.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin"><span class="TimesNewRoman-12FontSize pSpacing" style="font-family: "Times New Roman;font-size: 15px">Please be advised that from February 15 to September 30, Ultimate Health Plans may use alternative technologies to answer your call on weekends and federal holidays.</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin" style="margin-top:10px;"><span style="font-family: "Times New Roman;font-size: 15px">Thank You,<br />Ultimate Health Plans</span></p><p class="removeBottomMargin">&nbsp;</p><p class="removeBottomMargin">&nbsp;</p><div class="text-center TimesNewRoman-12FontSize" style="font-family: "Times New Roman;font-size: 15px"><strong><i>Ultimate Health Plans is an HMO plan with a Medicare contract.</i></strong></div><div class="text-center TimesNewRoman-12FontSize" style="font-family: "Times New Roman;font-size: 15px"><strong><i>Enrollment in Ultimate Health Plans depends on contract renewal.</i></strong></div><p class="removeBottomMargin">&nbsp;</p><footer><span style="float:right;" style="font-family: "Times New Roman;font-size: 15px;text-color:#D3D3D3"> H2962_Part C Approval Letter v216 Accepted</span></footer></div></div><div class="clearfix" style="border-bottom:ridge"></div><br /></div>';
        $("#preview_Approval").html(datacontent+Content0 + Content1 + Content2 + Content3);
    }
}

//--------- approval_batch
$('.approval_batch').off('click', '.approval_btn').on('click', '.approval_btn', function () {
    TabManager.openSideModal('~/Areas/UM/Views/LetterBatching/ApprovalLetter/_ApprovalLetterBatch.cshtml', 'BATCH LETTER', 'cancel', '', 'ApprovalBatchLetter', '');
    return;
});
