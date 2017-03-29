using IPDProductivityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityUI.Common
{

    public class Calculate
    {
        static int cls1;
        static int cls2;
        static int cls3;
        static int cls4;
        static int cls5;

        static void ClearCls()
        {
            cls1 = 0;
            cls2 = 0;
            cls3 = 0;
            cls4 = 0;
            cls5 = 0;
        }

        public static decimal GetPercentag(decimal sumOfMulClassmulClsx100, decimal sumOfRealStaff)
        {
            return decimal.Round(sumOfMulClassmulClsx100 / sumOfRealStaff, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal GetClsMul100(decimal sumOfMulClass)
        {
            return sumOfMulClass * 100;
        }


        public static decimal GetSumOfRealStaff(decimal totalStaffReal, string wardCase)
        {
            decimal result = 0;

            switch (wardCase)
            {
                case "W":
                    result = decimal.Round(totalStaffReal * 7, 2, MidpointRounding.AwayFromZero);
                    break;
                default:
                    break;
            }

            return result;
        }

        public static decimal PatientClassCalc(List<string> listClass, string wardCase, decimal add)
        {
            decimal result = 0;
            ClearCls();
            #region sum class
            foreach (var cls in listClass)
            {

                if (cls == "1")
                {
                    cls1 += 1;
                }
                else if (cls == "2")
                {
                    cls2 += 1;
                }
                else if (cls == "3")
                {
                    cls3 += 1;
                }
                else if (cls == "4")
                {
                    cls4 += 1;
                }
                else if (cls == "5")
                {
                    cls5 += 1;
                }
            }
            #endregion

            switch (wardCase)
            {
                case "W":
                    #region multiplier ward

                    result += cls1 > 0 ? Convert.ToDecimal(cls1 * 2.5) : 0;
                    result += cls2 > 0 ? Convert.ToDecimal(cls2 * 4.5) : 0;
                    result += cls3 > 0 ? Convert.ToDecimal(cls3 * 6) : 0;
                    result += cls4 > 0 ? Convert.ToDecimal(cls4 * 7) : 0;
                    result += cls5 > 0 ? Convert.ToDecimal(cls5 * 8) : 0;

                    #endregion
                    break;
                default:
                    break;
            }


            return result + add;
        }

        public static decimal SumStaffReal(StaffReal staff)
        {
            decimal result = 0;

            result = staff.DayRnReal + staff.DayNrReal + staff.EveRnReal + staff.EveNrReal + staff.NigRnReal + staff.NigNrReal;

            return result;
        }

        public static StaffData GetStaffData(decimal sumOfMulClass, string wardCase)
        {
            StaffData staff = new StaffData();

            string rn = Constants.RN;
            string na = Constants.NA;

            decimal nurseData = GetNurseData(sumOfMulClass, wardCase);
            decimal NurseDataDay = GetNurseDataDay(nurseData);
            decimal NurseDataEve = GetNurseDataEve(nurseData);
            decimal NurseDataNig = GetNurseDataNig(nurseData);

            staff.DayRnData = GetNurseDataRnNa(NurseDataDay, rn);
            staff.DayNrData = GetNurseDataRnNa(NurseDataDay, na);
            staff.EveRnData = GetNurseDataRnNa(NurseDataEve, rn);
            staff.EveNrData = GetNurseDataRnNa(NurseDataEve, na);
            staff.NigRnData = GetNurseDataRnNa(NurseDataNig, rn);
            staff.NigNrData = GetNurseDataRnNa(NurseDataNig, na);

            return staff;
        }

        public static decimal GetNurseData(decimal sumOfMulClass, string wardCase)
        {
            decimal result = 0;
            switch (wardCase)
            {
                case "W":
                    result = decimal.Round((sumOfMulClass / 7) + 1, 2, MidpointRounding.AwayFromZero);
                    break;
                case "ICU":
                    break;
                case "N":
                    break;
                default:
                    break;
            }

            return result;
        }

        public static decimal GetNurseDataDay(decimal nurseData)
        {
            return decimal.Round(nurseData * (decimal)0.47, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal GetNurseDataEve(decimal nurseData)
        {
            return decimal.Round(nurseData * (decimal)0.36, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal GetNurseDataNig(decimal nurseData)
        {
            return decimal.Round(nurseData * (decimal)0.17, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal GetNurseDataRnNa(decimal nurseDataTime, string RnNa)
        {
            decimal result = 0;

            switch (RnNa)
            {
                case "RN":
                    result = decimal.Round(nurseDataTime * (decimal)0.6, 2, MidpointRounding.AwayFromZero);
                    break;
                case "NA":
                    result = decimal.Round(nurseDataTime * (decimal)0.4, 2, MidpointRounding.AwayFromZero);
                    break;
                default:
                    break;
            }

            return result;
        }

        public static string GetKardex(string epi)
        {
            string value = "";


            return value;
        }
    }
}
