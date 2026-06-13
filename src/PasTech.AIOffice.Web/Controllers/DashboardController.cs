using Microsoft.AspNetCore.Mvc;
using PasTech.AIOffice.Application.DTOs;
using PasTech.AIOffice.Application.Interfaces;
using PasTech.AIOffice.Domain.Entities;
using PasTech.AIOffice.Domain.Enums;

namespace PasTech.AIOffice.Web.Controllers;

public class DashboardController(ITaskRepository repo, IEnumerable<IAgentService> agents) : Controller
{
    public async Task<IActionResult> Index()
    {
        var tasks = await repo.GetAllAsync();
        return View(tasks);
    }

    [HttpGet]
    public IActionResult NewTask() => View();

    [HttpPost]
    public async Task<IActionResult> NewTask(CreateTaskRequest req)
    {
        var task = new OfficeTask
        {
            TaskNumber = await repo.NextTaskNumberAsync(),
            CustomerName = req.CustomerName,
            CustomerEmail = req.CustomerEmail,
            CustomerPhone = req.CustomerPhone,
            Location = req.Location,
            Description = req.Description,
            JobType = req.JobType,
            Status = OfficeTaskStatus.Inquiry
        };
        await repo.CreateAsync(task);
        return RedirectToAction("Detail", new { id = task.Id });
    }

    public async Task<IActionResult> Detail(int id)
    {
        var task = await repo.GetByIdAsync(id);
        if (task == null) return NotFound();
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> RunAgent(int taskId, AgentType agentType, string input, string provider = "claude")
    {
        var task = await repo.GetByIdAsync(taskId);
        if (task == null) return NotFound();

        var agent = agents.FirstOrDefault(a => a.AgentType == agentType);
        if (agent == null) return BadRequest("Agent not found");

        var result = await agent.RunAsync(task, input, provider);

        var log = new AgentLog
        {
            OfficeTaskId = taskId,
            Agent = agentType,
            Action = $"{agentType} [{result.Provider}]",
            Input = input,
            Output = result.Success ? result.Output : result.Error ?? "",
            TokensUsed = result.TokensUsed
        };
        await repo.AddLogAsync(log);

        // Auto-advance task status
        task.Status = agentType switch
        {
            AgentType.SurveyDesign => OfficeTaskStatus.Survey,
            AgentType.Quotation    => OfficeTaskStatus.Quotation,
            AgentType.Project      => OfficeTaskStatus.PO,
            _                      => task.Status
        };
        await repo.UpdateAsync(task);

        return RedirectToAction("Detail", new { id = taskId });
    }
}
