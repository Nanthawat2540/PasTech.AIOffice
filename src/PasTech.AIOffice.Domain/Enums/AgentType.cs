namespace PasTech.AIOffice.Domain.Enums;

public enum AgentType
{
    Sales = 1,
    SurveyDesign = 2,
    Quotation = 3,
    Project = 4,
    Document = 5,
    Support = 6,
    Marketing = 7
}

public enum OfficeTaskStatus
{
    Inquiry = 1,
    Survey = 2,
    Quotation = 3,
    PO = 4,
    Installation = 5,
    Handover = 6,
    Closed = 7
}

public enum AgentStatus { Idle, Working, Done, Error }

public enum JobType { CCTV, Network, Server, Solar, SmartBuilding }
