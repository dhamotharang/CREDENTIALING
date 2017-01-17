

angular.module('CnDRouter.applicationForm', [])
.config(
  ['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider
          .state('application_form', {

              //abstract: true,

              url: '/application_form',

              templateUrl: '/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ViewForm.cshtml',

              //resolve: {
              //    applicationForms: applicationForms
              //},

              controller: ['$scope', '$state',
                function ($scope, $state) {

                    $scope.ApplicationForms = applicationForms;

                    $state.go('application_form.detail', { FormID: $scope.ApplicationForms[0].id });
                }]
          })
        
        .state('application_form.list', {
            url: '',
            templateUrl: '/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ViewForm.cshtml',

            controller: ['$scope', '$state',
                function ($scope, $state) {

                    $scope.ApplicationForms = applicationForms;

                    $state.go('application_form.detail', { FormID: $scope.ApplicationForms[0].id });
                }]
        })
          .state('application_form.detail', {

              url: '/{FormID:[0-9]{1,4}}',

              views: {

                  '': {
                      templateUrl: function ($stateParams) {
                          var url = "";
                          for (var i in applicationForms) {
                              if ($stateParams.FormID == applicationForms[i].id) {
                                  url = applicationForms[i].templateurl;
                                  break;
                              }
                          }
                          if (url != "") {
                              return '/../../AHCUtil/GetPartial?partialUrl=' + url;
                          } else {
                              return '/../../AHCUtil/GetPartial?partialUrl=' + applicationForms[0].templateurl;
                              //$stateParams.FormID = applicationForms[0].id;
                          }
                      },
                      controller: ['$scope', '$stateParams',
                        function ($scope, $stateParams) {

                            console.log($stateParams);
                            if ($stateParams.FormID == 12) {
                                $scope.count = 0;
                                $scope.increment = function () {
                                    count++;
                                };

                                $scope.disclosureQuestions = [{
                                    catName: "Licensure",
                                    instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                    questions:
                                        [{ questionText: "1. Has your license, registration or certification to practice in your profession, ever been voluntarily or involuntarily relinquished, denied, suspended, revoked, restricted, limited, or have you ever been subject to a fine, reprimand, consent order, probation or any conditions or limitations by any state or professional licensing, registration or certification board?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "2. Has there been any challenge to your licensure, registration or certification?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "3. Do you have a history of loss of license and/or felony convictions?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        ]
                                },
                                    {
                                        catName: "Hospital Privileges And Other  Affiliations",
                                        instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                            [{ questionText: "4. Have your clinical privileges or medical staff membership at any hospital or healthcare institution, voluntarily or involuntarily, ever been denied, suspended, revoked, restricted, denied renewal or subject to probationary or to other disciplinary conditions (for reasons other than non-completion of medical record when quality of care was not adversely affected) or have proceedings toward any of those ends been instituted or recommended by any hospital or healthcare institution, medical staff or committee, or governing board?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                            { questionText: "5. Have you voluntarily or involuntarily surrendered, limited your privileges or not reapplied for privileges while under investigation?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                            { questionText: "6. Have you ever been terminated for cause or not renewed for cause from participation, or been subject to any disciplinary action, by any managed care organizations (including HMOs, PPOs, or provider organizations such as IPAs, PHOs)?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                            ]
                                    },
                                   {
                                       catName: "Education, Training And Board Certification  ",
                                       instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                       questions:
                                           [{ questionText: "7. Were you ever placed on probation, disciplined, formally reprimanded, suspended or asked to resign during an internship, residency, fellowship, preceptorship or other clinical education program?  If you are currently in a training program, have you been placed on probation, disciplined, formally reprimanded, suspended or asked to resign?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                           { questionText: "8. Have you ever, while under investigation or to avoid an investigation, voluntarily withdrawn or prematurely terminated your status as a student or employee in any internship, residency, fellowship, preceptorship, or other clinical education program?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                           { questionText: "9. Have any of your board certifications or eligibility ever been revoked?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                           { questionText: "10. Have you ever chosen not to re-certify or voluntarily surrendered your board certification(s) while under investigation?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                           ]
                                   },
                                    {
                                        catName: "DEA Or State Controlled Substance Registration ",
                                        instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                            [{ questionText: "11. Have your Federal DEA and/or State Controlled Dangerous Substances (CDS) certificate(s) or authorization(s) ever been challenged,denied, suspended, revoked, restricted, denied renewal, or voluntarily or involuntarily relinquished?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                             { questionText: "12. Do you have any history of chemical dependency/substance abuse?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                            ]
                                    },
                                    {
                                        catName: "Medicare, Medicaid Or Other Governmental Program Participation",
                                        instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                            [{ questionText: "13. Have you ever been disciplined, excluded from, debarred, suspended, reprimanded, sanctioned, censured, disqualified or otherwise restricted in regard to participation in the Medicare or Medicaid program, or in regard to other federal or state governmental healthcare plans or programs?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                            ]
                                    },
                                    {
                                        catName: "Other Sanctions Or Investigations ",
                                        instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                        [{ questionText: "14. Are you currently the subject of an investigation by any hospital, licensing authority, DEA or CDS authorizing entities, education or training program, Medicare or Medicaid program, or any other private, federal or state health program or a defendant in any civil action that is reasonably related to your qualifications, competence, functions, or duties as a medical professional for alleged fraud, an act of violence, child abuse or a sexual offense or sexual misconduct?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "15. To your knowledge, has information pertaining to you ever been reported to the National Practitioner Data Bank or Healthcare Integrity and Protection Data Bank?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "16. Have you ever received sanctions from or are you currently the subject of investigation by any regulatory agencies (e.g., CLIA, OSHA, etc.)?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "17. Have you ever been convicted of, pled guilty to, pled nolo contendere to, sanctioned, reprimanded, restricted, disciplined or resigned in exchange for no investigation or adverse action within the last ten years for sexual harassment or other illegal misconduct?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "18. Are you currently being investigated or have you ever been sanctioned, reprimanded, or cautioned by a military hospital, facility, or agency, or voluntarily terminated or resigned while under investigation or in exchange for no investigation by a hospital or healthcare facility of any military agency?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "19. Do you have any physical or mental health problems that may affect your ability to provide health care?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        ]
                                    },
                                    {
                                        catName: "Professional Liability Insurance Information And Claims History",
                                        instruction: "* Answer all questions. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                        [{ questionText: "20. Has your professional liability coverage ever been canceled, restricted, declined or not renewed by the carrier based on your individual liability history?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "21. Have you ever been assessed a surcharge, or rated in a high-risk class for your specialty, by your professional liability insurance carrier, based on your individual liability history?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "22. Have you ever been denied professional liability insurance coverage?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "23. Has your present liability insurance carrier excluded any specific procedures from your insurance coverage?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "24. Are any professional liability  (i.e. malpractice)claims ,suits, judgments, settlements or arbitration proceedings involving you currently pending?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },

                                        ]
                                    },
                                    {
                                        catName: "Malpractice Claims History",
                                        instruction: "* Answer question. For any \"Yes\"  response, provide an explanation",
                                        questions:
                                        [{ questionText: "25. Have you had any professional liability actions (pending, settled, arbitrated, mediated or litigated) within the past 10 years?* If yes, provide information for each case.", placeholderText: "Please Elaborate", answerText: "Yes", reasonText: "The Malpractice occured on 04/08/2003, claim was filed on 09/03/2003 against MAG Mutual professional liability carrier and was setteld with the settlement/award amount of $240,000.00" },
                                        ]
                                    },
                                    //\\192.168.123.18\\client_communications\\AHCP -FLAH  ACCESS HEALTH CARE PHYSICIANS, LLC\\PERSONAL PER DR\\SINGH MD, PARIKSITH\\CAQH
                                    {
                                        catName: "Criminal/Civil History",
                                        instruction: "* If you answered Yes to \"Malpractice Claims History\", you must complete the supplemental Malpractice claims explanation",
                                        questions:
                                        [{ questionText: "26. Have you ever been convicted of, pled guilty to, or pled nolo contendere to any felony?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "27. In the past ten years have you been convicted of, pled guilty to, or pled nolo contendere to any misdemeanor (excluding minor traffic violations) or been found liable or responsible for any civil offense that is reasonably related to your qualifications, competence, functions, or duties as a medical professional, or for fraud, an act of violence, child abuse or a sexual offense or sexual misconduct?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                         { questionText: "28. Have you ever been court-martialed for actions related to your duties as a medical professional?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        ]
                                    },
                                    {
                                        catName: "Ability To Perform Job ",
                                        instruction: "* If you answered Yes to \"Malpractice Claims History\", you must complete the supplemental Malpractice claims explanation",
                                        questions:
                                        [{ questionText: "29. Are you currently engaged in the illegal use of drugs?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "30. Do you use any chemical substances that would in any way impair or limit your ability to practice medicine and perform the functions of your job with reasonable skill and safety?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "31. Are you currently engaged in illegal use of drugs (Currently means sufficiently recent to justify a reasonable belief that the use of drug may have an ongoing impact one's ability to practice medicine)?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "32. Do you have any reason to believe that you would pose a risk to the safety or well being of your patients?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        { questionText: "33. Are you unable to perform the essential functions of a practitioner in your area of practice even with reasonable accommodation?*", placeholderText: "Please Elaborate", answerText: "No", reasonText: "" },
                                        ]
                                    },

                                ];

                                $scope.saveQuestions = function (disclosureQuestions) {
                                    console.log(disclosureQuestions);
                                    $scope.changePartial = false;
                                }

                                $scope.showViewPartial = function () {
                                    $scope.changePartial = false;
                                }

                                $scope.showEditPartial = function () {
                                    $scope.changePartial = true;
                                }
                            }
                            //$scope.contact = utils.findById($scope.contacts, $stateParams.contactId);


                            
                        }]
                  }
              }
          });
    }
  ]
);


var applicationForms = [
    {
        id: 1,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewDemographics.cshtml",
        tagName: "Demographics"
    },
    {
        id: 2,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewIdentificationLicensing.cshtml",
        tagName: "Identification & Licenses"
    },
    {
        id: 3,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewEducation.cshtml",
        tagName: "Education History"
    },
    {
        id: 4,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewSpecialty.cshtml",
        tagName: "Specialty/Board"
    },
    {
        id: 5,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewPracticeLocationInformation.cshtml",
        tagName: "Practice Location"
    },
    {
        id: 6,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewHospitalPrivilegeInformation.cshtml",
        tagName: "Hospital Privilege"
    },
    {
        id: 7,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewLiability.cshtml",
        tagName: "Professional Liability"
    },
    {
        id: 8,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewWorkhistory.cshtml",
        tagName: "Work History"
    },
    {
        id: 9,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewProfessionalReference.cshtml",
        tagName: "Professional Reference"
    },
    {
        id: 10,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewProfessionalAffilation.cshtml",
        tagName: "Professional Affiliation"
    },
    {
        id: 11,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewContract.cshtml",
        tagName: "Contract Information"
    },
     {
        id: 12,
        templateurl: "~/Areas/Credentialing/Views/CnD/_DisclosureQuestions.cshtml",
        tagName: "Disclosure Questions"
     }

    

];