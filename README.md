## 📍 Overview

The `OnlineVotingSystem.api.git` project is a robust API designed to streamline online voting processes. It provides comprehensive endpoints for managing elections, candidates, positions, users, and votes. The system ensures secure and efficient voting through advanced authentication and authorization mechanisms. This project is structured to support multiple solutions, with a primary focus on the `OnlineVotingSystem.api` solution.

---

## 👾 Features

- **User Management**: Facilitates the creation, updating, and authentication of users.
- **Election Management**: Enables the creation, updating, and management of elections.
- **Candidate Management**: Allows the addition, updating, and retrieval of candidate information.
- **Position Management**: Defines and manages positions for elections.
- **Voting**: Securely casts and manages votes.
- **Results**: Provides real-time viewing of election results.
- **Authentication**: Secures API endpoints with JWT-based authentication.

---

# Online Voting System Project Structure

## Backend (ASP.NET Core API) - `OnlineVotingSystem.api/`

### Core Files
- **`Program.cs`**  
  Entry point configuring services, middleware, and API endpoints.

- **`appsettings.json`**  
  Configuration for database connections, JWT secrets, etc.

### Key Directories

#### `Data/`
- **`OnlineVotingSystemContext.cs`**  
  EF Core `DbContext` for database operations.
- **`DataExtensions.cs`**  
  Database seeding and migration helpers.

#### `DTOs/` (Data Transfer Objects)
- **User/**
  - `CreateUserDto.cs` → User registration  
  - `LoginUserDto.cs` → Authentication  
  - `UserDetailsDto.cs` → User profile responses
- **Election/**
  - `CreateElectionDto.cs` → Election creation  
  - `ElectionDetailsDto.cs` → Election data responses
- **Candidate/**
  - `CreateCandidateDto.cs` → Add candidates  
  - `CandidateDetailsDto.cs` → Candidate info responses

#### `Entities/` (Database Models)
- `User.cs` → Voters/Admins  
- `Election.cs` → Election metadata  
- `Candidate.cs` → Election candidates  
- `Vote.cs` → Cast votes

#### `Endpoints/` (Minimal APIs)
- `AuthEndpoints.cs` → Login/Register  
- `ElectionsEndpoints.cs` → CRUD for elections  
- `VotesEndpoints.cs` → Vote submission  

#### `Services/`
- **`JwtService.cs`**  
  Generates and validates JWT tokens.

---

## Frontend (Blazor WASM) - `WebUI/`

### Core Files
- **`Program.cs`**  
  Configures DI (HTTP client, auth services).

### Key Directories

#### `Components/Pages/`
- `Login.razor` → Authentication UI  
- `Dashboard.razor` → Admin overview  
- `ActiveElections.razor` → Ongoing elections list  
- `Result.razor` → Live results with charts  

#### `Services/`
- **`AuthService.cs`**  
  Handles API auth calls (login/logout).  
- **`ElectionService.cs`**  
  Fetches election data from API.  
- **`CustomAuthStateProvider.cs`**  
  Custom Blazor auth state management.

#### `wwwroot/` (Static Assets)
- `app.css` → Custom styles  
- `js/charts.js` → Visualization logic  
- `bootstrap/` → Bootstrap CSS framework  

---

## Solution & Config Files
- **`OnlineVotingSystem.sln`**  
  Main solution file linking both projects.  
- **`OnlineVotingSystem.api.csproj`**  
  Backend dependencies (EF Core, JWT).  
- **`WebUI.csproj`**  
  Frontend dependencies (Blazor, Syncfusion).  

---

### Architecture Summary
| Layer       | Technology       | Responsibility                          |
|-------------|------------------|----------------------------------------|
| **Backend** | ASP.NET Core API | Business logic, database, auth (JWT)   |
| **Frontend**| Blazor WASM-Hybrid SSR model      | Interactive UI, API communication      |
| **DB**      | SQLite/PostgreSQL| Persistent data storage                |

### 📂 Project Index

<details open>
	<summary><b><code>ONLINEVOTINGSYSTEM.API.GIT/</code></b></summary>
	<details> <!-- __root__ Submodule -->
		<summary><b>__root__</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api.sln.DotSettings.user'>OnlineVotingSystem.api.sln.DotSettings.user</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/identifier.sqlite'>identifier.sqlite</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.sln.DotSettings.user'>OnlineVotingSystem.sln.DotSettings.user</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.sln'>OnlineVotingSystem.sln</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api.sln'>OnlineVotingSystem.api.sln</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- OnlineVotingSystem.api Submodule -->
		<summary><b>OnlineVotingSystem.api</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/appsettings.Development.json'>appsettings.Development.json</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/appsettings.json'>appsettings.json</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Program.cs'>Program.cs</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/OnlineVotingSystem.api.csproj'>OnlineVotingSystem.api.csproj</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/OnlineVotingSystem.api.sln'>OnlineVotingSystem.api.sln</a></b></td>
				<td><code>❯ REPLACE-ME</code></td>
			</tr>
			</table>
			<details>
				<summary><b>Migrations</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Migrations/20250326232507_TestMigration.Designer.cs'>20250326232507_TestMigration.Designer.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Migrations/20250326233047_AddView.cs'>20250326233047_AddView.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Migrations/OnlineVotingSystemContextModelSnapshot.cs'>OnlineVotingSystemContextModelSnapshot.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Migrations/20250326233047_AddView.Designer.cs'>20250326233047_AddView.Designer.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Migrations/20250326232507_TestMigration.cs'>20250326232507_TestMigration.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Http</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/vote.http'>vote.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/candidate.http'>candidate.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/users.http'>users.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/elections.http'>elections.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/election_position.http'>election_position.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/positions.http'>positions.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Http/auth.http'>auth.http</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>DTOs</b></summary>
				<blockquote>
					<details>
						<summary><b>Vote</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Vote/VoteDetailsDto.cs'>VoteDetailsDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Vote/CreateVoteDto.cs'>CreateVoteDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Vote/VoteSerializedDto.cs'>VoteSerializedDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
					<details>
						<summary><b>Candidate</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Candidate/CandidateDetailsDto.cs'>CandidateDetailsDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Candidate/CreateCandidateDto.cs'>CreateCandidateDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Candidate/CandidateSerializedDto.cs'>CandidateSerializedDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Candidate/UpdateCandidateDto.cs'>UpdateCandidateDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
					<details>
						<summary><b>Position</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Position/PositionDetails.cs'>PositionDetails.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Position/CreatePositionDto.cs'>CreatePositionDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Position/UpdatePositionDto.cs'>UpdatePositionDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
					<details>
						<summary><b>ElectionPosition</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/ElectionPosition/CreateElectionPositionDto.cs'>CreateElectionPositionDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/ElectionPosition/ElectionPositionSerialized.cs'>ElectionPositionSerialized.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
					<details>
						<summary><b>User</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/User/LoginResponseDto.cs'>LoginResponseDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/User/CreateUserDto.cs'>CreateUserDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/User/LoginUserDto.cs'>LoginUserDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/User/UserDetailsDto.cs'>UserDetailsDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/User/UpdateUserDto.cs'>UpdateUserDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
					<details>
						<summary><b>Election</b></summary>
						<blockquote>
							<table>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Election/ElectionDetailsDto.cs'>ElectionDetailsDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Election/ElectionResultsView.cs'>ElectionResultsView.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Election/CreateElectionDto.cs'>CreateElectionDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							<tr>
								<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/DTOs/Election/UpdateElectionDto.cs'>UpdateElectionDto.cs</a></b></td>
								<td><code>❯ REPLACE-ME</code></td>
							</tr>
							</table>
						</blockquote>
					</details>
				</blockquote>
			</details>
			<details>
				<summary><b>Services</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Services/JwtService.cs'>JwtService.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Properties</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Properties/launchSettings.json'>launchSettings.json</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Endpoints</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/UsersEndpoints.cs'>UsersEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/CandidatesEndpoints.cs'>CandidatesEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/AuthEndpoints.cs'>AuthEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/VotesEndpoints.cs'>VotesEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/ElectionsEndpoints.cs'>ElectionsEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Endpoints/PositionsEndpoints.cs'>PositionsEndpoints.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Mapping</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/ElectionMapping.cs'>ElectionMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/CandidateMapping.cs'>CandidateMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/ElectionPositionMapping.cs'>ElectionPositionMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/UserMapping.cs'>UserMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/PositionMapping.cs'>PositionMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Mapping/VoteMapping.cs'>VoteMapping.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Data</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Data/ViewManagerService.cs'>ViewManagerService.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Data/DataExtensions.cs'>DataExtensions.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Data/OnlineVotingSystemContext.cs'>OnlineVotingSystemContext.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Entities</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/Vote.cs'>Vote.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/ElectionResult.cs'>ElectionResult.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/User.cs'>User.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/Position.cs'>Position.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/ElectionPosition.cs'>ElectionPosition.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/Election.cs'>Election.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/master/OnlineVotingSystem.api/Entities/Candidate.cs'>Candidate.cs</a></b></td>
						<td><code>❯ REPLACE-ME</code></td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
</details>

---

## 🚀 Getting Started

### ☑️ Prerequisites

Before getting started with `OnlineVotingSystem.api.git`, ensure your runtime environment meets the following requirements:

- **Programming Language:** CSharp
- **Package Manager:** Nuget
- **Database:** SQLite
- **.NET SDK:** Ensure you have the .NET SDK installed. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

### ⚙️ Installation
**THIS INSTRUCTIONS WILL WORK FOR 99.9% AND FOR THAT 0.1% HELP YOURSELF MAN!**

#### Clone the Repository

1. Clone the `OnlineVotingSystem.api.git` repository:
   ```sh
   ❯ git clone https://github.com/Flow-Pie/OnlineVotingSystem.api.git
   ```

2. Navigate to the project directory:
   ```sh
   ❯ cd OnlineVotingSystem.api.git
   ```

#### Cleaning the project

3. Clean the project:
   ```sh
   ❯ dotnet clean
   ```
   #### Build the project

4. Build the project:
   ```sh
   ❯ dotnet build
   ```
   #### Restore Dependencies

5. Restore the project dependencies using NuGet:
   ```sh
   ❯ dotnet restore
   ```

### 🤖 Running the Projects

The solution contains two projects: `OnlineVotingSystem.api` and application layer `WebUI`. Follow the steps below to run each project.

#### Running `OnlineVotingSystem.api`

1. Navigate to the `OnlineVotingSystem.api` directory:
   ```sh
   ❯ cd OnlineVotingSystem.api
   ```

2. Run the project:
   ```sh
   ❯ dotnet run 
   ```

#### Running `Interface Application layer ie WebUI`

1. Navigate to the `WebUI` directory:
   ```sh
   ❯ cd WebUI
   ```

2. Run the project:
   ```sh
   ❯ dotnet run 
   ```

The User Interface will launch

### 🧪 Testing(optional)

To run the test suite, use the following command:

```sh
❯ dotnet test
```

### 🛠 Migrations

The project uses Entity Framework Core for database migrations. Follow the steps below inccase you want to apply migrations.

#### Adding a New Migration

1. Navigate to the `OnlineVotingSystem.api` directory:
   ```sh
   ❯ cd OnlineVotingSystem.api
   ```

2. Add a new migration:
   ```sh
   ❯ dotnet ef migrations add <MigrationName>
   ```

#### Applying Migrations

3. Apply the migrations to update the database schema:
   ```sh
   ❯ dotnet ef database update
   ```

#### Adding a View

If you have a view and need to add it to the migrations, follow these steps:

1. Create a new migration for the view:
   ```sh
   ❯ dotnet ef migrations add AddView
   ```

2. Update the `OnlineVotingSystemContextModelSnapshot.cs` file up method to include the view definition. or you can as well create the view manually using IDE of your choice

3. Apply the migrations:
   ```sh
   ❯ dotnet ef database update
   ```

---

---

## 🔰 Contributing

- **💬 [Join the Discussions](https://github.com/Flow-Pie/OnlineVotingSystem.api.git/discussions)**: Share your insights, provide feedback, or ask questions.
- **🐛 [Report Issues](https://github.com/Flow-Pie/OnlineVotingSystem.api.git/issues)**: Submit bugs found or log feature requests for the `OnlineVotingSystem.api.git` project.
- **💡 [Submit Pull Requests](https://github.com/Flow-Pie/OnlineVotingSystem.api.git/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your GitHub account.
2. **Clone Locally**: Clone the forked repository to your local machine using a Git client.
   ```sh
   git clone https://github.com/Flow-Pie/OnlineVotingSystem.api.git
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to GitHub**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com{/Flow-Pie/OnlineVotingSystem.api.git/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=Flow-Pie/OnlineVotingSystem.api.git">
   </a>
</p>
</details>

---

## 🎗 License

This project is protected under the [MIT-LICENSE](https://choosealicense.com/licenses) License. For more details, refer to the [LICENSE](https://choosealicense.com/licenses/) file.

---

## 🙌 Acknowledgments

- List any resources, contributors, inspiration, etc. here.
