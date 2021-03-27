using BjBygg.Application.Application.Common.Dto;
using System.Collections.Generic;

namespace Application.IntegrationTests.Application.CommandTests
{
    public class MissionPdfExtractExpectedResults
    {
        public static KeyValuePair<string, MissionPdfDto>[] StrategyOneExpectedResults = new KeyValuePair<string, MissionPdfDto>[]
        {
            new KeyValuePair<string, MissionPdfDto>("pdf_report_test-11.pdf",
                 new MissionPdfDto { Address = "Bekkasinveien 59, 2008 FJERDINGBY", PhoneNumber = "99522324", DocumentName = "Skaderapport" }),
        };

        public static KeyValuePair<string, MissionPdfDto>[] StrategyTwoExpectedResults = new KeyValuePair<string, MissionPdfDto>[]
        {
            new KeyValuePair<string, MissionPdfDto>("pdf_report_test-21.pdf", 
                new MissionPdfDto { Address = "Oppsaltoppen 1A, 0687 OSLO", PhoneNumber = "97117350", DocumentName = "Skaderapport" }),
            new KeyValuePair<string, MissionPdfDto>("pdf_report_test-22.pdf", 
                new MissionPdfDto { Address = "Smiuvegen 79, 0981 OSLO", PhoneNumber = "22101078", DocumentName = "Skaderapport" }),
            new KeyValuePair<string, MissionPdfDto>("pdf_report_test-23.pdf", 
                new MissionPdfDto { Address = "Kringkollen 8, 0660 OSLO", PhoneNumber = "91196229", DocumentName = "Skaderapport" }),
        };
    }
}
