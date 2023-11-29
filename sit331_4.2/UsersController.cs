using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Windows.Input;
using Microsoft.AspNetCore.Server.IIS.Core;
using sit331_4._2.Persistence;
using sit331_4._1.Persistence;
using sit331_4._1;
using Microsoft.AspNetCore.Identity;
using System.Data;
using sit331_4._2.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace sit331_4._2
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private static readonly List<UserModel> _usermodels = new List<UserModel>
        {
            new UserModel(1, "a@gmail.com", "aa", "aa", "abc", DateTime.Now, DateTime.Now),
            new UserModel(2, "b@gmail.com", "bb", "bb", "cde", DateTime.Now, DateTime.Now),
            new UserModel(3, "c@gmail.com", "cc", "cc", "efg", DateTime.Now, DateTime.Now)
        };

        private static readonly List<LoginModel> _loginmodel = new List<LoginModel>
        { };
        

        /// <summary>
        /// Get a user model.
        /// </summary>
        /// <param>Get user model from the HTTP request.</param>
        /// <returns>user model</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/users
        /// {
        /// {
        ///    "id": 1,
        ///    "email": "a@gmail.com",
        ///    "firstname": "aa",
        ///    "lastname": "aa",
        ///    "passwordhash": "abc",
        ///    "createdDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T01:43:30.248298",
        ///    "description": null,
        ///    "role": null
        /// },
        /// {
        ///    "id": 2,
        ///    "email": "b@gmail.com",
        ///    "firstname": "bb",
        ///    "lastname": "bb",
        ///    "passwordhash": "cde",
        ///    "createdDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T01:43:30.248298",
        ///    "description": null,
        ///    "role": null
        ///},
        ///{
        ///    "id": 3,
        ///    "email": "c@gmail.com",
        ///    "firstname": "cc",
        ///    "lastname": "cc",
        ///    "passwordhash": "efg",
        ///    "createdDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T01:43:30.248298",
        ///    "description": null,
        ///    "role": null
        ///}
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the user model</response>
        /// <response code="400">If the user model is null</response>
        /// <response code="409">If a user model with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet()]
        public IEnumerable<UserModel> GetAllUsermodels()
        {
            //return _commands;
            return UserDataAccess.Getusermodels();
        }

        /// <summary>
        /// Get a user model id.
        /// </summary>
        /// <param name="role">Get user id from the HTTP request.</param>
        /// <returns>user model id</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/users/id
        /// {
        /// {
        ///    "id": 2,
        ///    "email": "b@gmail.com",
        ///    "firstname": "bb",
        ///    "lastname": "bb",
        ///    "passwordhash": "cde",
        ///    "createdDate": "0001-01-01T00:00:00",
        ///    "modifiedDate": "2023-05-15T01:43:30.248298",
        ///    "description": null,
        ///    "role": null
        /// },
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the selected user model</response>
        /// <response code="400">If the user model is null</response>
        /// <response code="409">If a user model with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("role")]
        public IActionResult GetusermodelByAdmin(string role)
        {
            //UserDataAccess.GetUserModelsByAdmin(role);
            if (role != "AdminOnly")
            {
                return NotFound();
            }
            else
            {
                return Ok(UserDataAccess.GetUserModelsByAdmin(role));
            }
        }

            /// <summary>
            /// Get a user model id.
            /// </summary>
            /// <param name="id">Get user id from the HTTP request.</param>
            /// <returns>user model id</returns>
            /// <remarks>
            /// Sample request:
            ///
            /// GET /api/users/id
            /// {
            /// {
            ///    "id": 2,
            ///    "email": "b@gmail.com",
            ///    "firstname": "bb",
            ///    "lastname": "bb",
            ///    "passwordhash": "cde",
            ///    "createdDate": "0001-01-01T00:00:00",
            ///    "modifiedDate": "2023-05-15T01:43:30.248298",
            ///    "description": null,
            ///    "role": null
            /// },
            /// }
            ///
            /// </remarks>
            /// <response code="201">Returns the selected user model</response>
            /// <response code="400">If the user model is null</response>
            /// <response code="409">If a user model with the same name already exists.</response>
            [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}", Name = "GetAllUsermodels")]
        public IActionResult GetusermodelByID(int id)
        {
            var model = _usermodels[id];

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_usermodels[id]);
            }

            /*var filter = from n in _usermodels
                         where n.Id == id
                         select n.Id;
            foreach (var n in _usermodels)
            {
                if (filter == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_usermodels[id]);
                }
            }
            return Ok(_usermodels[id]);*/

        }

        /// <summary>
        /// Creates a user model.
        /// </summary>
        /// <param name="newUser">A new user model from the HTTP request.</param>
        /// <returns>A newly created user model</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/users
        /// {
        ///    "email": "e@gmail.com",
        ///    "firstname": "ee",
        ///    "lastname": "ee",
        ///    "passwordhash": "cgb",
        ///    "description": ee ee e@gmail.com,
        ///    "role": user
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
        public IActionResult AddUsermodel(UserModel newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }
            else if (GetAllUsermodels().Contains(newUser))
            {
                return Conflict();
            }

            LoginModel newlogin = new(newUser.Email, newUser.PasswordHash);
            _loginmodel.Add(newlogin);

            var password = newUser.PasswordHash;
            var hasher = new PasswordHasher<UserModel>();
            var pwHash = hasher.HashPassword(newUser, password);
            var pwVerificationResult = hasher.VerifyHashedPassword(newUser, pwHash, password);
            if (pwVerificationResult == PasswordVerificationResult.Success)
            {
                newUser.PasswordHash = pwHash;
            }

            newUser.CreatedDate = DateTime.Now;
            newUser.ModifiedDate = DateTime.Now;
            _usermodels.Add(newUser);
            UserDataAccess.Insertusermodels(newUser);

            return CreatedAtRoute("GetRobotCommand", new { id = newUser.Id }, newUser);
        }

        /// <summary>
        /// Update a user model.
        /// </summary>
        /// <param name="updateUser">Update a user model from the HTTP request.</param>
        /// <param name="id">Select a user id</param>
        /// <returns>A updated user model</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/users/id
        /// {
        /// "id": 4,
        /// "email": "d@gmail.com",
        ///    "firstname": "dd",
        ///    "lastname": "dd",
        ///    "passwordhash": "cde",
        ///    "description": dd dd d@gmail.com,
        ///    "role": null
        /// "createDate": "0001-01-01T00:00:00",
        /// "modifiedDate": "2023-05-19T23:56:13.88429",
        /// "description": null
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the updated user model</response>
        /// <response code="400">If the user model is null</response>
        /// <response code="409">If a user model with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public IActionResult UpdateUsermodel(int id, UserModel updateUser)
        {
            var existingUser = _usermodels.FirstOrDefault(x => x.Id == id);
            var existingUsers = GetAllUsermodels();

            if (existingUsers == null)
            {
                return NotFound();
            }
            existingUser = updateUser;
            //existingUser.Email = updateUser.Email;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            //existingUser.PasswordHash = updateUser.PasswordHash;
            existingUser.CreatedDate = updateUser.CreatedDate;
            updateUser.ModifiedDate = DateTime.Now;
            existingUser.ModifiedDate = updateUser.ModifiedDate;

            if (existingUser.Id != id)
            {
                return BadRequest();
            }

            UserDataAccess.Updateusermodels(updateUser);
            return NoContent();
        }

        /// <summary>
        /// Delete a user model.
        /// </summary>
        /// <param name="id">Selete an id and Delete a user model from the HTTP request.</param>
        /// <returns>Deleted user model</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// DELETE /api/users/id
        /// {
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the deleted user model</response>
        /// <response code="400">If the user model is null</response>
        /// <response code="409">If a user model with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUsermodel(int id)
        {
            //var existingCommand = _commands.FirstOrDefault(x => x.Id == id);
            //var existingCommand = _commands[id];
            var existingUser = GetAllUsermodels();

            if (existingUser == null)
            {
                return NotFound();
            }
            else
            {
                UserDataAccess.Deleteusermodels(id);
            }

            return NoContent();
        }

        /// <summary>
        /// Update a user model.
        /// </summary>
        /// <param name="updatelogin">Update a user model from the HTTP request.</param>
        /// <returns>A updated user model</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/users/id
        /// {
        /// "id": 4,
        /// "email": "d@gmail.com",
        ///    "firstname": "dd",
        ///    "lastname": "dd",
        ///    "passwordhash": "cde",
        ///    "description": dd dd d@gmail.com,
        ///    "role": null
        /// "createDate": "0001-01-01T00:00:00",
        /// "modifiedDate": "2023-05-19T23:56:13.88429",
        /// "description": null
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns the updated user model</response>
        /// <response code="400">If the user model is null</response>
        /// <response code="409">If a user model with the same name already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id}")]
        public IActionResult UpdateEmailPassword(LoginModel updatelogin)
        {
            //var user = _usermodels.FirstOrDefault(x => x.Id == id);
            //var users = UserDataAccess.GetUserModelsByID(id);
            var users = GetAllUsermodels();
            var user = users.FirstOrDefault();
            //var exitinglogin = _loginmodel[id];
            //var password = "sit331password";
            var password = updatelogin.Password;
            //var user = new UserModel(5, existingloginmodel.Email, "as", "as", password, DateTime.Now, DateTime.Now); // use an actual user here
            var hasher = new PasswordHasher<UserModel>();

            if (user != null)
            {
                var pwHash = hasher.HashPassword(user, password);
                var pwVerificationResult = hasher.VerifyHashedPassword(user, pwHash, password);
                if (pwVerificationResult == PasswordVerificationResult.Success)
                {
                    user.Email = updatelogin.Email;
                    user.PasswordHash = pwHash;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    UserDataAccess.Updatelogin(user);
                }
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
