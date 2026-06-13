using PasTech.AIOffice.Application.Interfaces;
using PasTech.AIOffice.Domain.Entities;

namespace PasTech.AIOffice.Infrastructure.Agents;

public abstract class BaseAgent(LlmProviderFactory llm) : IAgentService
{
    protected abstract string SystemPrompt { get; }
    public abstract Domain.Enums.AgentType AgentType { get; }

    public async Task<AgentResult> RunAsync(
        OfficeTask task, string input,
        string provider = "claude",
        CancellationToken ct = default)
    {
        var p = llm.Get(provider);
        var result = await p.CompleteAsync(
            SystemPrompt + CompanyContext,
            BuildUserMessage(task, input),
            ct);

        return new AgentResult(result.Text, result.Tokens, result.Success, p.Name, result.Error);
    }

    protected virtual string BuildUserMessage(OfficeTask task, string input) =>
        $"Task: {task.TaskNumber} | ลูกค้า: {task.CustomerName} | ประเภทงาน: {task.JobType}\n\n{input}";

    private const string CompanyContext = """

        ## บริบทบริษัท
        บริษัท PasTech System จำกัด — ออกแบบ จัดหา และติดตั้งระบบเทคโนโลยีสำหรับองค์กรทั่วประเทศไทย
        สินค้า: CCTV & Security | Network Infrastructure | Server & Data Center | Solar Energy | Smart Building & IoT
        กลุ่มลูกค้า: โรงงาน, อาคารสำนักงาน, ห้างสรรพสินค้า, โรงพยาบาล
        กฎ: ตอบภาษาไทยเสมอ | ถ้าไม่มั่นใจราคาให้บอกว่าต้องตรวจสอบ | ทำเฉพาะหน้าที่ของตัวเอง
        """;
}
