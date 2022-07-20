module UnitPlanner.Apis.Main.Models.CadetPromotions

open UnitPlanner.Apis.Main.Models.Member

type CadetPromotionRequirements =
    { CadetAchvId: uint
      AchvName: string
      NextGrade: string
      CurrentGrade: string
      AchvNumberOrTitle: string
      OathRequired: bool
      LeadershipModule: string
      MentoringRequired: bool
      SDAServiceRequired: bool
      SDAWritingRequired: bool
      SDAPresentationRequired: bool
      DrillTest: string
      DrillItemsPass: uint
      DrillItemsTotal: uint
      AerospaceModule: string
      CharacterDevelopmentRequired: bool
      EncampmentRequired: bool
      RegionCadetLeadershipSchoolRequired: bool
      RequirementsWebLink: string
      LeadershipTestWebLink: string
      AerospaceTestWebLink: string
      DrillTestWebLink: string }

type CadetPromotionStatus = { CurrentCadetGradeId: uint }
