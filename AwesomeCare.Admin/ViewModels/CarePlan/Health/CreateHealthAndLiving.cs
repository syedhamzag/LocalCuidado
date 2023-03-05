using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.Health
{
    public class CreateHealthAndLiving
    {
        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }

        public string ActionName { get; set; } = "Save";
        public string Title { get; set; } = "Create Health And Living";
        public string ActiveTab { get; set; } = "careplan";
        public int HLId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string DOB { get; set; }
        public string IdNumber { get; set; }
        public string BriefHealth { get; set; }
        public string ObserveHealth { get; set; }
        public string WakeUp { get; set; }
        public string CareSupport { get; set; }
        public string MovingAndHandling { get; set; }
        public string SupportToBed { get; set; }
        public int DehydrationRisk { get; set; }
        public int LifeStyle { get; set; }
        public int PressureSore { get; set; }
        public int ContinenceIssue { get; set; }
        public string ContinenceNeeds { get; set; }
        public string ContinenceSource { get; set; }
        public int ConstraintRequired { get; set; }
        public string ConstraintDetails { get; set; }
        public string ConstraintAttachment { get; set; }
        public int MeansOfComm { get; set; }
        public int Smoking { get; set; }
        public int AbilityToRead { get; set; }
        public string AbilityToReadName { get; set; }
        public int TextFontSize { get; set; }
        public int Email { get; set; }
        public int FinanceManagement { get; set; }
        public int PostalService { get; set; }
        public int LetterOpening { get; set; }
        public int ShoppingRequired { get; set; }
        public int SpecialCleaning { get; set; }
        public int LaundaryRequired { get; set; }
        public int VideoCallRequired { get; set; }
        public int EatingWithStaff { get; set; }
        public int AllowChats { get; set; }
        public int TeaChocolateCoffee { get; set; }
        public int NeighbourInvolment { get; set; }
        public int FamilyUpdate { get; set; }
        public int AlcoholicDrink { get; set; }
        public int TVandMusic { get; set; }
        public string SpecialCaution { get; set; }

        public string EmailName { get; set; }

        public string DehydrationRiskName { get; set; }
        public string LifeStyleName { get; set; }
        public string PressureSoreName { get; set; }
        public string ContinenceIssueName { get; set; }
        public string ConstraintRequiredName { get; set; }
        public string MeansOfCommName { get; set; }
        public string SmokingName { get; set; }
        public string TextFontSizeName { get; set; }
        public string FinanceManagementName { get; set; }
        public string PostalServiceName { get; set; }
        public string LetterOpeningName { get; set; }
        public string ShoppingRequiredName { get; set; }
        public string SpecialCleaningName { get; set; }
        public string LaundaryRequiredName { get; set; }
        public string VideoCallRequiredName { get; set; }
        public string EatingWithStaffName { get; set; }
        public string AllowChatsName { get; set; }
        public string TeaChocolateCoffeeName { get; set; }
        public string NeighbourInvolmentName { get; set; }
        public string FamilyUpdateName { get; set; }
        public string AlcoholicDrinkName { get; set; }
        public string TVandMusicName { get; set; }
    }
}
