// EventController.cs: Manages the events on all the calendars
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

using Microsoft.AspNetCore.Mvc;
using UnitPlanner.Apis.Main.Services;
using UnitPlanner.Apis.Main.Models;

namespace UnitPlanner.Apis.Main.Controllers;

[ApiController]
[Route("/api/{unitId}/events/")]
public class EventController : ControllerBase
{
    private readonly IAccountsService _unitsService;
    private readonly IEventsService _eventsService;

    public EventController(IAccountsService unitsService, IEventsService eventsService) =>
        (_unitsService, _eventsService) = (unitsService, eventsService);

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(string unitId, string id)
    {
        Account? unit = (await _unitsService.GetUnit(unitId)).Case as Account;

        if (unit is null)
            return NotFound();

        var queriedEvent = await _eventsService.GetEvent(unit, Guid.Parse(id));

        if (queriedEvent is null)
            return NotFound();

        return Ok(queriedEvent);
    }
}
