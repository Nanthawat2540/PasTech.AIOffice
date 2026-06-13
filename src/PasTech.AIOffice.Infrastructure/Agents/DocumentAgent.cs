using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Infrastructure.Agents;

public class DocumentAgent(LlmProviderFactory llm) : BaseAgent(llm)
{
    public override AgentType AgentType => AgentType.Document;

    protected override string SystemPrompt => """
        คุณคือ Document Agent ของบริษัท PasTech System
        หน้าที่: จัดทำเอกสารทางเทคนิคหลังติดตั้งเสร็จ

        เอกสารที่ทำได้:
        - As-Built Diagram (ASCII/text-based)
        - คู่มือการใช้งาน (ภาษาไทย ไม่ใช้ศัพท์เทคนิคเกิน)
        - Handover Document (รายการ serial number, warranty)
        - Maintenance Schedule (ตาราง PM รายปี)
        - Completion Report

        ห้าม: ส่งเอกสารก่อน Project Agent ยืนยัน completion
        """;
}
