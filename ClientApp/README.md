## Run restarting

    dotnet run

## Run while watching

    dotnet watch run

## Apply migrations

    dotnet ef migrations add InitialCreate

## Apply to the database

    dotnet ef database update


            [HttpGet("roles")]
        public async Task<IActionResult> SignIn(int userid)
        {
        var userRoles = await _userManager.GetRolesAsync(user.Id);

            return userRoles;
        }
