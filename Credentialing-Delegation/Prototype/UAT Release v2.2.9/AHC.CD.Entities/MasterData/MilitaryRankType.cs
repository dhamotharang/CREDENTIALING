using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData
{
    public enum MilitaryRankType
    {
        Private = 1,
        [Display(Name = "Private 2")]
        Private2,
        [Display(Name = "Private First Class")]
        PrivateFirstClass,
        Specialist,
        Corporal,
        Sergeant,
        [Display(Name = "Staff Sergeant")]
        StaffSergeant,
        [Display(Name = "Sergeant First Class")]
        SergeantFirstClass,
        [Display(Name = "Master Sergeant")]
        MasterSergeant,
        [Display(Name = "First Sergeant")]
        FirstSergeant,
        [Display(Name = "Sergeant Major")]
        SergeantMajor,
        [Display(Name = "Command Sergeant Major")]
        CommandSergeantMajor,
        [Display(Name = " Sergeant Major of the Army")]
        SergeantMajoroftheArmy,
        [Display(Name = "Gennery Sergeant")]
        GennerySergeant,
        [Display(Name = "Master Gunnery Sergeant")]
        MasterGunnerySergeant,
        [Display(Name = "Sergeant Major of the Marine Corps")]
        SergeantMajoroftheMarineCorps,
        [Display(Name = "Warrant Officer")]
        WarrantOfficer,
        [Display(Name = "Chief Warrant Officer 2")]
        ChiefWarrantOfficer2,
        [Display(Name = "Chief Warrant Officer 3")]
        ChiefWarrantOfficer3,
        [Display(Name = "Chief Warrant Officer 4")]
        ChiefWarrantOfficer4,
        [Display(Name = "Chief Warrant Officer 5")]
        ChiefWarrantOfficer5,
        [Display(Name = "Second Lieutenant")]
        SecondLieutenant,
        [Display(Name = "First Lieutenant")]
        FirstLieutenant,
        Captain,
        Major,
        [Display(Name = "Lieutenant Colonel")]
        LieutenantColonel,
        Colonel,
        [Display(Name = "Brigadier General")]
        BrigadierGeneral,
        [Display(Name = "Major General")]
        MajorGeneral,
        [Display(Name = "Lieutenant General")]
        LieutenantGeneral,
        General,
        [Display(Name = "Airman Basic")]
        AirmanBasic,
        Airman,
        [Display(Name = "Airman First Class")]
        AirmanFirstClass,
        [Display(Name = "Senior Airman")]
        SeniorAirman,
        [Display(Name = "Technical Sergeant")]
        TechnicalSergeant,
        [Display(Name = "Master Sergeant (Diamond)")]
        MasterSergeantDiamond,
        [Display(Name = "Senior Master Sergeant")]
        SeniorMasterSergeant,
        [Display(Name = "Senior Master Sergeant (Diamond)")]
        SeniorMasterSergeantDiamond,
        [Display(Name = "Chief Master Sergeant")]
        ChiefMasterSergeant,
        [Display(Name = "Chief Master Sergeant (Diamond)")]
        ChiefMasterSergeantDiamond,
        [Display(Name = "Command Chief Master Sergeant")]
        CommandChiefMasterSergeant,
        [Display(Name = "Chief Master Sergeant of the Air Force")]
        ChiefMasterSergeantoftheAirForce,
        [Display(Name = "General Air Force Chief of Staff")]
        GeneralAirForceChiefofStaff,
        [Display(Name = "General of the Air Force")]
        GeneraloftheAirForce,
        [Display(Name = "Seaman Recruit")]
        SeamanRecruit,
        [Display(Name = "Seaman Apprentice")]
        SeamanApprentice,
        Seaman,
        [Display(Name = "Petty Officer 3rd Class")]
        PettyOfficer3rdClass,
        [Display(Name = "Petty Officer 2nd Class")]
        PettyOfficer2ndClass,
        [Display(Name = "Petty Officer 1st Class")]
        PettyOfficer1stClass,
        [Display(Name = "Chief Petty officer")]
        ChiefPettyofficer,
        [Display(Name = "Senior Chief Petty Officer")]
        SeniorChiefPettyOfficer,
        [Display(Name = "Master Chief Petty officer")]
        MasterChiefPettyofficer,
        [Display(Name = "Fleet/ Commander Master Chief Petty Officer")]
        FleetCommanderMasterChiefPettyOfficer,
        [Display(Name = "Master Chief Navy Officer")]
        MasterChiefNavyOfficer,
        Ensign,
        [Display(Name = "Lieutenant Junior Grade")]
        LieutenantJuniorGrade,
        Lieutenant,
        [Display(Name = "Lieutenant Commander")]
        LieutenantCommander,
        Commander,
        [Display(Name = "Rear Admiral")]
        RearAdmiral,
        [Display(Name = "Vice Admiral")]
        ViceAdmiral,
        [Display(Name = "Admiral Chief of Naval ops / Commandant of the CG")]
        AdmiralChiefofNavalopsCommandantoftheCG,
        [Display(Name = "Fleet Admiral")]
        FleetAdmiral,
        [Display(Name = "Fireman Apprentice")]
        FiremanApprentice,
        [Display(Name = "Airman Apprentice")]
        AirmanApprentice,
        Fireman,
        [Display(Name = "Master Chief Petty officer of the Coast Guard")]
        MasterChiefPettyofficeroftheCoastGuard
    }
}
