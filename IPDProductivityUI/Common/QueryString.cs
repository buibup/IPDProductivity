using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDProductivityUI.Common
{
    public class QueryString
    {
        public static string GetBedRoomQueryString(string wardCode)
        {
            var queryString = @"

                Select BED_RowID, BED_Code, BED_WARD_ParRef->WARD_Code, BED_WARD_ParRef->WARD_Desc,
                BED_BedType_DR-> BEDTP_Desc From PAC_BED 
                Where  BED_WARD_ParRef-> WARD_Code = '{wardCode}'
                and bed_rcflag = 'Y' and BED_BedType_DR <> '23'
                and  ( ({fn convert(BED_Code,SQL_varchar)} not like '%/%')  or 
                ({fn convert(BED_Code,SQL_varchar)} like '%/%' and  BED_Available ='N') )
                order by BED_Code
                ";

            queryString = queryString.Replace("{wardCode}", wardCode);

            return queryString.Trim();
        }

        public static string GetPatientIPDQueryString(string bedRowId)
        {
            var queryString = @"

                Select RN_No, EpisodeNo
                From VSVH_PAIPD2
                Where BED_RowID = '{bedRowId}'
                AND ADMISSIONTYPE = 'I' AND EPISODENO LIKE 'I%'
                AND DISCHARGEDATE IS NULL AND WARD_CODE <> '' 
                AND VISITSTATUS  <> 'C' and rn_no <>''
                Order by PAADM_RowID Desc
            ";

            queryString = queryString.Replace("{bedRowId}", bedRowId);


            return queryString.Trim();
        }

        public static string GetPatientClassQueryString(string papmiNo)
        {
            var queryString = @"

                Select CLS From ipdclstm
                Where RN = '{papmiNo}' and rowid = (select max(rowid) from ipdclstm where rn = '{papmiNo}')                
            
            ";

            queryString = queryString.Replace("{papmiNo}", papmiNo);

            return queryString.Trim();
        }

        public static string GetStatReasonNotAvailQueryString(string bedRowId)
        {
            var queryString = @"
                
                Select stat_reasonnotavail_dr From vsvh_pacbed 
                Where bed_rowid = '{bedRowId}' and STAT_DateTo is null and stat_status_dr = 2

            ";

            queryString = queryString.Replace("{bedRowId}", bedRowId);

            return queryString.Trim();
        }

        public static string GetRealStaffQueryString(string ward, DateTime date)
        {
            var queryString = @"

                select 
                    isnull(grn1m,0)+isnull(grn2m,0)+isnull(grn3m,0) DayRnReal ,
                    isnull(gnrn1m,0)+isnull(gpn1m,0) DayNrReal,
                    isnull(grn1e,0)+isnull(grn2e,0)+isnull(grn3e,0) EveRnReal ,
                    isnull(gnrn1e,0)+isnull(gpn1e,0) EveNrReal,
                    isnull(grn1n,0)+isnull(grn2n,0)+isnull(grn3n,0) NigRnReal,
                    isnull(gnrn1n,0)+isnull(gpn1n,0) NigNrReal  
                from ipdclsn 
                where gward = '{ward}' 
                and gdte = '{date}'

            ";

            queryString = queryString.Replace("{ward}", ward);
            queryString = queryString.Replace("{date}", date.ToString("dd/MM/yyyy"));

            return queryString.Trim();
        }

        public static string GetKardexQueryString(string epi)
        {
            var queryString = @"

                select Infectious 
                from Kardex where en = '{epi}' 
                and rowid = (select max(rowid) from Kardex where en = '{epi}')

            ";

            queryString = queryString.Replace("{epi}", epi);

            return queryString;
        }

        public static string GetSetWardQueryString()
        {
            var queryString = @"

                Select Seq, WardCode, WardDesc
                From SetWard
                Order By Seq 

            ";

            
            return queryString;
        }

        public static string TruncateTable(string table)
        {
            return "Truncate Table " + table;
        }

        public static string InsertSetWardQueryString()
        {
            var queryString = @"

                Insert Into SetWard (WardCode, WardDesc)
                Values (@WardCode, @WardDesc)

            ";

            return queryString;
        }

        public static string GetBedTypeQueryString(string bedRowid)
        {
            var queryString = @"

                Select BED_BedType_DR->BEDTP_Code
                From pac_bed
                Where bed_rowid = '{bedRowid}'
            ";

            queryString = queryString.Replace("{bedRowid}", bedRowid);

            return queryString.Trim();
        }
    }
}
