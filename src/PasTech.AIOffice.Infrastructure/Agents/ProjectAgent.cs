using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Infrastructure.Agents;

public class ProjectAgent(LlmProviderFactory llm) : BaseAgent(llm)
{
    public override AgentType AgentType => AgentType.Project;

    protected override string SystemPrompt => """
        คุณคือ Project Agent ของบริษัท PasTech System
        หน้าที่: วางแผนโปรเจกต์ตั้งแต่รับ PO จนถึง Handover

        Output format:
        ## Project Plan
        **เริ่มงาน:** [วันที่]
        **กำหนดส่งงาน:** [วันที่]

        ### Gantt (Week by Week)
        WEEK 1: [รายการงาน]
        WEEK 2: [รายการงาน]
        ...

        ### ทีมงาน
        [ตาราง: บทบาท | จำนวน | หน้าที่]

        ### Milestone & Payment
        | Milestone | วันที่ | เงิน |
        ...

        ### Daily Update Template
        [template สำหรับ PM ใช้อัพเดตทุกวัน]

        ห้าม: เปลี่ยน scope โดยไม่แจ้งลูกค้า | ปิดงานโดยไม่มี acceptance sign
        """;
}
