var MasterAuthorizationTypes = [
   {
       "AuthorizationTypeID": 3,
       "Name": "PreService".toUpperCase(),
       "Code": null,
       "Description": null
   },
	{
	    "AuthorizationTypeID": 2,
	    "Name": "Concurrent".toUpperCase(),
	    "Code": null,
	    "Description": null
	},
	{
	    "AuthorizationTypeID": 1,
	    "Name": "Concurrent Initial".toUpperCase(),
	    "Code": null,
	    "Description": null
	},

	{
	    "AuthorizationTypeID": 4,
	    "Name": "Retrospective".toUpperCase(),
	    "Code": null,
	    "Description": null
	}
];


var MasterContactEntityTypes = [
	{
	    "ContactEntityID": 7,
	    "EntityName": "Admitting Provider",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 5,
	    "EntityName": "Attending Provider",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 4,
	    "EntityName": "Facility",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 13,
	    "EntityName": "Health Plan",
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 9,
	    "EntityName": "Hospital",
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 11,
	    "EntityName": "MD",
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 1,
	    "EntityName": "Member",
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 12,
	    "EntityName": "Nurse",
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 3,
	    "EntityName": "PCP",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 6,
	    "EntityName": "Requesting Provider",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 10,
	    "EntityName": "SNF",
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 2,
	    "EntityName": "Specialist",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	},
	{
	    "ContactEntityID": 8,
	    "EntityName": "Surgeon",
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null
	}
];


var MasterContactOutcomes = [
	{
	    "OutcomeID": 32,
	    "OutcomeType": "Agrees With Plan Decision"
	},
	{
	    "OutcomeID": 33,
	    "OutcomeType": "Coverage Determination Requested"
	},
	{
	    "OutcomeID": 20,
	    "OutcomeType": "Fax Sent"
	},
	{
	    "OutcomeID": 1,
	    "OutcomeType": "Information received"
	},
	{
	    "OutcomeID": 2,
	    "OutcomeType": "Left Message"
	},
	{
	    "OutcomeID": 3,
	    "OutcomeType": "Left Message – VM"
	},
	{
	    "OutcomeID": 4,
	    "OutcomeType": "Left message with other"
	},
	{
	    "OutcomeID": 19,
	    "OutcomeType": "Letter Pending"
	},
	{
	    "OutcomeID": 18,
	    "OutcomeType": "Letter Sent"
	},
	{
	    "OutcomeID": 5,
	    "OutcomeType": "Notified member of approval"
	},
	{
	    "OutcomeID": 6,
	    "OutcomeType": "Notified member of denial"
	},
	{
	    "OutcomeID": 17,
	    "OutcomeType": "Notified Partial Approval"
	},
	{
	    "OutcomeID": 7,
	    "OutcomeType": "Number busy"
	},
	{
	    "OutcomeID": 8,
	    "OutcomeType": "Number Disconnected"
	},
	{
	    "OutcomeID": 31,
	    "OutcomeType": "Pending For PCP Answer"
	},
	{
	    "OutcomeID": 24,
	    "OutcomeType": "Spoke To Admitting Provider"
	},
	{
	    "OutcomeID": 9,
	    "OutcomeType": "Spoke To Attending Provider"
	},
	{
	    "OutcomeID": 10,
	    "OutcomeType": "Spoke to Facility"
	},
	{
	    "OutcomeID": 11,
	    "OutcomeType": "Spoke to Health Plan"
	},
	{
	    "OutcomeID": 12,
	    "OutcomeType": "Spoke to Home Care"
	},
	{
	    "OutcomeID": 13,
	    "OutcomeType": "Spoke to Hospice"
	},
	{
	    "OutcomeID": 26,
	    "OutcomeType": "Spoke To Hospital"
	},
	{
	    "OutcomeID": 28,
	    "OutcomeType": "Spoke To MD"
	},
	{
	    "OutcomeID": 29,
	    "OutcomeType": "Spoke To Nurse"
	},
	{
	    "OutcomeID": 14,
	    "OutcomeType": "Spoke to Office"
	},
	{
	    "OutcomeID": 22,
	    "OutcomeType": "Spoke To PCP"
	},
	{
	    "OutcomeID": 23,
	    "OutcomeType": "Spoke To Requesting Provider"
	},
	{
	    "OutcomeID": 27,
	    "OutcomeType": "Spoke To SNF"
	},
	{
	    "OutcomeID": 21,
	    "OutcomeType": "Spoke To Specialist"
	},
	{
	    "OutcomeID": 25,
	    "OutcomeType": "Spoke To Surgeon"
	},
	{
	    "OutcomeID": 15,
	    "OutcomeType": "Spoke with Member"
	},
	{
	    "OutcomeID": 30,
	    "OutcomeType": "SUBMITTED TO PLAN"
	},
	{
	    "OutcomeID": 16,
	    "OutcomeType": "Wrong Number"
	}
];


var MasterContactOutcomesMapping = [
	{
	    "ContactOutcomeID": 1,
	    "OutComeID": 15,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 2,
	    "OutComeID": 5,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 3,
	    "OutComeID": 6,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 4,
	    "OutComeID": 5,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 5,
	    "OutComeID": 6,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 6,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 7,
	    "OutComeID": 5,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 8,
	    "OutComeID": 6,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 9,
	    "OutComeID": 5,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 10,
	    "OutComeID": 6,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 11,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 12,
	    "OutComeID": 17,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 13,
	    "OutComeID": 18,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 14,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 15,
	    "OutComeID": 10,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 16,
	    "OutComeID": 9,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 17,
	    "OutComeID": 12,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 18,
	    "OutComeID": 13,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 19,
	    "OutComeID": 11,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 20,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 21,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 22,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 23,
	    "OutComeID": 17,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 24,
	    "OutComeID": 18,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 25,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 26,
	    "OutComeID": 10,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 27,
	    "OutComeID": 9,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 28,
	    "OutComeID": 12,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 29,
	    "OutComeID": 13,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 30,
	    "OutComeID": 11,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 31,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 32,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 33,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 34,
	    "OutComeID": 17,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 35,
	    "OutComeID": 18,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 36,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 37,
	    "OutComeID": 10,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 38,
	    "OutComeID": 9,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 39,
	    "OutComeID": 12,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 40,
	    "OutComeID": 13,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 41,
	    "OutComeID": 11,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 42,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 43,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 44,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 45,
	    "OutComeID": 17,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 46,
	    "OutComeID": 18,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 47,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 48,
	    "OutComeID": 10,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 49,
	    "OutComeID": 9,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 50,
	    "OutComeID": 12,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 51,
	    "OutComeID": 13,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 52,
	    "OutComeID": 14,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 53,
	    "OutComeID": 11,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 54,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 55,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 56,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 57,
	    "OutComeID": 17,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 58,
	    "OutComeID": 18,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 59,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 60,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 61,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 62,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 63,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 64,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 65,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 66,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 67,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 68,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 69,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 70,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 71,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 72,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 73,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 74,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 75,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 76,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 77,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 78,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 79,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 80,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 81,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 82,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 83,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 84,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 85,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 86,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 87,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 88,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 89,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 90,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 91,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 92,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 93,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 94,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 95,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 96,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 97,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 98,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 99,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 100,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 101,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 102,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 103,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 104,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 105,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 106,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 107,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 108,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 109,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 110,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 111,
	    "OutComeID": 3,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 112,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 113,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 114,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 115,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 116,
	    "OutComeID": 16,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 117,
	    "OutComeID": 7,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 118,
	    "OutComeID": 8,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 119,
	    "OutComeID": 2,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 120,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 121,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 122,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 123,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 124,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 125,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 126,
	    "OutComeID": 1,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 2,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 127,
	    "OutComeID": 21,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 128,
	    "OutComeID": 22,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 129,
	    "OutComeID": 23,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 130,
	    "OutComeID": 24,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 131,
	    "OutComeID": 25,
	    "Outcome": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 132,
	    "OutComeID": 26,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 133,
	    "OutComeID": 27,
	    "Outcome": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 134,
	    "OutComeID": 28,
	    "Outcome": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 135,
	    "OutComeID": 29,
	    "Outcome": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 136,
	    "OutComeID": 11,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 137,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 138,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 139,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 140,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 141,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	},
	{
	    "ContactOutcomeID": 142,
	    "OutComeID": 30,
	    "Outcome": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "OutcomeTypeID": 1,
	    "OutcomeTypes": null
	}
];


var MasterContactOutcomeTypes = [
	{
	    "OutcomeTypeID": 5,
	    "OutcomeTypeName": "Approved",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 6,
	    "OutcomeTypeName": "Denied",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 9,
	    "OutcomeTypeName": "Letter Sent",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 3,
	    "OutcomeTypeName": "Not Applicable",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 8,
	    "OutcomeTypeName": "Pending",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 4,
	    "OutcomeTypeName": "Pending Approval",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 7,
	    "OutcomeTypeName": "Pending Denial",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 1,
	    "OutcomeTypeName": "Successful",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	},
	{
	    "OutcomeTypeID": 2,
	    "OutcomeTypeName": "UnSuccessful",
	    "Status": null,
	    "Code": null,
	    "LastModifiedDate": "2016-06-24T13:09:47.0291672Z"
	}
];


var MasterContactReasons = [
	{
	    "ReasonID": 1,
	    "ReasonDescription": "Additional Information"
	},
	{
	    "ReasonID": 5,
	    "ReasonDescription": "Approval Letter"
	},
	{
	    "ReasonID": 11,
	    "ReasonDescription": "Approval Number"
	},
	{
	    "ReasonID": 12,
	    "ReasonDescription": "CD Decision"
	},
	{
	    "ReasonID": 9,
	    "ReasonDescription": "DENC Letter"
	},
	{
	    "ReasonID": 6,
	    "ReasonDescription": "Denial Letter"
	},
	{
	    "ReasonID": 2,
	    "ReasonDescription": "Discharge Planning"
	},
	{
	    "ReasonID": 13,
	    "ReasonDescription": "Eligibility"
	},
	{
	    "ReasonID": 20,
	    "ReasonDescription": "Facility Changed"
	},
	{
	    "ReasonID": 3,
	    "ReasonDescription": "General Question"
	},
	{
	    "ReasonID": 8,
	    "ReasonDescription": "NOMNC Letter"
	},
	{
	    "ReasonID": 10,
	    "ReasonDescription": "Notification"
	},
	{
	    "ReasonID": 7,
	    "ReasonDescription": "Partial Approval Letter"
	},
	{
	    "ReasonID": 18,
	    "ReasonDescription": "PCP Response"
	},
	{
	    "ReasonID": 23,
	    "ReasonDescription": "Provider Added & Deleted Codes"
	},
	{
	    "ReasonID": 21,
	    "ReasonDescription": "Provider Added Codes"
	},
	{
	    "ReasonID": 19,
	    "ReasonDescription": "Provider Changed"
	},
	{
	    "ReasonID": 22,
	    "ReasonDescription": "Provider Deleted Codes"
	},
	{
	    "ReasonID": 4,
	    "ReasonDescription": "Requested Clinicals"
	},
	{
	    "ReasonID": 14,
	    "ReasonDescription": "User Error"
	},
	{
	    "ReasonID": 17,
	    "ReasonDescription": "Withdrawn By Member Clinical"
	},
	{
	    "ReasonID": 16,
	    "ReasonDescription": "Withdrawn By Member Financial"
	},
	{
	    "ReasonID": 15,
	    "ReasonDescription": "Withdrawn By Provider"
	}
];


var MasterContactReasonsMapping = [
	{
	    "ContactReasonID": 1,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 2,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 3,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 4,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 5,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 6,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 7,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 8,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 9,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 10,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 11,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 12,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 13,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 14,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 15,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 16,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 17,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 18,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 19,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 20,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 21,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 22,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 23,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 24,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 25,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 26,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 27,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 28,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 29,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 30,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 31,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 32,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 33,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 34,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 35,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 36,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 37,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 38,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 39,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 40,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 41,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 42,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 43,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 44,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 45,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 46,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 47,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 48,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 49,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 50,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 51,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 52,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 53,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 54,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 55,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 56,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 57,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 58,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 59,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 60,
	    "ReasonID": 1,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 61,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 62,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 63,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 64,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 65,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 66,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 67,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 68,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 69,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 70,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 71,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 72,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 73,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 74,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 75,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 76,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 77,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 78,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 79,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 80,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 81,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 82,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 83,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 84,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 85,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 86,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 87,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 88,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 89,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 90,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 91,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 92,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 93,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 94,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 95,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 96,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 97,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 98,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 99,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 100,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 101,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 102,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 103,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 104,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 105,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 106,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 107,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 108,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 109,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 110,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 111,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 112,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 113,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 114,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 115,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 116,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 117,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 118,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 119,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 120,
	    "ReasonID": 2,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 121,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 122,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 123,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 124,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 125,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 126,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 127,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 128,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 129,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 130,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 131,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 132,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 133,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 134,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 135,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 136,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 137,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 138,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 139,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 140,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 141,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 142,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 143,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 144,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 145,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 146,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 147,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 148,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 149,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 150,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 151,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 152,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 153,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 154,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 155,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 156,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 157,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 158,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 159,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 160,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 161,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 162,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 163,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 164,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 165,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 166,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 167,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 168,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 169,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 170,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 171,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 172,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 173,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 174,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 175,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 176,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 177,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 178,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 179,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 180,
	    "ReasonID": 3,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 181,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 182,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 183,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 184,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 185,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 186,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 187,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 188,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 189,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 190,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 191,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 192,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 193,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 194,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 195,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 196,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 197,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 198,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 199,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 200,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 201,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 202,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 203,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 204,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 205,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 206,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 207,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 208,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 209,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 210,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 211,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 212,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 213,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 214,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 215,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 216,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 217,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 218,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 219,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 220,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 221,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 222,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 223,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 224,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 225,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 226,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 227,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 228,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 229,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 230,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 231,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 232,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 233,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 234,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 235,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 236,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 237,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 238,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 239,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 240,
	    "ReasonID": 4,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 241,
	    "ReasonID": 5,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 242,
	    "ReasonID": 5,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 243,
	    "ReasonID": 5,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 244,
	    "ReasonID": 5,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 245,
	    "ReasonID": 5,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 246,
	    "ReasonID": 6,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 247,
	    "ReasonID": 6,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 248,
	    "ReasonID": 6,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 249,
	    "ReasonID": 6,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 250,
	    "ReasonID": 6,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 251,
	    "ReasonID": 7,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 252,
	    "ReasonID": 7,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 253,
	    "ReasonID": 7,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 254,
	    "ReasonID": 7,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 255,
	    "ReasonID": 7,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 256,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 257,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 258,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 259,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 260,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 261,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 262,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 263,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 264,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 265,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 266,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 267,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 268,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 269,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 270,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 271,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 272,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 273,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 274,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 275,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 276,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 277,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 278,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 279,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 280,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 281,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 282,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 283,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 284,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 285,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 286,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 287,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 288,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 289,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 290,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 291,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 292,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 293,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 294,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 295,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 296,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 297,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 298,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 299,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 300,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 301,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 302,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 303,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 304,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 305,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 306,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 307,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 308,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 309,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 310,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 311,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 312,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 313,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 314,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 315,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 316,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 317,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 318,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 319,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 320,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 321,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 322,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 323,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 324,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 325,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 326,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 327,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 328,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 329,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 330,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 331,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 332,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 333,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 334,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 335,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 336,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 337,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 338,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 339,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 340,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 341,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 342,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 343,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 344,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 345,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 346,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 347,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 348,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 349,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 350,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 351,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 352,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 353,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 354,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 355,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 356,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 357,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 358,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 359,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 360,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 361,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 362,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 363,
	    "ReasonID": 11,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 364,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 365,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 366,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 367,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 368,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 369,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 370,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 371,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 372,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 373,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 374,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 375,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 376,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 377,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 378,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 379,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 380,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 381,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 382,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 383,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 384,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 385,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 386,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 387,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 388,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 389,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 390,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 391,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 392,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 393,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 394,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 395,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 396,
	    "ReasonID": 12,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 397,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 398,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 399,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 1,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 400,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 401,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 402,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 2,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 403,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 404,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 405,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 3,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 406,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 407,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 408,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 4,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 409,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 410,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 411,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 5,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 412,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 413,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 414,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 415,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 416,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 417,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 418,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 419,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 420,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 421,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 422,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 423,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 424,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 425,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 426,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 427,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 428,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 429,
	    "ReasonID": 13,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 430,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 431,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 432,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 1,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 433,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 434,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 435,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 2,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 436,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 437,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 438,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 3,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 439,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 440,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 441,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 4,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 442,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 443,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 444,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 5,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 445,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 1,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 446,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 2,
	    "ContactDirection": null
	},
	{
	    "ContactReasonID": 447,
	    "ReasonID": 10,
	    "Reason": null,
	    "ContactEntityTypeID": 6,
	    "ContactEntityType": null,
	    "ContactTypeID": 6,
	    "ContactType": null,
	    "ContactDirectionID": 3,
	    "ContactDirection": null
	}
];


var MasterContactTypes = [
	{
	    "ContactTypeID": 2,
	    "ContactTypeName": "Email"
	},
	{
	    "ContactTypeID": 3,
	    "ContactTypeName": "Fax Communication"
	},
	{
	    "ContactTypeID": 5,
	    "ContactTypeName": "Letter"
	},
	{
	    "ContactTypeID": 4,
	    "ContactTypeName": "Other"
	},
	{
	    "ContactTypeID": 6,
	    "ContactTypeName": "Portal"
	},
	{
	    "ContactTypeID": 1,
	    "ContactTypeName": "Telephone Call"
	}
];


var MasterDeniedReasons = [
	{
	    "DenialLOSReasonID": 2,
	    "ReasonDescription": "Lack of Information"
	},
	{
	    "DenialLOSReasonID": 1,
	    "ReasonDescription": "Lack of Medical Necessity"
	},
	{
	    "DenialLOSReasonID": 3,
	    "ReasonDescription": "OON"
	},
	{
	    "DenialLOSReasonID": 4,
	    "ReasonDescription": "Retrospective"
	}
]


var MasterDisciplines = [
	{
	    "DisciplineID": 8,
	    "Name": "HHA",
	    "Code": null,
	    "Description": "Home health agency"
	},
	{
	    "DisciplineID": 7,
	    "Name": "MSW",
	    "Code": null,
	    "Description": "Medical social worker"
	},
	{
	    "DisciplineID": 5,
	    "Name": "OT EVAL",
	    "Code": null,
	    "Description": "Occupational therapy evaluation"
	},
	{
	    "DisciplineID": 6,
	    "Name": "OT VISIT",
	    "Code": null,
	    "Description": "Occupational therapy visit"
	},
	{
	    "DisciplineID": 3,
	    "Name": "PT EVAL",
	    "Code": null,
	    "Description": "Physical therapy evaluation"
	},
	{
	    "DisciplineID": 4,
	    "Name": "PT VISIT",
	    "Code": null,
	    "Description": "Physical therapy visit"
	},
	{
	    "DisciplineID": 1,
	    "Name": "SN EVAL",
	    "Code": null,
	    "Description": "Skilled nurse evaluation"
	},
	{
	    "DisciplineID": 2,
	    "Name": "SN VISIT",
	    "Code": null,
	    "Description": "Skilled nurse visit"
	},
	{
	    "DisciplineID": 9,
	    "Name": "ST EVAL",
	    "Code": null,
	    "Description": "Skilled theraphy evaluation"
	},
	{
	    "DisciplineID": 10,
	    "Name": "ST VISIT",
	    "Code": null,
	    "Description": "Skilled theraphy visit"
	}
];



var MasterDocumentNames = [
	{
	    "DocumentNameID": 1,
	    "DocumentNameValue": "Hospital Records"
	},
	{
	    "DocumentNameID": 2,
	    "DocumentNameValue": "Specialist Records"
	},
	{
	    "DocumentNameID": 3,
	    "DocumentNameValue": "PCP Records"
	},
	{
	    "DocumentNameID": 4,
	    "DocumentNameValue": "Office Facesheet"
	},
	{
	    "DocumentNameID": 5,
	    "DocumentNameValue": "Plan Authorization"
	}
];



var MasterDocumentTypes = [
	{
	    "DocumentID": 1,
	    "DocumentTypeValue": "Clinical"
	},
	{
	    "DocumentID": 2,
	    "DocumentTypeValue": "Progress Notes"
	},
	{
	    "DocumentID": 3,
	    "DocumentTypeValue": "Fax"
	},
	{
	    "DocumentID": 4,
	    "DocumentTypeValue": "LOA"
	},
	{
	    "DocumentID": 5,
	    "DocumentTypeValue": "Plan Authorization"
	}
];



var MasterDRGCodes = [
	{
	    "DRGCodeID": 1,
	    "DRGCode": 1,
	    "Description": "HEART TRANSPLANT OR IMPLANT OF HEART ASSIST SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 2,
	    "DRGCode": 2,
	    "Description": "HEART TRANSPLANT OR IMPLANT OF HEART ASSIST SYSTEM W/O MCC"
	},
	{
	    "DRGCodeID": 3,
	    "DRGCode": 3,
	    "Description": "ECMO OR TRACH W MV 96+ HRS OR PDX EXC FACE, MOUTH & NECK W MAJ O.R."
	},
	{
	    "DRGCodeID": 4,
	    "DRGCode": 4,
	    "Description": "TRACH W MV 96+ HRS OR PDX EXC FACE, MOUTH & NECK W/O MAJ O.R."
	},
	{
	    "DRGCodeID": 5,
	    "DRGCode": 5,
	    "Description": "LIVER TRANSPLANT W MCC OR INTESTINAL TRANSPLANT"
	},
	{
	    "DRGCodeID": 6,
	    "DRGCode": 6,
	    "Description": "LIVER TRANSPLANT W/O MCC"
	},
	{
	    "DRGCodeID": 7,
	    "DRGCode": 7,
	    "Description": "LUNG TRANSPLANT"
	},
	{
	    "DRGCodeID": 8,
	    "DRGCode": 8,
	    "Description": "SIMULTANEOUS PANCREAS/KIDNEY TRANSPLANT"
	},
	{
	    "DRGCodeID": 9,
	    "DRGCode": 10,
	    "Description": "PANCREAS TRANSPLANT"
	},
	{
	    "DRGCodeID": 10,
	    "DRGCode": 11,
	    "Description": "TRACHEOSTOMY FOR FACE,MOUTH & NECK DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 11,
	    "DRGCode": 12,
	    "Description": "TRACHEOSTOMY FOR FACE,MOUTH & NECK DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 12,
	    "DRGCode": 13,
	    "Description": "TRACHEOSTOMY FOR FACE,MOUTH & NECK DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 13,
	    "DRGCode": 14,
	    "Description": "ALLOGENEIC BONE MARROW TRANSPLANT"
	},
	{
	    "DRGCodeID": 14,
	    "DRGCode": 16,
	    "Description": "AUTOLOGOUS BONE MARROW TRANSPLANT W CC/MCC"
	},
	{
	    "DRGCodeID": 15,
	    "DRGCode": 17,
	    "Description": "AUTOLOGOUS BONE MARROW TRANSPLANT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 16,
	    "DRGCode": 20,
	    "Description": "INTRACRANIAL VASCULAR PROCEDURES W PDX HEMORRHAGE W MCC"
	},
	{
	    "DRGCodeID": 17,
	    "DRGCode": 21,
	    "Description": "INTRACRANIAL VASCULAR PROCEDURES W PDX HEMORRHAGE W CC"
	},
	{
	    "DRGCodeID": 18,
	    "DRGCode": 22,
	    "Description": "INTRACRANIAL VASCULAR PROCEDURES W PDX HEMORRHAGE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 19,
	    "DRGCode": 23,
	    "Description": "CRANIO W MAJOR DEV IMPL/ACUTE COMPLEX CNS PDX W MCC OR CHEMO IMPLANT"
	},
	{
	    "DRGCodeID": 20,
	    "DRGCode": 24,
	    "Description": "CRANIO W MAJOR DEV IMPL/ACUTE COMPLEX CNS PDX W/O MCC"
	},
	{
	    "DRGCodeID": 21,
	    "DRGCode": 25,
	    "Description": "CRANIOTOMY & ENDOVASCULAR INTRACRANIAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 22,
	    "DRGCode": 26,
	    "Description": "CRANIOTOMY & ENDOVASCULAR INTRACRANIAL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 23,
	    "DRGCode": 27,
	    "Description": "CRANIOTOMY & ENDOVASCULAR INTRACRANIAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 24,
	    "DRGCode": 28,
	    "Description": "SPINAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 25,
	    "DRGCode": 29,
	    "Description": "SPINAL PROCEDURES W CC OR SPINAL NEUROSTIMULATORS"
	},
	{
	    "DRGCodeID": 26,
	    "DRGCode": 30,
	    "Description": "SPINAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 27,
	    "DRGCode": 31,
	    "Description": "VENTRICULAR SHUNT PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 28,
	    "DRGCode": 32,
	    "Description": "VENTRICULAR SHUNT PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 29,
	    "DRGCode": 33,
	    "Description": "VENTRICULAR SHUNT PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 30,
	    "DRGCode": 34,
	    "Description": "CAROTID ARTERY STENT PROCEDURE W MCC"
	},
	{
	    "DRGCodeID": 31,
	    "DRGCode": 35,
	    "Description": "CAROTID ARTERY STENT PROCEDURE W CC"
	},
	{
	    "DRGCodeID": 32,
	    "DRGCode": 36,
	    "Description": "CAROTID ARTERY STENT PROCEDURE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 33,
	    "DRGCode": 37,
	    "Description": "EXTRACRANIAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 34,
	    "DRGCode": 38,
	    "Description": "EXTRACRANIAL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 35,
	    "DRGCode": 39,
	    "Description": "EXTRACRANIAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 36,
	    "DRGCode": 40,
	    "Description": "PERIPH/CRANIAL NERVE & OTHER NERV SYST PROC W MCC"
	},
	{
	    "DRGCodeID": 37,
	    "DRGCode": 41,
	    "Description": "PERIPH/CRANIAL NERVE & OTHER NERV SYST PROC W CC OR PERIPH NEUROSTIM"
	},
	{
	    "DRGCodeID": 38,
	    "DRGCode": 42,
	    "Description": "PERIPH/CRANIAL NERVE & OTHER NERV SYST PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 39,
	    "DRGCode": 52,
	    "Description": "SPINAL DISORDERS & INJURIES W CC/MCC"
	},
	{
	    "DRGCodeID": 40,
	    "DRGCode": 53,
	    "Description": "SPINAL DISORDERS & INJURIES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 41,
	    "DRGCode": 54,
	    "Description": "NERVOUS SYSTEM NEOPLASMS W MCC"
	},
	{
	    "DRGCodeID": 42,
	    "DRGCode": 55,
	    "Description": "NERVOUS SYSTEM NEOPLASMS W/O MCC"
	},
	{
	    "DRGCodeID": 43,
	    "DRGCode": 56,
	    "Description": "DEGENERATIVE NERVOUS SYSTEM DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 44,
	    "DRGCode": 57,
	    "Description": "DEGENERATIVE NERVOUS SYSTEM DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 45,
	    "DRGCode": 58,
	    "Description": "MULTIPLE SCLEROSIS & CEREBELLAR ATAXIA W MCC"
	},
	{
	    "DRGCodeID": 46,
	    "DRGCode": 59,
	    "Description": "MULTIPLE SCLEROSIS & CEREBELLAR ATAXIA W CC"
	},
	{
	    "DRGCodeID": 47,
	    "DRGCode": 60,
	    "Description": "MULTIPLE SCLEROSIS & CEREBELLAR ATAXIA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 48,
	    "DRGCode": 61,
	    "Description": "ACUTE ISCHEMIC STROKE W USE OF THROMBOLYTIC AGENT W MCC"
	},
	{
	    "DRGCodeID": 49,
	    "DRGCode": 62,
	    "Description": "ACUTE ISCHEMIC STROKE W USE OF THROMBOLYTIC AGENT W CC"
	},
	{
	    "DRGCodeID": 50,
	    "DRGCode": 63,
	    "Description": "ACUTE ISCHEMIC STROKE W USE OF THROMBOLYTIC AGENT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 51,
	    "DRGCode": 64,
	    "Description": "INTRACRANIAL HEMORRHAGE OR CEREBRAL INFARCTION W MCC"
	},
	{
	    "DRGCodeID": 52,
	    "DRGCode": 65,
	    "Description": "INTRACRANIAL HEMORRHAGE OR CEREBRAL INFARCTION W CC OR TPA IN 24 HRS"
	},
	{
	    "DRGCodeID": 53,
	    "DRGCode": 66,
	    "Description": "INTRACRANIAL HEMORRHAGE OR CEREBRAL INFARCTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 54,
	    "DRGCode": 67,
	    "Description": "NONSPECIFIC CVA & PRECEREBRAL OCCLUSION W/O INFARCT W MCC"
	},
	{
	    "DRGCodeID": 55,
	    "DRGCode": 68,
	    "Description": "NONSPECIFIC CVA & PRECEREBRAL OCCLUSION W/O INFARCT W/O MCC"
	},
	{
	    "DRGCodeID": 56,
	    "DRGCode": 69,
	    "Description": "TRANSIENT ISCHEMIA"
	},
	{
	    "DRGCodeID": 57,
	    "DRGCode": 70,
	    "Description": "NONSPECIFIC CEREBROVASCULAR DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 58,
	    "DRGCode": 71,
	    "Description": "NONSPECIFIC CEREBROVASCULAR DISORDERS W CC"
	},
	{
	    "DRGCodeID": 59,
	    "DRGCode": 72,
	    "Description": "NONSPECIFIC CEREBROVASCULAR DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 60,
	    "DRGCode": 73,
	    "Description": "CRANIAL & PERIPHERAL NERVE DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 61,
	    "DRGCode": 74,
	    "Description": "CRANIAL & PERIPHERAL NERVE DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 62,
	    "DRGCode": 75,
	    "Description": "VIRAL MENINGITIS W CC/MCC"
	},
	{
	    "DRGCodeID": 63,
	    "DRGCode": 76,
	    "Description": "VIRAL MENINGITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 64,
	    "DRGCode": 77,
	    "Description": "HYPERTENSIVE ENCEPHALOPATHY W MCC"
	},
	{
	    "DRGCodeID": 65,
	    "DRGCode": 78,
	    "Description": "HYPERTENSIVE ENCEPHALOPATHY W CC"
	},
	{
	    "DRGCodeID": 66,
	    "DRGCode": 79,
	    "Description": "HYPERTENSIVE ENCEPHALOPATHY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 67,
	    "DRGCode": 80,
	    "Description": "NONTRAUMATIC STUPOR & COMA W MCC"
	},
	{
	    "DRGCodeID": 68,
	    "DRGCode": 81,
	    "Description": "NONTRAUMATIC STUPOR & COMA W/O MCC"
	},
	{
	    "DRGCodeID": 69,
	    "DRGCode": 82,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA >1 HR W MCC"
	},
	{
	    "DRGCodeID": 70,
	    "DRGCode": 83,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA >1 HR W CC"
	},
	{
	    "DRGCodeID": 71,
	    "DRGCode": 84,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA >1 HR W/O CC/MCC"
	},
	{
	    "DRGCodeID": 72,
	    "DRGCode": 85,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA <1 HR W MCC"
	},
	{
	    "DRGCodeID": 73,
	    "DRGCode": 86,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA <1 HR W CC"
	},
	{
	    "DRGCodeID": 74,
	    "DRGCode": 87,
	    "Description": "TRAUMATIC STUPOR & COMA, COMA <1 HR W/O CC/MCC"
	},
	{
	    "DRGCodeID": 75,
	    "DRGCode": 88,
	    "Description": "CONCUSSION W MCC"
	},
	{
	    "DRGCodeID": 76,
	    "DRGCode": 89,
	    "Description": "CONCUSSION W CC"
	},
	{
	    "DRGCodeID": 77,
	    "DRGCode": 90,
	    "Description": "CONCUSSION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 78,
	    "DRGCode": 91,
	    "Description": "OTHER DISORDERS OF NERVOUS SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 79,
	    "DRGCode": 92,
	    "Description": "OTHER DISORDERS OF NERVOUS SYSTEM W CC"
	},
	{
	    "DRGCodeID": 80,
	    "DRGCode": 93,
	    "Description": "OTHER DISORDERS OF NERVOUS SYSTEM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 81,
	    "DRGCode": 94,
	    "Description": "BACTERIAL & TUBERCULOUS INFECTIONS OF NERVOUS SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 82,
	    "DRGCode": 95,
	    "Description": "BACTERIAL & TUBERCULOUS INFECTIONS OF NERVOUS SYSTEM W CC"
	},
	{
	    "DRGCodeID": 83,
	    "DRGCode": 96,
	    "Description": "BACTERIAL & TUBERCULOUS INFECTIONS OF NERVOUS SYSTEM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 84,
	    "DRGCode": 97,
	    "Description": "NON-BACTERIAL INFECT OF NERVOUS SYS EXC VIRAL MENINGITIS W MCC"
	},
	{
	    "DRGCodeID": 85,
	    "DRGCode": 98,
	    "Description": "NON-BACTERIAL INFECT OF NERVOUS SYS EXC VIRAL MENINGITIS W CC"
	},
	{
	    "DRGCodeID": 86,
	    "DRGCode": 99,
	    "Description": "NON-BACTERIAL INFECT OF NERVOUS SYS EXC VIRAL MENINGITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 87,
	    "DRGCode": 100,
	    "Description": " SEIZURES W MCC"
	},
	{
	    "DRGCodeID": 88,
	    "DRGCode": 101,
	    "Description": " SEIZURES W/O MCC"
	},
	{
	    "DRGCodeID": 89,
	    "DRGCode": 102,
	    "Description": " HEADACHES W MCC"
	},
	{
	    "DRGCodeID": 90,
	    "DRGCode": 103,
	    "Description": " HEADACHES W/O MCC"
	},
	{
	    "DRGCodeID": 91,
	    "DRGCode": 113,
	    "Description": " ORBITAL PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 92,
	    "DRGCode": 114,
	    "Description": " ORBITAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 93,
	    "DRGCode": 115,
	    "Description": " EXTRAOCULAR PROCEDURES EXCEPT ORBIT"
	},
	{
	    "DRGCodeID": 94,
	    "DRGCode": 116,
	    "Description": " INTRAOCULAR PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 95,
	    "DRGCode": 117,
	    "Description": " INTRAOCULAR PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 96,
	    "DRGCode": 121,
	    "Description": " ACUTE MAJOR EYE INFECTIONS W CC/MCC"
	},
	{
	    "DRGCodeID": 97,
	    "DRGCode": 122,
	    "Description": " ACUTE MAJOR EYE INFECTIONS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 98,
	    "DRGCode": 123,
	    "Description": " NEUROLOGICAL EYE DISORDERS"
	},
	{
	    "DRGCodeID": 99,
	    "DRGCode": 124,
	    "Description": " OTHER DISORDERS OF THE EYE W MCC"
	},
	{
	    "DRGCodeID": 100,
	    "DRGCode": 125,
	    "Description": " OTHER DISORDERS OF THE EYE W/O MCC"
	},
	{
	    "DRGCodeID": 101,
	    "DRGCode": 129,
	    "Description": " MAJOR HEAD & NECK PROCEDURES W CC/MCC OR MAJOR DEVICE"
	},
	{
	    "DRGCodeID": 102,
	    "DRGCode": 130,
	    "Description": " MAJOR HEAD & NECK PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 103,
	    "DRGCode": 131,
	    "Description": " CRANIAL/FACIAL PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 104,
	    "DRGCode": 132,
	    "Description": " CRANIAL/FACIAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 105,
	    "DRGCode": 133,
	    "Description": " OTHER EAR, NOSE, MOUTH & THROAT O.R. PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 106,
	    "DRGCode": 134,
	    "Description": " OTHER EAR, NOSE, MOUTH & THROAT O.R. PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 107,
	    "DRGCode": 135,
	    "Description": " SINUS & MASTOID PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 108,
	    "DRGCode": 136,
	    "Description": " SINUS & MASTOID PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 109,
	    "DRGCode": 137,
	    "Description": " MOUTH PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 110,
	    "DRGCode": 138,
	    "Description": " MOUTH PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 111,
	    "DRGCode": 139,
	    "Description": " SALIVARY GLAND PROCEDURES"
	},
	{
	    "DRGCodeID": 112,
	    "DRGCode": 146,
	    "Description": " EAR, NOSE, MOUTH & THROAT MALIGNANCY W MCC"
	},
	{
	    "DRGCodeID": 113,
	    "DRGCode": 147,
	    "Description": " EAR, NOSE, MOUTH & THROAT MALIGNANCY W CC"
	},
	{
	    "DRGCodeID": 114,
	    "DRGCode": 148,
	    "Description": " EAR, NOSE, MOUTH & THROAT MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 115,
	    "DRGCode": 149,
	    "Description": " DYSEQUILIBRIUM"
	},
	{
	    "DRGCodeID": 116,
	    "DRGCode": 150,
	    "Description": " EPISTAXIS W MCC"
	},
	{
	    "DRGCodeID": 117,
	    "DRGCode": 151,
	    "Description": " EPISTAXIS W/O MCC"
	},
	{
	    "DRGCodeID": 118,
	    "DRGCode": 152,
	    "Description": " OTITIS MEDIA & URI W MCC"
	},
	{
	    "DRGCodeID": 119,
	    "DRGCode": 153,
	    "Description": " OTITIS MEDIA & URI W/O MCC"
	},
	{
	    "DRGCodeID": 120,
	    "DRGCode": 154,
	    "Description": " OTHER EAR, NOSE, MOUTH & THROAT DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 121,
	    "DRGCode": 155,
	    "Description": " OTHER EAR, NOSE, MOUTH & THROAT DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 122,
	    "DRGCode": 156,
	    "Description": " OTHER EAR, NOSE, MOUTH & THROAT DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 123,
	    "DRGCode": 157,
	    "Description": " DENTAL & ORAL DISEASES W MCC"
	},
	{
	    "DRGCodeID": 124,
	    "DRGCode": 158,
	    "Description": " DENTAL & ORAL DISEASES W CC"
	},
	{
	    "DRGCodeID": 125,
	    "DRGCode": 159,
	    "Description": " DENTAL & ORAL DISEASES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 126,
	    "DRGCode": 163,
	    "Description": " MAJOR CHEST PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 127,
	    "DRGCode": 164,
	    "Description": " MAJOR CHEST PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 128,
	    "DRGCode": 165,
	    "Description": " MAJOR CHEST PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 129,
	    "DRGCode": 166,
	    "Description": " OTHER RESP SYSTEM O.R. PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 130,
	    "DRGCode": 167,
	    "Description": " OTHER RESP SYSTEM O.R. PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 131,
	    "DRGCode": 168,
	    "Description": " OTHER RESP SYSTEM O.R. PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 132,
	    "DRGCode": 175,
	    "Description": " PULMONARY EMBOLISM W MCC"
	},
	{
	    "DRGCodeID": 133,
	    "DRGCode": 176,
	    "Description": " PULMONARY EMBOLISM W/O MCC"
	},
	{
	    "DRGCodeID": 134,
	    "DRGCode": 177,
	    "Description": " RESPIRATORY INFECTIONS & INFLAMMATIONS W MCC"
	},
	{
	    "DRGCodeID": 135,
	    "DRGCode": 178,
	    "Description": " RESPIRATORY INFECTIONS & INFLAMMATIONS W CC"
	},
	{
	    "DRGCodeID": 136,
	    "DRGCode": 179,
	    "Description": " RESPIRATORY INFECTIONS & INFLAMMATIONS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 137,
	    "DRGCode": 180,
	    "Description": " RESPIRATORY NEOPLASMS W MCC"
	},
	{
	    "DRGCodeID": 138,
	    "DRGCode": 181,
	    "Description": " RESPIRATORY NEOPLASMS W CC"
	},
	{
	    "DRGCodeID": 139,
	    "DRGCode": 182,
	    "Description": " RESPIRATORY NEOPLASMS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 140,
	    "DRGCode": 183,
	    "Description": " MAJOR CHEST TRAUMA W MCC"
	},
	{
	    "DRGCodeID": 141,
	    "DRGCode": 184,
	    "Description": " MAJOR CHEST TRAUMA W CC"
	},
	{
	    "DRGCodeID": 142,
	    "DRGCode": 185,
	    "Description": " MAJOR CHEST TRAUMA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 143,
	    "DRGCode": 186,
	    "Description": " PLEURAL EFFUSION W MCC"
	},
	{
	    "DRGCodeID": 144,
	    "DRGCode": 187,
	    "Description": " PLEURAL EFFUSION W CC"
	},
	{
	    "DRGCodeID": 145,
	    "DRGCode": 188,
	    "Description": " PLEURAL EFFUSION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 146,
	    "DRGCode": 189,
	    "Description": " PULMONARY EDEMA & RESPIRATORY FAILURE"
	},
	{
	    "DRGCodeID": 147,
	    "DRGCode": 190,
	    "Description": " CHRONIC OBSTRUCTIVE PULMONARY DISEASE W MCC"
	},
	{
	    "DRGCodeID": 148,
	    "DRGCode": 191,
	    "Description": " CHRONIC OBSTRUCTIVE PULMONARY DISEASE W CC"
	},
	{
	    "DRGCodeID": 149,
	    "DRGCode": 192,
	    "Description": " CHRONIC OBSTRUCTIVE PULMONARY DISEASE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 150,
	    "DRGCode": 193,
	    "Description": " SIMPLE PNEUMONIA & PLEURISY W MCC"
	},
	{
	    "DRGCodeID": 151,
	    "DRGCode": 194,
	    "Description": " SIMPLE PNEUMONIA & PLEURISY W CC"
	},
	{
	    "DRGCodeID": 152,
	    "DRGCode": 195,
	    "Description": " SIMPLE PNEUMONIA & PLEURISY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 153,
	    "DRGCode": 196,
	    "Description": " INTERSTITIAL LUNG DISEASE W MCC"
	},
	{
	    "DRGCodeID": 154,
	    "DRGCode": 197,
	    "Description": " INTERSTITIAL LUNG DISEASE W CC"
	},
	{
	    "DRGCodeID": 155,
	    "DRGCode": 198,
	    "Description": " INTERSTITIAL LUNG DISEASE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 156,
	    "DRGCode": 199,
	    "Description": " PNEUMOTHORAX W MCC"
	},
	{
	    "DRGCodeID": 157,
	    "DRGCode": 200,
	    "Description": " PNEUMOTHORAX W CC"
	},
	{
	    "DRGCodeID": 158,
	    "DRGCode": 201,
	    "Description": " PNEUMOTHORAX W/O CC/MCC"
	},
	{
	    "DRGCodeID": 159,
	    "DRGCode": 202,
	    "Description": " BRONCHITIS & ASTHMA W CC/MCC"
	},
	{
	    "DRGCodeID": 160,
	    "DRGCode": 203,
	    "Description": " BRONCHITIS & ASTHMA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 161,
	    "DRGCode": 204,
	    "Description": " RESPIRATORY SIGNS & SYMPTOMS"
	},
	{
	    "DRGCodeID": 162,
	    "DRGCode": 205,
	    "Description": " OTHER RESPIRATORY SYSTEM DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 163,
	    "DRGCode": 206,
	    "Description": " OTHER RESPIRATORY SYSTEM DIAGNOSES W/O MCC"
	},
	{
	    "DRGCodeID": 164,
	    "DRGCode": 207,
	    "Description": " RESPIRATORY SYSTEM DIAGNOSIS W VENTILATOR SUPPORT 96+ HOURS"
	},
	{
	    "DRGCodeID": 165,
	    "DRGCode": 208,
	    "Description": " RESPIRATORY SYSTEM DIAGNOSIS W VENTILATOR SUPPORT <96 HOURS"
	},
	{
	    "DRGCodeID": 166,
	    "DRGCode": 215,
	    "Description": " OTHER HEART ASSIST SYSTEM IMPLANT"
	},
	{
	    "DRGCodeID": 167,
	    "DRGCode": 216,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W CARD CATH W MCC"
	},
	{
	    "DRGCodeID": 168,
	    "DRGCode": 217,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W CARD CATH W CC"
	},
	{
	    "DRGCodeID": 169,
	    "DRGCode": 218,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W CARD CATH W/O CC/MCC"
	},
	{
	    "DRGCodeID": 170,
	    "DRGCode": 219,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W/O CARD CATH W MCC"
	},
	{
	    "DRGCodeID": 171,
	    "DRGCode": 220,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W/O CARD CATH W CC"
	},
	{
	    "DRGCodeID": 172,
	    "DRGCode": 221,
	    "Description": " CARDIAC VALVE & OTH MAJ CARDIOTHORACIC PROC W/O CARD CATH W/O CC/MCC"
	},
	{
	    "DRGCodeID": 173,
	    "DRGCode": 222,
	    "Description": " CARDIAC DEFIB IMPLANT W CARDIAC CATH W AMI/HF/SHOCK W MCC"
	},
	{
	    "DRGCodeID": 174,
	    "DRGCode": 223,
	    "Description": " CARDIAC DEFIB IMPLANT W CARDIAC CATH W AMI/HF/SHOCK W/O MCC"
	},
	{
	    "DRGCodeID": 175,
	    "DRGCode": 224,
	    "Description": " CARDIAC DEFIB IMPLANT W CARDIAC CATH W/O AMI/HF/SHOCK W MCC"
	},
	{
	    "DRGCodeID": 176,
	    "DRGCode": 225,
	    "Description": " CARDIAC DEFIB IMPLANT W CARDIAC CATH W/O AMI/HF/SHOCK W/O MCC"
	},
	{
	    "DRGCodeID": 177,
	    "DRGCode": 226,
	    "Description": " CARDIAC DEFIBRILLATOR IMPLANT W/O CARDIAC CATH W MCC"
	},
	{
	    "DRGCodeID": 178,
	    "DRGCode": 227,
	    "Description": " CARDIAC DEFIBRILLATOR IMPLANT W/O CARDIAC CATH W/O MCC"
	},
	{
	    "DRGCodeID": 179,
	    "DRGCode": 228,
	    "Description": " OTHER CARDIOTHORACIC PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 180,
	    "DRGCode": 229,
	    "Description": " OTHER CARDIOTHORACIC PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 181,
	    "DRGCode": 230,
	    "Description": " OTHER CARDIOTHORACIC PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 182,
	    "DRGCode": 231,
	    "Description": " CORONARY BYPASS W PTCA W MCC"
	},
	{
	    "DRGCodeID": 183,
	    "DRGCode": 232,
	    "Description": " CORONARY BYPASS W PTCA W/O MCC"
	},
	{
	    "DRGCodeID": 184,
	    "DRGCode": 233,
	    "Description": " CORONARY BYPASS W CARDIAC CATH W MCC"
	},
	{
	    "DRGCodeID": 185,
	    "DRGCode": 234,
	    "Description": " CORONARY BYPASS W CARDIAC CATH W/O MCC"
	},
	{
	    "DRGCodeID": 186,
	    "DRGCode": 235,
	    "Description": " CORONARY BYPASS W/O CARDIAC CATH W MCC"
	},
	{
	    "DRGCodeID": 187,
	    "DRGCode": 236,
	    "Description": " CORONARY BYPASS W/O CARDIAC CATH W/O MCC"
	},
	{
	    "DRGCodeID": 188,
	    "DRGCode": 237,
	    "Description": " MAJOR CARDIOVASC PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 189,
	    "DRGCode": 238,
	    "Description": " MAJOR CARDIOVASC PROCEDURES W/O MCC"
	},
	{
	    "DRGCodeID": 190,
	    "DRGCode": 239,
	    "Description": " AMPUTATION FOR CIRC SYS DISORDERS EXC UPPER LIMB & TOE W MCC"
	},
	{
	    "DRGCodeID": 191,
	    "DRGCode": 240,
	    "Description": " AMPUTATION FOR CIRC SYS DISORDERS EXC UPPER LIMB & TOE W CC"
	},
	{
	    "DRGCodeID": 192,
	    "DRGCode": 241,
	    "Description": " AMPUTATION FOR CIRC SYS DISORDERS EXC UPPER LIMB & TOE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 193,
	    "DRGCode": 242,
	    "Description": " PERMANENT CARDIAC PACEMAKER IMPLANT W MCC"
	},
	{
	    "DRGCodeID": 194,
	    "DRGCode": 243,
	    "Description": " PERMANENT CARDIAC PACEMAKER IMPLANT W CC"
	},
	{
	    "DRGCodeID": 195,
	    "DRGCode": 244,
	    "Description": " PERMANENT CARDIAC PACEMAKER IMPLANT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 196,
	    "DRGCode": 245,
	    "Description": " AICD GENERATOR PROCEDURES"
	},
	{
	    "DRGCodeID": 197,
	    "DRGCode": 246,
	    "Description": " PERC CARDIOVASC PROC W DRUG-ELUTING STENT W MCC OR 4+ VESSELS/STENTS"
	},
	{
	    "DRGCodeID": 198,
	    "DRGCode": 247,
	    "Description": " PERC CARDIOVASC PROC W DRUG-ELUTING STENT W/O MCC"
	},
	{
	    "DRGCodeID": 199,
	    "DRGCode": 248,
	    "Description": " PERC CARDIOVASC PROC W NON-DRUG-ELUTING STENT W MCC OR 4+ VES/STENTS"
	},
	{
	    "DRGCodeID": 200,
	    "DRGCode": 249,
	    "Description": " PERC CARDIOVASC PROC W NON-DRUG-ELUTING STENT W/O MCC"
	},
	{
	    "DRGCodeID": 201,
	    "DRGCode": 250,
	    "Description": " PERC CARDIOVASC PROC W/O CORONARY ARTERY STENT W MCC"
	},
	{
	    "DRGCodeID": 202,
	    "DRGCode": 251,
	    "Description": " PERC CARDIOVASC PROC W/O CORONARY ARTERY STENT W/O MCC"
	},
	{
	    "DRGCodeID": 203,
	    "DRGCode": 252,
	    "Description": " OTHER VASCULAR PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 204,
	    "DRGCode": 253,
	    "Description": " OTHER VASCULAR PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 205,
	    "DRGCode": 254,
	    "Description": " OTHER VASCULAR PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 206,
	    "DRGCode": 255,
	    "Description": " UPPER LIMB & TOE AMPUTATION FOR CIRC SYSTEM DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 207,
	    "DRGCode": 256,
	    "Description": " UPPER LIMB & TOE AMPUTATION FOR CIRC SYSTEM DISORDERS W CC"
	},
	{
	    "DRGCodeID": 208,
	    "DRGCode": 257,
	    "Description": " UPPER LIMB & TOE AMPUTATION FOR CIRC SYSTEM DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 209,
	    "DRGCode": 258,
	    "Description": " CARDIAC PACEMAKER DEVICE REPLACEMENT W MCC"
	},
	{
	    "DRGCodeID": 210,
	    "DRGCode": 259,
	    "Description": " CARDIAC PACEMAKER DEVICE REPLACEMENT W/O MCC"
	},
	{
	    "DRGCodeID": 211,
	    "DRGCode": 260,
	    "Description": " CARDIAC PACEMAKER REVISION EXCEPT DEVICE REPLACEMENT W MCC"
	},
	{
	    "DRGCodeID": 212,
	    "DRGCode": 261,
	    "Description": " CARDIAC PACEMAKER REVISION EXCEPT DEVICE REPLACEMENT W CC"
	},
	{
	    "DRGCodeID": 213,
	    "DRGCode": 262,
	    "Description": " CARDIAC PACEMAKER REVISION EXCEPT DEVICE REPLACEMENT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 214,
	    "DRGCode": 263,
	    "Description": " VEIN LIGATION & STRIPPING"
	},
	{
	    "DRGCodeID": 215,
	    "DRGCode": 264,
	    "Description": " OTHER CIRCULATORY SYSTEM O.R. PROCEDURES"
	},
	{
	    "DRGCodeID": 216,
	    "DRGCode": 265,
	    "Description": " AICD LEAD PROCEDURES"
	},
	{
	    "DRGCodeID": 217,
	    "DRGCode": 280,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, DISCHARGED ALIVE W MCC"
	},
	{
	    "DRGCodeID": 218,
	    "DRGCode": 281,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, DISCHARGED ALIVE W CC"
	},
	{
	    "DRGCodeID": 219,
	    "DRGCode": 282,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, DISCHARGED ALIVE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 220,
	    "DRGCode": 283,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, EXPIRED W MCC"
	},
	{
	    "DRGCodeID": 221,
	    "DRGCode": 284,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, EXPIRED W CC"
	},
	{
	    "DRGCodeID": 222,
	    "DRGCode": 285,
	    "Description": " ACUTE MYOCARDIAL INFARCTION, EXPIRED W/O CC/MCC"
	},
	{
	    "DRGCodeID": 223,
	    "DRGCode": 286,
	    "Description": " CIRCULATORY DISORDERS EXCEPT AMI, W CARD CATH W MCC"
	},
	{
	    "DRGCodeID": 224,
	    "DRGCode": 287,
	    "Description": " CIRCULATORY DISORDERS EXCEPT AMI, W CARD CATH W/O MCC"
	},
	{
	    "DRGCodeID": 225,
	    "DRGCode": 288,
	    "Description": " ACUTE & SUBACUTE ENDOCARDITIS W MCC"
	},
	{
	    "DRGCodeID": 226,
	    "DRGCode": 289,
	    "Description": " ACUTE & SUBACUTE ENDOCARDITIS W CC"
	},
	{
	    "DRGCodeID": 227,
	    "DRGCode": 290,
	    "Description": " ACUTE & SUBACUTE ENDOCARDITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 228,
	    "DRGCode": 291,
	    "Description": " HEART FAILURE & SHOCK W MCC"
	},
	{
	    "DRGCodeID": 229,
	    "DRGCode": 292,
	    "Description": " HEART FAILURE & SHOCK W CC"
	},
	{
	    "DRGCodeID": 230,
	    "DRGCode": 293,
	    "Description": " HEART FAILURE & SHOCK W/O CC/MCC"
	},
	{
	    "DRGCodeID": 231,
	    "DRGCode": 294,
	    "Description": " DEEP VEIN THROMBOPHLEBITIS W CC/MCC"
	},
	{
	    "DRGCodeID": 232,
	    "DRGCode": 295,
	    "Description": " DEEP VEIN THROMBOPHLEBITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 233,
	    "DRGCode": 296,
	    "Description": " CARDIAC ARREST, UNEXPLAINED W MCC"
	},
	{
	    "DRGCodeID": 234,
	    "DRGCode": 297,
	    "Description": " CARDIAC ARREST, UNEXPLAINED W CC"
	},
	{
	    "DRGCodeID": 235,
	    "DRGCode": 298,
	    "Description": " CARDIAC ARREST, UNEXPLAINED W/O CC/MCC"
	},
	{
	    "DRGCodeID": 236,
	    "DRGCode": 299,
	    "Description": " PERIPHERAL VASCULAR DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 237,
	    "DRGCode": 300,
	    "Description": " PERIPHERAL VASCULAR DISORDERS W CC"
	},
	{
	    "DRGCodeID": 238,
	    "DRGCode": 301,
	    "Description": " PERIPHERAL VASCULAR DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 239,
	    "DRGCode": 302,
	    "Description": " ATHEROSCLEROSIS W MCC"
	},
	{
	    "DRGCodeID": 240,
	    "DRGCode": 303,
	    "Description": " ATHEROSCLEROSIS W/O MCC"
	},
	{
	    "DRGCodeID": 241,
	    "DRGCode": 304,
	    "Description": " HYPERTENSION W MCC"
	},
	{
	    "DRGCodeID": 242,
	    "DRGCode": 305,
	    "Description": " HYPERTENSION W/O MCC"
	},
	{
	    "DRGCodeID": 243,
	    "DRGCode": 306,
	    "Description": " CARDIAC CONGENITAL & VALVULAR DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 244,
	    "DRGCode": 307,
	    "Description": " CARDIAC CONGENITAL & VALVULAR DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 245,
	    "DRGCode": 308,
	    "Description": " CARDIAC ARRHYTHMIA & CONDUCTION DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 246,
	    "DRGCode": 309,
	    "Description": " CARDIAC ARRHYTHMIA & CONDUCTION DISORDERS W CC"
	},
	{
	    "DRGCodeID": 247,
	    "DRGCode": 310,
	    "Description": " CARDIAC ARRHYTHMIA & CONDUCTION DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 248,
	    "DRGCode": 311,
	    "Description": " ANGINA PECTORIS"
	},
	{
	    "DRGCodeID": 249,
	    "DRGCode": 312,
	    "Description": " SYNCOPE & COLLAPSE"
	},
	{
	    "DRGCodeID": 250,
	    "DRGCode": 313,
	    "Description": " CHEST PAIN"
	},
	{
	    "DRGCodeID": 251,
	    "DRGCode": 314,
	    "Description": " OTHER CIRCULATORY SYSTEM DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 252,
	    "DRGCode": 315,
	    "Description": " OTHER CIRCULATORY SYSTEM DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 253,
	    "DRGCode": 316,
	    "Description": " OTHER CIRCULATORY SYSTEM DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 254,
	    "DRGCode": 326,
	    "Description": " STOMACH, ESOPHAGEAL & DUODENAL PROC W MCC"
	},
	{
	    "DRGCodeID": 255,
	    "DRGCode": 327,
	    "Description": " STOMACH, ESOPHAGEAL & DUODENAL PROC W CC"
	},
	{
	    "DRGCodeID": 256,
	    "DRGCode": 328,
	    "Description": " STOMACH, ESOPHAGEAL & DUODENAL PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 257,
	    "DRGCode": 329,
	    "Description": " MAJOR SMALL & LARGE BOWEL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 258,
	    "DRGCode": 330,
	    "Description": " MAJOR SMALL & LARGE BOWEL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 259,
	    "DRGCode": 331,
	    "Description": " MAJOR SMALL & LARGE BOWEL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 260,
	    "DRGCode": 332,
	    "Description": " RECTAL RESECTION W MCC"
	},
	{
	    "DRGCodeID": 261,
	    "DRGCode": 333,
	    "Description": " RECTAL RESECTION W CC"
	},
	{
	    "DRGCodeID": 262,
	    "DRGCode": 334,
	    "Description": " RECTAL RESECTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 263,
	    "DRGCode": 335,
	    "Description": " PERITONEAL ADHESIOLYSIS W MCC"
	},
	{
	    "DRGCodeID": 264,
	    "DRGCode": 336,
	    "Description": " PERITONEAL ADHESIOLYSIS W CC"
	},
	{
	    "DRGCodeID": 265,
	    "DRGCode": 337,
	    "Description": " PERITONEAL ADHESIOLYSIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 266,
	    "DRGCode": 338,
	    "Description": " APPENDECTOMY W COMPLICATED PRINCIPAL DIAG W MCC"
	},
	{
	    "DRGCodeID": 267,
	    "DRGCode": 339,
	    "Description": " APPENDECTOMY W COMPLICATED PRINCIPAL DIAG W CC"
	},
	{
	    "DRGCodeID": 268,
	    "DRGCode": 340,
	    "Description": " APPENDECTOMY W COMPLICATED PRINCIPAL DIAG W/O CC/MCC"
	},
	{
	    "DRGCodeID": 269,
	    "DRGCode": 341,
	    "Description": " APPENDECTOMY W/O COMPLICATED PRINCIPAL DIAG W MCC"
	},
	{
	    "DRGCodeID": 270,
	    "DRGCode": 342,
	    "Description": " APPENDECTOMY W/O COMPLICATED PRINCIPAL DIAG W CC"
	},
	{
	    "DRGCodeID": 271,
	    "DRGCode": 343,
	    "Description": " APPENDECTOMY W/O COMPLICATED PRINCIPAL DIAG W/O CC/MCC"
	},
	{
	    "DRGCodeID": 272,
	    "DRGCode": 344,
	    "Description": " MINOR SMALL & LARGE BOWEL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 273,
	    "DRGCode": 345,
	    "Description": " MINOR SMALL & LARGE BOWEL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 274,
	    "DRGCode": 346,
	    "Description": " MINOR SMALL & LARGE BOWEL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 275,
	    "DRGCode": 347,
	    "Description": " ANAL & STOMAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 276,
	    "DRGCode": 348,
	    "Description": " ANAL & STOMAL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 277,
	    "DRGCode": 349,
	    "Description": " ANAL & STOMAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 278,
	    "DRGCode": 350,
	    "Description": " INGUINAL & FEMORAL HERNIA PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 279,
	    "DRGCode": 351,
	    "Description": " INGUINAL & FEMORAL HERNIA PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 280,
	    "DRGCode": 352,
	    "Description": " INGUINAL & FEMORAL HERNIA PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 281,
	    "DRGCode": 353,
	    "Description": " HERNIA PROCEDURES EXCEPT INGUINAL & FEMORAL W MCC"
	},
	{
	    "DRGCodeID": 282,
	    "DRGCode": 354,
	    "Description": " HERNIA PROCEDURES EXCEPT INGUINAL & FEMORAL W CC"
	},
	{
	    "DRGCodeID": 283,
	    "DRGCode": 355,
	    "Description": " HERNIA PROCEDURES EXCEPT INGUINAL & FEMORAL W/O CC/MCC"
	},
	{
	    "DRGCodeID": 284,
	    "DRGCode": 356,
	    "Description": " OTHER DIGESTIVE SYSTEM O.R. PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 285,
	    "DRGCode": 357,
	    "Description": " OTHER DIGESTIVE SYSTEM O.R. PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 286,
	    "DRGCode": 358,
	    "Description": " OTHER DIGESTIVE SYSTEM O.R. PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 287,
	    "DRGCode": 368,
	    "Description": " MAJOR ESOPHAGEAL DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 288,
	    "DRGCode": 369,
	    "Description": " MAJOR ESOPHAGEAL DISORDERS W CC"
	},
	{
	    "DRGCodeID": 289,
	    "DRGCode": 370,
	    "Description": " MAJOR ESOPHAGEAL DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 290,
	    "DRGCode": 371,
	    "Description": " MAJOR GASTROINTESTINAL DISORDERS & PERITONEAL INFECTIONS W MCC"
	},
	{
	    "DRGCodeID": 291,
	    "DRGCode": 372,
	    "Description": " MAJOR GASTROINTESTINAL DISORDERS & PERITONEAL INFECTIONS W CC"
	},
	{
	    "DRGCodeID": 292,
	    "DRGCode": 373,
	    "Description": " MAJOR GASTROINTESTINAL DISORDERS & PERITONEAL INFECTIONS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 293,
	    "DRGCode": 374,
	    "Description": " DIGESTIVE MALIGNANCY W MCC"
	},
	{
	    "DRGCodeID": 294,
	    "DRGCode": 375,
	    "Description": " DIGESTIVE MALIGNANCY W CC"
	},
	{
	    "DRGCodeID": 295,
	    "DRGCode": 376,
	    "Description": " DIGESTIVE MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 296,
	    "DRGCode": 377,
	    "Description": " G.I. HEMORRHAGE W MCC"
	},
	{
	    "DRGCodeID": 297,
	    "DRGCode": 378,
	    "Description": " G.I. HEMORRHAGE W CC"
	},
	{
	    "DRGCodeID": 298,
	    "DRGCode": 379,
	    "Description": " G.I. HEMORRHAGE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 299,
	    "DRGCode": 380,
	    "Description": " COMPLICATED PEPTIC ULCER W MCC"
	},
	{
	    "DRGCodeID": 300,
	    "DRGCode": 381,
	    "Description": " COMPLICATED PEPTIC ULCER W CC"
	},
	{
	    "DRGCodeID": 301,
	    "DRGCode": 382,
	    "Description": " COMPLICATED PEPTIC ULCER W/O CC/MCC"
	},
	{
	    "DRGCodeID": 302,
	    "DRGCode": 383,
	    "Description": " UNCOMPLICATED PEPTIC ULCER W MCC"
	},
	{
	    "DRGCodeID": 303,
	    "DRGCode": 384,
	    "Description": " UNCOMPLICATED PEPTIC ULCER W/O MCC"
	},
	{
	    "DRGCodeID": 304,
	    "DRGCode": 385,
	    "Description": " INFLAMMATORY BOWEL DISEASE W MCC"
	},
	{
	    "DRGCodeID": 305,
	    "DRGCode": 386,
	    "Description": " INFLAMMATORY BOWEL DISEASE W CC"
	},
	{
	    "DRGCodeID": 306,
	    "DRGCode": 387,
	    "Description": " INFLAMMATORY BOWEL DISEASE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 307,
	    "DRGCode": 388,
	    "Description": " G.I. OBSTRUCTION W MCC"
	},
	{
	    "DRGCodeID": 308,
	    "DRGCode": 389,
	    "Description": " G.I. OBSTRUCTION W CC"
	},
	{
	    "DRGCodeID": 309,
	    "DRGCode": 390,
	    "Description": " G.I. OBSTRUCTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 310,
	    "DRGCode": 391,
	    "Description": " ESOPHAGITIS, GASTROENT & MISC DIGEST DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 311,
	    "DRGCode": 392,
	    "Description": " ESOPHAGITIS, GASTROENT & MISC DIGEST DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 312,
	    "DRGCode": 393,
	    "Description": " OTHER DIGESTIVE SYSTEM DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 313,
	    "DRGCode": 394,
	    "Description": " OTHER DIGESTIVE SYSTEM DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 314,
	    "DRGCode": 395,
	    "Description": " OTHER DIGESTIVE SYSTEM DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 315,
	    "DRGCode": 405,
	    "Description": " PANCREAS, LIVER & SHUNT PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 316,
	    "DRGCode": 406,
	    "Description": " PANCREAS, LIVER & SHUNT PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 317,
	    "DRGCode": 407,
	    "Description": " PANCREAS, LIVER & SHUNT PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 318,
	    "DRGCode": 408,
	    "Description": " BILIARY TRACT PROC EXCEPT ONLY CHOLECYST W OR W/O C.D.E. W MCC"
	},
	{
	    "DRGCodeID": 319,
	    "DRGCode": 409,
	    "Description": " BILIARY TRACT PROC EXCEPT ONLY CHOLECYST W OR W/O C.D.E. W CC"
	},
	{
	    "DRGCodeID": 320,
	    "DRGCode": 410,
	    "Description": " BILIARY TRACT PROC EXCEPT ONLY CHOLECYST W OR W/O C.D.E. W/O CC/MCC"
	},
	{
	    "DRGCodeID": 321,
	    "DRGCode": 411,
	    "Description": " CHOLECYSTECTOMY W C.D.E. W MCC"
	},
	{
	    "DRGCodeID": 322,
	    "DRGCode": 412,
	    "Description": " CHOLECYSTECTOMY W C.D.E. W CC"
	},
	{
	    "DRGCodeID": 323,
	    "DRGCode": 413,
	    "Description": " CHOLECYSTECTOMY W C.D.E. W/O CC/MCC"
	},
	{
	    "DRGCodeID": 324,
	    "DRGCode": 414,
	    "Description": " CHOLECYSTECTOMY EXCEPT BY LAPAROSCOPE W/O C.D.E. W MCC"
	},
	{
	    "DRGCodeID": 325,
	    "DRGCode": 415,
	    "Description": " CHOLECYSTECTOMY EXCEPT BY LAPAROSCOPE W/O C.D.E. W CC"
	},
	{
	    "DRGCodeID": 326,
	    "DRGCode": 416,
	    "Description": " CHOLECYSTECTOMY EXCEPT BY LAPAROSCOPE W/O C.D.E. W/O CC/MCC"
	},
	{
	    "DRGCodeID": 327,
	    "DRGCode": 417,
	    "Description": " LAPAROSCOPIC CHOLECYSTECTOMY W/O C.D.E. W MCC"
	},
	{
	    "DRGCodeID": 328,
	    "DRGCode": 418,
	    "Description": " LAPAROSCOPIC CHOLECYSTECTOMY W/O C.D.E. W CC"
	},
	{
	    "DRGCodeID": 329,
	    "DRGCode": 419,
	    "Description": " LAPAROSCOPIC CHOLECYSTECTOMY W/O C.D.E. W/O CC/MCC"
	},
	{
	    "DRGCodeID": 330,
	    "DRGCode": 420,
	    "Description": " HEPATOBILIARY DIAGNOSTIC PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 331,
	    "DRGCode": 421,
	    "Description": " HEPATOBILIARY DIAGNOSTIC PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 332,
	    "DRGCode": 422,
	    "Description": " HEPATOBILIARY DIAGNOSTIC PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 333,
	    "DRGCode": 423,
	    "Description": " OTHER HEPATOBILIARY OR PANCREAS O.R. PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 334,
	    "DRGCode": 424,
	    "Description": " OTHER HEPATOBILIARY OR PANCREAS O.R. PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 335,
	    "DRGCode": 425,
	    "Description": " OTHER HEPATOBILIARY OR PANCREAS O.R. PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 336,
	    "DRGCode": 432,
	    "Description": " CIRRHOSIS & ALCOHOLIC HEPATITIS W MCC"
	},
	{
	    "DRGCodeID": 337,
	    "DRGCode": 433,
	    "Description": " CIRRHOSIS & ALCOHOLIC HEPATITIS W CC"
	},
	{
	    "DRGCodeID": 338,
	    "DRGCode": 434,
	    "Description": " CIRRHOSIS & ALCOHOLIC HEPATITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 339,
	    "DRGCode": 435,
	    "Description": " MALIGNANCY OF HEPATOBILIARY SYSTEM OR PANCREAS W MCC"
	},
	{
	    "DRGCodeID": 340,
	    "DRGCode": 436,
	    "Description": " MALIGNANCY OF HEPATOBILIARY SYSTEM OR PANCREAS W CC"
	},
	{
	    "DRGCodeID": 341,
	    "DRGCode": 437,
	    "Description": " MALIGNANCY OF HEPATOBILIARY SYSTEM OR PANCREAS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 342,
	    "DRGCode": 438,
	    "Description": " DISORDERS OF PANCREAS EXCEPT MALIGNANCY W MCC"
	},
	{
	    "DRGCodeID": 343,
	    "DRGCode": 439,
	    "Description": " DISORDERS OF PANCREAS EXCEPT MALIGNANCY W CC"
	},
	{
	    "DRGCodeID": 344,
	    "DRGCode": 440,
	    "Description": " DISORDERS OF PANCREAS EXCEPT MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 345,
	    "DRGCode": 441,
	    "Description": " DISORDERS OF LIVER EXCEPT MALIG,CIRR,ALC HEPA W MCC"
	},
	{
	    "DRGCodeID": 346,
	    "DRGCode": 442,
	    "Description": " DISORDERS OF LIVER EXCEPT MALIG,CIRR,ALC HEPA W CC"
	},
	{
	    "DRGCodeID": 347,
	    "DRGCode": 443,
	    "Description": " DISORDERS OF LIVER EXCEPT MALIG,CIRR,ALC HEPA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 348,
	    "DRGCode": 444,
	    "Description": " DISORDERS OF THE BILIARY TRACT W MCC"
	},
	{
	    "DRGCodeID": 349,
	    "DRGCode": 445,
	    "Description": " DISORDERS OF THE BILIARY TRACT W CC"
	},
	{
	    "DRGCodeID": 350,
	    "DRGCode": 446,
	    "Description": " DISORDERS OF THE BILIARY TRACT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 351,
	    "DRGCode": 453,
	    "Description": " COMBINED ANTERIOR/POSTERIOR SPINAL FUSION W MCC"
	},
	{
	    "DRGCodeID": 352,
	    "DRGCode": 454,
	    "Description": " COMBINED ANTERIOR/POSTERIOR SPINAL FUSION W CC"
	},
	{
	    "DRGCodeID": 353,
	    "DRGCode": 455,
	    "Description": " COMBINED ANTERIOR/POSTERIOR SPINAL FUSION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 354,
	    "DRGCode": 456,
	    "Description": " SPINAL FUS EXC CERV W SPINAL CURV/MALIG/INFEC OR 9+ FUS W MCC"
	},
	{
	    "DRGCodeID": 355,
	    "DRGCode": 457,
	    "Description": " SPINAL FUS EXC CERV W SPINAL CURV/MALIG/INFEC OR 9+ FUS W CC"
	},
	{
	    "DRGCodeID": 356,
	    "DRGCode": 458,
	    "Description": " SPINAL FUS EXC CERV W SPINAL CURV/MALIG/INFEC OR 9+ FUS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 357,
	    "DRGCode": 459,
	    "Description": " SPINAL FUSION EXCEPT CERVICAL W MCC"
	},
	{
	    "DRGCodeID": 358,
	    "DRGCode": 460,
	    "Description": " SPINAL FUSION EXCEPT CERVICAL W/O MCC"
	},
	{
	    "DRGCodeID": 359,
	    "DRGCode": 461,
	    "Description": " BILATERAL OR MULTIPLE MAJOR JOINT PROCS OF LOWER EXTREMITY W MCC"
	},
	{
	    "DRGCodeID": 360,
	    "DRGCode": 462,
	    "Description": " BILATERAL OR MULTIPLE MAJOR JOINT PROCS OF LOWER EXTREMITY W/O MCC"
	},
	{
	    "DRGCodeID": 361,
	    "DRGCode": 463,
	    "Description": " WND DEBRID & SKN GRFT EXC HAND, FOR MUSCULO-CONN TISS DIS W MCC"
	},
	{
	    "DRGCodeID": 362,
	    "DRGCode": 464,
	    "Description": " WND DEBRID & SKN GRFT EXC HAND, FOR MUSCULO-CONN TISS DIS W CC"
	},
	{
	    "DRGCodeID": 363,
	    "DRGCode": 465,
	    "Description": " WND DEBRID & SKN GRFT EXC HAND, FOR MUSCULO-CONN TISS DIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 364,
	    "DRGCode": 466,
	    "Description": " REVISION OF HIP OR KNEE REPLACEMENT W MCC"
	},
	{
	    "DRGCodeID": 365,
	    "DRGCode": 467,
	    "Description": " REVISION OF HIP OR KNEE REPLACEMENT W CC"
	},
	{
	    "DRGCodeID": 366,
	    "DRGCode": 468,
	    "Description": " REVISION OF HIP OR KNEE REPLACEMENT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 367,
	    "DRGCode": 469,
	    "Description": " MAJOR JOINT REPLACEMENT OR REATTACHMENT OF LOWER EXTREMITY W MCC"
	},
	{
	    "DRGCodeID": 368,
	    "DRGCode": 470,
	    "Description": " MAJOR JOINT REPLACEMENT OR REATTACHMENT OF LOWER EXTREMITY W/O MCC"
	},
	{
	    "DRGCodeID": 369,
	    "DRGCode": 471,
	    "Description": " CERVICAL SPINAL FUSION W MCC"
	},
	{
	    "DRGCodeID": 370,
	    "DRGCode": 472,
	    "Description": " CERVICAL SPINAL FUSION W CC"
	},
	{
	    "DRGCodeID": 371,
	    "DRGCode": 473,
	    "Description": " CERVICAL SPINAL FUSION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 372,
	    "DRGCode": 474,
	    "Description": " AMPUTATION FOR MUSCULOSKELETAL SYS & CONN TISSUE DIS W MCC"
	},
	{
	    "DRGCodeID": 373,
	    "DRGCode": 475,
	    "Description": " AMPUTATION FOR MUSCULOSKELETAL SYS & CONN TISSUE DIS W CC"
	},
	{
	    "DRGCodeID": 374,
	    "DRGCode": 476,
	    "Description": " AMPUTATION FOR MUSCULOSKELETAL SYS & CONN TISSUE DIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 375,
	    "DRGCode": 477,
	    "Description": " BIOPSIES OF MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W MCC"
	},
	{
	    "DRGCodeID": 376,
	    "DRGCode": 478,
	    "Description": " BIOPSIES OF MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W CC"
	},
	{
	    "DRGCodeID": 377,
	    "DRGCode": 479,
	    "Description": " BIOPSIES OF MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 378,
	    "DRGCode": 480,
	    "Description": " HIP & FEMUR PROCEDURES EXCEPT MAJOR JOINT W MCC"
	},
	{
	    "DRGCodeID": 379,
	    "DRGCode": 481,
	    "Description": " HIP & FEMUR PROCEDURES EXCEPT MAJOR JOINT W CC"
	},
	{
	    "DRGCodeID": 380,
	    "DRGCode": 482,
	    "Description": " HIP & FEMUR PROCEDURES EXCEPT MAJOR JOINT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 381,
	    "DRGCode": 483,
	    "Description": " MAJOR JOINT & LIMB REATTACHMENT PROC OF UPPER EXTREMITY W CC/MCC"
	},
	{
	    "DRGCodeID": 382,
	    "DRGCode": 484,
	    "Description": " MAJOR JOINT & LIMB REATTACHMENT PROC OF UPPER EXTREMITY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 383,
	    "DRGCode": 485,
	    "Description": " KNEE PROCEDURES W PDX OF INFECTION W MCC"
	},
	{
	    "DRGCodeID": 384,
	    "DRGCode": 486,
	    "Description": " KNEE PROCEDURES W PDX OF INFECTION W CC"
	},
	{
	    "DRGCodeID": 385,
	    "DRGCode": 487,
	    "Description": " KNEE PROCEDURES W PDX OF INFECTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 386,
	    "DRGCode": 488,
	    "Description": " KNEE PROCEDURES W/O PDX OF INFECTION W CC/MCC"
	},
	{
	    "DRGCodeID": 387,
	    "DRGCode": 489,
	    "Description": " KNEE PROCEDURES W/O PDX OF INFECTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 388,
	    "DRGCode": 490,
	    "Description": " BACK & NECK PROC EXC SPINAL FUSION W CC/MCC OR DISC DEVICE/NEUROSTIM"
	},
	{
	    "DRGCodeID": 389,
	    "DRGCode": 491,
	    "Description": " BACK & NECK PROC EXC SPINAL FUSION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 390,
	    "DRGCode": 492,
	    "Description": " LOWER EXTREM & HUMER PROC EXCEPT HIP,FOOT,FEMUR W MCC"
	},
	{
	    "DRGCodeID": 391,
	    "DRGCode": 493,
	    "Description": " LOWER EXTREM & HUMER PROC EXCEPT HIP,FOOT,FEMUR W CC"
	},
	{
	    "DRGCodeID": 392,
	    "DRGCode": 494,
	    "Description": " LOWER EXTREM & HUMER PROC EXCEPT HIP,FOOT,FEMUR W/O CC/MCC"
	},
	{
	    "DRGCodeID": 393,
	    "DRGCode": 495,
	    "Description": " LOCAL EXCISION & REMOVAL INT FIX DEVICES EXC HIP & FEMUR W MCC"
	},
	{
	    "DRGCodeID": 394,
	    "DRGCode": 496,
	    "Description": " LOCAL EXCISION & REMOVAL INT FIX DEVICES EXC HIP & FEMUR W CC"
	},
	{
	    "DRGCodeID": 395,
	    "DRGCode": 497,
	    "Description": " LOCAL EXCISION & REMOVAL INT FIX DEVICES EXC HIP & FEMUR W/O CC/MCC"
	},
	{
	    "DRGCodeID": 396,
	    "DRGCode": 498,
	    "Description": " LOCAL EXCISION & REMOVAL INT FIX DEVICES OF HIP & FEMUR W CC/MCC"
	},
	{
	    "DRGCodeID": 397,
	    "DRGCode": 499,
	    "Description": " LOCAL EXCISION & REMOVAL INT FIX DEVICES OF HIP & FEMUR W/O CC/MCC"
	},
	{
	    "DRGCodeID": 398,
	    "DRGCode": 500,
	    "Description": " SOFT TISSUE PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 399,
	    "DRGCode": 501,
	    "Description": " SOFT TISSUE PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 400,
	    "DRGCode": 502,
	    "Description": " SOFT TISSUE PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 401,
	    "DRGCode": 503,
	    "Description": " FOOT PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 402,
	    "DRGCode": 504,
	    "Description": " FOOT PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 403,
	    "DRGCode": 505,
	    "Description": " FOOT PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 404,
	    "DRGCode": 506,
	    "Description": " MAJOR THUMB OR JOINT PROCEDURES"
	},
	{
	    "DRGCodeID": 405,
	    "DRGCode": 507,
	    "Description": " MAJOR SHOULDER OR ELBOW JOINT PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 406,
	    "DRGCode": 508,
	    "Description": " MAJOR SHOULDER OR ELBOW JOINT PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 407,
	    "DRGCode": 509,
	    "Description": " ARTHROSCOPY"
	},
	{
	    "DRGCodeID": 408,
	    "DRGCode": 510,
	    "Description": " SHOULDER,ELBOW OR FOREARM PROC,EXC MAJOR JOINT PROC W MCC"
	},
	{
	    "DRGCodeID": 409,
	    "DRGCode": 511,
	    "Description": " SHOULDER,ELBOW OR FOREARM PROC,EXC MAJOR JOINT PROC W CC"
	},
	{
	    "DRGCodeID": 410,
	    "DRGCode": 512,
	    "Description": " SHOULDER,ELBOW OR FOREARM PROC,EXC MAJOR JOINT PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 411,
	    "DRGCode": 513,
	    "Description": " HAND OR WRIST PROC, EXCEPT MAJOR THUMB OR JOINT PROC W CC/MCC"
	},
	{
	    "DRGCodeID": 412,
	    "DRGCode": 514,
	    "Description": " HAND OR WRIST PROC, EXCEPT MAJOR THUMB OR JOINT PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 413,
	    "DRGCode": 515,
	    "Description": " OTHER MUSCULOSKELET SYS & CONN TISS O.R. PROC W MCC"
	},
	{
	    "DRGCodeID": 414,
	    "DRGCode": 516,
	    "Description": " OTHER MUSCULOSKELET SYS & CONN TISS O.R. PROC W CC"
	},
	{
	    "DRGCodeID": 415,
	    "DRGCode": 517,
	    "Description": " OTHER MUSCULOSKELET SYS & CONN TISS O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 416,
	    "DRGCode": 533,
	    "Description": " FRACTURES OF FEMUR W MCC"
	},
	{
	    "DRGCodeID": 417,
	    "DRGCode": 534,
	    "Description": " FRACTURES OF FEMUR W/O MCC"
	},
	{
	    "DRGCodeID": 418,
	    "DRGCode": 535,
	    "Description": " FRACTURES OF HIP & PELVIS W MCC"
	},
	{
	    "DRGCodeID": 419,
	    "DRGCode": 536,
	    "Description": " FRACTURES OF HIP & PELVIS W/O MCC"
	},
	{
	    "DRGCodeID": 420,
	    "DRGCode": 537,
	    "Description": " SPRAINS, STRAINS, & DISLOCATIONS OF HIP, PELVIS & THIGH W CC/MCC"
	},
	{
	    "DRGCodeID": 421,
	    "DRGCode": 538,
	    "Description": " SPRAINS, STRAINS, & DISLOCATIONS OF HIP, PELVIS & THIGH W/O CC/MCC"
	},
	{
	    "DRGCodeID": 422,
	    "DRGCode": 539,
	    "Description": " OSTEOMYELITIS W MCC"
	},
	{
	    "DRGCodeID": 423,
	    "DRGCode": 540,
	    "Description": " OSTEOMYELITIS W CC"
	},
	{
	    "DRGCodeID": 424,
	    "DRGCode": 541,
	    "Description": " OSTEOMYELITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 425,
	    "DRGCode": 542,
	    "Description": " PATHOLOGICAL FRACTURES & MUSCULOSKELET & CONN TISS MALIG W MCC"
	},
	{
	    "DRGCodeID": 426,
	    "DRGCode": 543,
	    "Description": " PATHOLOGICAL FRACTURES & MUSCULOSKELET & CONN TISS MALIG W CC"
	},
	{
	    "DRGCodeID": 427,
	    "DRGCode": 544,
	    "Description": " PATHOLOGICAL FRACTURES & MUSCULOSKELET & CONN TISS MALIG W/O CC/MCC"
	},
	{
	    "DRGCodeID": 428,
	    "DRGCode": 545,
	    "Description": " CONNECTIVE TISSUE DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 429,
	    "DRGCode": 546,
	    "Description": " CONNECTIVE TISSUE DISORDERS W CC"
	},
	{
	    "DRGCodeID": 430,
	    "DRGCode": 547,
	    "Description": " CONNECTIVE TISSUE DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 431,
	    "DRGCode": 548,
	    "Description": " SEPTIC ARTHRITIS W MCC"
	},
	{
	    "DRGCodeID": 432,
	    "DRGCode": 549,
	    "Description": " SEPTIC ARTHRITIS W CC"
	},
	{
	    "DRGCodeID": 433,
	    "DRGCode": 550,
	    "Description": " SEPTIC ARTHRITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 434,
	    "DRGCode": 551,
	    "Description": " MEDICAL BACK PROBLEMS W MCC"
	},
	{
	    "DRGCodeID": 435,
	    "DRGCode": 552,
	    "Description": " MEDICAL BACK PROBLEMS W/O MCC"
	},
	{
	    "DRGCodeID": 436,
	    "DRGCode": 553,
	    "Description": " BONE DISEASES & ARTHROPATHIES W MCC"
	},
	{
	    "DRGCodeID": 437,
	    "DRGCode": 554,
	    "Description": " BONE DISEASES & ARTHROPATHIES W/O MCC"
	},
	{
	    "DRGCodeID": 438,
	    "DRGCode": 555,
	    "Description": " SIGNS & SYMPTOMS OF MUSCULOSKELETAL SYSTEM & CONN TISSUE W MCC"
	},
	{
	    "DRGCodeID": 439,
	    "DRGCode": 556,
	    "Description": " SIGNS & SYMPTOMS OF MUSCULOSKELETAL SYSTEM & CONN TISSUE W/O MCC"
	},
	{
	    "DRGCodeID": 440,
	    "DRGCode": 557,
	    "Description": " TENDONITIS, MYOSITIS & BURSITIS W MCC"
	},
	{
	    "DRGCodeID": 441,
	    "DRGCode": 558,
	    "Description": " TENDONITIS, MYOSITIS & BURSITIS W/O MCC"
	},
	{
	    "DRGCodeID": 442,
	    "DRGCode": 559,
	    "Description": " AFTERCARE, MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W MCC"
	},
	{
	    "DRGCodeID": 443,
	    "DRGCode": 560,
	    "Description": " AFTERCARE, MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W CC"
	},
	{
	    "DRGCodeID": 444,
	    "DRGCode": 561,
	    "Description": " AFTERCARE, MUSCULOSKELETAL SYSTEM & CONNECTIVE TISSUE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 445,
	    "DRGCode": 562,
	    "Description": " FX, SPRN, STRN & DISL EXCEPT FEMUR, HIP, PELVIS & THIGH W MCC"
	},
	{
	    "DRGCodeID": 446,
	    "DRGCode": 563,
	    "Description": " FX, SPRN, STRN & DISL EXCEPT FEMUR, HIP, PELVIS & THIGH W/O MCC"
	},
	{
	    "DRGCodeID": 447,
	    "DRGCode": 564,
	    "Description": " OTHER MUSCULOSKELETAL SYS & CONNECTIVE TISSUE DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 448,
	    "DRGCode": 565,
	    "Description": " OTHER MUSCULOSKELETAL SYS & CONNECTIVE TISSUE DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 449,
	    "DRGCode": 566,
	    "Description": " OTHER MUSCULOSKELETAL SYS & CONNECTIVE TISSUE DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 450,
	    "DRGCode": 570,
	    "Description": " SKIN DEBRIDEMENT W MCC"
	},
	{
	    "DRGCodeID": 451,
	    "DRGCode": 571,
	    "Description": " SKIN DEBRIDEMENT W CC"
	},
	{
	    "DRGCodeID": 452,
	    "DRGCode": 572,
	    "Description": " SKIN DEBRIDEMENT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 453,
	    "DRGCode": 573,
	    "Description": " SKIN GRAFT FOR SKIN ULCER OR CELLULITIS W MCC"
	},
	{
	    "DRGCodeID": 454,
	    "DRGCode": 574,
	    "Description": " SKIN GRAFT FOR SKIN ULCER OR CELLULITIS W CC"
	},
	{
	    "DRGCodeID": 455,
	    "DRGCode": 575,
	    "Description": " SKIN GRAFT FOR SKIN ULCER OR CELLULITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 456,
	    "DRGCode": 576,
	    "Description": " SKIN GRAFT EXC FOR SKIN ULCER OR CELLULITIS W MCC"
	},
	{
	    "DRGCodeID": 457,
	    "DRGCode": 577,
	    "Description": " SKIN GRAFT EXC FOR SKIN ULCER OR CELLULITIS W CC"
	},
	{
	    "DRGCodeID": 458,
	    "DRGCode": 578,
	    "Description": " SKIN GRAFT EXC FOR SKIN ULCER OR CELLULITIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 459,
	    "DRGCode": 579,
	    "Description": " OTHER SKIN, SUBCUT TISS & BREAST PROC W MCC"
	},
	{
	    "DRGCodeID": 460,
	    "DRGCode": 580,
	    "Description": " OTHER SKIN, SUBCUT TISS & BREAST PROC W CC"
	},
	{
	    "DRGCodeID": 461,
	    "DRGCode": 581,
	    "Description": " OTHER SKIN, SUBCUT TISS & BREAST PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 462,
	    "DRGCode": 582,
	    "Description": " MASTECTOMY FOR MALIGNANCY W CC/MCC"
	},
	{
	    "DRGCodeID": 463,
	    "DRGCode": 583,
	    "Description": " MASTECTOMY FOR MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 464,
	    "DRGCode": 584,
	    "Description": " BREAST BIOPSY, LOCAL EXCISION & OTHER BREAST PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 465,
	    "DRGCode": 585,
	    "Description": " BREAST BIOPSY, LOCAL EXCISION & OTHER BREAST PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 466,
	    "DRGCode": 592,
	    "Description": " SKIN ULCERS W MCC"
	},
	{
	    "DRGCodeID": 467,
	    "DRGCode": 593,
	    "Description": " SKIN ULCERS W CC"
	},
	{
	    "DRGCodeID": 468,
	    "DRGCode": 594,
	    "Description": " SKIN ULCERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 469,
	    "DRGCode": 595,
	    "Description": " MAJOR SKIN DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 470,
	    "DRGCode": 596,
	    "Description": " MAJOR SKIN DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 471,
	    "DRGCode": 597,
	    "Description": " MALIGNANT BREAST DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 472,
	    "DRGCode": 598,
	    "Description": " MALIGNANT BREAST DISORDERS W CC"
	},
	{
	    "DRGCodeID": 473,
	    "DRGCode": 599,
	    "Description": " MALIGNANT BREAST DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 474,
	    "DRGCode": 600,
	    "Description": " NON-MALIGNANT BREAST DISORDERS W CC/MCC"
	},
	{
	    "DRGCodeID": 475,
	    "DRGCode": 601,
	    "Description": " NON-MALIGNANT BREAST DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 476,
	    "DRGCode": 602,
	    "Description": " CELLULITIS W MCC"
	},
	{
	    "DRGCodeID": 477,
	    "DRGCode": 603,
	    "Description": " CELLULITIS W/O MCC"
	},
	{
	    "DRGCodeID": 478,
	    "DRGCode": 604,
	    "Description": " TRAUMA TO THE SKIN, SUBCUT TISS & BREAST W MCC"
	},
	{
	    "DRGCodeID": 479,
	    "DRGCode": 605,
	    "Description": " TRAUMA TO THE SKIN, SUBCUT TISS & BREAST W/O MCC"
	},
	{
	    "DRGCodeID": 480,
	    "DRGCode": 606,
	    "Description": " MINOR SKIN DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 481,
	    "DRGCode": 607,
	    "Description": " MINOR SKIN DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 482,
	    "DRGCode": 614,
	    "Description": " ADRENAL & PITUITARY PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 483,
	    "DRGCode": 615,
	    "Description": " ADRENAL & PITUITARY PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 484,
	    "DRGCode": 616,
	    "Description": " AMPUTAT OF LOWER LIMB FOR ENDOCRINE,NUTRIT,& METABOL DIS W MCC"
	},
	{
	    "DRGCodeID": 485,
	    "DRGCode": 617,
	    "Description": " AMPUTAT OF LOWER LIMB FOR ENDOCRINE,NUTRIT,& METABOL DIS W CC"
	},
	{
	    "DRGCodeID": 486,
	    "DRGCode": 618,
	    "Description": " AMPUTAT OF LOWER LIMB FOR ENDOCRINE,NUTRIT,& METABOL DIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 487,
	    "DRGCode": 619,
	    "Description": " O.R. PROCEDURES FOR OBESITY W MCC"
	},
	{
	    "DRGCodeID": 488,
	    "DRGCode": 620,
	    "Description": " O.R. PROCEDURES FOR OBESITY W CC"
	},
	{
	    "DRGCodeID": 489,
	    "DRGCode": 621,
	    "Description": " O.R. PROCEDURES FOR OBESITY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 490,
	    "DRGCode": 622,
	    "Description": " SKIN GRAFTS & WOUND DEBRID FOR ENDOC, NUTRIT & METAB DIS W MCC"
	},
	{
	    "DRGCodeID": 491,
	    "DRGCode": 623,
	    "Description": " SKIN GRAFTS & WOUND DEBRID FOR ENDOC, NUTRIT & METAB DIS W CC"
	},
	{
	    "DRGCodeID": 492,
	    "DRGCode": 624,
	    "Description": " SKIN GRAFTS & WOUND DEBRID FOR ENDOC, NUTRIT & METAB DIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 493,
	    "DRGCode": 625,
	    "Description": " THYROID, PARATHYROID & THYROGLOSSAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 494,
	    "DRGCode": 626,
	    "Description": " THYROID, PARATHYROID & THYROGLOSSAL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 495,
	    "DRGCode": 627,
	    "Description": " THYROID, PARATHYROID & THYROGLOSSAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 496,
	    "DRGCode": 628,
	    "Description": " OTHER ENDOCRINE, NUTRIT & METAB O.R. PROC W MCC"
	},
	{
	    "DRGCodeID": 497,
	    "DRGCode": 629,
	    "Description": " OTHER ENDOCRINE, NUTRIT & METAB O.R. PROC W CC"
	},
	{
	    "DRGCodeID": 498,
	    "DRGCode": 630,
	    "Description": " OTHER ENDOCRINE, NUTRIT & METAB O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 499,
	    "DRGCode": 637,
	    "Description": " DIABETES W MCC"
	},
	{
	    "DRGCodeID": 500,
	    "DRGCode": 638,
	    "Description": " DIABETES W CC"
	},
	{
	    "DRGCodeID": 501,
	    "DRGCode": 639,
	    "Description": " DIABETES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 502,
	    "DRGCode": 640,
	    "Description": " MISC DISORDERS OF NUTRITION,METABOLISM,FLUIDS/ELECTROLYTES W MCC"
	},
	{
	    "DRGCodeID": 503,
	    "DRGCode": 641,
	    "Description": " MISC DISORDERS OF NUTRITION,METABOLISM,FLUIDS/ELECTROLYTES W/O MCC"
	},
	{
	    "DRGCodeID": 504,
	    "DRGCode": 642,
	    "Description": " INBORN AND OTHER DISORDERS OF METABOLISM"
	},
	{
	    "DRGCodeID": 505,
	    "DRGCode": 643,
	    "Description": " ENDOCRINE DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 506,
	    "DRGCode": 644,
	    "Description": " ENDOCRINE DISORDERS W CC"
	},
	{
	    "DRGCodeID": 507,
	    "DRGCode": 645,
	    "Description": " ENDOCRINE DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 508,
	    "DRGCode": 652,
	    "Description": " KIDNEY TRANSPLANT"
	},
	{
	    "DRGCodeID": 509,
	    "DRGCode": 653,
	    "Description": " MAJOR BLADDER PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 510,
	    "DRGCode": 654,
	    "Description": " MAJOR BLADDER PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 511,
	    "DRGCode": 655,
	    "Description": " MAJOR BLADDER PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 512,
	    "DRGCode": 656,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NEOPLASM W MCC"
	},
	{
	    "DRGCodeID": 513,
	    "DRGCode": 657,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NEOPLASM W CC"
	},
	{
	    "DRGCodeID": 514,
	    "DRGCode": 658,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NEOPLASM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 515,
	    "DRGCode": 659,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NON-NEOPLASM W MCC"
	},
	{
	    "DRGCodeID": 516,
	    "DRGCode": 660,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NON-NEOPLASM W CC"
	},
	{
	    "DRGCodeID": 517,
	    "DRGCode": 661,
	    "Description": " KIDNEY & URETER PROCEDURES FOR NON-NEOPLASM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 518,
	    "DRGCode": 662,
	    "Description": " MINOR BLADDER PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 519,
	    "DRGCode": 663,
	    "Description": " MINOR BLADDER PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 520,
	    "DRGCode": 664,
	    "Description": " MINOR BLADDER PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 521,
	    "DRGCode": 665,
	    "Description": " PROSTATECTOMY W MCC"
	},
	{
	    "DRGCodeID": 522,
	    "DRGCode": 666,
	    "Description": " PROSTATECTOMY W CC"
	},
	{
	    "DRGCodeID": 523,
	    "DRGCode": 667,
	    "Description": " PROSTATECTOMY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 524,
	    "DRGCode": 668,
	    "Description": " TRANSURETHRAL PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 525,
	    "DRGCode": 669,
	    "Description": " TRANSURETHRAL PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 526,
	    "DRGCode": 670,
	    "Description": " TRANSURETHRAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 527,
	    "DRGCode": 671,
	    "Description": " URETHRAL PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 528,
	    "DRGCode": 672,
	    "Description": " URETHRAL PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 529,
	    "DRGCode": 673,
	    "Description": " OTHER KIDNEY & URINARY TRACT PROCEDURES W MCC"
	},
	{
	    "DRGCodeID": 530,
	    "DRGCode": 674,
	    "Description": " OTHER KIDNEY & URINARY TRACT PROCEDURES W CC"
	},
	{
	    "DRGCodeID": 531,
	    "DRGCode": 675,
	    "Description": " OTHER KIDNEY & URINARY TRACT PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 532,
	    "DRGCode": 682,
	    "Description": " RENAL FAILURE W MCC"
	},
	{
	    "DRGCodeID": 533,
	    "DRGCode": 683,
	    "Description": " RENAL FAILURE W CC"
	},
	{
	    "DRGCodeID": 534,
	    "DRGCode": 684,
	    "Description": " RENAL FAILURE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 535,
	    "DRGCode": 685,
	    "Description": " ADMIT FOR RENAL DIALYSIS"
	},
	{
	    "DRGCodeID": 536,
	    "DRGCode": 686,
	    "Description": " KIDNEY & URINARY TRACT NEOPLASMS W MCC"
	},
	{
	    "DRGCodeID": 537,
	    "DRGCode": 687,
	    "Description": " KIDNEY & URINARY TRACT NEOPLASMS W CC"
	},
	{
	    "DRGCodeID": 538,
	    "DRGCode": 688,
	    "Description": " KIDNEY & URINARY TRACT NEOPLASMS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 539,
	    "DRGCode": 689,
	    "Description": " KIDNEY & URINARY TRACT INFECTIONS W MCC"
	},
	{
	    "DRGCodeID": 540,
	    "DRGCode": 690,
	    "Description": " KIDNEY & URINARY TRACT INFECTIONS W/O MCC"
	},
	{
	    "DRGCodeID": 541,
	    "DRGCode": 691,
	    "Description": " URINARY STONES W ESW LITHOTRIPSY W CC/MCC"
	},
	{
	    "DRGCodeID": 542,
	    "DRGCode": 692,
	    "Description": " URINARY STONES W ESW LITHOTRIPSY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 543,
	    "DRGCode": 693,
	    "Description": " URINARY STONES W/O ESW LITHOTRIPSY W MCC"
	},
	{
	    "DRGCodeID": 544,
	    "DRGCode": 694,
	    "Description": " URINARY STONES W/O ESW LITHOTRIPSY W/O MCC"
	},
	{
	    "DRGCodeID": 545,
	    "DRGCode": 695,
	    "Description": " KIDNEY & URINARY TRACT SIGNS & SYMPTOMS W MCC"
	},
	{
	    "DRGCodeID": 546,
	    "DRGCode": 696,
	    "Description": " KIDNEY & URINARY TRACT SIGNS & SYMPTOMS W/O MCC"
	},
	{
	    "DRGCodeID": 547,
	    "DRGCode": 697,
	    "Description": " URETHRAL STRICTURE"
	},
	{
	    "DRGCodeID": 548,
	    "DRGCode": 698,
	    "Description": " OTHER KIDNEY & URINARY TRACT DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 549,
	    "DRGCode": 699,
	    "Description": " OTHER KIDNEY & URINARY TRACT DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 550,
	    "DRGCode": 700,
	    "Description": " OTHER KIDNEY & URINARY TRACT DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 551,
	    "DRGCode": 707,
	    "Description": " MAJOR MALE PELVIC PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 552,
	    "DRGCode": 708,
	    "Description": " MAJOR MALE PELVIC PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 553,
	    "DRGCode": 709,
	    "Description": " PENIS PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 554,
	    "DRGCode": 710,
	    "Description": " PENIS PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 555,
	    "DRGCode": 711,
	    "Description": " TESTES PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 556,
	    "DRGCode": 712,
	    "Description": " TESTES PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 557,
	    "DRGCode": 713,
	    "Description": " TRANSURETHRAL PROSTATECTOMY W CC/MCC"
	},
	{
	    "DRGCodeID": 558,
	    "DRGCode": 714,
	    "Description": " TRANSURETHRAL PROSTATECTOMY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 559,
	    "DRGCode": 715,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM O.R. PROC FOR MALIGNANCY W CC/MCC"
	},
	{
	    "DRGCodeID": 560,
	    "DRGCode": 716,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM O.R. PROC FOR MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 561,
	    "DRGCode": 717,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM O.R. PROC EXC MALIGNANCY W CC/MCC"
	},
	{
	    "DRGCodeID": 562,
	    "DRGCode": 718,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM O.R. PROC EXC MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 563,
	    "DRGCode": 722,
	    "Description": " MALIGNANCY, MALE REPRODUCTIVE SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 564,
	    "DRGCode": 723,
	    "Description": " MALIGNANCY, MALE REPRODUCTIVE SYSTEM W CC"
	},
	{
	    "DRGCodeID": 565,
	    "DRGCode": 724,
	    "Description": " MALIGNANCY, MALE REPRODUCTIVE SYSTEM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 566,
	    "DRGCode": 725,
	    "Description": " BENIGN PROSTATIC HYPERTROPHY W MCC"
	},
	{
	    "DRGCodeID": 567,
	    "DRGCode": 726,
	    "Description": " BENIGN PROSTATIC HYPERTROPHY W/O MCC"
	},
	{
	    "DRGCodeID": 568,
	    "DRGCode": 727,
	    "Description": " INFLAMMATION OF THE MALE REPRODUCTIVE SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 569,
	    "DRGCode": 728,
	    "Description": " INFLAMMATION OF THE MALE REPRODUCTIVE SYSTEM W/O MCC"
	},
	{
	    "DRGCodeID": 570,
	    "DRGCode": 729,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM DIAGNOSES W CC/MCC"
	},
	{
	    "DRGCodeID": 571,
	    "DRGCode": 730,
	    "Description": " OTHER MALE REPRODUCTIVE SYSTEM DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 572,
	    "DRGCode": 734,
	    "Description": " PELVIC EVISCERATION, RAD HYSTERECTOMY & RAD VULVECTOMY W CC/MCC"
	},
	{
	    "DRGCodeID": 573,
	    "DRGCode": 735,
	    "Description": " PELVIC EVISCERATION, RAD HYSTERECTOMY & RAD VULVECTOMY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 574,
	    "DRGCode": 736,
	    "Description": " UTERINE & ADNEXA PROC FOR OVARIAN OR ADNEXAL MALIGNANCY W MCC"
	},
	{
	    "DRGCodeID": 575,
	    "DRGCode": 737,
	    "Description": " UTERINE & ADNEXA PROC FOR OVARIAN OR ADNEXAL MALIGNANCY W CC"
	},
	{
	    "DRGCodeID": 576,
	    "DRGCode": 738,
	    "Description": " UTERINE & ADNEXA PROC FOR OVARIAN OR ADNEXAL MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 577,
	    "DRGCode": 739,
	    "Description": " UTERINE,ADNEXA PROC FOR NON-OVARIAN/ADNEXAL MALIG W MCC"
	},
	{
	    "DRGCodeID": 578,
	    "DRGCode": 740,
	    "Description": " UTERINE,ADNEXA PROC FOR NON-OVARIAN/ADNEXAL MALIG W CC"
	},
	{
	    "DRGCodeID": 579,
	    "DRGCode": 741,
	    "Description": " UTERINE,ADNEXA PROC FOR NON-OVARIAN/ADNEXAL MALIG W/O CC/MCC"
	},
	{
	    "DRGCodeID": 580,
	    "DRGCode": 742,
	    "Description": " UTERINE & ADNEXA PROC FOR NON-MALIGNANCY W CC/MCC"
	},
	{
	    "DRGCodeID": 581,
	    "DRGCode": 743,
	    "Description": " UTERINE & ADNEXA PROC FOR NON-MALIGNANCY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 582,
	    "DRGCode": 744,
	    "Description": " D&C, CONIZATION, LAPAROSCOPY & TUBAL INTERRUPTION W CC/MCC"
	},
	{
	    "DRGCodeID": 583,
	    "DRGCode": 745,
	    "Description": " D&C, CONIZATION, LAPAROSCOPY & TUBAL INTERRUPTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 584,
	    "DRGCode": 746,
	    "Description": " VAGINA, CERVIX & VULVA PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 585,
	    "DRGCode": 747,
	    "Description": " VAGINA, CERVIX & VULVA PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 586,
	    "DRGCode": 748,
	    "Description": " FEMALE REPRODUCTIVE SYSTEM RECONSTRUCTIVE PROCEDURES"
	},
	{
	    "DRGCodeID": 587,
	    "DRGCode": 749,
	    "Description": " OTHER FEMALE REPRODUCTIVE SYSTEM O.R. PROCEDURES W CC/MCC"
	},
	{
	    "DRGCodeID": 588,
	    "DRGCode": 750,
	    "Description": " OTHER FEMALE REPRODUCTIVE SYSTEM O.R. PROCEDURES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 589,
	    "DRGCode": 754,
	    "Description": " MALIGNANCY, FEMALE REPRODUCTIVE SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 590,
	    "DRGCode": 755,
	    "Description": " MALIGNANCY, FEMALE REPRODUCTIVE SYSTEM W CC"
	},
	{
	    "DRGCodeID": 591,
	    "DRGCode": 756,
	    "Description": " MALIGNANCY, FEMALE REPRODUCTIVE SYSTEM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 592,
	    "DRGCode": 757,
	    "Description": " INFECTIONS, FEMALE REPRODUCTIVE SYSTEM W MCC"
	},
	{
	    "DRGCodeID": 593,
	    "DRGCode": 758,
	    "Description": " INFECTIONS, FEMALE REPRODUCTIVE SYSTEM W CC"
	},
	{
	    "DRGCodeID": 594,
	    "DRGCode": 759,
	    "Description": " INFECTIONS, FEMALE REPRODUCTIVE SYSTEM W/O CC/MCC"
	},
	{
	    "DRGCodeID": 595,
	    "DRGCode": 760,
	    "Description": " MENSTRUAL & OTHER FEMALE REPRODUCTIVE SYSTEM DISORDERS W CC/MCC"
	},
	{
	    "DRGCodeID": 596,
	    "DRGCode": 761,
	    "Description": " MENSTRUAL & OTHER FEMALE REPRODUCTIVE SYSTEM DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 597,
	    "DRGCode": 765,
	    "Description": " CESAREAN SECTION W CC/MCC"
	},
	{
	    "DRGCodeID": 598,
	    "DRGCode": 766,
	    "Description": " CESAREAN SECTION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 599,
	    "DRGCode": 767,
	    "Description": " VAGINAL DELIVERY W STERILIZATION &/OR D&C"
	},
	{
	    "DRGCodeID": 600,
	    "DRGCode": 768,
	    "Description": " VAGINAL DELIVERY W O.R. PROC EXCEPT STERIL &/OR D&C"
	},
	{
	    "DRGCodeID": 601,
	    "DRGCode": 769,
	    "Description": " POSTPARTUM & POST ABORTION DIAGNOSES W O.R. PROCEDURE"
	},
	{
	    "DRGCodeID": 602,
	    "DRGCode": 770,
	    "Description": " ABORTION W D&C, ASPIRATION CURETTAGE OR HYSTEROTOMY"
	},
	{
	    "DRGCodeID": 603,
	    "DRGCode": 774,
	    "Description": " VAGINAL DELIVERY W COMPLICATING DIAGNOSES"
	},
	{
	    "DRGCodeID": 604,
	    "DRGCode": 775,
	    "Description": " VAGINAL DELIVERY W/O COMPLICATING DIAGNOSES"
	},
	{
	    "DRGCodeID": 605,
	    "DRGCode": 776,
	    "Description": " POSTPARTUM & POST ABORTION DIAGNOSES W/O O.R. PROCEDURE"
	},
	{
	    "DRGCodeID": 606,
	    "DRGCode": 777,
	    "Description": " ECTOPIC PREGNANCY"
	},
	{
	    "DRGCodeID": 607,
	    "DRGCode": 778,
	    "Description": " THREATENED ABORTION"
	},
	{
	    "DRGCodeID": 608,
	    "DRGCode": 779,
	    "Description": " ABORTION W/O D&C"
	},
	{
	    "DRGCodeID": 609,
	    "DRGCode": 780,
	    "Description": " FALSE LABOR"
	},
	{
	    "DRGCodeID": 610,
	    "DRGCode": 781,
	    "Description": " OTHER ANTEPARTUM DIAGNOSES W MEDICAL COMPLICATIONS"
	},
	{
	    "DRGCodeID": 611,
	    "DRGCode": 782,
	    "Description": " OTHER ANTEPARTUM DIAGNOSES W/O MEDICAL COMPLICATIONS"
	},
	{
	    "DRGCodeID": 612,
	    "DRGCode": 789,
	    "Description": " NEONATES, DIED OR TRANSFERRED TO ANOTHER ACUTE CARE FACILITY"
	},
	{
	    "DRGCodeID": 613,
	    "DRGCode": 790,
	    "Description": " EXTREME IMMATURITY OR RESPIRATORY DISTRESS SYNDROME, NEONATE"
	},
	{
	    "DRGCodeID": 614,
	    "DRGCode": 791,
	    "Description": " PREMATURITY W MAJOR PROBLEMS"
	},
	{
	    "DRGCodeID": 615,
	    "DRGCode": 792,
	    "Description": " PREMATURITY W/O MAJOR PROBLEMS"
	},
	{
	    "DRGCodeID": 616,
	    "DRGCode": 793,
	    "Description": " FULL TERM NEONATE W MAJOR PROBLEMS"
	},
	{
	    "DRGCodeID": 617,
	    "DRGCode": 794,
	    "Description": " NEONATE W OTHER SIGNIFICANT PROBLEMS"
	},
	{
	    "DRGCodeID": 618,
	    "DRGCode": 795,
	    "Description": " NORMAL NEWBORN"
	},
	{
	    "DRGCodeID": 619,
	    "DRGCode": 799,
	    "Description": " SPLENECTOMY W MCC"
	},
	{
	    "DRGCodeID": 620,
	    "DRGCode": 800,
	    "Description": " SPLENECTOMY W CC"
	},
	{
	    "DRGCodeID": 621,
	    "DRGCode": 801,
	    "Description": " SPLENECTOMY W/O CC/MCC"
	},
	{
	    "DRGCodeID": 622,
	    "DRGCode": 802,
	    "Description": " OTHER O.R. PROC OF THE BLOOD & BLOOD FORMING ORGANS W MCC"
	},
	{
	    "DRGCodeID": 623,
	    "DRGCode": 803,
	    "Description": " OTHER O.R. PROC OF THE BLOOD & BLOOD FORMING ORGANS W CC"
	},
	{
	    "DRGCodeID": 624,
	    "DRGCode": 804,
	    "Description": " OTHER O.R. PROC OF THE BLOOD & BLOOD FORMING ORGANS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 625,
	    "DRGCode": 808,
	    "Description": " MAJOR HEMATOL/IMMUN DIAG EXC SICKLE CELL CRISIS & COAGUL W MCC"
	},
	{
	    "DRGCodeID": 626,
	    "DRGCode": 809,
	    "Description": " MAJOR HEMATOL/IMMUN DIAG EXC SICKLE CELL CRISIS & COAGUL W CC"
	},
	{
	    "DRGCodeID": 627,
	    "DRGCode": 810,
	    "Description": " MAJOR HEMATOL/IMMUN DIAG EXC SICKLE CELL CRISIS & COAGUL W/O CC/MCC"
	},
	{
	    "DRGCodeID": 628,
	    "DRGCode": 811,
	    "Description": " RED BLOOD CELL DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 629,
	    "DRGCode": 812,
	    "Description": " RED BLOOD CELL DISORDERS W/O MCC"
	},
	{
	    "DRGCodeID": 630,
	    "DRGCode": 813,
	    "Description": " COAGULATION DISORDERS"
	},
	{
	    "DRGCodeID": 631,
	    "DRGCode": 814,
	    "Description": " RETICULOENDOTHELIAL & IMMUNITY DISORDERS W MCC"
	},
	{
	    "DRGCodeID": 632,
	    "DRGCode": 815,
	    "Description": " RETICULOENDOTHELIAL & IMMUNITY DISORDERS W CC"
	},
	{
	    "DRGCodeID": 633,
	    "DRGCode": 816,
	    "Description": " RETICULOENDOTHELIAL & IMMUNITY DISORDERS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 634,
	    "DRGCode": 820,
	    "Description": " LYMPHOMA & LEUKEMIA W MAJOR O.R. PROCEDURE W MCC"
	},
	{
	    "DRGCodeID": 635,
	    "DRGCode": 821,
	    "Description": " LYMPHOMA & LEUKEMIA W MAJOR O.R. PROCEDURE W CC"
	},
	{
	    "DRGCodeID": 636,
	    "DRGCode": 822,
	    "Description": " LYMPHOMA & LEUKEMIA W MAJOR O.R. PROCEDURE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 637,
	    "DRGCode": 823,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W OTHER O.R. PROC W MCC"
	},
	{
	    "DRGCodeID": 638,
	    "DRGCode": 824,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W OTHER O.R. PROC W CC"
	},
	{
	    "DRGCodeID": 639,
	    "DRGCode": 825,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W OTHER O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 640,
	    "DRGCode": 826,
	    "Description": " MYELOPROLIF DISORD OR POORLY DIFF NEOPL W MAJ O.R. PROC W MCC"
	},
	{
	    "DRGCodeID": 641,
	    "DRGCode": 827,
	    "Description": " MYELOPROLIF DISORD OR POORLY DIFF NEOPL W MAJ O.R. PROC W CC"
	},
	{
	    "DRGCodeID": 642,
	    "DRGCode": 828,
	    "Description": " MYELOPROLIF DISORD OR POORLY DIFF NEOPL W MAJ O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 643,
	    "DRGCode": 829,
	    "Description": " MYELOPROLIF DISORD OR POORLY DIFF NEOPL W OTHER O.R. PROC W CC/MCC"
	},
	{
	    "DRGCodeID": 644,
	    "DRGCode": 830,
	    "Description": " MYELOPROLIF DISORD OR POORLY DIFF NEOPL W OTHER O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 645,
	    "DRGCode": 834,
	    "Description": " ACUTE LEUKEMIA W/O MAJOR O.R. PROCEDURE W MCC"
	},
	{
	    "DRGCodeID": 646,
	    "DRGCode": 835,
	    "Description": " ACUTE LEUKEMIA W/O MAJOR O.R. PROCEDURE W CC"
	},
	{
	    "DRGCodeID": 647,
	    "DRGCode": 836,
	    "Description": " ACUTE LEUKEMIA W/O MAJOR O.R. PROCEDURE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 648,
	    "DRGCode": 837,
	    "Description": " CHEMO W ACUTE LEUKEMIA AS SDX OR W HIGH DOSE CHEMO AGENT W MCC"
	},
	{
	    "DRGCodeID": 649,
	    "DRGCode": 838,
	    "Description": " CHEMO W ACUTE LEUKEMIA AS SDX W CC OR HIGH DOSE CHEMO AGENT"
	},
	{
	    "DRGCodeID": 650,
	    "DRGCode": 839,
	    "Description": " CHEMO W ACUTE LEUKEMIA AS SDX W/O CC/MCC"
	},
	{
	    "DRGCodeID": 651,
	    "DRGCode": 840,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W MCC"
	},
	{
	    "DRGCodeID": 652,
	    "DRGCode": 841,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W CC"
	},
	{
	    "DRGCodeID": 653,
	    "DRGCode": 842,
	    "Description": " LYMPHOMA & NON-ACUTE LEUKEMIA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 654,
	    "DRGCode": 843,
	    "Description": " OTHER MYELOPROLIF DIS OR POORLY DIFF NEOPL DIAG W MCC"
	},
	{
	    "DRGCodeID": 655,
	    "DRGCode": 844,
	    "Description": " OTHER MYELOPROLIF DIS OR POORLY DIFF NEOPL DIAG W CC"
	},
	{
	    "DRGCodeID": 656,
	    "DRGCode": 845,
	    "Description": " OTHER MYELOPROLIF DIS OR POORLY DIFF NEOPL DIAG W/O CC/MCC"
	},
	{
	    "DRGCodeID": 657,
	    "DRGCode": 846,
	    "Description": " CHEMOTHERAPY W/O ACUTE LEUKEMIA AS SECONDARY DIAGNOSIS W MCC"
	},
	{
	    "DRGCodeID": 658,
	    "DRGCode": 847,
	    "Description": " CHEMOTHERAPY W/O ACUTE LEUKEMIA AS SECONDARY DIAGNOSIS W CC"
	},
	{
	    "DRGCodeID": 659,
	    "DRGCode": 848,
	    "Description": " CHEMOTHERAPY W/O ACUTE LEUKEMIA AS SECONDARY DIAGNOSIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 660,
	    "DRGCode": 849,
	    "Description": " RADIOTHERAPY"
	},
	{
	    "DRGCodeID": 661,
	    "DRGCode": 853,
	    "Description": " INFECTIOUS & PARASITIC DISEASES W O.R. PROCEDURE W MCC"
	},
	{
	    "DRGCodeID": 662,
	    "DRGCode": 854,
	    "Description": " INFECTIOUS & PARASITIC DISEASES W O.R. PROCEDURE W CC"
	},
	{
	    "DRGCodeID": 663,
	    "DRGCode": 855,
	    "Description": " INFECTIOUS & PARASITIC DISEASES W O.R. PROCEDURE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 664,
	    "DRGCode": 856,
	    "Description": " POSTOPERATIVE OR POST-TRAUMATIC INFECTIONS W O.R. PROC W MCC"
	},
	{
	    "DRGCodeID": 665,
	    "DRGCode": 857,
	    "Description": " POSTOPERATIVE OR POST-TRAUMATIC INFECTIONS W O.R. PROC W CC"
	},
	{
	    "DRGCodeID": 666,
	    "DRGCode": 858,
	    "Description": " POSTOPERATIVE OR POST-TRAUMATIC INFECTIONS W O.R. PROC W/O CC/MCC"
	},
	{
	    "DRGCodeID": 667,
	    "DRGCode": 862,
	    "Description": " POSTOPERATIVE & POST-TRAUMATIC INFECTIONS W MCC"
	},
	{
	    "DRGCodeID": 668,
	    "DRGCode": 863,
	    "Description": " POSTOPERATIVE & POST-TRAUMATIC INFECTIONS W/O MCC"
	},
	{
	    "DRGCodeID": 669,
	    "DRGCode": 864,
	    "Description": " FEVER"
	},
	{
	    "DRGCodeID": 670,
	    "DRGCode": 865,
	    "Description": " VIRAL ILLNESS W MCC"
	},
	{
	    "DRGCodeID": 671,
	    "DRGCode": 866,
	    "Description": " VIRAL ILLNESS W/O MCC"
	},
	{
	    "DRGCodeID": 672,
	    "DRGCode": 867,
	    "Description": " OTHER INFECTIOUS & PARASITIC DISEASES DIAGNOSES W MCC"
	},
	{
	    "DRGCodeID": 673,
	    "DRGCode": 868,
	    "Description": " OTHER INFECTIOUS & PARASITIC DISEASES DIAGNOSES W CC"
	},
	{
	    "DRGCodeID": 674,
	    "DRGCode": 869,
	    "Description": " OTHER INFECTIOUS & PARASITIC DISEASES DIAGNOSES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 675,
	    "DRGCode": 870,
	    "Description": " SEPTICEMIA OR SEVERE SEPSIS W MV 96+ HOURS"
	},
	{
	    "DRGCodeID": 676,
	    "DRGCode": 871,
	    "Description": " SEPTICEMIA OR SEVERE SEPSIS W/O MV 96+ HOURS W MCC"
	},
	{
	    "DRGCodeID": 677,
	    "DRGCode": 872,
	    "Description": " SEPTICEMIA OR SEVERE SEPSIS W/O MV 96+ HOURS W/O MCC"
	},
	{
	    "DRGCodeID": 678,
	    "DRGCode": 876,
	    "Description": " O.R. PROCEDURE W PRINCIPAL DIAGNOSES OF MENTAL ILLNESS"
	},
	{
	    "DRGCodeID": 679,
	    "DRGCode": 880,
	    "Description": " ACUTE ADJUSTMENT REACTION & PSYCHOSOCIAL DYSFUNCTION"
	},
	{
	    "DRGCodeID": 680,
	    "DRGCode": 881,
	    "Description": " DEPRESSIVE NEUROSES"
	},
	{
	    "DRGCodeID": 681,
	    "DRGCode": 882,
	    "Description": " NEUROSES EXCEPT DEPRESSIVE"
	},
	{
	    "DRGCodeID": 682,
	    "DRGCode": 883,
	    "Description": " DISORDERS OF PERSONALITY & IMPULSE CONTROL"
	},
	{
	    "DRGCodeID": 683,
	    "DRGCode": 884,
	    "Description": " ORGANIC DISTURBANCES & MENTAL RETARDATION"
	},
	{
	    "DRGCodeID": 684,
	    "DRGCode": 885,
	    "Description": " PSYCHOSES"
	},
	{
	    "DRGCodeID": 685,
	    "DRGCode": 886,
	    "Description": " BEHAVIORAL & DEVELOPMENTAL DISORDERS"
	},
	{
	    "DRGCodeID": 686,
	    "DRGCode": 887,
	    "Description": " OTHER MENTAL DISORDER DIAGNOSES"
	},
	{
	    "DRGCodeID": 687,
	    "DRGCode": 894,
	    "Description": " ALCOHOL/DRUG ABUSE OR DEPENDENCE, LEFT AMA"
	},
	{
	    "DRGCodeID": 688,
	    "DRGCode": 895,
	    "Description": " ALCOHOL/DRUG ABUSE OR DEPENDENCE W REHABILITATION THERAPY"
	},
	{
	    "DRGCodeID": 689,
	    "DRGCode": 896,
	    "Description": " ALCOHOL/DRUG ABUSE OR DEPENDENCE W/O REHABILITATION THERAPY W MCC"
	},
	{
	    "DRGCodeID": 690,
	    "DRGCode": 897,
	    "Description": " ALCOHOL/DRUG ABUSE OR DEPENDENCE W/O REHABILITATION THERAPY W/O MCC"
	},
	{
	    "DRGCodeID": 691,
	    "DRGCode": 901,
	    "Description": " WOUND DEBRIDEMENTS FOR INJURIES W MCC"
	},
	{
	    "DRGCodeID": 692,
	    "DRGCode": 902,
	    "Description": " WOUND DEBRIDEMENTS FOR INJURIES W CC"
	},
	{
	    "DRGCodeID": 693,
	    "DRGCode": 903,
	    "Description": " WOUND DEBRIDEMENTS FOR INJURIES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 694,
	    "DRGCode": 904,
	    "Description": " SKIN GRAFTS FOR INJURIES W CC/MCC"
	},
	{
	    "DRGCodeID": 695,
	    "DRGCode": 905,
	    "Description": " SKIN GRAFTS FOR INJURIES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 696,
	    "DRGCode": 906,
	    "Description": " HAND PROCEDURES FOR INJURIES"
	},
	{
	    "DRGCodeID": 697,
	    "DRGCode": 907,
	    "Description": " OTHER O.R. PROCEDURES FOR INJURIES W MCC"
	},
	{
	    "DRGCodeID": 698,
	    "DRGCode": 908,
	    "Description": " OTHER O.R. PROCEDURES FOR INJURIES W CC"
	},
	{
	    "DRGCodeID": 699,
	    "DRGCode": 909,
	    "Description": " OTHER O.R. PROCEDURES FOR INJURIES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 700,
	    "DRGCode": 913,
	    "Description": " TRAUMATIC INJURY W MCC"
	},
	{
	    "DRGCodeID": 701,
	    "DRGCode": 914,
	    "Description": " TRAUMATIC INJURY W/O MCC"
	},
	{
	    "DRGCodeID": 702,
	    "DRGCode": 915,
	    "Description": " ALLERGIC REACTIONS W MCC"
	},
	{
	    "DRGCodeID": 703,
	    "DRGCode": 916,
	    "Description": " ALLERGIC REACTIONS W/O MCC"
	},
	{
	    "DRGCodeID": 704,
	    "DRGCode": 917,
	    "Description": " POISONING & TOXIC EFFECTS OF DRUGS W MCC"
	},
	{
	    "DRGCodeID": 705,
	    "DRGCode": 918,
	    "Description": " POISONING & TOXIC EFFECTS OF DRUGS W/O MCC"
	},
	{
	    "DRGCodeID": 706,
	    "DRGCode": 919,
	    "Description": " COMPLICATIONS OF TREATMENT W MCC"
	},
	{
	    "DRGCodeID": 707,
	    "DRGCode": 920,
	    "Description": " COMPLICATIONS OF TREATMENT W CC"
	},
	{
	    "DRGCodeID": 708,
	    "DRGCode": 921,
	    "Description": " COMPLICATIONS OF TREATMENT W/O CC/MCC"
	},
	{
	    "DRGCodeID": 709,
	    "DRGCode": 922,
	    "Description": " OTHER INJURY, POISONING & TOXIC EFFECT DIAG W MCC"
	},
	{
	    "DRGCodeID": 710,
	    "DRGCode": 923,
	    "Description": " OTHER INJURY, POISONING & TOXIC EFFECT DIAG W/O MCC"
	},
	{
	    "DRGCodeID": 711,
	    "DRGCode": 927,
	    "Description": " EXTENSIVE BURNS OR FULL THICKNESS BURNS W MV 96+ HRS W SKIN GRAFT"
	},
	{
	    "DRGCodeID": 712,
	    "DRGCode": 928,
	    "Description": " FULL THICKNESS BURN W SKIN GRAFT OR INHAL INJ W CC/MCC"
	},
	{
	    "DRGCodeID": 713,
	    "DRGCode": 929,
	    "Description": " FULL THICKNESS BURN W SKIN GRAFT OR INHAL INJ W/O CC/MCC"
	},
	{
	    "DRGCodeID": 714,
	    "DRGCode": 933,
	    "Description": " EXTENSIVE BURNS OR FULL THICKNESS BURNS W MV 96+ HRS W/O SKIN GRAFT"
	},
	{
	    "DRGCodeID": 715,
	    "DRGCode": 934,
	    "Description": " FULL THICKNESS BURN W/O SKIN GRFT OR INHAL INJ"
	},
	{
	    "DRGCodeID": 716,
	    "DRGCode": 935,
	    "Description": " NON-EXTENSIVE BURNS"
	},
	{
	    "DRGCodeID": 717,
	    "DRGCode": 939,
	    "Description": " O.R. PROC W DIAGNOSES OF OTHER CONTACT W HEALTH SERVICES W MCC"
	},
	{
	    "DRGCodeID": 718,
	    "DRGCode": 940,
	    "Description": " O.R. PROC W DIAGNOSES OF OTHER CONTACT W HEALTH SERVICES W CC"
	},
	{
	    "DRGCodeID": 719,
	    "DRGCode": 941,
	    "Description": " O.R. PROC W DIAGNOSES OF OTHER CONTACT W HEALTH SERVICES W/O CC/MCC"
	},
	{
	    "DRGCodeID": 720,
	    "DRGCode": 945,
	    "Description": " REHABILITATION W CC/MCC"
	},
	{
	    "DRGCodeID": 721,
	    "DRGCode": 946,
	    "Description": " REHABILITATION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 722,
	    "DRGCode": 947,
	    "Description": " SIGNS & SYMPTOMS W MCC"
	},
	{
	    "DRGCodeID": 723,
	    "DRGCode": 948,
	    "Description": " SIGNS & SYMPTOMS W/O MCC"
	},
	{
	    "DRGCodeID": 724,
	    "DRGCode": 949,
	    "Description": " AFTERCARE W CC/MCC"
	},
	{
	    "DRGCodeID": 725,
	    "DRGCode": 950,
	    "Description": " AFTERCARE W/O CC/MCC"
	},
	{
	    "DRGCodeID": 726,
	    "DRGCode": 951,
	    "Description": " OTHER FACTORS INFLUENCING HEALTH STATUS"
	},
	{
	    "DRGCodeID": 727,
	    "DRGCode": 955,
	    "Description": " CRANIOTOMY FOR MULTIPLE SIGNIFICANT TRAUMA"
	},
	{
	    "DRGCodeID": 728,
	    "DRGCode": 956,
	    "Description": " LIMB REATTACHMENT, HIP & FEMUR PROC FOR MULTIPLE SIGNIFICANT TRAUMA"
	},
	{
	    "DRGCodeID": 729,
	    "DRGCode": 957,
	    "Description": " OTHER O.R. PROCEDURES FOR MULTIPLE SIGNIFICANT TRAUMA W MCC"
	},
	{
	    "DRGCodeID": 730,
	    "DRGCode": 958,
	    "Description": " OTHER O.R. PROCEDURES FOR MULTIPLE SIGNIFICANT TRAUMA W CC"
	},
	{
	    "DRGCodeID": 731,
	    "DRGCode": 959,
	    "Description": " OTHER O.R. PROCEDURES FOR MULTIPLE SIGNIFICANT TRAUMA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 732,
	    "DRGCode": 963,
	    "Description": " OTHER MULTIPLE SIGNIFICANT TRAUMA W MCC"
	},
	{
	    "DRGCodeID": 733,
	    "DRGCode": 964,
	    "Description": " OTHER MULTIPLE SIGNIFICANT TRAUMA W CC"
	},
	{
	    "DRGCodeID": 734,
	    "DRGCode": 965,
	    "Description": " OTHER MULTIPLE SIGNIFICANT TRAUMA W/O CC/MCC"
	},
	{
	    "DRGCodeID": 735,
	    "DRGCode": 969,
	    "Description": " HIV W EXTENSIVE O.R. PROCEDURE W MCC"
	},
	{
	    "DRGCodeID": 736,
	    "DRGCode": 970,
	    "Description": " HIV W EXTENSIVE O.R. PROCEDURE W/O MCC"
	},
	{
	    "DRGCodeID": 737,
	    "DRGCode": 974,
	    "Description": " HIV W MAJOR RELATED CONDITION W MCC"
	},
	{
	    "DRGCodeID": 738,
	    "DRGCode": 975,
	    "Description": " HIV W MAJOR RELATED CONDITION W CC"
	},
	{
	    "DRGCodeID": 739,
	    "DRGCode": 976,
	    "Description": " HIV W MAJOR RELATED CONDITION W/O CC/MCC"
	},
	{
	    "DRGCodeID": 740,
	    "DRGCode": 977,
	    "Description": " HIV W OR W/O OTHER RELATED CONDITION"
	},
	{
	    "DRGCodeID": 741,
	    "DRGCode": 981,
	    "Description": " EXTENSIVE O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W MCC"
	},
	{
	    "DRGCodeID": 742,
	    "DRGCode": 982,
	    "Description": " EXTENSIVE O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W CC"
	},
	{
	    "DRGCodeID": 743,
	    "DRGCode": 983,
	    "Description": " EXTENSIVE O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 744,
	    "DRGCode": 984,
	    "Description": " PROSTATIC O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W MCC"
	},
	{
	    "DRGCodeID": 745,
	    "DRGCode": 985,
	    "Description": " PROSTATIC O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W CC"
	},
	{
	    "DRGCodeID": 746,
	    "DRGCode": 986,
	    "Description": " PROSTATIC O.R. PROCEDURE UNRELATED TO PRINCIPAL DIAGNOSIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 747,
	    "DRGCode": 987,
	    "Description": " NON-EXTENSIVE O.R. PROC UNRELATED TO PRINCIPAL DIAGNOSIS W MCC"
	},
	{
	    "DRGCodeID": 748,
	    "DRGCode": 988,
	    "Description": " NON-EXTENSIVE O.R. PROC UNRELATED TO PRINCIPAL DIAGNOSIS W CC"
	},
	{
	    "DRGCodeID": 749,
	    "DRGCode": 989,
	    "Description": " NON-EXTENSIVE O.R. PROC UNRELATED TO PRINCIPAL DIAGNOSIS W/O CC/MCC"
	},
	{
	    "DRGCodeID": 750,
	    "DRGCode": 998,
	    "Description": " PRINCIPAL DIAGNOSIS INVALID AS DISCHARGE DIAGNOSIS"
	},
	{
	    "DRGCodeID": 751,
	    "DRGCode": 999,
	    "Description": " UNGROUPABLE"
	}
];



var MasterLetterTemplates = [
	{
	    "LetterTemplateID": 11,
	    "LetterTemplateName": "Approval Letter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 1,
	    "LetterTemplateName": "CMCloseLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 5,
	    "LetterTemplateName": "CMCloseLetterNonCompletionLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 6,
	    "LetterTemplateName": "CMDisenrolledLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 3,
	    "LetterTemplateName": "CMIntroLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 8,
	    "LetterTemplateName": "CMUnabletoReachLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 14,
	    "LetterTemplateName": "DENC Letter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 12,
	    "LetterTemplateName": "Denial Letter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 2,
	    "LetterTemplateName": "DMCloseLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 7,
	    "LetterTemplateName": "DMDisenrolledLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 4,
	    "LetterTemplateName": "DMIntroLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 9,
	    "LetterTemplateName": "DMUnabletoReachLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 10,
	    "LetterTemplateName": "ERLetter",
	    "LetterTemplateDescription": null
	},
	{
	    "LetterTemplateID": 13,
	    "LetterTemplateName": "NOMNC Letter",
	    "LetterTemplateDescription": null
	}
];



var MasterLevelOfCares = [

	{
	    "LevelOfCareID": 1,
	    "Name": "Medical".toUpperCase(),
	    "Code": null,
	    "Description": null
	}//,
	//{
	//    "LevelOfCareID": 4,
	//    "Name": "Rehab",
	//    "Code": null,
	//    "Description": null
	//}
    ,
    {
        "LevelOfCareID": 3,
        "Name": "Maternity".toUpperCase(),
        "Code": null,
        "Description": null
    },
	{
	    "LevelOfCareID": 2,
	    "Name": "Surgical".toUpperCase(),
	    "Code": null,
	    "Description": null
	}
    //,
	//{
	//    "LevelOfCareID": 5,
	//    "Name": "Vent",
	//    "Code": null,
	//    "Description": null
	//}
];
var MasterLevelOfCaresDuplicate = [

	{
	    "LevelOfCareID": 1,
	    "Name": "Medical",
	    "Code": null,
	    "Description": null
	}//,
	//{
	//    "LevelOfCareID": 4,
	//    "Name": "Rehab",
	//    "Code": null,
	//    "Description": null
	//}
    ,
    {
        "LevelOfCareID": 3,
        "Name": "Maternity",
        "Code": null,
        "Description": null
    },
	{
	    "LevelOfCareID": 2,
	    "Name": "Surgical",
	    "Code": null,
	    "Description": null
	}
    //,
	//{
	//    "LevelOfCareID": 5,
	//    "Name": "Vent",
	//    "Code": null,
	//    "Description": null
	//}
];


var MasterMDCCodes = [
	{
	    "MDCCodeID": 1,
	    "MDCCode": "0",
	    "Description": "PRINCIPAL DX CAN NOT BE ASSIGNED TO MDC"
	},
	{
	    "MDCCodeID": 2,
	    "MDCCode": "1",
	    "Description": "DISEASES & DISORDERS OF THE NERVOUS SYSTEM"
	},
	{
	    "MDCCodeID": 3,
	    "MDCCode": "10",
	    "Description": "ENDOCRINE, NUTRITIONAL & METABOLIC DISEASES & DISORDERS"
	},
	{
	    "MDCCodeID": 4,
	    "MDCCode": "11",
	    "Description": "DISEASES & DISORDERS OF THE KIDNEY & URINARY TRACT"
	},
	{
	    "MDCCodeID": 5,
	    "MDCCode": "12",
	    "Description": "DISEASES & DISORDERS OF THE MALE REPRODUCTIVE SYSTEM"
	},
	{
	    "MDCCodeID": 6,
	    "MDCCode": "13",
	    "Description": "DISEASES & DISORDERS OF THE FEMALE REPRODUCTIVE SYSTEM"
	},
	{
	    "MDCCodeID": 7,
	    "MDCCode": "14",
	    "Description": "PREGNANCY, CHILDBIRTH & THE PUERPERIUM"
	},
	{
	    "MDCCodeID": 8,
	    "MDCCode": "15",
	    "Description": "NEWBORNS & OTHER NEONATES WITH CONDTN ORIG IN PERINATAL PERIOD"
	},
	{
	    "MDCCodeID": 9,
	    "MDCCode": "16",
	    "Description": "DISEASES & DISORDERS OF BLOOD, BLOOD FORMING ORGANS, IMMUNOLOG DISORD"
	},
	{
	    "MDCCodeID": 10,
	    "MDCCode": "17",
	    "Description": "MYELOPROLIFERATIVE DISEASES & DISORDERS, POORLY DIFFERENTIATED NEOPLASM"
	},
	{
	    "MDCCodeID": 11,
	    "MDCCode": "18",
	    "Description": "INFECTIOUS & PARASITIC DISEASES, SYSTEMIC OR UNSPECIFIED SITES"
	},
	{
	    "MDCCodeID": 12,
	    "MDCCode": "19",
	    "Description": "MENTAL DISEASES & DISORDERS"
	},
	{
	    "MDCCodeID": 13,
	    "MDCCode": "2",
	    "Description": "DISEASES & DISORDERS OF THE EYE"
	},
	{
	    "MDCCodeID": 14,
	    "MDCCode": "20",
	    "Description": "ALCOHOL/DRUG USE & ALCOHOL/DRUG INDUCED ORGANIC MENTAL DISORDERS"
	},
	{
	    "MDCCodeID": 15,
	    "MDCCode": "21",
	    "Description": "INJURIES, POISONINGS & TOXIC EFFECTS OF DRUGS"
	},
	{
	    "MDCCodeID": 16,
	    "MDCCode": "22",
	    "Description": "BURNS"
	},
	{
	    "MDCCodeID": 17,
	    "MDCCode": "23",
	    "Description": "FACTORS INFLUENCING HLTH STAT & OTHR CONTACTS WITH HLTH SERVCS"
	},
	{
	    "MDCCodeID": 18,
	    "MDCCode": "24",
	    "Description": "MULTIPLE SIGNIFICANT TRAUMA"
	},
	{
	    "MDCCodeID": 19,
	    "MDCCode": "25",
	    "Description": "HUMAN IMMUNODEFICIENCY VIRUS INFECTIONS"
	},
	{
	    "MDCCodeID": 20,
	    "MDCCode": "3",
	    "Description": "DISEASES & DISORDERS OF THE EAR, NOSE, MOUTH & THROAT"
	},
	{
	    "MDCCodeID": 21,
	    "MDCCode": "4",
	    "Description": "DISEASES & DISORDERS OF THE RESPIRATORY SYSTEM"
	},
	{
	    "MDCCodeID": 22,
	    "MDCCode": "5",
	    "Description": "DISEASES & DISORDERS OF THE CIRCULATORY SYSTEM"
	},
	{
	    "MDCCodeID": 23,
	    "MDCCode": "6",
	    "Description": "DISEASES & DISORDERS OF THE DIGESTIVE SYSTEM"
	},
	{
	    "MDCCodeID": 24,
	    "MDCCode": "7",
	    "Description": "DISEASES & DISORDERS OF THE HEPATOBILIARY SYSTEM & PANCREAS"
	},
	{
	    "MDCCodeID": 25,
	    "MDCCode": "8",
	    "Description": "DISEASES & DISORDERS OF THE MUSCULOSKELETAL SYSTEM & CONN TISSUE"
	},
	{
	    "MDCCodeID": 26,
	    "MDCCode": "9",
	    "Description": "DISEASES & DISORDERS OF THE SKIN, SUBCUTANEOUS TISSUE & BREAST"
	}
];



var MasterNoteSubjects = [
	{
	    "NoteSubjectID": 1,
	    "Subject": "Claims Review"
	},
	{
	    "NoteSubjectID": 6,
	    "Subject": "DENC"
	},
	{
	    "NoteSubjectID": 3,
	    "Subject": "Negotiate Rate"
	},
	{
	    "NoteSubjectID": 5,
	    "Subject": "NOMNC"
	},
	{
	    "NoteSubjectID": 4,
	    "Subject": "OON"
	},
	{
	    "NoteSubjectID": 2,
	    "Subject": "UM Note"
	}
];



var MasterNoteTypes = [
	{
	    "NoteTypeID": 1,
	    "TypeOfNote": "Nurse Review"
	},
	{
	    "NoteTypeID": 2,
	    "TypeOfNote": "MD Review"
	},
	{
	    "NoteTypeID": 3,
	    "TypeOfNote": "Additional Information"
	},
	{
	    "NoteTypeID": 4,
	    "TypeOfNote": "Other"
	},
	{
	    "NoteTypeID": 5,
	    "TypeOfNote": "Auth Note"
	},
	{
	    "NoteTypeID": 6,
	    "TypeOfNote": "PreAuth"
	},
	{
	    "NoteTypeID": 7,
	    "TypeOfNote": "Member House"
	}
];


var MasterODAGQuestions = [
	{
	    "QuestionID": 1,
	    "Description": "Has PCP approved this request?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 1,
			    "Value": "Y"
			},
			{
			    "OptionID": 2,
			    "Value": "N"
			},
			{
			    "OptionID": 3,
			    "Value": "I am the PCP"
			}
	    ],
	    "QuestionType": null,
	    "QuestionType1": null
	},
	{
	    "QuestionID": 2,
	    "Description": "Beneficiary request expedited?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 4,
			    "Value": "Y"
			},
			{
			    "OptionID": 5,
			    "Value": "N"
			},
			{
			    "OptionID": 6,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": null,
	    "QuestionType1": null
	},
	{
	    "QuestionID": 3,
	    "Description": "Provider request expedited?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 7,
			    "Value": "Y"
			},
			{
			    "OptionID": 8,
			    "Value": "N"
			},
			{
			    "OptionID": 9,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": null,
	    "QuestionType1": null
	},
	{
	    "QuestionID": 4,
	    "Description": "Process expedited?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 10,
			    "Value": "Y"
			},
			{
			    "OptionID": 11,
			    "Value": "N"
			},
			{
			    "OptionID": 12,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": null,
	    "QuestionType1": null
	},
	{
	    "QuestionID": 5,
	    "Description": "Did plan extend timeframe?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 13,
			    "Value": "Y"
			},
			{
			    "OptionID": 14,
			    "Value": "N"
			},
			{
			    "OptionID": 15,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 6,
	    "Description": "Was time frame extension taken?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 16,
			    "Value": "Y"
			},
			{
			    "OptionID": 17,
			    "Value": "N"
			},
			{
			    "OptionID": 18,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 7,
	    "Description": "Was member notified by phone?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 19,
			    "Value": "Y"
			},
			{
			    "OptionID": 20,
			    "Value": "N"
			},
			{
			    "OptionID": 21,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 8,
	    "Description": "Was member notified by letter?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 22,
			    "Value": "Y"
			},
			{
			    "OptionID": 23,
			    "Value": "N"
			},
			{
			    "OptionID": 24,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 9,
	    "Description": "Case Disposition",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 25,
			    "Value": "Approved"
			},
			{
			    "OptionID": 26,
			    "Value": "Denied"
			}
	    ],
	    "QuestionType": null,
	    "QuestionType1": null
	},
	{
	    "QuestionID": 10,
	    "Description": "Date & Time of Disposition",
	    "Code": null,
	    "Options": [],
	    "QuestionType": "OnlyDateTime",
	    "QuestionType1": 5
	},
	{
	    "QuestionID": 11,
	    "Description": "Was request denied for LOMN? ",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 27,
			    "Value": "Y"
			},
			{
			    "OptionID": 28,
			    "Value": "N"
			},
			{
			    "OptionID": 29,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 12,
	    "Description": "Was case reviewed by Medical Director?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 30,
			    "Value": "Y"
			},
			{
			    "OptionID": 31,
			    "Value": "N"
			},
			{
			    "OptionID": 32,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 13,
	    "Description": "Was reconsideration reviewed by Medical Director?",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 33,
			    "Value": "Y"
			},
			{
			    "OptionID": 34,
			    "Value": "N"
			},
			{
			    "OptionID": 35,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 14,
	    "Description": "Date & Time of Effectuation",
	    "Code": null,
	    "Options": [],
	    "QuestionType": "OnlyDateTime",
	    "QuestionType1": 5
	},
	{
	    "QuestionID": 15,
	    "Description": "Member notified orally",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 36,
			    "Value": "Y"
			},
			{
			    "OptionID": 37,
			    "Value": "N"
			},
			{
			    "OptionID": 38,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 16,
	    "Description": "Member written notification",
	    "Code": null,
	    "Options": [
			{
			    "OptionID": 39,
			    "Value": "Y"
			},
			{
			    "OptionID": 40,
			    "Value": "N"
			},
			{
			    "OptionID": 41,
			    "Value": "NA"
			}
	    ],
	    "QuestionType": "ObjectiveWithDateTime",
	    "QuestionType1": 2
	},
	{
	    "QuestionID": 17,
	    "Description": "AOR Receipt Date",
	    "Code": null,
	    "Options": [],
	    "QuestionType": "OnlyDateTime",
	    "QuestionType1": 5
	},
	{
	    "QuestionID": 18,
	    "Description": "Name of Plan",
	    "Code": null,
	    "Options": [],
	    "QuestionType": "Descriptive",
	    "QuestionType1": 3
	}
];



var MasterPlacesOfService = [
	{
	    "PlaceOfServiceID": 1,
	    "Name": "11- OFFICE",
	    "Code": null,
	    "Description": "11(a)-Office",
	    "POSRoomTypes": []
	},
    //,
	//{
	//    "PlaceOfServiceID": 3,
	//    "Name": "12- PATIENT HOME",
	//    "Code": null,
	//    "Description": "12-Patient Home",
	//    "POSRoomTypes": []
	//},
	{
	    "PlaceOfServiceID": 4,
	    "Name": "21- IP HOSPITAL",
	    "Code": null,
	    "Description": "21-InPatient Hospital",
	    "POSRoomTypes": [
			{
			    "POSRoomTypeID": 5,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 1,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 1,
			        "RoomTypeName": "Med"
			    }
			},
			{
			    "POSRoomTypeID": 11,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 2,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 2,
			        "RoomTypeName": "Surg"
			    }
			},
			{
			    "POSRoomTypeID": 14,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 3,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 3,
			        "RoomTypeName": "ICU"
			    }
			},
			{
			    "POSRoomTypeID": 16,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 4,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 4,
			        "RoomTypeName": "CCU"
			    }
			},
			{
			    "POSRoomTypeID": 18,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 5,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 5,
			        "RoomTypeName": "CICU"
			    }
			},
			{
			    "POSRoomTypeID": 23,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 6,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 6,
			        "RoomTypeName": "Tele"
			    }
			},
			{
			    "POSRoomTypeID": 29,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 7,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 7,
			        "RoomTypeName": "CDU"
			    }
			},
			{
			    "POSRoomTypeID": 32,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 8,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 8,
			        "RoomTypeName": "CV-ICU"
			    }
			},
			{
			    "POSRoomTypeID": 34,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 9,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 9,
			        "RoomTypeName": "NS-ICU"
			    }
			},
			{
			    "POSRoomTypeID": 86,
			    "PlaceOfServiceID": 4,
			    "PlaceOfService": null,
			    "TypeOfRoomID": 17,
			    "TypeOfRoom": {
			        "TypeOfRoomID": 17,
			        "RoomTypeName": "ER HOLD"
			    }
			}
	    ]
	}//,
	//{
	//    "PlaceOfServiceID": 5,
	//    "Name": "22- OP HOSPITAL",
	//    "Code": null,
	//    "Description": "22-OutPatient Hospital",
	//    "POSRoomTypes": [
	//		{
	//		    "POSRoomTypeID": 6,
	//		    "PlaceOfServiceID": 5,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 1,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 1,
	//		        "RoomTypeName": "Med"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 12,
	//		    "PlaceOfServiceID": 5,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 2,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 2,
	//		        "RoomTypeName": "Surg"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 24,
	//		    "PlaceOfServiceID": 5,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 6,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 6,
	//		        "RoomTypeName": "Tele"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 30,
	//		    "PlaceOfServiceID": 5,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 7,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 7,
	//		        "RoomTypeName": "CDU"
	//		    }
	//		}
	//    ]
	//},
	//{
	//    "PlaceOfServiceID": 6,
	//    "Name": "23-Emergency Room - Hospital",
	//    "Code": null,
	//    "Description": "23-Emergency Room - Hospital",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 7,
	//    "Name": "24- ASC",
	//    "Code": null,
	//    "Description": "24-Ambulatory Surgical Center",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 10,
	//    "Name": "31- SNF",
	//    "Code": null,
	//    "Description": "31-Skilled Nursing Facility",
	//    "POSRoomTypes": [
	//		{
	//		    "POSRoomTypeID": 35,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 10,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 10,
	//		        "RoomTypeName": "Level 1"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 43,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 11,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 11,
	//		        "RoomTypeName": "Level 2"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 51,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 12,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 12,
	//		        "RoomTypeName": "Level 3"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 59,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 13,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 13,
	//		        "RoomTypeName": "Level 4"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 67,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 14,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 14,
	//		        "RoomTypeName": "Level 5"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 76,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 16,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 16,
	//		        "RoomTypeName": "NEG FEE"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 87,
	//		    "PlaceOfServiceID": 10,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 18,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 18,
	//		        "RoomTypeName": "RUG"
	//		    }
	//		}
	//    ]
	//},
	//{
	//    "PlaceOfServiceID": 14,
	//    "Name": "41-Ambulance - Land",
	//    "Code": null,
	//    "Description": "41-Ambulance - Land",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 15,
	//    "Name": "42-Ambulance – Air or Water",
	//    "Code": null,
	//    "Description": "42-Ambulance – Air or Water",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 28,
	//    "Name": "49-Independent Clinic",
	//    "Code": null,
	//    "Description": "49-Independent Clinic",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 29,
	//    "Name": "50-Federally Qualified Health Center",
	//    "Code": null,
	//    "Description": "50-Federally Qualified Health Center",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 30,
	//    "Name": "60-Mass Immunization Center",
	//    "Code": null,
	//    "Description": "60-Mass Immunization Center",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 22,
	//    "Name": "61(a)-IRF",
	//    "Code": null,
	//    "Description": "61(a)-IRF(Inpatient Rehabilitation Facility)",
	//    "POSRoomTypes": [
	//		{
	//		    "POSRoomTypeID": 95,
	//		    "PlaceOfServiceID": 22,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 15,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 15,
	//		        "RoomTypeName": "DRG"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 96,
	//		    "PlaceOfServiceID": 22,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 16,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 16,
	//		        "RoomTypeName": "NEG FEE"
	//		    }
	//		}
	//    ]
	//},
	//{
	//    "PlaceOfServiceID": 32,
	//    "Name": "61(b)-LTAC",
	//    "Code": null,
	//    "Description": "61(b)-LTAC(Long Term Acute Care)",
	//    "POSRoomTypes": [
	//		{
	//		    "POSRoomTypeID": 97,
	//		    "PlaceOfServiceID": 32,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 15,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 15,
	//		        "RoomTypeName": "DRG"
	//		    }
	//		},
	//		{
	//		    "POSRoomTypeID": 98,
	//		    "PlaceOfServiceID": 32,
	//		    "PlaceOfService": null,
	//		    "TypeOfRoomID": 16,
	//		    "TypeOfRoom": {
	//		        "TypeOfRoomID": 16,
	//		        "RoomTypeName": "NEG FEE"
	//		    }
	//		}
	//    ]
	//},
	//{
	//    "PlaceOfServiceID": 23,
	//    "Name": "62- CORF",
	//    "Code": null,
	//    "Description": "62-Comprehensive Outpatient Rehab Facility",
	//    "POSRoomTypes": []
	//}
	//{
	//    "PlaceOfServiceID": 24,
	//    "Name": "65-End-Stage Renal Disease Treatment Facility",
	//    "Code": null,
	//    "Description": "65-End-Stage Renal Disease Treatment Facility",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 25,
	//    "Name": "71-State or Local Public Health Clinic",
	//    "Code": null,
	//    "Description": "71-State or Local Public Health Clinic",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 31,
	//    "Name": "72-Rural Health Clinic",
	//    "Code": null,
	//    "Description": "72-Rural Health Clinic",
	//    "POSRoomTypes": []
	//},
	//{
	//    "PlaceOfServiceID": 26,
	//    "Name": "81-Independent Lab",
	//    "Code": null,
	//    "Description": "81-Independent Lab",
	//    "POSRoomTypes": []
	//}
];

AuthHistoryData =
    [
        {
            "ABV": "OFC",
            "REFNO": "1607132022",
            "FROM": "08/10/16",
            "TO": "09/10/16",
            "SVC": "MICHAEL COLE",
            "REQUEST": "STANDARD",
            "FACILITY": "HOME THERAPY THERAPY",
            "AUTH": "PS",
            "DOS": "08/11/16",
            "UNITS": "1",
            "STATUS": "APP",
            "POS": "11",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OPH",
            "REFNO": "1607120453",
            "FROM": "07/28/16",
            "TO": "11/19/16",
            "SVC": "V MANJUSRI",
            "REQUEST": "STANDARD",
            "FACILITY": "ALL SAINTS THERAPY",
            "AUTH": "PS",
            "DOS": "07/29/16",
            "UNITS": "3",
            "STATUS": "PEND-CD",
            "POS": "22",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "HHS",
            "REFNO": "1607120332",
            "FROM": "07/12/16",
            "TO": "10/10/16",
            "SVC": "JACQUELINE DOE",
            "REQUEST": "EXPEDITED",
            "FACILITY": "MEDICAL CENT THERAPY",
            "AUTH": "PS",
            "DOS": "07/13/16",
            "UNITS": "4",
            "STATUS": "PEND-NURSE",
            "POS": "12",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OFC",
            "REFNO": "1587120022",
            "FROM": "06/22/16",
            "TO": "09/21/16",
            "SVC": "YOUNG ROBERT",
            "REQUEST": "EXPEDITED",
            "FACILITY": "HOME THERAPY THERAPY",
            "AUTH": "PS",
            "DOS": "06/23/16",
            "UNITS": "2",
            "STATUS": "APP",
            "POS": "11",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OFC",
            "REFNO": "1597120022",
            "FROM": "07/12/16",
            "TO": "10/10/16",
            "SVC": "JACQUELINE MATT",
            "REQUEST": "STANDARD",
            "FACILITY": "ALL SAINTS THERAPY",
            "AUTH": "PS",
            "DOS": "07/13/16",
            "UNITS": "4",
            "STATUS": "PEND-NURSE",
            "POS": "11",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OPH",
            "REFNO": "1607144442",
            "FROM": "07/08/16",
            "TO": "10/30/16",
            "SVC": "YOUNG ROBERT",
            "REQUEST": "EXPEDITED",
            "FACILITY": "MEDICAL CENT THERAPY  THERAPY",
            "AUTH": "PS",
            "DOS": "07/09/16",
            "UNITS": "5",
            "STATUS": "PEND-CD",
            "POS": "22",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "HHS",
            "REFNO": "1607120067",
            "FROM": "05/06/16",
            "TO": "08/04/16",
            "SVC": "MICHAEL JOY",
            "REQUEST": "STANDARD",
            "FACILITY": "HOME THERAPY THERAPY THERAPY",
            "AUTH": "PS",
            "DOS": "05/07/16",
            "UNITS": "1",
            "STATUS": "APP",
            "POS": "12",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OPH",
            "REFNO": "1609420021",
            "FROM": "06/15/16",
            "TO": "09/14/16",
            "SVC": "V MANJUSRI",
            "REQUEST": "STANDARD",
            "FACILITY": "ALL SAINTS THERAPY",
            "AUTH": "PS",
            "DOS": "06/16/16",
            "UNITS": "3",
            "STATUS": "PEND-CD",
            "POS": "22",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "HHS",
            "REFNO": "1559820022",
            "FROM": "07/18/16",
            "TO": "09/10/16",
            "SVC": "JACQUELINE SONG",
            "REQUEST": "EXPEDITED",
            "FACILITY": "HOME THERAPY THERAPY",
            "AUTH": "PS",
            "DOS": "07/19/16",
            "UNITS": "4",
            "STATUS": "APP",
            "POS": "12",
            "DX": "216.4",
            "ACTION": ""
        },
        {
            "ABV": "OFC",
            "REFNO": "1607125555",
            "FROM": "06/12/16",
            "TO": "08/10/16",
            "SVC": "YOUNG ROBERT",
            "REQUEST": "EXPEDITED",
            "FACILITY": "MEDICAL CENT THERAPY",
            "AUTH": "PS",
            "DOS": "06/13/16",
            "UNITS": "2",
            "STATUS": "PEND-NURSE",
            "POS": "11",
            "DX": "216.4",
            "ACTION": ""
        }

    ]

ViewAuthHistoryData =
   [
       {
           "EXPDOS": "07/08/2016",
           "FROM": "07/12/16",
           "TO": "10/10/16",
           "SVC": "MICHAEL COLE",
           "PRIMARYDX": "M02.89",
           "DXDESC": "OTHER REACTIVE ARTH...",
           "POS": "11(A)",
           "PROCCODE": "75716",
           "PROCDESC": "ARTERY X-RAYS...",
           "REQUNITS": "1",
           "AUTHUNITS": "0"
       },
       {
           "EXPDOS": "07/08/2016",
           "FROM": "07/12/16",
           "TO": "10/10/16",
           "SVC": "MICHAEL COLE",
           "PRIMARYDX": "M02.89",
           "DXDESC": "OTHER REACTIVE ARTH...",
           "POS": "11(A)",
           "PROCCODE": "36252",
           "PROCDESC": "INS GATH RENAR...",
           "REQUNITS": "1",
           "AUTHUNITS": "0"
       },
       {
           "EXPDOS": "07/08/2016",
           "FROM": "07/12/16",
           "TO": "10/10/16",
           "SVC": "MICHAEL COLE",
           "PRIMARYDX": "M02.89",
           "DXDESC": "OTHER REACTIVE ARTH...",
           "POS": "11(A)",
           "PROCCODE": "Q9967",
           "PROCDESC": "LOCM 300-399M...",
           "REQUNITS": "1",
           "AUTHUNITS": "0"
       },
       {
           "EXPDOS": "07/08/2016",
           "FROM": "07/12/16",
           "TO": "10/10/16",
           "SVC": "MICHAEL COLE",
           "PRIMARYDX": "M02.89",
           "DXDESC": "OTHER REACTIVE ARTH...",
           "POS": "11(A)",
           "PROCCODE": "J7030",
           "PROCDESC": "INFUSION-NORM...",
           "REQUNITS": "1",
           "AUTHUNITS": "0"
       }

   ];

var MasterPlainLanguages = [
	{
	    "PlainLanguageID": 1,
	    "PlainLanguageName": "AN AMBULATORY SURGERY CENTER APPROVAL, FOR YOUR EYE SURGERY"
	},
	{
	    "PlainLanguageID": 2,
	    "PlainLanguageName": "BLADDER PROCEDURE"
	},
	{
	    "PlainLanguageID": 3,
	    "PlainLanguageName": "BLADDER PROCEDURE WITH BIOPSY"
	},
	{
	    "PlainLanguageID": 4,
	    "PlainLanguageName": "BLOOD DRAW"
	},
	{
	    "PlainLanguageID": 5,
	    "PlainLanguageName": "BLOOD TRANSFUSION"
	},
	{
	    "PlainLanguageID": 6,
	    "PlainLanguageName": "BREAST PROSTHESIS AND MASTECTOMY BRAS AND CAMISOLE"
	},
	{
	    "PlainLanguageID": 7,
	    "PlainLanguageName": "BREATHING MACHINE - CPAP AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 8,
	    "PlainLanguageName": "BREATHING MACHINE - CPAP MACHINE AND A HEATED HUMIDIFIER"
	},
	{
	    "PlainLanguageID": 9,
	    "PlainLanguageName": "BREATHING MACHINE - CPAP MACHINE AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 10,
	    "PlainLanguageName": "BREATHING MACHINE - HUMIDIFIER"
	},
	{
	    "PlainLanguageID": 11,
	    "PlainLanguageName": "BREATHING MACHINE - NEBULIZER"
	},
	{
	    "PlainLanguageID": 12,
	    "PlainLanguageName": "BREATHING MACHINE - NEBULIZER AND OXYGEN EQUIPMENT"
	},
	{
	    "PlainLanguageID": 13,
	    "PlainLanguageName": "BREATHING MACHINE - NEBULIZER AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 14,
	    "PlainLanguageName": "CASTING AND CASTING SUPPLIES"
	},
	{
	    "PlainLanguageID": 15,
	    "PlainLanguageName": "CATHETERS AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 16,
	    "PlainLanguageName": "CERVICAL TRACTION EQUIPMENT"
	},
	{
	    "PlainLanguageID": 17,
	    "PlainLanguageName": "CHEMOTHERAPY ADMINISTRATION"
	},
	{
	    "PlainLanguageID": 18,
	    "PlainLanguageName": "CHEMOTHERAPY ADMINISTRATION, PUMP MANAGEMENT AND PUMP SUPPLIES"
	},
	{
	    "PlainLanguageID": 19,
	    "PlainLanguageName": "COLOSTOMY SUPPLIES"
	},
	{
	    "PlainLanguageID": 20,
	    "PlainLanguageName": "CPAP SLEEP STUDY"
	},
	{
	    "PlainLanguageID": 21,
	    "PlainLanguageName": "CPM MACHINE"
	},
	{
	    "PlainLanguageID": 22,
	    "PlainLanguageName": "DIABETIC SHOES"
	},
	{
	    "PlainLanguageID": 23,
	    "PlainLanguageName": "DIABETIC SUPPLIES"
	},
	{
	    "PlainLanguageID": 24,
	    "PlainLanguageName": "DIAGNOSTIC TEST OF THE ABDOMEN, CTA - COMPUTED TOMOGRAPHY ANGIOGRAPHY"
	},
	{
	    "PlainLanguageID": 25,
	    "PlainLanguageName": "DIAGNOSTIC TEST OF THE GALLBLADDER"
	},
	{
	    "PlainLanguageID": 26,
	    "PlainLanguageName": "DIAGNOSTIC TEST OF THE HEART, ECHO - ECHOCARDIOGRAPHY"
	},
	{
	    "PlainLanguageID": 27,
	    "PlainLanguageName": "DIAGNOSTIC TEST TO ASSESS MUSCLES AND NERVE CELLS"
	},
	{
	    "PlainLanguageID": 28,
	    "PlainLanguageName": "DIAGNOSTIC TEST TO MEASURE YOUR OXYGEN LEVEL"
	},
	{
	    "PlainLanguageID": 30,
	    "PlainLanguageName": "DIAGNOSTIC TEST, BONE SCAN"
	},
	{
	    "PlainLanguageID": 31,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAPSULENDOSCOPY"
	},
	{
	    "PlainLanguageID": 32,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN"
	},
	{
	    "PlainLanguageID": 33,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN AND DUPLEX CAROTID SCAN"
	},
	{
	    "PlainLanguageID": 34,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN AND PET SCAN"
	},
	{
	    "PlainLanguageID": 35,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN AND THYROID UPTAKE"
	},
	{
	    "PlainLanguageID": 36,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN NEEDLE GUIDED BIOPSY"
	},
	{
	    "PlainLanguageID": 37,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN OF ABDOMEN AND PELVIS"
	},
	{
	    "PlainLanguageID": 38,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN OF THE BRAIN"
	},
	{
	    "PlainLanguageID": 39,
	    "PlainLanguageName": "DIAGNOSTIC TEST, CAT SCAN OF THE SPINE"
	},
	{
	    "PlainLanguageID": 40,
	    "PlainLanguageName": "DIAGNOSTIC TEST, COGNITIVE"
	},
	{
	    "PlainLanguageID": 41,
	    "PlainLanguageName": "DIAGNOSTIC TEST, DUPLEX SCAN"
	},
	{
	    "PlainLanguageID": 42,
	    "PlainLanguageName": "DIAGNOSTIC TEST, EEG - A BRAIN WAVE PATTERNS TEST"
	},
	{
	    "PlainLanguageID": 43,
	    "PlainLanguageName": "DIAGNOSTIC TEST, EGD - UPPER ENDOSCOPY"
	},
	{
	    "PlainLanguageID": 44,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ENG - INNER EAR"
	},
	{
	    "PlainLanguageID": 45,
	    "PlainLanguageName": "DIAGNOSTIC TEST, FIBROSCAN"
	},
	{
	    "PlainLanguageID": 46,
	    "PlainLanguageName": "DIAGNOSTIC TEST, GASTROINTESTINAL IMAGING"
	},
	{
	    "PlainLanguageID": 47,
	    "PlainLanguageName": "DIAGNOSTIC TEST, GUIDED BIOPSY AND ULTRASOUND"
	},
	{
	    "PlainLanguageID": 48,
	    "PlainLanguageName": "DIAGNOSTIC TEST, H. PYLORIC BREATH TEST"
	},
	{
	    "PlainLanguageID": 49,
	    "PlainLanguageName": "DIAGNOSTIC TEST, KIDNEY IMAGING"
	},
	{
	    "PlainLanguageID": 50,
	    "PlainLanguageName": "DIAGNOSTIC TEST, LOWER ENDOSCOPY"
	},
	{
	    "PlainLanguageID": 53,
	    "PlainLanguageName": "DIAGNOSTIC TEST, MAMMOGRAM"
	},
	{
	    "PlainLanguageID": 51,
	    "PlainLanguageName": "DIAGNOSTIC TEST, MRA - MAGNETIC RESONANCE ANGIOGRAM"
	},
	{
	    "PlainLanguageID": 52,
	    "PlainLanguageName": "DIAGNOSTIC TEST, MRI - MAGNETIC RESONANCE IMAGING"
	},
	{
	    "PlainLanguageID": 54,
	    "PlainLanguageName": "DIAGNOSTIC TEST, NERVE CONDUCTION STUDY"
	},
	{
	    "PlainLanguageID": 55,
	    "PlainLanguageName": "DIAGNOSTIC TEST, NUCLEAR MEDICINE"
	},
	{
	    "PlainLanguageID": 56,
	    "PlainLanguageName": "DIAGNOSTIC TEST, OF THE LIVER"
	},
	{
	    "PlainLanguageID": 57,
	    "PlainLanguageName": "DIAGNOSTIC TEST, PET SCAN"
	},
	{
	    "PlainLanguageID": 29,
	    "PlainLanguageName": "DIAGNOSTIC TEST, PFT - PULMONARY (LUNG) TEST"
	},
	{
	    "PlainLanguageID": 58,
	    "PlainLanguageName": "DIAGNOSTIC TEST, PFT - PULMONARY FUNCTION TEST OF YOUR LUNGS"
	},
	{
	    "PlainLanguageID": 59,
	    "PlainLanguageName": "DIAGNOSTIC TEST, STRESS TEST"
	},
	{
	    "PlainLanguageID": 60,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ULTRASOUND"
	},
	{
	    "PlainLanguageID": 61,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ULTRASOUND AND MAMMOGRAM"
	},
	{
	    "PlainLanguageID": 62,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ULTRASOUND GUIDED BREAST BIOPSY"
	},
	{
	    "PlainLanguageID": 63,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ULTRASOUND NEEDLE GUIDED BIOPSY "
	},
	{
	    "PlainLanguageID": 64,
	    "PlainLanguageName": "DIAGNOSTIC TEST, ULTRASOUND OF THE HEART - ECHOCARDIOGRAM"
	},
	{
	    "PlainLanguageID": 65,
	    "PlainLanguageName": "DIAGNOSTIC TEST, VIDEO SWALLOW"
	},
	{
	    "PlainLanguageID": 66,
	    "PlainLanguageName": "DIAGNOSTIC TEST, WHOLE BODY IMAGING "
	},
	{
	    "PlainLanguageID": 67,
	    "PlainLanguageName": "DIAGNOSTIC TESTING BREAST BIOPSY AND ULTRASOUND"
	},
	{
	    "PlainLanguageID": 68,
	    "PlainLanguageName": "DIAGNOSTIC TESTING UPPER GI TRACT AND ULTRASOUND"
	},
	{
	    "PlainLanguageID": 69,
	    "PlainLanguageName": "DIAGNOSTIC TESTING, A BONE SCAN AND MAMMOGRAM"
	},
	{
	    "PlainLanguageID": 70,
	    "PlainLanguageName": "DIAGNOSTIC TESTING, MRI - MAGNETIC RESONANCE IMAGING AND A DUPLEX SCAN; AND SPINAL SURGERY"
	},
	{
	    "PlainLanguageID": 71,
	    "PlainLanguageName": "DIAGNOSTIC TESTING, UPPER GI TRACT AND CAT SCAN"
	},
	{
	    "PlainLanguageID": 72,
	    "PlainLanguageName": "DOPPLER STUDY"
	},
	{
	    "PlainLanguageID": 73,
	    "PlainLanguageName": "ELECTRIC WHEELCHAIR REPAIR OF THE AXEL"
	},
	{
	    "PlainLanguageID": 74,
	    "PlainLanguageName": "EXTERNAL DEFIBRILLATOR -LIFE VEST"
	},
	{
	    "PlainLanguageID": 75,
	    "PlainLanguageName": "HEART CATHETERIZATION"
	},
	{
	    "PlainLanguageID": 76,
	    "PlainLanguageName": "HEART MONITOR"
	},
	{
	    "PlainLanguageID": 77,
	    "PlainLanguageName": "HOME HEALTH AIDE, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS, PHYSICAL THERAPY, 1 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS AND OCCUPATION THERAPY, 1 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS "
	},
	{
	    "PlainLanguageID": 78,
	    "PlainLanguageName": "HOME HEALTH, CASEWORKER VISIT"
	},
	{
	    "PlainLanguageID": 79,
	    "PlainLanguageName": "HOSPITAL BED, TRAPEZE AND SKIN PROTECTION CUSHION"
	},
	{
	    "PlainLanguageID": 80,
	    "PlainLanguageName": "HOSPITAL BED, WHEELCHAIR AND COMMODE"
	},
	{
	    "PlainLanguageID": 81,
	    "PlainLanguageName": "HOSPITAL BED, WHEELCHAIR, COMMODE, AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 82,
	    "PlainLanguageName": "INFUSION SUPPLIES "
	},
	{
	    "PlainLanguageID": 83,
	    "PlainLanguageName": "IV FLUID ADMINISTRATION"
	},
	{
	    "PlainLanguageID": 84,
	    "PlainLanguageName": "KNEE BRACE"
	},
	{
	    "PlainLanguageID": 85,
	    "PlainLanguageName": "LAB WORK"
	},
	{
	    "PlainLanguageID": 86,
	    "PlainLanguageName": "LUMBAR SUPPORT"
	},
	{
	    "PlainLanguageID": 87,
	    "PlainLanguageName": "MEDICAL SUPPLIES"
	},
	{
	    "PlainLanguageID": 88,
	    "PlainLanguageName": "MEDICINE AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 89,
	    "PlainLanguageName": "MOBILE CARDIAC OUTPATIENT HEART MONITOR"
	},
	{
	    "PlainLanguageID": 90,
	    "PlainLanguageName": "MOHS SURGERY"
	},
	{
	    "PlainLanguageID": 91,
	    "PlainLanguageName": "NIGHT SPLINT"
	},
	{
	    "PlainLanguageID": 92,
	    "PlainLanguageName": "NUCLEAR STUDY OF THE HEART"
	},
	{
	    "PlainLanguageID": 93,
	    "PlainLanguageName": "OFFICE VISIT"
	},
	{
	    "PlainLanguageID": 94,
	    "PlainLanguageName": "OFFICE VISIT AND A BLADDER PROCEDURE"
	},
	{
	    "PlainLanguageID": 95,
	    "PlainLanguageName": "OFFICE VISIT AND A PROCEDURE, EPIDURAL INJECTIONS"
	},
	{
	    "PlainLanguageID": 96,
	    "PlainLanguageName": "OFFICE VISIT AND A PROCEDURE, INJECTIONS"
	},
	{
	    "PlainLanguageID": 97,
	    "PlainLanguageName": "OFFICE VISIT AND A PROCEDURE, JOINT INJECTIONS"
	},
	{
	    "PlainLanguageID": 98,
	    "PlainLanguageName": "OFFICE VISIT AND BIOPSIES"
	},
	{
	    "PlainLanguageID": 99,
	    "PlainLanguageName": "OFFICE VISIT AND INFUSION PUMP"
	},
	{
	    "PlainLanguageID": 100,
	    "PlainLanguageName": "OFFICE VISIT AND INFUSIONS"
	},
	{
	    "PlainLanguageID": 101,
	    "PlainLanguageName": "OFFICE VISIT AND LUMBAR FACET INJECTIONS"
	},
	{
	    "PlainLanguageID": 102,
	    "PlainLanguageName": "OFFICE VISIT AND MEDICATION"
	},
	{
	    "PlainLanguageID": 103,
	    "PlainLanguageName": "OFFICE VISIT AND NEEDLE GUIDANCE TEST"
	},
	{
	    "PlainLanguageID": 104,
	    "PlainLanguageName": "OFFICE VISIT AND TESTING"
	},
	{
	    "PlainLanguageID": 105,
	    "PlainLanguageName": "OFFICE VISIT AND TREATMENT"
	},
	{
	    "PlainLanguageID": 106,
	    "PlainLanguageName": "OFFICE VISIT AND TRIGGER POINT INJECTIONS"
	},
	{
	    "PlainLanguageID": 107,
	    "PlainLanguageName": "OFFICE VISITS"
	},
	{
	    "PlainLanguageID": 108,
	    "PlainLanguageName": "OFFICE VISITS AND A BLADDER PROCEDURE"
	},
	{
	    "PlainLanguageID": 109,
	    "PlainLanguageName": "OFFICE VISITS AND A PROCEDURE, EPIDURAL INJECTIONS"
	},
	{
	    "PlainLanguageID": 110,
	    "PlainLanguageName": "OFFICE VISITS AND INJECTIONS"
	},
	{
	    "PlainLanguageID": 111,
	    "PlainLanguageName": "OFFICE VISITS AND LAB WORK"
	},
	{
	    "PlainLanguageID": 112,
	    "PlainLanguageName": "ORTHOTIC DEVICE"
	},
	{
	    "PlainLanguageID": 113,
	    "PlainLanguageName": "OSTOMY SUPPLIES"
	},
	{
	    "PlainLanguageID": 114,
	    "PlainLanguageName": "OXYGEN CONCENTRATOR"
	},
	{
	    "PlainLanguageID": 115,
	    "PlainLanguageName": "OXYGEN EQUIPMENT"
	},
	{
	    "PlainLanguageID": 116,
	    "PlainLanguageName": "OXYGEN EQUIPMENT AND BREATHING MACHINE - NEBULIZER"
	},
	{
	    "PlainLanguageID": 117,
	    "PlainLanguageName": "OXYGEN EQUIPMENT AND CPAP SUPPLIES"
	},
	{
	    "PlainLanguageID": 118,
	    "PlainLanguageName": "OXYGEN EQUIPMENT AND WHEEL CHAIR"
	},
	{
	    "PlainLanguageID": 119,
	    "PlainLanguageName": "PARTS FOR YOUR PROSTHETIC ARM"
	},
	{
	    "PlainLanguageID": 120,
	    "PlainLanguageName": "PESSARY INSERTION"
	},
	{
	    "PlainLanguageID": 121,
	    "PlainLanguageName": "PHYSCIAL THERAPY, 2 VISITS FOR 1 WEEK"
	},
	{
	    "PlainLanguageID": 122,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 1 RE EVALUATION AND 3 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 11 VISITS"
	},
	{
	    "PlainLanguageID": 123,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 1 RE-EVALUATION AND 3 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 8 VISITS"
	},
	{
	    "PlainLanguageID": 124,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT FOR 1 WEEK, FOR A TOTAL OF 2 VISITS"
	},
	{
	    "PlainLanguageID": 125,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS, OCCUPATIONAL THERAPY, 1 EVALUATION AND 1 VISIT FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS, AND HOME HEALTH AIDE, 1 VISIT FOR THE FIRST WEEK AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 126,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT, FOR A TOTAL OF 2 VISITS"
	},
	{
	    "PlainLanguageID": 127,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS AND THE LAST WEEK 1 MORE VISIT, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 128,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 129,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 2 WEEKS, FOR A TOTAL OF 3 VISITS "
	},
	{
	    "PlainLanguageID": 130,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 9 VISITS"
	},
	{
	    "PlainLanguageID": 131,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK, FOR 6 WEEKS, FOR A TOTAL OF 13 VISITS,"
	},
	{
	    "PlainLanguageID": 132,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS FOR 1 WEEK, FOR A TOTAL OF 3 VISITS"
	},
	{
	    "PlainLanguageID": 133,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 3 VISITS A WEEK FOR 3 WEEKS AND 2 MORE VISITS THE LAST WEEK, FOR A TOTAL OF 12 VISITS"
	},
	{
	    "PlainLanguageID": 134,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATION AND 3 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 13 VISITS"
	},
	{
	    "PlainLanguageID": 135,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 EVALUATON AND 2 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 9 VISITS"
	},
	{
	    "PlainLanguageID": 136,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 4 VISITS,"
	},
	{
	    "PlainLanguageID": 137,
	    "PlainLanguageName": "PHYSICAL THERAPY, 1 VISIT A WEEK, FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 138,
	    "PlainLanguageName": "PHYSICAL THERAPY, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS "
	},
	{
	    "PlainLanguageID": 139,
	    "PlainLanguageName": "PHYSICAL THERAPY, 2 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 8 VISITS"
	},
	{
	    "PlainLanguageID": 140,
	    "PlainLanguageName": "PHYSICAL THERAPY, 2 VISITS A WEEK FOR 6 WEEKS, FOR A TOTAL OF 12 VISITS"
	},
	{
	    "PlainLanguageID": 141,
	    "PlainLanguageName": "PHYSICAL THERAPY, 3 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 9 VISITS"
	},
	{
	    "PlainLanguageID": 142,
	    "PlainLanguageName": "POWER CHAIR AND BATTERIES"
	},
	{
	    "PlainLanguageID": 143,
	    "PlainLanguageName": "PROCEDURE, ANALYSIS OF IMPLANTED PULSE GENERATOR SYSTEM"
	},
	{
	    "PlainLanguageID": 144,
	    "PlainLanguageName": "PROCEDURE, BACK INJECTIONS"
	},
	{
	    "PlainLanguageID": 145,
	    "PlainLanguageName": "PROCEDURE, BLADDER OPERATION"
	},
	{
	    "PlainLanguageID": 146,
	    "PlainLanguageName": "PROCEDURE, CARDIAC"
	},
	{
	    "PlainLanguageID": 147,
	    "PlainLanguageName": "PROCEDURE, CARDIOVERSION"
	},
	{
	    "PlainLanguageID": 148,
	    "PlainLanguageName": "PROCEDURE, CARPAL TUNNEL"
	},
	{
	    "PlainLanguageID": 149,
	    "PlainLanguageName": "PROCEDURE, CATARACT REMOVAL "
	},
	{
	    "PlainLanguageID": 150,
	    "PlainLanguageName": "PROCEDURE, COLONOSCOPY"
	},
	{
	    "PlainLanguageID": 151,
	    "PlainLanguageName": "PROCEDURE, COLONOSCOPY AND EGD - UPPER ENDOSCOPY"
	},
	{
	    "PlainLanguageID": 152,
	    "PlainLanguageName": "PROCEDURE, CRP - CANALITH REPOSITIONING"
	},
	{
	    "PlainLanguageID": 153,
	    "PlainLanguageName": "PROCEDURE, CYSTOURETHROSCOPY"
	},
	{
	    "PlainLanguageID": 154,
	    "PlainLanguageName": "PROCEDURE, DALTEPARIN INJECTION"
	},
	{
	    "PlainLanguageID": 155,
	    "PlainLanguageName": "PROCEDURE, DIPHENHYDRAMINE INJECTIONS"
	},
	{
	    "PlainLanguageID": 156,
	    "PlainLanguageName": "PROCEDURE, EPIDURAL INJECTIONS"
	},
	{
	    "PlainLanguageID": 157,
	    "PlainLanguageName": "PROCEDURE, EXAMINES THE PANCREATIC AND BILE DUCTS"
	},
	{
	    "PlainLanguageID": 158,
	    "PlainLanguageName": "PROCEDURE, FACET JOINT INJECTIONS"
	},
	{
	    "PlainLanguageID": 159,
	    "PlainLanguageName": "PROCEDURE, FINE NEEDLE ASPIRATION OF EAR"
	},
	{
	    "PlainLanguageID": 160,
	    "PlainLanguageName": "PROCEDURE, GALL BLADDER AND PANCREAS TEST"
	},
	{
	    "PlainLanguageID": 161,
	    "PlainLanguageName": "PROCEDURE, GUIDED EIPDURAL INJECTION"
	},
	{
	    "PlainLanguageID": 162,
	    "PlainLanguageName": "PROCEDURE, INJECTIONS"
	},
	{
	    "PlainLanguageID": 163,
	    "PlainLanguageName": "PROCEDURE, INSERT OF CENTRAL LINE"
	},
	{
	    "PlainLanguageID": 164,
	    "PlainLanguageName": "PROCEDURE, IRON INJECTIONS"
	},
	{
	    "PlainLanguageID": 165,
	    "PlainLanguageName": "PROCEDURE, JOINT INJECTION,"
	},
	{
	    "PlainLanguageID": 166,
	    "PlainLanguageName": "PROCEDURE, LEFT SHOULDER ROTATOR CUFF"
	},
	{
	    "PlainLanguageID": 167,
	    "PlainLanguageName": "PROCEDURE, LEVAQUIN INJECTIONS"
	},
	{
	    "PlainLanguageID": 168,
	    "PlainLanguageName": "PROCEDURE, LITHOTRIPSY - MEDICAL PROCEDURE FOR KIDNEY STONES"
	},
	{
	    "PlainLanguageID": 169,
	    "PlainLanguageName": "PROCEDURE, LIVER BIOPSY"
	},
	{
	    "PlainLanguageID": 170,
	    "PlainLanguageName": "PROCEDURE, LUMBAR FACET INJECTIONS"
	},
	{
	    "PlainLanguageID": 171,
	    "PlainLanguageName": "PROCEDURE, MUSCLE BIOPSY AND ULTRASOUND "
	},
	{
	    "PlainLanguageID": 172,
	    "PlainLanguageName": "PROCEDURE, NEEDLE GUIDED INJECTIONS"
	},
	{
	    "PlainLanguageID": 173,
	    "PlainLanguageName": "PROCEDURE, NEUPOGEN INJECTION"
	},
	{
	    "PlainLanguageID": 174,
	    "PlainLanguageName": "PROCEDURE, PAIN MEDICATION INJECTIONS"
	},
	{
	    "PlainLanguageID": 175,
	    "PlainLanguageName": "PROCEDURE, PICC LINE AND ANTIBIOTIC ADMINISTRATION "
	},
	{
	    "PlainLanguageID": 176,
	    "PlainLanguageName": "PROCEDURE, PORT FLUSH"
	},
	{
	    "PlainLanguageID": 177,
	    "PlainLanguageName": "PROCEDURE, PORT PLACEMENT"
	},
	{
	    "PlainLanguageID": 178,
	    "PlainLanguageName": "PROCEDURE, PORT REMOVAL"
	},
	{
	    "PlainLanguageID": 179,
	    "PlainLanguageName": "PROCEDURE, PROCRIT INJECTION "
	},
	{
	    "PlainLanguageID": 180,
	    "PlainLanguageName": "PROCEDURE, PROLIA INJECTION"
	},
	{
	    "PlainLanguageID": 181,
	    "PlainLanguageName": "PROCEDURE, REMOVAL LEFT THIGH MASS"
	},
	{
	    "PlainLanguageID": 182,
	    "PlainLanguageName": "PROCEDURE, REMOVAL OF LESION"
	},
	{
	    "PlainLanguageID": 183,
	    "PlainLanguageName": "PROCEDURE, REMOVAL OF LESIONS ON THE FACE"
	},
	{
	    "PlainLanguageID": 184,
	    "PlainLanguageName": "PROCEDURE, REMOVAL OF SKIN LESIONS"
	},
	{
	    "PlainLanguageID": 185,
	    "PlainLanguageName": "PROCEDURE, SACROILIAC JOINT INJECTIONS"
	},
	{
	    "PlainLanguageID": 186,
	    "PlainLanguageName": "PROCEDURE, SESAMOIDECTOMY, REMOVAL IMPLANT"
	},
	{
	    "PlainLanguageID": 187,
	    "PlainLanguageName": "PROCEDURE, THERAPEUTIC RADIOLOGY"
	},
	{
	    "PlainLanguageID": 188,
	    "PlainLanguageName": "PROCEDURE, THORACENTESIS"
	},
	{
	    "PlainLanguageID": 189,
	    "PlainLanguageName": "PROCEDURE, THYROID BIOPSY"
	},
	{
	    "PlainLanguageID": 190,
	    "PlainLanguageName": "PROCEDURE, TRIGGER POINT INJECTION"
	},
	{
	    "PlainLanguageID": 191,
	    "PlainLanguageName": "PROCEDURE, TYMPANOSTOMY - INSERTION OF EAR TUBES"
	},
	{
	    "PlainLanguageID": 192,
	    "PlainLanguageName": "PROCEDURE, ULTRA SOUND GUIDANCE"
	},
	{
	    "PlainLanguageID": 193,
	    "PlainLanguageName": "PROCEDURE, ULTRASOUND GUIDED BIOPSY"
	},
	{
	    "PlainLanguageID": 194,
	    "PlainLanguageName": "PROCEDURE, ULTRASOUND GUIDED BIOPSY OF THYROID"
	},
	{
	    "PlainLanguageID": 195,
	    "PlainLanguageName": "PROCEDURE, ZOFRAN INJECTIONS"
	},
	{
	    "PlainLanguageID": 196,
	    "PlainLanguageName": "PROTHETIC DEVICE"
	},
	{
	    "PlainLanguageID": 197,
	    "PlainLanguageName": "PUMP CHANGE AND CHEMOTHERAPY ADMINISTRATION"
	},
	{
	    "PlainLanguageID": 198,
	    "PlainLanguageName": "RADIATION THERAPY"
	},
	{
	    "PlainLanguageID": 199,
	    "PlainLanguageName": "RENEWAL -  BREATHING MACHINE - CPAP AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 200,
	    "PlainLanguageName": "RENEWAL -  BREATHING MACHINE - NEBULIZER"
	},
	{
	    "PlainLanguageID": 201,
	    "PlainLanguageName": "RENEWAL -  HOSPITAL BED, WHEELCHAIR, COMMODE, AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 202,
	    "PlainLanguageName": "RENEWAL -  MACHINE FOR BREATHING - NEBULIZER AND OXYGEN EQUIPMENT"
	},
	{
	    "PlainLanguageID": 203,
	    "PlainLanguageName": "RENEWAL -  POWER CHAIR AND BATTERIES"
	},
	{
	    "PlainLanguageID": 204,
	    "PlainLanguageName": "RENEWAL -  WHEELCHAIR"
	},
	{
	    "PlainLanguageID": 205,
	    "PlainLanguageName": "RENEWAL - BREAST PROSTHESIS AND MASTECTOMY BRAS AND CAMISOLE"
	},
	{
	    "PlainLanguageID": 206,
	    "PlainLanguageName": "RENEWAL - CATHETERS AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 207,
	    "PlainLanguageName": "RENEWAL - CPAP SUPPLIES FOR YOUR BREATHING MACHINE"
	},
	{
	    "PlainLanguageID": 208,
	    "PlainLanguageName": "RENEWAL - DIABETIC SUPPLIES"
	},
	{
	    "PlainLanguageID": 209,
	    "PlainLanguageName": "RENEWAL - HOSPITAL BED, TRAPEZE AND SKIN PROTECTION CUSHION"
	},
	{
	    "PlainLanguageID": 210,
	    "PlainLanguageName": "RENEWAL - MACHINE FOR YOUR BREATHING - CPAP MACHINE AND A HEATED HUMIDIFIER"
	},
	{
	    "PlainLanguageID": 211,
	    "PlainLanguageName": "RENEWAL - MEDICAL SUPPLIES"
	},
	{
	    "PlainLanguageID": 212,
	    "PlainLanguageName": "RENEWAL - OXYGEN CONCENTRATOR"
	},
	{
	    "PlainLanguageID": 213,
	    "PlainLanguageName": "RENEWAL - OXYGEN EQUIPMENT"
	},
	{
	    "PlainLanguageID": 214,
	    "PlainLanguageName": "RENEWAL - OXYGEN EQUIPMENT AND A BREATHING MACHINE - NEBULIZER "
	},
	{
	    "PlainLanguageID": 215,
	    "PlainLanguageName": "RENEWAL - OXYGEN EQUIPMENT AND CPAP SUPPLIES "
	},
	{
	    "PlainLanguageID": 216,
	    "PlainLanguageName": "RENEWAL - OXYGEN EQUIPMENT AND WHEEL CHAIR"
	},
	{
	    "PlainLanguageID": 217,
	    "PlainLanguageName": "RENEWAL - TENS UNIT"
	},
	{
	    "PlainLanguageID": 218,
	    "PlainLanguageName": "RENEWAL - TRANSPORT CHAIR"
	},
	{
	    "PlainLanguageID": 219,
	    "PlainLanguageName": "RENEWAL - UROLOGICAL SUPPLIES"
	},
	{
	    "PlainLanguageID": 220,
	    "PlainLanguageName": "RENEWAL - WHEELCHAIR AND SUPPLIES "
	},
	{
	    "PlainLanguageID": 221,
	    "PlainLanguageName": "RENEWAL - WOUND CARE SUPPLIES"
	},
	{
	    "PlainLanguageID": 222,
	    "PlainLanguageName": "RENEWAL - WOUND VACUUM AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 223,
	    "PlainLanguageName": "ROLLABOUT CHAIR"
	},
	{
	    "PlainLanguageID": 224,
	    "PlainLanguageName": "SALINE FLUSH, CATHETER CARE AND INFUSION SUPPLIES"
	},
	{
	    "PlainLanguageID": 225,
	    "PlainLanguageName": "SEAT ATTACHMENT"
	},
	{
	    "PlainLanguageID": 226,
	    "PlainLanguageName": "SEVERAL INJECTIONS, HERCEPTIN, NAVELBINE AND ZOFRAN"
	},
	{
	    "PlainLanguageID": 227,
	    "PlainLanguageName": "SEVERAL PROCEDURES - REMOVAL OF SKIN CANCER ON CHIN, RIGHT ARM, RIGHT TEMPLE"
	},
	{
	    "PlainLanguageID": 228,
	    "PlainLanguageName": "SKILLED NURSING - 1 EVALUATION AND 6 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 13 VISITS"
	},
	{
	    "PlainLanguageID": 229,
	    "PlainLanguageName": "SKILLED NURSING - 1 VISIT"
	},
	{
	    "PlainLanguageID": 230,
	    "PlainLanguageName": "SKILLED NURSING - 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 231,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUAITON AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 1 WEEK, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 232,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 233,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 234,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY 1 EVALUATION AND 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 235,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT THE FIRST WEEK AND 2 VISITS FOR THE NEXT 2 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 236,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 237,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 238,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS FOR 2 WEEKS AND THEN 1 VISIT FOR THE NEXT WEEK, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 239,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT FOR THE FIRST WEEK, 3 VISITS A WEEK FOR THE SECOND WEEK, 2 VISITS A WEEK FOR THE THIRD WEEK AND 1 VISIT A WEEK FOR 6 WEEKS, FOR A TOTAL OF 13 VISITS"
	},
	{
	    "PlainLanguageID": 240,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT FOR THE FIRST WEEK, AND 2 VISITS PER WEEK FOR 3 WEEKS, FOR A TOTAL OF 8 VISITS"
	},
	{
	    "PlainLanguageID": 241,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT PER WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 242,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 1 VISIT PER WEEK FOR 4 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT FOR THE FIRST WEEK AND 2 VISITS FOR THE NEXT 2 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 243,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS AND ON THE LAST WEEK 1 MORE VISIT, FOR A TOTAL OF 6 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS AND FOR THE LAST WEEK ONE MORE VISIT, FOR A TOTAL OF 8 VISITS AND HOME HEALTH AIDE, 1 VISIT A WEEK FOR 2 WEEKS, FOR A TOTAL OF 2 VISITS"
	},
	{
	    "PlainLanguageID": 244,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 245,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 246,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS AND FOR THE LAST WEEK ONE MORE VISIT, FOR A TOTAL OF 8 VISITS "
	},
	{
	    "PlainLanguageID": 247,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS"
	},
	{
	    "PlainLanguageID": 248,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND PHYSICAL THERAPY, 1 EVALUATON AND 2 VISITS PER WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS "
	},
	{
	    "PlainLanguageID": 249,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS; PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK, FOR 2 WEEKS FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 250,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR TOTAL OF 5 VISITS, AND PHYSICAL THERAPY 1 EVALUATION AND 2 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 251,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 5 VISITS, PHYSICAL THERAPY 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS, AND OCCUPATIONAL THERAPY 1 EVALUATION AND 2 VISITS PER WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS"
	},
	{
	    "PlainLanguageID": 252,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS"
	},
	{
	    "PlainLanguageID": 253,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 7 VISITS; PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 6 VISITS AND HOME HEALTH AIDE, 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 4 VISITS"
	},
	{
	    "PlainLanguageID": 254,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 5 WEEKS, FOR A TOTAL OF 11 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 5 WEEKS, FOR A TOTAL OF 11 VISITS"
	},
	{
	    "PlainLanguageID": 255,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 5 WEEKS, FOR A TOTAL OF 11 VISITS AND PHYSICAL THERAPY, 1 EVALUATION AND 2 VISITS A WEEK FOR 5 WEEKS, FOR A TOTAL OF 11 VISITS"
	},
	{
	    "PlainLanguageID": 256,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS A WEEK FOR 6 WEEKS, FOR A TOTAL OF 7 VISITS"
	},
	{
	    "PlainLanguageID": 257,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 2 VISITS FOR 2 WEEKS, FOR A TOTAL OF 5 VISITS AND OCCUPATIONAL THERAPY, 1 EVALUATION AND 1 VISIT THE FIRST WEEK AND 1 VISITS FOR THE NEXT 4 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 258,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 4 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 17 VISITS; PHYSICAL THERAPY, 1 EVALUATION AND 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 6 VISITS AND A SOCIAL EVALUATION"
	},
	{
	    "PlainLanguageID": 259,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 5 VISITS FOR 1 WEEK THEN 4 VISITS A WEEK FOR 4 WEEKS, AND 3 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 31 VISITS"
	},
	{
	    "PlainLanguageID": 260,
	    "PlainLanguageName": "SKILLED NURSING, 1 EVALUATION AND 7 VISITS A WEEK FOR 2 WEEKS, FOR A TOTAL OF 15 VISITS"
	},
	{
	    "PlainLanguageID": 261,
	    "PlainLanguageName": "SKILLED NURSING, 1 TIME A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS"
	},
	{
	    "PlainLanguageID": 262,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 2 WEEKS, FOR A TOTAL OF 2 VISITS"
	},
	{
	    "PlainLanguageID": 263,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 2 WEEKS, FOR A TOTAL OF 2 VISITS AND PHYSICAL THERAPY, 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 264,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS"
	},
	{
	    "PlainLanguageID": 265,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 4 WEEKS, FOR A TOTAL OF 4 VISITS"
	},
	{
	    "PlainLanguageID": 266,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 267,
	    "PlainLanguageName": "SKILLED NURSING, 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS AND OCCUPATIONAL THERAPY, 1 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 268,
	    "PlainLanguageName": "SKILLED NURSING, 2 TIMES A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 269,
	    "PlainLanguageName": "SKILLED NURSING, 2 TIMES A WEEK FOR 5 WEEKS, FOR A TOTAL OF 10 VISITS AND HOME HEALTH AIDE, 2 TIMES A WEEK FOR 8 WEEKS, FOR A TOTAL OF 16 VISITS"
	},
	{
	    "PlainLanguageID": 270,
	    "PlainLanguageName": "SKILLED NURSING, 2 VISIT A WEEK FOR 2 WEEKS AND 1 MORE VISIT THE LAST WEEK, FOR A TOTAL OF 5 VISITS"
	},
	{
	    "PlainLanguageID": 271,
	    "PlainLanguageName": "SKILLED NURSING, 2 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS, PHYSICAL THERAPY, 1 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS, OCCUPATIONAL THERAPY, 1 VISIT A WEEK FOR 3 WEEKS, FOR A TOTAL OF 3 VISITS AND HOME HEALTH AIDE, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 272,
	    "PlainLanguageName": "SKILLED NURSING, 2 VISIT A WEEK FOR 5 WEEKS, FOR A TOTAL OF 10 VISITS"
	},
	{
	    "PlainLanguageID": 273,
	    "PlainLanguageName": "SKILLED NURSING, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS, PHYSICAL THERAPY, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS AND HOME HEALTH AIDE, 2 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 6 VISITS"
	},
	{
	    "PlainLanguageID": 274,
	    "PlainLanguageName": "SKILLED NURSING, 3 VISITS  A WEEK FOR 7 WEEKS, FOR TOTAL OF 21 VISITS"
	},
	{
	    "PlainLanguageID": 275,
	    "PlainLanguageName": "SKILLED NURSING, 3 VISITS A WEEK FOR 4 WEEKS, FOR A TOTAL OF 12 VISITS"
	},
	{
	    "PlainLanguageID": 276,
	    "PlainLanguageName": "SKILLED NURSING, 3 VISITS FOR 1 WEEK, FOR A TOTAL OF 3 VISITS"
	},
	{
	    "PlainLanguageID": 277,
	    "PlainLanguageName": "SKILLED NURSING, 3 VISITS PER WEEK FOR 2 WEEKS WITH 1 MORE VISIT ON THE THIRD WEEK, FOR A TOTAL OF 7 VISITS"
	},
	{
	    "PlainLanguageID": 278,
	    "PlainLanguageName": "SKILLED NURSING, 4 VISITS A WEEK FOR 3 WEEKS, FOR A TOTAL OF 12 VISITS"
	},
	{
	    "PlainLanguageID": 279,
	    "PlainLanguageName": "SKILLED NURSING, 5 VISITS FOR THE NEXT WEEK"
	},
	{
	    "PlainLanguageID": 280,
	    "PlainLanguageName": "SKILLED NURSING, DAILY FOR 7 DAYS FOR WOUND CARE"
	},
	{
	    "PlainLanguageID": 281,
	    "PlainLanguageName": "SLEEP STUDY"
	},
	{
	    "PlainLanguageID": 282,
	    "PlainLanguageName": "SOCIAL WORKER, 1 VISIT A WEEK FOR 2 WEEKS, FOR A TOTAL OF 2 VISITS"
	},
	{
	    "PlainLanguageID": 283,
	    "PlainLanguageName": "SPEACH THERAPY,  1 EVALUATION AND SWALLOW TEST"
	},
	{
	    "PlainLanguageID": 284,
	    "PlainLanguageName": "STOCKINGS, COMPRESSION"
	},
	{
	    "PlainLanguageID": 285,
	    "PlainLanguageName": "SURGERY - BACK"
	},
	{
	    "PlainLanguageID": 286,
	    "PlainLanguageName": "SURGERY - BREAST "
	},
	{
	    "PlainLanguageID": 287,
	    "PlainLanguageName": "SURGERY - BREAST MASTECTOMY"
	},
	{
	    "PlainLanguageID": 288,
	    "PlainLanguageName": "SURGERY - CARPAL TUNNEL RELEASE"
	},
	{
	    "PlainLanguageID": 289,
	    "PlainLanguageName": "SURGERY - CATARACT"
	},
	{
	    "PlainLanguageID": 290,
	    "PlainLanguageName": "SURGERY - CATHETER INSERTION "
	},
	{
	    "PlainLanguageID": 291,
	    "PlainLanguageName": "SURGERY - CERVICAL"
	},
	{
	    "PlainLanguageID": 292,
	    "PlainLanguageName": "SURGERY - CHOLECYSTECTOMY"
	},
	{
	    "PlainLanguageID": 293,
	    "PlainLanguageName": "SURGERY - EXCISION TO RIGHT SCALP"
	},
	{
	    "PlainLanguageID": 294,
	    "PlainLanguageName": "SURGERY - EYE"
	},
	{
	    "PlainLanguageID": 295,
	    "PlainLanguageName": "SURGERY - GANGLION CYST ON LEFT MIDDLE FINGER"
	},
	{
	    "PlainLanguageID": 296,
	    "PlainLanguageName": "SURGERY - HEMORRHOID REMOVAL"
	},
	{
	    "PlainLanguageID": 297,
	    "PlainLanguageName": "SURGERY - HERNIA REPAIR"
	},
	{
	    "PlainLanguageID": 298,
	    "PlainLanguageName": "SURGERY - INSERTION OF SPINAL STIMULATOR"
	},
	{
	    "PlainLanguageID": 299,
	    "PlainLanguageName": "SURGERY - KYPHOPLASTY,"
	},
	{
	    "PlainLanguageID": 300,
	    "PlainLanguageName": "SURGERY - LOWER LEG"
	},
	{
	    "PlainLanguageID": 301,
	    "PlainLanguageName": "SURGERY - OF THE BOWELS"
	},
	{
	    "PlainLanguageID": 302,
	    "PlainLanguageName": "SURGERY - OF THE LYMPH NODES "
	},
	{
	    "PlainLanguageID": 303,
	    "PlainLanguageName": "SURGERY - PROSTATE "
	},
	{
	    "PlainLanguageID": 304,
	    "PlainLanguageName": "SURGERY - REMOVAL OF A LESION ON THE TONGUE "
	},
	{
	    "PlainLanguageID": 305,
	    "PlainLanguageName": "SURGERY - REMOVAL OF BREAST IMPLANT"
	},
	{
	    "PlainLanguageID": 306,
	    "PlainLanguageName": "SURGERY - SHOULDER"
	},
	{
	    "PlainLanguageID": 307,
	    "PlainLanguageName": "SURGERY - STENT PLACEMENT"
	},
	{
	    "PlainLanguageID": 308,
	    "PlainLanguageName": "SURGERY - STENT PLACEMENT"
	},
	{
	    "PlainLanguageID": 309,
	    "PlainLanguageName": "SURGERY - TOE"
	},
	{
	    "PlainLanguageID": 310,
	    "PlainLanguageName": "SURGERY - TOTAL HIP"
	},
	{
	    "PlainLanguageID": 311,
	    "PlainLanguageName": "SURGERY - TOTAL KNEE"
	},
	{
	    "PlainLanguageID": 312,
	    "PlainLanguageName": "SURGERY - TRIGGER FINGER RELEASE"
	},
	{
	    "PlainLanguageID": 313,
	    "PlainLanguageName": "SURGERY - TUMOR REMOVAL"
	},
	{
	    "PlainLanguageID": 314,
	    "PlainLanguageName": "TENS UNIT"
	},
	{
	    "PlainLanguageID": 315,
	    "PlainLanguageName": "TESTING OF YOUR LUNGS, TO SEE HOW WELL THEY WORK"
	},
	{
	    "PlainLanguageID": 316,
	    "PlainLanguageName": "TESTING, CONGITIVE TEST"
	},
	{
	    "PlainLanguageID": 317,
	    "PlainLanguageName": "TESTING, LAB WORK"
	},
	{
	    "PlainLanguageID": 318,
	    "PlainLanguageName": "TESTING, PULMONARY (LUNG)"
	},
	{
	    "PlainLanguageID": 319,
	    "PlainLanguageName": "TESTING, X-RAY THROAT"
	},
	{
	    "PlainLanguageID": 320,
	    "PlainLanguageName": "THE AMBULATORY SURGERY CENTER FOR YOUR EYE SURGERY"
	},
	{
	    "PlainLanguageID": 321,
	    "PlainLanguageName": "TRANSPORT CHAIR"
	},
	{
	    "PlainLanguageID": 322,
	    "PlainLanguageName": "TREATMENT, WOUND CARE"
	},
	{
	    "PlainLanguageID": 323,
	    "PlainLanguageName": "UROLOGICAL SUPPLIES"
	},
	{
	    "PlainLanguageID": 324,
	    "PlainLanguageName": "VITAMIN B12 INJECTIONS"
	},
	{
	    "PlainLanguageID": 325,
	    "PlainLanguageName": "VITAMIN B12 INJECTIONS AND LABWORK"
	},
	{
	    "PlainLanguageID": 326,
	    "PlainLanguageName": "WALKER"
	},
	{
	    "PlainLanguageID": 327,
	    "PlainLanguageName": "WALKER AND CPM MACHINE"
	},
	{
	    "PlainLanguageID": 328,
	    "PlainLanguageName": "WALKING BOOT"
	},
	{
	    "PlainLanguageID": 329,
	    "PlainLanguageName": "WHEELCHAIR"
	},
	{
	    "PlainLanguageID": 330,
	    "PlainLanguageName": "WHEELCHAIR AND SUPPLIES "
	},
	{
	    "PlainLanguageID": 331,
	    "PlainLanguageName": "WOUND CARE"
	},
	{
	    "PlainLanguageID": 332,
	    "PlainLanguageName": "WOUND CARE AND HYPERBARIC TREATMENTS"
	},
	{
	    "PlainLanguageID": 333,
	    "PlainLanguageName": "WOUND CARE SUPPLIES "
	},
	{
	    "PlainLanguageID": 334,
	    "PlainLanguageName": "WOUND VACUUM AND SUPPLIES"
	},
	{
	    "PlainLanguageID": 335,
	    "PlainLanguageName": "WRIST SPLINT "
	}
];


var ProviderServiceData = [
  {
      "ProfileID": 12,
      "NPINumber": "1116191884",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "Jean",
      "LastName": "Kelly",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mhanleynp@gmail.com"
  },
  {
      "ProfileID": 13,
      "NPINumber": "5252890974",
      "Titles": [
        "MD"
      ],
      "FirstName": "Gregory",
      "LastName": "Roberts",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "ribon.tiffany14@gmail.com"
  },
  {
      "ProfileID": 14,
      "NPINumber": "6466481084",
      "Titles": [
        "MD"
      ],
      "FirstName": "Frank",
      "LastName": "Tucker",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "majoud22@hotmail.com"
  },
  {
      "ProfileID": 15,
      "NPINumber": "4864317044",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "Heather",
      "LastName": "Meyer",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "neerajmahajan24@yahoo.com"
  },
  {
      "ProfileID": 16,
      "NPINumber": "2945709264",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "Ashley",
      "LastName": "Thompson",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "1cardiology1@gmail.com"
  },
  {
      "ProfileID": 18,
      "NPINumber": "8314012394",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "Phillip",
      "LastName": "Weaver",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jchowdappa@gmail.com"
  },
  {
      "ProfileID": 19,
      "NPINumber": "9172378954",
      "Titles": [
        "MD"
      ],
      "FirstName": "Amanda",
      "LastName": "Wallace",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "eliasdamae@hotmail.com"
  },
  {
      "ProfileID": 20,
      "NPINumber": "4722671984",
      "Titles": [
        "MD"
      ],
      "FirstName": "John",
      "LastName": "Dunn",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mounab@live.com"
  },
  {
      "ProfileID": 21,
      "NPINumber": "1836141684",
      "Titles": [
        "MD"
      ],
      "FirstName": "ADA",
      "LastName": "SMITH",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "dawood4@verizon.net"
  },
  {
      "ProfileID": 22,
      "NPINumber": "1356369227",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "BINNO",
      "LastName": "DHAR",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "kulgulnur@yahoo.com"
  },
  {
      "ProfileID": 23,
      "NPINumber": "1257336534",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "Patrick",
      "LastName": "Torres",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "bjdramar@aol.com"
  },
  {
      "ProfileID": 24,
      "NPINumber": "8925135524",
      "Titles": [
        "DO"
      ],
      "FirstName": "Doris",
      "LastName": "Pierce",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "m.blumdo@outlook.com"
  },
  {
      "ProfileID": 26,
      "NPINumber": "4934850774",
      "Titles": [
        "MD"
      ],
      "FirstName": "Douglas",
      "LastName": "Wood",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mlorenzo@accesshealthcarellc.net"
  },
  {
      "ProfileID": 27,
      "NPINumber": "9561633504",
      "Titles": [
        "MD"
      ],
      "FirstName": "Andrea",
      "LastName": "Collins",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "sindhudadlani@yahoo.com"
  },
  {
      "ProfileID": 28,
      "NPINumber": "0518262594",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "Wanda",
      "LastName": "Wright",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jbelisle@accesshealthcarellc.net"
  },
  {
      "ProfileID": 29,
      "NPINumber": "7607980874",
      "Titles": [
        "MD"
      ],
      "FirstName": "OXSVONVKF",
      "LastName": "VROEW",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "vmuehl@accesshealthcarellc.net"
  },
  {
      "ProfileID": 30,
      "NPINumber": "9984870574",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "KSBYVQ",
      "LastName": "JOZYV",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "glopez@accesshealthcarellc.net"
  },
  {
      "ProfileID": 32,
      "NPINumber": "9679222494",
      "Titles": [
        "DO"
      ],
      "FirstName": "OCYT",
      "LastName": "VKOBBKVVSF",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "villarre_j49@yahoo.com"
  },
  {
      "ProfileID": 33,
      "NPINumber": "7435127444",
      "Titles": [
        "MD"
      ],
      "FirstName": "RCOTKB",
      "LastName": "OFKN",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "gulfwestmd@hotmail.com"
  },
  {
      "ProfileID": 34,
      "NPINumber": "0460506134",
      "Titles": [
        "MD"
      ],
      "FirstName": "KDDK",
      "LastName": "DDEL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "drbuttmd@hotmail.com"
  },
  {
      "ProfileID": 35,
      "NPINumber": "6113314314",
      "Titles": [
        "MD"
      ],
      "FirstName": "KRUOB",
      "LastName": "SWYYRL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "rkbhoomi@hotmail.com"
  },
  {
      "ProfileID": 36,
      "NPINumber": "5160359564",
      "Titles": [
        "MD"
      ],
      "FirstName": "XYB",
      "LastName": "GORDKW",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mathewron@yahoo.com"
  },
  {
      "ProfileID": 38,
      "NPINumber": "2150021484",
      "Titles": [
        "MD"
      ],
      "FirstName": "OSUYV",
      "LastName": "BKNXS",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "lokieindar@yahoo.com"
  },
  {
      "ProfileID": 39,
      "NPINumber": "0659173904",
      "Titles": [
        "MD"
      ],
      "FirstName": "NOIC",
      "LastName": "BKNPKC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "syed_safdar@msn.com"
  },
  {
      "ProfileID": 40,
      "NPINumber": "6693996134",
      "Titles": [
        "MD"
      ],
      "FirstName": "BOUCKRLKIKNE",
      "LastName": "INNOB",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "ureddy@hotmail.com"
  },
  {
      "ProfileID": 41,
      "NPINumber": "0552969864",
      "Titles": [
        "MD"
      ],
      "FirstName": "YXXSL",
      "LastName": "BKRN",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "dhar_b@yahoo.com"
  },
  {
      "ProfileID": 42,
      "NPINumber": "6840682094",
      "Titles": [
        "MD"
      ],
      "FirstName": "KZVSRC",
      "LastName": "KRXEM'N",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "officemanagerdrbutt@gmail.com"
  },
  {
      "ProfileID": 43,
      "NPINumber": "8099943304",
      "Titles": [
        "DO"
      ],
      "FirstName": "KSFSVY",
      "LastName": "OSZCOVVSQ",
      "ProviderRelationships": [
        "Employee"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "cmlaughlinmgt@gmail.com"
  },
  {
      "ProfileID": 44,
      "NPINumber": "6867752694",
      "Titles": [
        "MD"
      ],
      "FirstName": "OBNXK",
      "LastName": "CUYYBL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "heartofcc2@gmail.com"
  },
  {
      "ProfileID": 45,
      "NPINumber": "6773688754",
      "Titles": [
        "MD"
      ],
      "FirstName": "EBSRL",
      "LastName": "YSVO-VODKZ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "cmclaughlinmgt@gmail.com"
  },
  {
      "ProfileID": 46,
      "NPINumber": "8909805024",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "KXSKB",
      "LastName": "YWOB",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "remoraina@yahoo.com"
  },
  {
      "ProfileID": 47,
      "NPINumber": "8110598154",
      "Titles": [
        "MD"
      ],
      "FirstName": "KBNXOTKB",
      "LastName": "VKCXKL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "unknown@unknown.com"
  },
  {
      "ProfileID": 49,
      "NPINumber": "2058265824",
      "Titles": [
        "MD"
      ],
      "FirstName": "KVSWBE",
      "LastName": "IBDCSW",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jugroup6@yahoo.com"
  },
  {
      "ProfileID": 50,
      "NPINumber": "5566012394",
      "Titles": [
        "MD"
      ],
      "FirstName": "NKWWKRYW",
      "LastName": "SXKWKI",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "katie.o@allcare4u.com"
  },
  {
      "ProfileID": 51,
      "NPINumber": "2139073904",
      "Titles": [
        "MD"
      ],
      "FirstName": "KXKRCUYB",
      "LastName": "KPSBKRC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "none@nowhere.com"
  },
  {
      "ProfileID": 52,
      "NPINumber": "5260398054",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "SFKB",
      "LastName": "KVKBSQQEN",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "docravid@yahoo.com"
  },
  {
      "ProfileID": 53,
      "NPINumber": "0946349664",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "RSEQKG",
      "LastName": "IBCKW VO",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "elmasry1997@yahoo.com"
  },
  {
      "ProfileID": 54,
      "NPINumber": "1775437544",
      "Titles": [
        "MD"
      ],
      "FirstName": "YSVO",
      "LastName": "KYFYX",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "notsure@somewhere.com"
  },
  {
      "ProfileID": 56,
      "NPINumber": "7341801284",
      "Titles": [
        "MD"
      ],
      "FirstName": "OMEBL",
      "LastName": "OVVSFSXBKL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "notsure@somewhere.net"
  },
  {
      "ProfileID": 57,
      "NPINumber": "8555394114",
      "Titles": [
        "MD"
      ],
      "FirstName": "DOBKQBKW",
      "LastName": "XKQO",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "somewhere@notsure.com"
  },
  {
      "ProfileID": 58,
      "NPINumber": "8566888954",
      "Titles": [
        "MD"
      ],
      "FirstName": "XOOVSO",
      "LastName": "XSWBOP",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "eylary@yahoo.wm"
  },
  {
      "ProfileID": 59,
      "NPINumber": "4230515224",
      "Titles": [
        "MD"
      ],
      "FirstName": "YWBOVVSEQ",
      "LastName": "VOBYW",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "guillermorel@hotmail.com"
  },
  {
      "ProfileID": 60,
      "NPINumber": "4118134314",
      "Titles": [
        "PA"
      ],
      "FirstName": "XSFOU",
      "LastName": "COUVSG",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "somewhere@overtherainbow.com"
  },
  {
      "ProfileID": 61,
      "NPINumber": "7341694114",
      "Titles": [
        "PA"
      ],
      "FirstName": "YNBKXYOV",
      "LastName": "YXKBKTOL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "notsure@somwhere.org"
  },
  {
      "ProfileID": 62,
      "NPINumber": "1760895080",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "IBKW",
      "LastName": "BOCER",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "unsure@somewhere.com"
  },
  {
      "ProfileID": 63,
      "NPINumber": "3014478954",
      "Titles": [
        "ARNP",
        "Doctor of Dental Medicine"
      ],
      "FirstName": "Lara",
      "LastName": "Craft",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "somewhere@where.com"
  },
  {
      "ProfileID": 64,
      "NPINumber": "9164850674",
      "Titles": [
        "Medical Doctor",
        "ARNP"
      ],
      "FirstName": "DXKIKT5d",
      "LastName": "KBOTKQ5d",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jaygajera@aol.com"
  },
  {
      "ProfileID": 65,
      "NPINumber": "9499826334",
      "Titles": [
        "MD"
      ],
      "FirstName": "VSMOM",
      "LastName": "QXSC-RKG-OEC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "suewahsing@aol.com"
  },
  {
      "ProfileID": 66,
      "NPINumber": "7290088054",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "CYVBKM",
      "LastName": "KFVSC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "casilva@tampabay.rr.com"
  },
  {
      "ProfileID": 67,
      "NPINumber": "1688525424",
      "Titles": [
        "PA"
      ],
      "FirstName": "MSBO",
      "LastName": "HYMVSG",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "iortizsilva@tampabay.rr.com"
  },
  {
      "ProfileID": 68,
      "NPINumber": "2078060874",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "VSXK",
      "LastName": "KSDKRL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "bhatia@hotmail.com"
  },
  {
      "ProfileID": 69,
      "NPINumber": "2292466834",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "YVKJXYQ",
      "LastName": "JOVKJXYQ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "atgonzalez12@aol.com"
  },
  {
      "ProfileID": 70,
      "NPINumber": "2489548554",
      "Titles": [
        "DO"
      ],
      "FirstName": "WKNK",
      "LastName": "NVOSPXOOBQ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "dr_greenfield@hotmail.com"
  },
  {
      "ProfileID": 71,
      "NPINumber": "1621608254",
      "Titles": [
        "MD"
      ],
      "FirstName": "RDKIKR",
      "LastName": "NOOFKT",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "hayathjaveed@yahoo.com"
  },
  {
      "ProfileID": 72,
      "NPINumber": "7077860874",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "KXSRKRC",
      "LastName": "NOOFKT",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "shjaveed@gmail.com"
  },
  {
      "ProfileID": 73,
      "NPINumber": "4785298154",
      "Titles": [
        "MD"
      ],
      "FirstName": "DSXFKX",
      "LastName": "VODKZ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "patelnupmdpa@hotmail.com"
  },
  {
      "ProfileID": 75,
      "NPINumber": "0604168854",
      "Titles": [
        "MD"
      ],
      "FirstName": "BONXKHOVK",
      "LastName": "XSUXYC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "sonkin54@gmail.com"
  },
  {
      "ProfileID": 77,
      "NPINumber": "8077288054",
      "Titles": [
        "MD"
      ],
      "FirstName": "KRKW",
      "LastName": "CSGOV",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mahalewis@yahoo.com"
  },
  {
      "ProfileID": 78,
      "NPINumber": "9030975924",
      "Titles": [
        "MD"
      ],
      "FirstName": "ADAM",
      "LastName": "GREENFIELD",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "affcare1@gmail.com"
  },
  {
      "ProfileID": 79,
      "NPINumber": "2859332594",
      "Titles": [
        "MD"
      ],
      "FirstName": "NKCKBZEXKRL",
      "LastName": "VODKZ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "patel.bhanu1@yahoo.com"
  },
  {
      "ProfileID": 80,
      "NPINumber": "2404775824",
      "Titles": [
        "MD"
      ],
      "FirstName": "XIVOMYT",
      "LastName": "YXOEL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "unsure@somewhere.org"
  },
  {
      "ProfileID": 81,
      "NPINumber": "4182171984",
      "Titles": [
        "MD"
      ],
      "FirstName": "KIDKC",
      "LastName": "SVVKZKVVEQ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "notsure@nothere.com"
  },
  {
      "ProfileID": 82,
      "NPINumber": "7024434514",
      "Titles": [
        "MD"
      ],
      "FirstName": "IWWST",
      "LastName": "NXYWNO",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jedmondmd@gericares.net"
  },
  {
      "ProfileID": 83,
      "NPINumber": "0377621384",
      "Titles": [
        "MD"
      ],
      "FirstName": "VKVRUECIKT",
      "LastName": "KBKXKZ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "jvpanara@gmail.com"
  },
  {
      "ProfileID": 84,
      "NPINumber": "5205312394",
      "Titles": [
        "MD"
      ],
      "FirstName": "LKRSO",
      "LastName": "USPGKD",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "drtawfik@citrusdiabetestreatment.com"
  },
  {
      "ProfileID": 85,
      "NPINumber": "8712332294",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "ZKVK",
      "LastName": "BKCFKRL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "alap.bhavsar@gmail.com"
  },
  {
      "ProfileID": 86,
      "NPINumber": "3615884814",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "KDXKXKIDKC",
      "LastName": "SNSVOF",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "vspsivarka@yahoo.com"
  },
  {
      "ProfileID": 87,
      "NPINumber": "3221863804",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "DSXK",
      "LastName": "QBKQ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "anit.garg@gmail.com"
  },
  {
      "ProfileID": 88,
      "NPINumber": "0007767444",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "KSGVIC",
      "LastName": "USCKDC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "someone@123456.com"
  },
  {
      "ProfileID": 89,
      "NPINumber": "2600613304",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "KXKRNKC",
      "LastName": "RKRC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "drshah@americanprimarycare.com"
  },
  {
      "ProfileID": 90,
      "NPINumber": "4001848354",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "VVODXKRC",
      "LastName": "BYTKW",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "someone@gmail.com"
  },
  {
      "ProfileID": 91,
      "NPINumber": "1564886734",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "KCSV",
      "LastName": "OVKNBONEKV",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "lmlauderdale@aol.com"
  },
  {
      "ProfileID": 92,
      "NPINumber": "9876621484",
      "Titles": [
        "MD"
      ],
      "FirstName": "DOOXFKB",
      "LastName": "ERNXKC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "rsandhu570@gmail.com"
  },
  {
      "ProfileID": 93,
      "NPINumber": "0077631584",
      "Titles": [
        "MD"
      ],
      "FirstName": "ZOOQNKT",
      "LastName": "ERNXKC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "someones@gmail.com"
  },
  {
      "ProfileID": 94,
      "NPINumber": "7543986034",
      "Titles": [
        "MD"
      ],
      "FirstName": "CSEV",
      "LastName": "VOFYT",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "office@baysidefamilycare.com"
  },
  {
      "ProfileID": 95,
      "NPINumber": "8638213204",
      "Titles": [
        "MD"
      ],
      "FirstName": "NOIC",
      "LastName": "XKCKR",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "westcoastmedical1@hotmail.com"
  },
  {
      "ProfileID": 96,
      "NPINumber": "7976713204",
      "Titles": [
        "Osteopathic Doctor"
      ],
      "FirstName": "XEIKWER",
      "LastName": "POOBKRC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "momocool@aol.com"
  },
  {
      "ProfileID": 97,
      "NPINumber": "3444544614",
      "Titles": [
        "MD"
      ],
      "FirstName": "XSNNEAKSV",
      "LastName": "RUSKRC",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "lshaikhmd@yahoo.com"
  },
  {
      "ProfileID": 98,
      "NPINumber": "2853658754",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "XIVOFO",
      "LastName": "JSDBY-JOVKJXYQ",
      "ProviderRelationships": [
        "Employee"
      ],
      "IPAGroupNames": [
        "PrimeCare",
        "PRIMECARE DOCTORS LLC"
      ],
      "ProviderLevel": null,
      "EmailIds": "draevelyngonzalez@hotmail.com"
  },
  {
      "ProfileID": 99,
      "NPINumber": "0889021284",
      "Titles": [
        "Medical Doctor"
      ],
      "FirstName": "CYDXKC",
      "LastName": "YBONBYM JSEB",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "icdoruizcordero@gmail.com"
  },
  {
      "ProfileID": 100,
      "NPINumber": "9916731584",
      "Titles": [
        "MD"
      ],
      "FirstName": "OCYT",
      "LastName": "YDXSZ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "drfil@hotmail.com"
  },
  {
      "ProfileID": 101,
      "NPINumber": "2168085024",
      "Titles": [
        "MD"
      ],
      "FirstName": "XKFS",
      "LastName": "JKSN",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "ivandiazmd@myproactivecare.com"
  },
  {
      "ProfileID": 102,
      "NPINumber": "0916809864",
      "Titles": [
        "ARNP"
      ],
      "FirstName": "KRZKDCEW",
      "LastName": "SVEYTXOL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "mustapb@aol.com"
  },
  {
      "ProfileID": 103,
      "NPINumber": "0887595124",
      "Titles": [
        "PA"
      ],
      "FirstName": "KSRDXIM",
      "LastName": "VVOGNSL",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "somewhere@gmail.com"
  },
  {
      "ProfileID": 106,
      "NPINumber": "6209773904",
      "Titles": [
        "MD"
      ],
      "FirstName": "OCYT",
      "LastName": "KXKVZKVVSF",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "javcmd1@aol.com"
  },
  {
      "ProfileID": 111,
      "NPINumber": "2943798154",
      "Titles": [
        "MD"
      ],
      "FirstName": "NKWRK",
      "LastName": "VONKP",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "ahmadfadel@yahoo.com"
  },
  {
      "ProfileID": 114,
      "NPINumber": "10987654",
      "Titles": [
        "MD"
      ],
      "FirstName": "DSUXK",
      "LastName": "QBKQ",
      "ProviderRelationships": [
        "Affiliate"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "ankitg@gmail.com"
  },
  {
      "ProfileID": 145,
      "NPINumber": "3424342424",
      "Titles": [
        "Clinical Psychologist"
      ],
      "FirstName": "dV",
      "LastName": "sA",
      "ProviderRelationships": [
        "Employee"
      ],
      "IPAGroupNames": [
        "PrimeCare"
      ],
      "ProviderLevel": null,
      "EmailIds": "dv@asia.com"
  }
];

var MemberSearchData = [
  {
      "Member": {
          "AMI": "ACCMEM2425",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Barbara",
              "MiddleName": null,
              "LastName": "Aanonsen",
              "Suffix": null,
              "DOB": "1941-01-21T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526846385"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3106 Saw Mill Ln",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0003910",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 2425
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM7048",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Roy",
              "MiddleName": null,
              "LastName": "Aanonsen",
              "Suffix": null,
              "DOB": "1942-01-23T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526846385"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3106 Saw Mill Ln",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0003909",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 7048
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6242",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Carol",
              "MiddleName": null,
              "LastName": "Acevedo",
              "Suffix": null,
              "DOB": "1939-02-19T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525566905"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7258 Apache Trail",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000757",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6242
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM1223",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "William",
              "MiddleName": null,
              "LastName": "Acevedo",
              "Suffix": null,
              "DOB": "1934-10-23T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525566905"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7258 Apache Trail",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000776",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "2015-12-31T00:00:00",
                    "EligibleMonth": "2015-12-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "2015-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 1223
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3747",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Andrew",
              "MiddleName": null,
              "LastName": "Acker",
              "Suffix": null,
              "DOB": "1943-09-22T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526838961"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "1041 Larkin Road",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34608",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001127",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3747
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM1227",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Marie",
              "MiddleName": null,
              "LastName": "Ackerman",
              "Suffix": null,
              "DOB": "1931-05-07T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525973625"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "4266 Craigdarragh Drive",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002312",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "2015-12-31T00:00:00",
                    "EligibleMonth": "2015-12-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "2015-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 1227
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5750",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Fred",
              "MiddleName": null,
              "LastName": "Adner",
              "Suffix": null,
              "DOB": "1935-07-03T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": null
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "16380 Melissa Drive",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34601",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002052",
                    "MemberEffectiveDate": "2013-12-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2013-12-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5750
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM7023",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Roger",
              "MiddleName": null,
              "LastName": "Adrian",
              "Suffix": null,
              "DOB": "1962-02-12T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3522931653"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "2925 W Antioch Ln",
                      "AddressLine2": null,
                      "City": "Lecanto",
                      "County": "CITRUS",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34461",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0004511",
                    "MemberEffectiveDate": "2015-03-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "004",
                    "PBPDesc": "Ultimate-H2962 Elite Plus 004",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-03-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 7023
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6735",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Andrea",
              "MiddleName": null,
              "LastName": "Aiello",
              "Suffix": null,
              "DOB": "1948-12-07T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525970335"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "5132 Winterville Rd",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34608",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001390",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6735
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6734",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Anthony",
              "MiddleName": null,
              "LastName": "Aiello",
              "Suffix": null,
              "DOB": "1947-09-20T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525970335"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "5132 Winterville Rd",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34608",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001392",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6734
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6124",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Ivan",
              "MiddleName": null,
              "LastName": "Allicock",
              "Suffix": null,
              "DOB": "1942-11-01T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "7184154314"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "13114 Kildeer Rd",
                      "AddressLine2": null,
                      "City": "Weeki Wachee",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34614",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002331",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6124
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM502",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Earl",
              "MiddleName": null,
              "LastName": "Altizer",
              "Suffix": null,
              "DOB": "1935-10-14T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525662260"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "8025 St Andrews Blvd",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001334",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2015-03-01T00:00:00",
                    "PBPID": "002",
                    "PBPDesc": "Ultimate-H2962 Premier Plus  002",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 502
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM488",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Yvonne",
              "MiddleName": null,
              "LastName": "Altizer",
              "Suffix": null,
              "DOB": "1936-03-06T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525562260"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "8025 St Andrews Blvd",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001333",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2015-03-01T00:00:00",
                    "PBPID": "002",
                    "PBPDesc": "Ultimate-H2962 Premier Plus  002",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 488
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6272",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "David",
              "MiddleName": null,
              "LastName": "Amaral",
              "Suffix": null,
              "DOB": "1937-06-12T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526835569"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "6246 Pinehurst Dr",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000971",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6272
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM2159",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Allan",
              "MiddleName": null,
              "LastName": "Amburgy",
              "Suffix": null,
              "DOB": "1951-04-17T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526863985"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3520 Orion Rd",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007493",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-05-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 2159
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5921",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Joseph",
              "MiddleName": null,
              "LastName": "Americo",
              "Suffix": null,
              "DOB": "1946-10-09T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3522776051"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "5035 Oak Leaf Ln",
                      "AddressLine2": null,
                      "City": "Hernando Beach",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34607",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0003408",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5921
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5261",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Barbara",
              "MiddleName": null,
              "LastName": "Anderson",
              "Suffix": null,
              "DOB": "1935-05-05T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3522933520"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "10365 Palmwood Pl",
                      "AddressLine2": null,
                      "City": "Weeki Wachee",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001964",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "002",
                    "PBPDesc": "Ultimate-H2962 Premier Plus  002",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5261
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM56",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Robert",
              "MiddleName": null,
              "LastName": "Anderson",
              "Suffix": null,
              "DOB": "1937-10-04T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525964436"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7569 Western Cir Dr",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001371",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2014-05-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 56
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Dennis",
              "MiddleName": null,
              "LastName": "Anton Sr",
              "Suffix": null,
              "DOB": "1941-07-01T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3528353266"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "8286 Weatherford Ave",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002911",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2014-01-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5727",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Gary",
              "MiddleName": null,
              "LastName": "Arnault",
              "Suffix": null,
              "DOB": "1948-05-31T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3523462954"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "2473 Running Oak Ct",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "346084475",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001867",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5727
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5529",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Marilyn",
              "MiddleName": null,
              "LastName": "Bacon",
              "Suffix": null,
              "DOB": "1946-09-11T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525977782"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "8397 Nuzum Rd",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002914",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5529
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM4940",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Robert",
              "MiddleName": null,
              "LastName": "Bacon",
              "Suffix": null,
              "DOB": "1941-03-02T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525977782"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "8397 Nuzum Rd",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002912",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 4940
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3427",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Mary",
              "MiddleName": null,
              "LastName": "Baird",
              "Suffix": null,
              "DOB": "1935-07-06T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525964407"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "4493 Haiti Dr",
                      "AddressLine2": null,
                      "City": "Hernando Beach",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34607",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007292",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3427
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM4069",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Ronald",
              "MiddleName": null,
              "LastName": "Barraco",
              "Suffix": null,
              "DOB": "1951-09-21T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3524108260"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "9058 Dupont Ave",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34608",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000740",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-07-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 4069
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5723",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Thomas",
              "MiddleName": null,
              "LastName": "Barton",
              "Suffix": null,
              "DOB": "1945-10-09T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3527973551"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "6223 Drew St",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34604",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001935",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5723
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3830",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Rose",
              "MiddleName": null,
              "LastName": "Basile",
              "Suffix": null,
              "DOB": "1930-09-26T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525975418"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "12406 Pitcairn St",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002084",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3830
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3719",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Albert",
              "MiddleName": null,
              "LastName": "Bathiany Iii",
              "Suffix": null,
              "DOB": "1929-07-27T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525972556"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7475 Harlow St",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34613",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001476",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3719
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM248",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "W",
              "MiddleName": null,
              "LastName": "Baumgartner",
              "Suffix": null,
              "DOB": "1929-02-03T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526864124"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "5500 Applegate Dr",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "346064711",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002854",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "2014-12-31T00:00:00",
                    "EligibleMonth": "2014-12-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "2014-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 248
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM203",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Vickie",
              "MiddleName": null,
              "LastName": "Beck",
              "Suffix": null,
              "DOB": "1959-10-28T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526865229"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "12555 Brookside St",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34609",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0002453",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "2014-12-31T00:00:00",
                    "EligibleMonth": "2014-12-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-01-01T00:00:00",
                          "PCPTermDate": "2014-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 203
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5787",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Louis",
              "MiddleName": null,
              "LastName": "Beneduce",
              "Suffix": null,
              "DOB": "1935-01-03T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526887877"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "6484 Pine Meadows Dr",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0001811",
                    "MemberEffectiveDate": "2016-02-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-02-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5787
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6659",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Lucille",
              "MiddleName": null,
              "LastName": "Beneduce",
              "Suffix": null,
              "DOB": "1937-09-09T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526887877"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "6484 Pine Meadows Dr",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000255",
                    "MemberEffectiveDate": "2016-02-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-02-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6659
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3011",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "David",
              "MiddleName": null,
              "LastName": "Bennett",
              "Suffix": null,
              "DOB": "1951-01-10T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525563042"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "2355 Jasper Park Ct",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007443",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3011
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3247",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Judith",
              "MiddleName": null,
              "LastName": "Bennett",
              "Suffix": null,
              "DOB": "1951-10-17T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3525563042"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "2355 Jasper Park Ct",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007442",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3247
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM6219",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Edith",
              "MiddleName": null,
              "LastName": "Berge",
              "Suffix": null,
              "DOB": "1920-05-25T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526664688"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3662 Beaumont Loop",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34609",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000195",
                    "MemberEffectiveDate": "2013-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2013-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 6219
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3917",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Henry",
              "MiddleName": null,
              "LastName": "Berge",
              "Suffix": null,
              "DOB": "1919-11-02T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526664688"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3662 Beaumont Loop",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34609",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000192",
                    "MemberEffectiveDate": "2013-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2013-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3917
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM2078",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Mark",
              "MiddleName": null,
              "LastName": "Bertramson",
              "Suffix": null,
              "DOB": "1951-10-27T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "7272524878"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "11185 Commercial Way Unit 38",
                      "AddressLine2": null,
                      "City": "Weeki Wachee",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34614",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007839",
                    "MemberEffectiveDate": "2016-03-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-04-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-03-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 2078
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM4844",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Judy",
              "MiddleName": null,
              "LastName": "Bettiga",
              "Suffix": null,
              "DOB": "1942-03-27T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526104674"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7373 Rosemont Lane",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0004650",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 4844
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM5472",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Lester",
              "MiddleName": null,
              "LastName": "Binder Jr",
              "Suffix": null,
              "DOB": "1947-10-04T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3523405091"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "7265 Sugarbush Dr",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000932",
                    "MemberEffectiveDate": "2014-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2014-12-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 5472
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3915",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Frank",
              "MiddleName": null,
              "LastName": "Bishop",
              "Suffix": null,
              "DOB": "1936-03-01T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3527965287"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "24326 Kiwi Ln",
                      "AddressLine2": null,
                      "City": "Brooksville",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34601",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0000248",
                    "MemberEffectiveDate": "2013-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2013-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3915
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM3013",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Margie",
              "MiddleName": null,
              "LastName": "Black",
              "Suffix": null,
              "DOB": "1948-02-16T00:00:00",
              "DOD": null,
              "Gender": "F",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3522007742"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "2060 Deltona Blvd",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34606",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0007431",
                    "MemberEffectiveDate": "2016-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2016-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 3013
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM7084",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Donald",
              "MiddleName": null,
              "LastName": "Bodewes",
              "Suffix": null,
              "DOB": "1937-09-20T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "3526668325"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "3305 Lighthouse Way",
                      "AddressLine2": null,
                      "City": "Spring Hill",
                      "County": "HERNANDO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34607",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0004091",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "001",
                    "PBPDesc": "Ultimate-H2962 Premier 001",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 7084
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  },
  {
      "Member": {
          "AMI": "ACCMEM7026",
          "PersonalInformation": {
              "Prefix": null,
              "FirstName": "Terrence",
              "MiddleName": null,
              "LastName": "Bodewes",
              "Suffix": null,
              "DOB": "1938-12-24T00:00:00",
              "DOD": null,
              "Gender": "M",
              "MaritalStatus": null,
              "SSN": null
          },
          "CulturalInformation": null,
          "ContactInformation": [
            {
                "ContactPerson": null,
                "PhoneInformation": [
                  {
                      "PhoneType": 0,
                      "Choice": 0,
                      "ContactNumber": "7278690067"
                  }
                ],
                "AddressInformation": [
                  {
                      "AddressLine1": "18729 Bascomb Ln",
                      "AddressLine2": null,
                      "City": "Hudson",
                      "County": "PASCO",
                      "State": "FL",
                      "StateCode": null,
                      "Country": null,
                      "CountryCode": null,
                      "ZipCode": "34667",
                      "AddressType": 0,
                      "Choice": 0
                  }
                ],
                "EmailInformation": null,
                "CommunicationPreference": null
            }
          ],
          "MemberLanguages": null,
          "Notes": null,
          "ResponsiblePerson": null,
          "MemberMemberships": [
            {
                "Membership": {
                    "SNPIndicator": null,
                    "SubscriberID": "UL0004366",
                    "MemberEffectiveDate": "2015-01-01T00:00:00",
                    "TermDate": "9999-12-31T00:00:00",
                    "EligibleMonth": "2016-06-01T00:00:00",
                    "PBPID": "003",
                    "PBPDesc": "Ultimate-H2962 Elite 003",
                    "PayerID": null,
                    "PaymentContract": null,
                    "IPAEffectiveDate": null,
                    "PlanEffectiveDate": null,
                    "IPA": {
                        "IPAID": 0,
                        "Name": "UNI",
                        "StatusType": null,
                        "Status": null,
                        "UpdatedBy": null
                    },
                    "Plan": {
                        "PlanID": 0,
                        "PlanName": "Ultimate",
                        "InsuranceComapanyID": null,
                        "InsuranceCompany": {
                            "InsuranceCompanyID": 0,
                            "Name": "Ultimate",
                            "UpdatedBy": null,
                            "StatusType": null,
                            "Status": null
                        },
                        "UpdatedBy": null,
                        "StatusType": null,
                        "Status": null
                    },
                    "MembershipProviderInformation": [
                      {
                          "MembershipProviderInformationID": 0,
                          "PCPEffectiveDate": "2015-01-01T00:00:00",
                          "PCPTermDate": "9999-12-31T00:00:00",
                          "Provider": {
                              "NPI": "1417989625",
                              "TaxId": null,
                              "PersonalInformation": {
                                  "Prefix": null,
                                  "FirstName": "PARIKSITH",
                                  "MiddleName": null,
                                  "LastName": "SINGH",
                                  "Suffix": null,
                                  "DOB": null,
                                  "DOD": null,
                                  "Gender": null,
                                  "MaritalStatus": null,
                                  "SSN": null
                              }
                          },
                          "Facility": null
                      }
                    ],
                    "MembershipPlanTypes": null
                },
                "Source": null,
                "MemberMembershipID": 7026
            }
          ],
          "PersonalIdentificationInformations": [
            {
                "IdentificationNumber": null,
                "IdentificationType": null
            }
          ]
      },
      "Provider": {
          "PlanNames": [
            "TestNon-DelegatedPlan",
            "TestDelegatedPlan",
            "OHIO WORKCOMP   ",
            "AETNA",
            "AVMED",
            "BLUE CROSS BLUE SHIELD",
            "TRICARE PRIME",
            "TRICARE STANDARD",
            "RR MCR - ACCESS LAB LLC",
            "CHOICE PROVIDER NETWORK ",
            "CIGNA ",
            "COVENTRY",
            "FREEDOM",
            "EMBLEM HEALTH - GHI",
            "HERITAGE SUMMIT",
            "HUMANA",
            "INTEGRAL QUALITY CARE",
            "MEDICAID",
            "MEDICARE",
            "NPI",
            "OPTIMUM",
            "PREFERRED CARE PARTNERS",
            "RAILROAD MCR",
            "SIMPLY HEALTHCARE PLANS ",
            "SIMPLY HEALTHCARE PLAN",
            "SUNCOAST HOSPICE - PINELLAS ",
            "ULTIMATE",
            "UNITEDHEALTHCARE",
            "UNIVERSAL HEALTH CARE(IPA)",
            "UNIVERSAL HEALTHCARE FFS ",
            "US DEPT OF LABOR AND INDUSTRIES  ",
            "WELLCARE",
            "MULTIPLAN"
          ],
          "Specialties": [
            {
                "Name": "Internal Medicine",
                "Taxonomy": "207R00000X"
            },
            {
                "Name": "Internal Medicine, Geriatric Medicine",
                "Taxonomy": "207RG0300X"
            }
          ],
          "PhoneNumber": "+1-3525853690",
          "FaxNumber": null,
          "ContactName": "Pariksith Singh",
          "EmailID": "psingh@accesshealthcarellc.net",
          "GroupTaxId1": "455171702",
          "GroupTaxId2": null,
          "CurrentPraticeLocation": [
            {
                "Building": null,
                "Street": "5350 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3526661848"
            },
            {
                "Building": null,
                "Street": "dhsjdhjd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Florida Ridge",
                "ZipCode": "143242",
                "FaxNumber": null
            },
            {
                "Building": null,
                "Street": "12037 Cortez Blvd",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Brooksville",
                "ZipCode": "34613",
                "FaxNumber": "+1-3453453534"
            },
            {
                "Building": null,
                "Street": "4144 North Armenia Ave.",
                "Country": "United States",
                "State": "Florida",
                "County": null,
                "City": "Tampa",
                "ZipCode": "33607",
                "FaxNumber": "+1-1111111111"
            },
            {
                "Building": null,
                "Street": "5382 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3522002191"
            },
            {
                "Building": null,
                "Street": "5362 Spring Hill Drive",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "346064562",
                "FaxNumber": "+1-3523981333"
            },
            {
                "Building": null,
                "Street": "7556 Spring Hill Dr.",
                "Country": "United States",
                "State": "Florida",
                "County": "Hernando",
                "City": "Spring Hill",
                "ZipCode": "34606",
                "FaxNumber": "+1-3526109965"
            }
          ],
          "ProviderPracticeLocationAddress": {
              "Street": "5350 Spring Hill Drive",
              "Building": null,
              "City": "Spring Hill",
              "State": "Florida",
              "ZipCode": "346064562",
              "Country": "United States",
              "County": "Hernando",
              "MobileNumber": "+1-3526888116",
              "FaxNumber": "+1-3526661848",
              "EmailAddress": "dburkhardt@accesshealthcarellc.net"
          }
      }
  }];