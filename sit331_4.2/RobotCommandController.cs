using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Collections;
using sit331_4._1.Persistence;
using System.Windows.Input;
using Microsoft.AspNetCore.Authorization;

namespace sit331_4._1
{
    [ApiController]
    [Route("api/robot-commands")]
    public class RobotCommandsController : ControllerBase
    {
        private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    {
        new RobotCommand(1, "LEFT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(2, "RIGHT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now),
        new RobotCommand(4, "PLACE", true, DateTime.Now, DateTime.Now),
        new RobotCommand(5, "REPORT", true, DateTime.Now, DateTime.Now),
    };

        /// <summary>
        /// Get a robot command.
        /// </summary>
        /// <param>Get robot command from the HTTP request.</param>
        /// <returns>robot command</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/robot-commands
        /// {
        /// {
        /// id": 2,
        ///"name": "UP",
        ///"isMoveCommand": false,
        ///"createDate": "0001-01-01T00:00:00",
        ///"modifiedDate": "2023-05-15T01:43:30.248298",
        ///"description": null
        /// },
        /// {
        ///    "id": 4,
        ///    "name": "BACK",
        ///    "isMoveCommand": false,
        ///    "createDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T11:30:35.926674",
        ///    "description": null
        ///},
        ///{
        ///    "id": 7,
        ///    "name": "DOWN",
        ///    "isMoveCommand": false,
        ///    "createDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T13:36:00.049734",
        ///    "description": null
        ///}
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet()]
        public IEnumerable<RobotCommand> GetAllRobotCommands()
        {
            //return _commands;
            return RobotCommandDataAccess.GetRobotCommands();
        }

        /// <summary>
        /// Get robot command move.
        /// </summary>
        /// <param>Get command move from the HTTP request.</param>
        /// <returns>robot command move</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/robot-commands/move
        /// {
        /// {
        /// "id":1,
        /// "name":"LEFT",
        /// "isMoveCommand":true,
        /// "createDate":"2023-05-19T23:39:41.9203375+10:00",
        /// "modifiedDate":"2023-05-19T23:39:41.9203488+10:00",
        /// "description":null
        /// },
        /// {
        /// "id":2,
        /// "name":"RIGHT",
        /// "isMoveCommand":true,
        /// "createDate":"2023-05-19T23:39:41.9207616+10:00",
        /// "modifiedDate":"2023-05-19T23:39:41.9207638+10:00",
        /// "description":null
        /// },
        /// {
        /// "id":3,
        /// "name":"MOVE",
        /// "isMoveCommand":true,
        /// "createDate":"2023-05-19T23:39:41.9207645+10:00",
        /// "modifiedDate":"2023-05-19T23:39:41.9207649+10:00",
        /// "description":null
        /// },
        /// {
        /// "id":4,
        /// "name":"PLACE",
        /// "isMoveCommand":true,
        /// "createDate":"2023-05-19T23:39:41.920768+10:00",
        /// "modifiedDate":"2023-05-19T23:39:41.9207683+10:00",
        /// "description":null
        /// },
        /// {
        /// "id":5,
        /// "name":"REPORT",
        /// "isMoveCommand":true,
        /// "createDate":"2023-05-19T23:39:41.9207686+10:00",
        /// "modifiedDate":"2023-05-19T23:39:41.9207688+10:00",
        /// "description":null
        /// }
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("move"), Authorize(Policy = "UserOnly")]
        public IEnumerable<RobotCommand> GetMoveRobotCommandsOnly()
        {
            var move = _commands.Where(x => x.IsMoveCommand);
            return move;
            /*var filter = from n in _commands
                         where n.IsMoveCommand = false
                         select n.IsMoveCommand;
            foreach (var n in _commands)
            {
                yield return n;
            }*/

        }

        /// <summary>
        /// Get a robot command id.
        /// </summary>
        /// <param name="id">Get command id from the HTTP request.</param>
        /// <returns>robot command id</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/robot-commands/id
        /// {
        /// {
        /// id": 2,
        ///"name": "UP",
        ///"isMoveCommand": false,
        ///"createDate": "0001-01-01T00:00:00",
        ///"modifiedDate": "2023-05-15T01:43:30.248298",
        ///"description": null
        /// },
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the selected robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}", Name = "GetRobotCommand")]
        public IActionResult GetRobotCommandByID(int id)
        {
            var filter = from n in _commands
                         where n.Id == id
                         select n.Id;
            foreach (var n in _commands)
            {
                if (filter == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_commands[id]);
                }
            }
            return Ok(_commands[id]);

        }

        /// <summary>
        /// Creates a robot command.
        /// </summary>
        /// <param name="newCommand">A new robot command from the HTTP request.</param>
        /// <returns>A newly created robot command</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/robot-commands
        /// {
        /// "name": "DANCE",
        /// "isMoveCommand": true,
        /// "description": "Salsa on the Moon"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPost()]
        public IActionResult AddRobotCommand(RobotCommand newCommand)
        {
            if (newCommand == null)
            {
                return BadRequest();
            }
            else if (GetAllRobotCommands().Contains(newCommand))
            {
                return Conflict();
            }
            newCommand.CreateDate = DateTime.Now;
            newCommand.ModifiedDate = DateTime.Now;
            _commands.Add(newCommand);
            RobotCommandDataAccess.InsertRobotCommands(newCommand);
            return CreatedAtRoute("GetRobotCommand", new { id = newCommand.Id }, newCommand);
        }

        /// <summary>
        /// Update a robot command.
        /// </summary>
        /// <param name="updateCommand">Update a robot command from the HTTP request.</param>
        /// <param name="id">Select a robot id</param>
        /// <returns>A updated robot command</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/robot-commands/id
        /// {
        /// "id": 7,
        ///"name": "DOWN",
        ///"isMoveCommand": false,
        ///"createDate": "0001-01-01T00:00:00",
        ///"modifiedDate": "2023-05-19T23:56:13.88429",
        ///"description": null
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the updated robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public IActionResult UpdateRobotCommand(int id, RobotCommand updateCommand)
        {
            var existingCommand = _commands.FirstOrDefault(x => x.Id == id);
            var existingCommands = GetAllRobotCommands();

            if (existingCommands == null)
            {
                return NotFound();
            }
            existingCommand = updateCommand;
            existingCommand.Name = updateCommand.Name;
            existingCommand.IsMoveCommand = updateCommand.IsMoveCommand;
            existingCommand.CreateDate = existingCommand.CreateDate;
            updateCommand.ModifiedDate = DateTime.Now;
            existingCommand.ModifiedDate = updateCommand.ModifiedDate;

            if (existingCommand.Id != id)
            {
                return BadRequest();
            }

            RobotCommandDataAccess.UpdateRobotCommands(updateCommand);
            return NoContent();
        }

        /// <summary>
        /// Delete a robot command.
        /// </summary>
        /// <param name="id">Selete an id and Delete a robot command from the HTTP request.</param>
        /// <returns>Deleted robot command</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// DELETE /api/robot-commands/id
        /// {
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the deleted robot command</response>
        /// <response code="400">If the robot command is null</response>
        /// <response code="409">If a robot command with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRobotCommand(int id)
        {
            //var existingCommand = _commands.FirstOrDefault(x => x.Id == id);
            //var existingCommand = _commands[id];
            var existingCommand = GetAllRobotCommands();

            if (existingCommand == null)
            {
                return NotFound();
            }
            else
            {
                RobotCommandDataAccess.DeleteRobotCommands(id);
            }

            return NoContent();
        }
        // Robot commands endpoints here
    }

}
