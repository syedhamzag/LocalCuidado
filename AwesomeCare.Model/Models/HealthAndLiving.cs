using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HealthAndLiving
    {
        public int HLId {get; set;}
        public int ClientId { get; set; }
        public string BriefHealth { get; set; }
        public string ObserveHealth {get; set;}
        public string WakeUp {get; set;}
        public string CareSupport {get; set;}
        public string MovingAndHandling {get; set;}
        public string SupportToBed {get; set;}
        public int DehydrationRisk {get; set;}
        public int LifeStyle {get; set;}
        public int PressureSore {get; set;}
        public int ContinenceIssue {get; set;}
        public string ContinenceNeeds {get; set;}
        public string ContinenceSource {get; set;}
        public int ConstraintRequired {get; set;}
        public string ConstraintDetails {get; set;}
        public string ConstraintAttachment {get; set;}
        public int MeansOfComm {get; set;}
        public int Smoking {get; set;}
        public int AbilityToRead {get; set;}
        public int TextFontSize {get; set;}
        public int Email {get; set;}
        public int FinanceManagement {get; set;}
        public int PostalService {get; set;}
        public int LetterOpening {get; set;}
        public int ShoppingRequired {get; set;}
        public int SpecialCleaning {get; set;}
        public int LaundaryRequired {get; set;}
        public int VideoCallRequired {get; set;}
        public int EatingWithStaff {get; set;}
        public int AllowChats {get; set;}
        public int TeaChocolateCoffee {get; set;}
        public int NeighbourInvolment {get; set;}
        public int FamilyUpdate {get; set;}
        public int AlcoholicDrink {get; set;}
        public int TVandMusic {get; set;}
        public string SpecialCaution {get; set;}

        public virtual Client Client { get; set; }
    }
}
