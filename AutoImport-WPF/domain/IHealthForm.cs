namespace AutoImport_WPF.domain;

public interface IHealthForm
{
    string CheckDate { get; }

    string Name { get; }

    string Id { get; }

    bool IsElder { get; }

    bool IsHypertension { get; }

    bool IsDiabetes { get; }

    string Temperature { get; }

    string LeftEye { get; }

    string RightEye { get; }

    string HeartRate { get; }

    string BreathRate { get; }

    string RightConstriction { get; }

    string RightDiastolic { get; }

    string LeftConstriction { get; }

    string LeftDiastolic { get; }

    string Height { get; }

    string Weight { get; }

    string Waistline { get; }

    string Hemoglobin { get; }

    string WhiteBloodCell { get; }

    string Platelet { get; }

    string FastingBloodGlucose { get; }

    bool IsEcgNormal { get; }

    string EcgText { get; }

    string SerumAlanineAminotransferase { get; }

    string SerumGlutamicOxalaceticTransaminase { get; }

    string TotalBilirubin { get; }

    string SerumCreatinine { get; }

    string BloodUreaNitrogen { get; }

    string TotalCholesterol { get; }

    string Triglyceride { get; }

    string SerumLowDensityLipoproteinCholesterol { get; }

    string SerumHighDensityLipoproteinCholesterol { get; }

    bool IsChestXrayNormal { get; }

    string Xtext { get; }

    bool IsBUltrasonicNormal { get; }

    string BText { get; }

    string OtherSystemDisease { get; }

    string Medicine1 { get; }

    string MedicineUse1 { get; }

    string MedicineEachDose1 { get; }

    string MedicineUseDate1 { get; }

    string MedicineYield1 { get; }

    string Medicine2 { get; }

    string MedicineUse2 { get; }

    string MedicineEachDose2 { get; }

    string MedicineUseDate2 { get; }

    string MedicineYield2 { get; }

    string Medicine3 { get; }

    string MedicineUse3 { get; }

    string MedicineEachDose3 { get; }

    string MedicineUseDate3 { get; }

    string MedicineYield3 { get; }

    string Medicine4 { get; }

    string MedicineUse4 { get; }

    string MedicineEachDose4 { get; }

    string MedicineUseDate4 { get; }

    string MedicineYield4 { get; }

    bool HasAbnormal { get; }

    string Abnormality1 { get; }

    string Abnormality2 { get; }

    string Abnormality3 { get; }

    string Abnormality4 { get; }

    bool IsPutIntoAdministration { get; }

    bool IsSuggestedReview { get; }

    bool IsSuggestReferral { get; }

    bool IsNeedQuitSmoking { get; }

    bool IsNeedHealthDrinking { get; }

    bool IsNeedDiet { get; }

    bool IsNeedExercise { get; }

    bool IsNeedLostWeight { get; }

    string TargetWeight { get; }

    bool HasOther { get; }
    
    string OtherSuggestion { get; }
}