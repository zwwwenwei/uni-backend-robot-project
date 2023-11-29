using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using sit331_4._1.Persistence;
using System.Windows.Input;
using Microsoft.AspNetCore.Authorization;

namespace sit331_4._1
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        private static readonly List<Map> _maps = new List<Map>
    {
        new Map(1, 2, 2, "LEFT", DateTime.Now, DateTime.Now, null),
        new Map(2, 2, 2, "RIGHT", DateTime.Now, DateTime.Now, null),
        new Map(3, 2, 2, "PLACE", DateTime.Now, DateTime.Now, null),
        // maps here
    };

        /// <summary>
        /// Get a map.
        /// </summary>
        /// <param>Get maps from the HTTP request.</param>
        /// <returns>maps</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/maps
        /// {
        /// {
        ///    "id": 1,
        ///    "columns": 351,
        ///    "rows": 406,
        ///    "name": "LEFT",
        ///    "description": null,
        ///    "createdDate": "2022-07-30T00:00:00",
        ///    "modifiedDate": "2022-07-30T00:00:00"
        ///},
        ///{
        ///    "id": 1,
        ///    "columns": 5,
        ///    "rows": 5,
        ///    "name": "LEFT",
        ///    "description": null,
        ///    "createdDate": "2023-05-15T12:22:48.831512",
        ///    "modifiedDate": "2023-05-15T12:22:48.831515"
        ///},
        ///{
        ///"id": 3,
        ///    "columns": 100,
        ///    "rows": 100,
        ///    "name": "GO",
        ///    "description": null,
        ///    "createdDate": "2023-05-15T13:21:32.659911",
        ///    "modifiedDate": "2023-05-15T13:21:32.659916"
        ///},
        ///{
        ///"id": 2,
        ///    "columns": 10,
        ///    "rows": 10,
        ///    "name": "GO",
        ///    "description": null,
        ///    "createdDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-19T15:09:34.247326"
        ///}
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet()]
        public IEnumerable<Map> GetAllMap()
        {
            //return _maps;
            return MapDataAccess.GetMaps();
        }

        /// <summary>
        /// Get a map.
        /// </summary>
        /// <param>Get maps from the HTTP request.</param>
        /// <returns>maps</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/maps
        /// {
        ///"id":1,
        ///"columns":2,
        ///"rows":2,
        ///"name":"LEFT",
        ///"description":null,
        ///"createdDate":"2023-05-20T00:09:33.5143544+10:00",
        ///"modifiedDate":"2023-05-20T00:09:33.5144588+10:00"
        ///},
        ///{
        ///"id":2,
        ///"columns":2,
        ///"rows":2,
        ///"name":"RIGHT",
        ///"description":null,
        ///"createdDate":"2023-05-20T00:09:33.5144635+10:00",
        ///"modifiedDate":"2023-05-20T00:09:33.5144645+10:00"
        ///},
        ///{
        ///"id":3,
        ///"columns":2,
        ///"rows":2,
        ///"name":"PLACE",
        ///"description":null,
        ///"createdDate":"2023-05-20T00:09:33.5144687+10:00",
        ///"modifiedDate":"2023-05-20T00:09:33.5144703+10:00"}
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("square")]
        public IEnumerable<Map> GetmapsquareOnly()
        {
            var filter = from n in _maps
                         where n.Rows == n.Columns
                         select n.Rows;
            foreach (var n in _maps)
            {
                yield return n;
            }
        }

        /// <summary>
        /// Get a map id.
        /// </summary>
        /// <param>Get maps id from the HTTP request.</param>
        /// <returns>select maps id</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/maps/id
        ///{
        ///"id": 2,
        ///"columns": 2,
        ///"rows": 2,
        ///"name": "RIGHT",
        ///"description": null,
        ///"createdDate": "2023-05-20T00:09:33.5144635+10:00",
        ///"modifiedDate": "2023-05-20T00:09:33.5144645+10:00"
        /// }
        /// </remarks>
        /// <response code="201">Returns the map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}", Name = "GetMap")]
        public IActionResult GetMapByID(int id)
        {
            var filter = from n in _maps
                         where n.Id == id
                         select n.Id;
            foreach (var n in _maps)
            {
                if (filter == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_maps[id]);
                }
            }
            return Ok(_maps[id]);

        }

        /// <summary>
        /// Creates a map.
        /// </summary>
        /// <param name="newMap">A new map from the HTTP request.</param>
        /// <returns>A newly map</returns>
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
        /// <response code="201">Returns the newly created map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPost()]
        public IActionResult AddMap(Map newMap)
        {
            if (newMap == null)
            {
                return BadRequest();
            }
            else if (_maps.Contains(newMap))
            {
                return Conflict();
            }

            newMap.CreatedDate = DateTime.Now;
            newMap.ModifiedDate = DateTime.Now;

            // Set the correct ID.
            var newID = _maps.Last().Id + 1;
            //var lastElement = _commands[_commands.Count - 1];
            newMap.Id = newID;
            _maps.Add(newMap);
            MapDataAccess.InsertMaps(newMap);
            return CreatedAtRoute("GetMap", new { id = newMap.Id }, newMap);
        }

        /// <summary>
        /// update a map.
        /// </summary>
        /// <param name="updatemap">A updated map from the HTTP request.</param>
        /// <param name="id">Select map id</param>
        /// <returns>A updated map</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/robot-commands/id
        ///{
        ///"id": 2,
        ///"columns": 10,
        ///"rows": 10,
        ///"name": "GO"
        /// }
        /// </remarks>
        /// <response code="201">Returns the update map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public IActionResult UpdateMap(int id, Map updatemap)
        {
            var existingmap = _maps.FirstOrDefault(existingmap => existingmap.Id == id);
            var existingmaps = GetAllMap();
            if (existingmaps == null)
            {
                return NotFound();
            }

            existingmap = updatemap;
            existingmap.Rows = updatemap.Rows;
            existingmap.Columns = updatemap.Columns;
            existingmap.Name = updatemap.Name;
            existingmap.CreatedDate = existingmap.CreatedDate;
            updatemap.ModifiedDate = DateTime.Now;
            existingmap.ModifiedDate = updatemap.ModifiedDate;

            if (existingmap.Id != id)
            {
                return BadRequest();
            }
            MapDataAccess.UpdateMaps(updatemap);
            return NoContent();
        }

        /// <summary>
        /// delete a map.
        /// </summary>
        /// <param name="id">Select a map and delete</param>
        /// <returns>A deleted map</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// DELETE /api/robot-commands/id
        /// {
        /// }
        /// </remarks>
        /// <response code="201">Returns the delete map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMap(int id)
        {
            var existingmap = GetAllMap();
            if (existingmap == null)
            {
                return NotFound();
            }
            else
            {
                MapDataAccess.DeleteMaps(id);
            }

            return NoContent();
        }

        /// <summary>
        /// check a map.
        /// </summary>
        /// <param name="id">Select a map and check</param>
        /// <param name="x">row</param>
        /// <param name="y">column</param>
        /// <returns>A deleted map</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// DELETE /api/robot-commands/id
        /// {
        /// true
        /// }
        /// </remarks>
        /// <response code="201">Returns the map</response>
        /// <response code="400">If the map is null</response>
        /// <response code="409">If a map with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}/{x}-{y}")]
        public IActionResult CheckCoordinate(int id, int x, int y)
        {
            var existingmap = _maps.FirstOrDefault(existingmap => existingmap.Id == id);
            if (x < 0 || y < 0)
            {
                return BadRequest();
            }

            if (existingmap == null)
            {
                return NotFound();
            }

            bool isOnMap = false;
            if (x > 0 && y > 0 && x <= existingmap.Rows && y <= existingmap.Columns)
            {
                isOnMap = true;
            }

            return Ok(isOnMap);
        }
        // Endpoints here
    }

}
