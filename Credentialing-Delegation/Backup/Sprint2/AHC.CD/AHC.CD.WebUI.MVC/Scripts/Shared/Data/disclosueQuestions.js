

profileApp.controller('DisclosureController', function ($scope) {

    $scope.count = 0;
    $scope.increment=function(){
        count++;
    };

    $scope.disclosureQuestions = [{
        catName: "Licensure",
        questions:
            [{ questionText: "1. Has your license, registration or certification to practice in your profession, ever been voluntarily or involuntarily relinquished, denied, suspended, revoked, restricted, or have you ever been subject to a fine, reprimand, consent order, probation or any con- ditions or limitations by any state or professional licensing, registration or certification board?*" },
            { questionText: "2. Has there been any challenge to your licensure, registration or certification?*" },
             { questionText: "3. Do you have a history of loss of license and/or felony convictions?*" },
            ]},
        {catName: "Hospital Privileges And Other  Affiliations",
            questions:
                [{ questionText: "1. Have your clinical privileges or medical staff membership at any hospital or healthcare institution, voluntarily or involuntarily, ever been denied, suspended, revoked, restricted, denied renewal or subject to probationary or to other disciplinary conditions (for reasons other than non-completion of medical record when quality of care was not adversely affected) or have proceedings toward any of those ends been instituted or recommended by any hospital or healthcare institution, medical staff or committee, or governing board?*" },
                { questionText: "2. Have you voluntarily or involuntarily surrendered, limited your privileges or not reapplied for privileges while under investigation?*" },
                { questionText: "3. Have you ever been terminated for cause or not renewed for cause from participation, or been subject to any disciplinary action, by any managed care organizations (including HMOs, PPOs, or provider organizations such as IPAs, PHOs)?*" },
                ]},
       { catName: "Education, Training And Board Certification  ",
           questions:
               [{ questionText: "1. Were you ever placed on probation, disciplined, formally reprimanded, suspended or asked to resign during an internship, resi- dency, fellowship, preceptorship or other clinical education program?  If you are currently in a training program, have you been placed on probation, disciplined, formally reprimanded, suspended or asked to resign?*" },
               { questionText: "2. Have you ever, while under investigation or to avoid an investigation, voluntarily withdrawn or prematurely terminated your status as a student or employee in any internship, residency, fellowship, preceptorship, or other clinical education program?*" },
               { questionText: "3. Have any of your board certifications or eligibility ever been revoked?*" },
               { questionText: "4. Have you ever chosen not to re-certify or voluntarily surrendered your board certification(s) while under investigation?*" },
               ]},
        {catName: "DEA Or State Controlled Substance Registration ",
            questions:
                [{ questionText: "1. Have your Federal DEA and/or State Controlled Dangerous Substances (CDS) certificate(s) or authorization(s) ever been challenged,denied, suspended, revoked, restricted, denied renewal, or voluntarily or involuntarily relinquished?*" },
                     { questionText: "2. Do you have any history of chemical dependency/substance abuse?*" },
                ]
        },
        {catName: "Medicare, Medicaid Or Other Governmental Program Participation",
            questions:
                [{ questionText: "1. Have you ever been disciplined, excluded from, debarred, suspended, reprimanded, sanctioned, censured, disqualified or otherwise restricted in regard to participation in the Medicare or Medicaid program, or in regard to other federal or state governmental healthcare plans or programs?*" },
                ]},
        {catName: "Other Sanctions Or Investigations ",
            questions:
            [{ questionText: "1. Are you currently the subject of an investigation by any hospital, licensing authority, DEA or CDS authorizing entities, education or training program, Medicare or Medicaid program, or any other private, federal or state health program or a defendant in any civil action that is reasonably related to your qualifications, competence, functions, or duties as a medical professional for alleged fraud, an act of violence, child abuse or a sexual offense or sexual misconduct?*" },
            { questionText: "2. To your knowledge, has information pertaining to you ever been reported to the National Practitioner Data Bank or Healthcare Integrity and Protection Data Bank?*" },
            { questionText: "3. Have you ever received sanctions from or are you currently the subject of investigation by any regulatory agencies (e.g., CLIA, OSHA, etc.)?*" },
            { questionText: "4. Have you ever been convicted of, pled guilty to, pled nolo contendere to, sanctioned, reprimanded, restricted, disciplined or resigned in exchange for no investigation or adverse action within the last ten years for sexual harassment or other illegal misconduct?*" },
            { questionText: "5. Are you currently being investigated or have you ever been sanctioned, reprimanded, or cautioned by a military hospital, facility, oragency, or voluntarily terminated or resigned while under investigation or in exchange for no investigation by a hospital or healthcare facility of any military agency?*" },
             { questionText: "6. Do you have any physical or mental health problems that may affect your ability to provide health care?*" },
            ]
        },
        {catName: "Professional Liability Insurance Information And Claims History",
            questions:
            [{ questionText: "1. Has your professional liability coverage ever been cancelled, restricted, declined or not renewed by the carrier based on your individual liability history?*" },
                    { questionText: "2. Have you ever been assessed a surcharge, or rated in a high-risk class for your specialty, by your professional liability insurance carrier, based on your individual liability history?*" },
            ]},
        {catName: "Malpractice Claims History",
            questions:
            [{ questionText: "1. Have you had any professional liability actions (pending, settled, arbitrated, mediated or litigated) within the past 10 years?* If yes, provide information for each case." },
            ]},
        {catName: "Criminal/Civil History",
            questions:
            [{ questionText: "1. Have you ever been convicted of, pled guilty to, or pled nolo contendere to any felony?*" },
            { questionText: "2. In the past ten years have you been convicted of, pled guilty to, or pled nolo contendere to any misdemeanor (excluding minor traffic violations) or been found liable or responsible for any civil offense that is reasonably related to your qualifications, competence, functions, or duties as a medical professional, or for fraud, an act of violence, child abuse or a sexual offense or sexua  misconduct?*" },
                    { questionText: "3. Have you ever been court-martialed for actions related to your duties as a medical professional?*" },
            ]},
        {catName: "Ability To Perform Job ",
            questions:
            [{ questionText: "1. Are you currently engaged in the illegal use of drugs?*" },
            { questionText: "2. Do you use any chemical substances that would in any way impair or limit your ability to practice medicine and perform the functions of your job with reasonable skill and safety?*" },
                    { questionText: "3. Do you have any reason to believe that you would pose a risk to the safety or well being of your patients?*" },
            { questionText: "4. Are you unable to perform the essential functions of a practitioner in your area of practice even with reasonable accommodation?*" },
            ]},

    ];
});