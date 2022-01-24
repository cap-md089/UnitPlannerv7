// UnitController.cs: Interface for creating, enumerating, and managing units
//
// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Services;

namespace UnitPlanner.Apis.Main.Controllers;

[ApiController]
[Route("/api/unit")]
public class AccountController : ControllerBase
{
    private readonly IAccountsService _unitsService;

    public AccountController(IAccountsService unitsService) =>
        (_unitsService) = (unitsService);

    [HttpPost]
    [Route("squadron")]
    public async Task<IActionResult> CreateNewSquadron(NewSquadronAccountRequest request)
    {
        var orgs = new List<Models.NHQ.Organization>();

        var wingOption = (await _unitsService.GetUnit(request.wingId))
            .Bind(wingMaybe =>
                        wingMaybe is CAPWing wing
                            ? Option<CAPWing>.Some(wing)
                            : Option<CAPWing>.None);
        var groupOption = (await _unitsService.GetUnit(request.groupId))
            .Bind(groupOption =>
                groupOption is CAPGroup group
                    ? Option<CAPGroup>.Some(group)
                    : Option<CAPGroup>.None);

        var func = Option<Func<CAPWing, CAPGroup, (CAPWing, CAPGroup)>>.Some((wing, group) => (wing, group));

        return await func
            .Apply(wingOption, groupOption)
            .MapAsync((units) => _unitsService.CreateNewSquadron(units.Item1, units.Item2, request.unitId, orgs))
            .Match<IActionResult>(
                sq => CreatedAtAction(
                    nameof(GetUnit),
                    new { unitId = sq.Id },
                    sq
                ),
                NotFound
            );
    }

    [HttpPost]
    [Route("group")]
    public async Task<IActionResult> CreateNewGroup(NewGroupAccountRequest request)
    {
        var orgs = new List<Models.NHQ.Organization>();

        var wingOption = await _unitsService.GetUnit(request.wingId);

        return await wingOption
            .Bind(wingMaybe =>
                wingMaybe is CAPWing wing
                    ? Option<CAPWing>.Some(wing)
                    : Option<CAPWing>.None
            )
            .MatchAsync<CAPWing, IActionResult>(async wing =>
                CreatedAtAction(
                    nameof(GetUnit),
                    new { unitId = request.unitId },
                    await _unitsService.CreateNewGroup(wing, request.unitId, orgs)
                ),
                NotFound
            );
    }

    [HttpPost]
    [Route("wing")]
    public async Task<IActionResult> CreateNewWing(NewWingAccountRequest request)
    {
        var orgs = new List<Models.NHQ.Organization>();

        return CreatedAtAction(
            nameof(GetUnit),
            new { unitId = request.unitId },
            await _unitsService.CreateNewWing(request.unitId, orgs)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetUnits()
    {
        var units = await _unitsService.GetUnits();

        return new JsonResult(new UnitRequestResult(
            activityAccounts: units.OfType<CAPActivity>(),
            wingAccounts: units.OfType<CAPWing>(),
            groupAccounts: units.OfType<CAPGroup>(),
            squadronAccounts: units.OfType<CAPSquadron>()
        ));
    }

    [HttpGet]
    [Route("{unitId}")]
    public async Task<IActionResult> GetUnit(string unitId) =>
        (await _unitsService.GetUnit(unitId)).Match<IActionResult>(Ok, NotFound);

    [HttpDelete]
    [Route("{unitId}")]
    public async Task<IActionResult> DeleteUnit(string unitId) 
    {
        var unitOption = await _unitsService.GetUnit(unitId);

        Account? unit = unitOption.Case as Account;

        if(unit is null) {
            return NotFound();
        }

        await _unitsService.DeleteUnit(unit);

        return NoContent();
    }
}

public class NewSquadronAccountRequest
{
    [Required]
    public string unitId { get; set; } = null!;

    [Required]
    public string wingId { get; set; } = null!;

    [Required]
    public string groupId { get; set; } = null!;

    [Required]
    public IEnumerable<int> orgIds { get; set; } = null!;
}

public class NewGroupAccountRequest
{
    [Required]
    public string unitId { get; set; } = null!;

    [Required]
    public string wingId { get; set; } = null!;

    [Required]
    public IEnumerable<int> orgIds { get; set; } = null!;
}

public class NewWingAccountRequest
{
    [Required]
    public string unitId { get; set; } = null!;

    [Required]
    public IEnumerable<int> orgIds { get; set; } = null!;
}

public class UnitRequestResult
{
    public IEnumerable<CAPActivity> ActivityAccounts { get; set; }

    public IEnumerable<CAPWing> WingAccounts { get; set; }

    public IEnumerable<CAPGroup> GroupAccounts { get; set; }

    public IEnumerable<CAPSquadron> SquadronAccounts { get; set; }

    public UnitRequestResult(IEnumerable<CAPActivity> activityAccounts, IEnumerable<CAPWing> wingAccounts, IEnumerable<CAPGroup> groupAccounts, IEnumerable<CAPSquadron> squadronAccounts) =>
        (ActivityAccounts, WingAccounts, GroupAccounts, SquadronAccounts) = (activityAccounts, wingAccounts, groupAccounts, squadronAccounts);
    
}